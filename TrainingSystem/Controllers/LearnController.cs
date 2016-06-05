using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrainingSystem.Entities;
using TrainingSystem.Models;
using System.Data.Entity;
using System.IO;
using Microsoft.AspNet.Identity;
using TrainingSystem.Services;

namespace TrainingSystem.Controllers
{
    public class LearnController : TrainingSystemController
    {
        private ExerciseReviewerFinder _exerciseReviewerFinder;

        public LearnController()
        {
            _exerciseReviewerFinder = new ExerciseReviewerFinder();
        }

        public ActionResult LearnStep(int id)
        {
            if (CurrentUserId == null)
            {
                return RedirectToAction("Step", "Library", new { @id = id, @message = "Please login into your account to start learning." });
            }

            var student = CurrentStudent;
            if (student == null)
            {
                student = new Student(CurrentUser);
                Db.Students.Add(student);
            }

            var roadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == id);
            var studentXRoadStep = new StudentXRoadStep(student, roadStep, LearningStatus.StudyingResources);
            student.StudentXRoadSteps.Add(studentXRoadStep);
            Db.SaveChanges();

            return RedirectToAction("Step", "Library", new { @id = id });
        }

        public ActionResult FinishedResources(int id)
        {
            if (CurrentUserId == null)
            {
                return RedirectToAction("Step", "Library", new { @id = id, @message = "Please login into your account to start learning." });
            }

            var roadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == id);
            var student = CurrentStudentWithRoadSteps;
            student.FinishLearningResources(roadStep);
            Db.SaveChanges();

            return RedirectToAction("RateStepResource", new { @id = id });
        }

        public ActionResult RateStepResource(int id)
        {
            var roadStep = Db.RoadSteps
                .Include(p => p.StepResources)
                .FirstOrDefault(p => p.Id == id);

            var model = new RateStepContentViewModel();
            model.RoadStep = roadStep;
            model.RoadStepId = roadStep.Id;
            model.StepResourceRatings = MapStepResourceToDto(roadStep.StepResources);
            return View(model);
        }

        [HttpPost]
        public ActionResult RateStepResource(RateStepContentViewModel model)
        {
            var roadStep = Db.RoadSteps
                .Include(p => p.StepResources)
                .FirstOrDefault(p => p.Id == model.RoadStepId);
            var currentStudent = CurrentStudentWithRoadSteps;

            currentStudent.AddFinishedResourcesComment(roadStep, model.Comment);
            foreach (var rating in model.StepResourceRatings)
            {
                currentStudent.RateResource(roadStep, rating.Id, rating.RatingValue);
            }
            Db.SaveChanges();

            return RedirectToAction("Step", "Library", new { @id = model.RoadStepId });
        }

        private IList<StepResourceRating> MapStepResourceToDto(IList<StepResource> stepResources)
        {
            var ret = new List<StepResourceRating>();
            foreach (var stepResource in stepResources)
            {
                ret.Add(new StepResourceRating(stepResource.Id, stepResource.Name, stepResource.Description, RatingValue.Medium));
            }
            return ret;
        }

        public ActionResult StartExercise(int id, int roadStepId)
        {
            if (CurrentUserId == null)
            {
                return RedirectToAction("Step", "Library", new { @id = id, @message = "Please login into your account to start learning." });
            }

            var student = CurrentStudentWithRoadSteps;

            var roadStep = Db.RoadSteps
                .Include(p => p.StepExercises)
                .FirstOrDefault(p => p.Id == roadStepId);
            student.StartExercise(roadStep, id);
            Db.SaveChanges();

            return RedirectToAction("Step", "Library", new { @id = id });
        }

        public ActionResult FinishExercise(int id, int roadStepId)
        {
            if (CurrentUserId == null)
            {
                return RedirectToAction("Step", "Library", new { @id = id, @message = "Please login into your account to start learning." });
            }

            var model = new FinishExerciseViewModel();
            model.Exercise = Db.StepExercises.FirstOrDefault(p => p.Id == id);
            model.RoadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == roadStepId);

            return View(model);
        }

        public ActionResult UploadExerciseSolution(int exerciseId, int roadStepId)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ExerciseSolution/"), fileName);
                    file.SaveAs(path);

                    var student = CurrentStudentWithRoadSteps;

                    var roadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == roadStepId);
                    student.FinishExercise(roadStep, exerciseId, fileName);
                    Db.SaveChanges();

                    var studentExercise = student.GetStudentExercise(roadStep, exerciseId);
                    return RedirectToAction("RateExercise", new { id = studentExercise.Id });
                }
            }

            return RedirectToAction("Index", "Library");
        }

        public ActionResult RateExercise(int id)
        {
            var studentExercise = Db.StudentExercises
                .Include(p => p.StepExercise)
                .Include(p => p.StudentXRoadStep.RoadStep)
                .FirstOrDefault(p => p.Id == id);

            var model = new RateStepExerciseViewModel();
            model.StepExercise = studentExercise.StepExercise;
            model.StepExerciseId = studentExercise.StepExercise.Id;
            model.RatingValue = RatingValue.Medium;
            model.RoadStepId = studentExercise.StudentXRoadStep.RoadStep.Id;
            return View(model);
        }

        [HttpPost]
        public ActionResult RateExercise(RateStepExerciseViewModel model)
        {
            var roadStep = Db.RoadSteps
                .Include(p => p.StepExercises)
                .FirstOrDefault(p => p.Id == model.RoadStepId);

            var currentStudent = CurrentStudentWithRoadSteps;
            currentStudent.RateExercise(roadStep, model.StepExerciseId, model.Comment, model.RatingValue);

            var allTeachers = Db.Teachers
                .Include(p => p.TeacherXRoadSteps)
                .ToList();
            var reviewer = _exerciseReviewerFinder.Find(roadStep, allTeachers);
            reviewer.PrepareForReview(roadStep, model.StepExerciseId, currentStudent);

            Db.SaveChanges();

            return RedirectToAction("Step", "Library", new { @id = model.RoadStepId });
        }
    }
}