using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrainingSystem.Entities;
using TrainingSystem.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace TrainingSystem.Controllers
{
    public class LearnController : TrainingSystemController
    {
        public ActionResult LearnStep(int id)
        {
            if (CurrentUserId == null)
            {
                return RedirectToAction("Step", "Library", new { @id = id, @message = "Please login into your account to start learning." });
            }

            var student = CurrentStudent;
            if (student == null)
            {
                student = new Student(CurrentUserId);
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
    }
}