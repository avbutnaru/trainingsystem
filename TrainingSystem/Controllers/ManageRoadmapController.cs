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
    public class ManageRoadMapController : Controller
    {
        private TrainingSystemContext db = new TrainingSystemContext();

        public ManageRoadMapController()
        {
        }

        public ActionResult Edit(int? id)
        {
            var model = new ManageRoadMapViewModel();
            if (id != null)
            {
                var roadMap = db.RoadMaps
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
                roadMap = db.RoadMaps.FirstOrDefault(p => p.Id == model.Id);
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
                db.RoadMaps.Add(roadMap);
            }

            db.SaveChanges();
            
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ManageRoadMapListViewModel();
            var currentUserId = User.Identity.GetUserId();
            model.RoadMaps = db.RoadMaps.Where(p => p.UserId == currentUserId).ToList();
            return View(model);
        }



        public ActionResult EditRoad(int? roadmapId, int? id)
        {
            var model = new ManageRoadViewModel();
            if (id != null)
            {
                var road = db.Roads.FirstOrDefault(p => p.Id == id);
                if (road != null)
                {
                    model.Id = road.Id;
                    model.Name = road.Name;
                    model.Description = road.Description;

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
                road = db.Roads.FirstOrDefault(p => p.Id == model.Id);
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
                    roadmap = db.RoadMaps.FirstOrDefault(p => p.Id == model.RoadmapId);
                }

                road = new Road(model.Name, model.Description, currentUserId, roadmap);
                db.Roads.Add(road);
            }

            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}