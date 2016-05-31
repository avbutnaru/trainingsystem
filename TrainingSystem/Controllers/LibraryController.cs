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

            model.Message = message;
            return View(model);
        }
    }
}