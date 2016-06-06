using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TrainingSystem.Entities;
using TrainingSystem.Models;
using System.Data.Entity;

namespace TrainingSystem.Controllers
{
    [Authorize]
    public class ManageRoadMapController : TrainingSystemController
    {
        public ManageRoadMapController()
        {
        }

        public ActionResult Edit(int? id)
        {
            var model = new ManageRoadMapViewModel();
            if (id != null)
            {
                var roadMap = Db.RoadMaps
                    .Include(x => x.RoadmapXRoads.Select(y => y.Road))
                    .FirstOrDefault(p => p.Id == id);

                if (roadMap != null)
                {
                    model.Id = roadMap.Id;
                    model.Name = roadMap.Name;
                    model.Description = roadMap.Description;
                    model.Roads = roadMap.RoadmapXRoads.Select(p => p.Road).ToList();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManageRoadMapViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            RoadMap roadMap = null;
            if (model.Id != null)
            {
                roadMap = Db.RoadMaps.FirstOrDefault(p => p.Id == model.Id);
                if (roadMap != null)
                {
                    roadMap.Name = model.Name;
                    roadMap.Description = model.Description;
                }
            }
            else
            {
                var currentUserId = User.Identity.GetUserId();

                roadMap = new RoadMap(model.Name, model.Description, currentUserId);
                Db.RoadMaps.Add(roadMap);
            }

            Db.SaveChanges();
            
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ManageRoadMapListViewModel();
            var currentUserId = User.Identity.GetUserId();
            model.RoadMaps = Db.RoadMaps.Where(p => p.UserId == currentUserId).ToList();
            model.RoadSteps = Db.RoadSteps.Where(p => p.UserId == currentUserId).ToList();
            return View(model);
        }



        public ActionResult EditRoad(int? roadmapId, int? id)
        {
            var model = new ManageRoadViewModel();
            if (id != null)
            {
                var road = Db.Roads
                    .Include(x => x.RoadXRoadSteps.Select(y => y.RoadStep))
                    .FirstOrDefault(p => p.Id == id);
                if (road != null)
                {
                    model.Id = road.Id;
                    model.Name = road.Name;
                    model.Description = road.Description;
                    model.RoadSteps = road.RoadXRoadSteps.Select(p => p.RoadStep).ToList();

                    if (roadmapId != null)
                    {
                        model.RoadmapId = roadmapId;
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoad(ManageRoadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Road road = null;
            if (model.Id != null)
            {
                road = Db.Roads.FirstOrDefault(p => p.Id == model.Id);
                if (road != null)
                {
                    road.Name = model.Name;
                    road.Description = model.Description;
                }
            }
            else
            {
                var currentUserId = User.Identity.GetUserId();

                RoadMap roadmap = null;
                if (model.RoadmapId != null)
                {
                    roadmap = Db.RoadMaps.FirstOrDefault(p => p.Id == model.RoadmapId);
                }

                road = new Road(model.Name, model.Description, currentUserId, roadmap);
                Db.Roads.Add(road);
            }

            Db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult EditRoadStep(int? roadId, int? id)
        {
            var model = new ManageRoadStepViewModel();
            if (id != null)
            {
                var roadStep = Db.RoadSteps
                    .Include(x => x.StepResources)
                    .Include(x => x.StepExercises)
                    .FirstOrDefault(p => p.Id == id);
                if (roadStep != null)
                {
                    model.Id = roadStep.Id;
                    model.Name = roadStep.Name;
                    model.Description = roadStep.Description;

                    model.StepResources = roadStep.StepResources.ToList();
                    model.StepExercises = roadStep.StepExercises.ToList();

                    if (roadId != null)
                    {
                        model.RoadId = roadId;
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoadStep(ManageRoadStepViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            RoadStep roadStep = null;
            if (model.Id != null)
            {
                roadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == model.Id);
                if (roadStep != null)
                {
                    roadStep.Name = model.Name;
                    roadStep.Description = model.Description;
                }
            }
            else
            {
                Road road = null;
                if (model.RoadId != null)
                {
                    road = Db.Roads.FirstOrDefault(p => p.Id == model.RoadId);
                }

                roadStep = new RoadStep(model.Name, model.Description, CurrentUserId, road);
                Db.RoadSteps.Add(roadStep);

                var currentTeacher = CurrentTeacher;
                if (currentTeacher == null)
                {
                    currentTeacher = new Teacher(CurrentUser);
                    Db.Teachers.Add(currentTeacher);
                }

                currentTeacher.AddRoadStep(roadStep);
            }

            Db.SaveChanges();

            return RedirectToAction("List");
        }



        public ActionResult EditStepResource(int? roadStepId, int? id)
        {
            var model = new ManageStepResourceViewModel();
            if (id != null)
            {
                var stepResource = Db.StepResources
                    .FirstOrDefault(p => p.Id == id);
                if (stepResource != null)
                {
                    model.Id = stepResource.Id;
                    model.Name = stepResource.Name;
                    model.Description = stepResource.Description;

                    if (roadStepId != null)
                    {
                        model.RoadStepId = roadStepId;
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStepResource(ManageStepResourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StepResource stepResource = null;
            if (model.Id != null)
            {
                stepResource = Db.StepResources.FirstOrDefault(p => p.Id == model.Id);
                if (stepResource != null)
                {
                    stepResource.Name = model.Name;
                    stepResource.Description = model.Description;
                }
            }
            else
            {
                var currentUserId = User.Identity.GetUserId();

                RoadStep roadStep = null;
                if (model.RoadStepId != null)
                {
                    roadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == model.RoadStepId);
                }

                stepResource = new StepResource(model.Name, model.Description, currentUserId, roadStep);
                Db.StepResources.Add(stepResource);
            }

            Db.SaveChanges();

            return RedirectToAction("EditRoadStep", new { @id = model.RoadStepId });
        }

        public ActionResult EditStepExercise(int? roadStepId, int? id)
        {
            var model = new ManageStepExerciseViewModel();
            if (id != null)
            {
                var stepExercise = Db.StepExercises
                    .FirstOrDefault(p => p.Id == id);
                if (stepExercise != null)
                {
                    model.Id = stepExercise.Id;
                    model.Name = stepExercise.Name;
                    model.Description = stepExercise.Description;

                    if (roadStepId != null)
                    {
                        model.RoadStepId = roadStepId;
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStepExercise(ManageStepExerciseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StepExercise stepExercise = null;
            if (model.Id != null)
            {
                stepExercise = Db.StepExercises.FirstOrDefault(p => p.Id == model.Id);
                if (stepExercise != null)
                {
                    stepExercise.Name = model.Name;
                    stepExercise.Description = model.Description;
                }
            }
            else
            {
                var currentUserId = User.Identity.GetUserId();

                RoadStep roadStep = null;
                if (model.RoadStepId != null)
                {
                    roadStep = Db.RoadSteps.FirstOrDefault(p => p.Id == model.RoadStepId);
                }

                stepExercise = new StepExercise(model.Name, model.Description, currentUserId, roadStep);
                Db.StepExercises.Add(stepExercise);
            }

            Db.SaveChanges();

            return RedirectToAction("EditRoadStep", new { @id = model.RoadStepId });
        }
    }
}