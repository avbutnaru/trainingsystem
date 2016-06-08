using System.Linq;
using System.Web.Mvc;
using TrainingSystem.Entities;
using TrainingSystem.Models;
using System.Data.Entity;

namespace TrainingSystem.Controllers
{
    public class LibraryController : TrainingSystemController
    {
        private TrainingSystemContext db = new TrainingSystemContext();

        public ActionResult Index()
        {
            var model = new LibraryMainViewModel();
            model.RoadMaps = db.RoadMaps.ToList();
            return View(model);
        }

        public ActionResult RoadMap(int id)
        {
            var model = new RoadMapMainViewModel();
            model.RoadMap = db.RoadMaps
                .Include(p => p.RoadmapXRoads.Select(u => u.Road))
                .FirstOrDefault(p => p.Id == id);
            return View(model);
        }

        public ActionResult Road(int id)
        {
            var model = new RoadMainViewModel();
            model.Road = db.Roads
                .Include(p => p.RoadXRoadSteps.Select(u => u.RoadStep))
                .FirstOrDefault(p => p.Id == id);
            return View(model);
        }

        public ActionResult Step(int id, string message)
        {
            var model = new RoadStepMainViewModel();
            model.RoadStep = db.RoadSteps
                .Include(p => p.StepResources)
                .Include(p => p.StepExercises)
                .FirstOrDefault(p => p.Id == id);

            var student = CurrentStudentWithRoadSteps;
            if (student != null)
            {
                model.StudentXRoadStep = student.StudentXRoadSteps.FirstOrDefault(p => p.RoadStep.Id == model.RoadStep.Id);
            }

            model.ResourceRatings = Db.StudentResourceRatings
                .Include(p => p.StepResource)
                .ToList();
            model.ExerciseRatings = Db.StudentExercises
                .Include(p => p.StepExercise)
                .Where(p => p.RatingValue != null)
                .ToList();

            model.Message = message;
            return View(model);
        }

        public ActionResult SendMessage(int? roadStepId, int? roadMapId, int? roadId)
        {
            var model = new SendMessageViewModel();
            if (CurrentUserId == null)
            {
                model.Message = "Please login into your account to send messages.";
                return View(model);
            }

            if (model.RoadStepId != null)
            {
                var roadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == model.RoadStepId.Value);
                model.RoadStep = roadStep;
                model.RoadStepId = roadStepId;
            }
            if (model.RoadId != null)
            {
                var road = Db.Roads.FirstOrDefault(p => p.Id == model.RoadId.Value);
                model.Road = road;
                model.RoadId = roadId;
            }
            if (model.RoadMapId != null)
            {
                var roadMap = Db.RoadMaps.FirstOrDefault(p => p.Id == model.RoadMapId.Value);
                model.RoadMap = roadMap;
                model.RoadMapId = roadMapId;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SendMessage(SendMessageViewModel model)
        {
            if (CurrentUserId == null)
            {
                model.Message = "Please login into your account to send messages.";
                return View(model);
            }

            var sender = CurrentUser;
            AspNetUsers recipient = null;
            RoadStep roadStep = null;
            if (model.RoadStepId != null)
            {
                roadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == model.RoadStepId.Value);
                recipient = Db.AspNetUsers.FirstOrDefault(p => p.Id == roadStep.UserId);
            }
            Road road = null;
            if (model.RoadId != null)
            {
                road = Db.Roads.FirstOrDefault(p => p.Id == model.RoadId.Value);
                recipient = Db.AspNetUsers.FirstOrDefault(p => p.Id == road.UserId);
            }
            RoadMap roadMap = null;
            if (model.RoadMapId != null)
            {
                roadMap = Db.RoadMaps.FirstOrDefault(p => p.Id == model.RoadMapId.Value);
                recipient = Db.AspNetUsers.FirstOrDefault(p => p.Id == roadMap.UserId);
            }

            Db.TrainingMessages.Add(new TrainingMessage(model.Content, sender, recipient, road, roadMap, roadStep));
            Db.SaveChanges();

            model.Message = "Your message has been sent. Thank you.";
            return View(model);
        }

        public ActionResult People()
        {
            var model = new PeopleMainViewModel();
            model.Users = db.AspNetUsers
                .ToList();
            return View(model);
        }

        public ActionResult ViewProfile(string id)
        {
            var model = new ProfileViewModel();

            model.User = Db.AspNetUsers.FirstOrDefault(p => p.Id == id);
            model.ReviewsReceived = Db.ExerciseReviews
                .Include(p => p.StudentExercise.StepExercise)
                .Where(p => p.StudentExercise.StudentXRoadStep.Student.ParentUser.Id == id && p.ExerciseReviewStatus == ExerciseReviewStatus.Reviewed).ToList();
            model.GroupMembers = Db.GroupMembers
                .Include(p => p.TrainingGroup)
                .Where(p => p.AspNetUser.Id == id).ToList();
            model.StudentExercises =
                Db.StudentExercises
                .Include(p => p.StepExercise)
                .Where(
                    p => p.StudentXRoadStep.Student.ParentUser.Id == id && p.ExerciseStatus == ExerciseStatus.Finished).ToList();
            model.RoadSteps = Db.StudentXRoadSteps
                .Include(p => p.RoadStep)
                .Where(p => p.Student.ParentUser.Id == id).ToList();
            model.ReviewsGiven = Db.ExerciseReviews
                .Include(p => p.StudentExercise.StepExercise)
                .Where(p => p.Teacher.ParentUser.Id == id).ToList();

            return View(model);
        }
    }
}