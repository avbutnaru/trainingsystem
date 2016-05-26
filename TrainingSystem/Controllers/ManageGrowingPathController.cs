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

namespace TrainingSystem.Controllers
{
    [Authorize]
    public class ManageGrowingPathController : Controller
    {
        private TrainingSystemContext db = new TrainingSystemContext();

        public ManageGrowingPathController()
        {
        }

        public ActionResult Edit(int? id)
        {
            var model = new ManageGrowingPathViewModel();
            if (id != null)
            {
                var growingPath = db.GrowingPaths.FirstOrDefault(p => p.Id == id);
                if (growingPath != null)
                {
                    model.Id = growingPath.Id;
                    model.Name = growingPath.Name;
                    model.Description = growingPath.Description;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManageGrowingPathViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            GrowingPath growingPath = null;
            if (model.Id != null)
            {
                growingPath = db.GrowingPaths.FirstOrDefault(p => p.Id == model.Id);
                if (growingPath != null)
                {
                    growingPath.Name = model.Name;
                    growingPath.Description = model.Description;
                }
            }
            else
            {
                var currentUserId = User.Identity.GetUserId();

                growingPath = new GrowingPath(model.Name, model.Description, currentUserId);
                db.GrowingPaths.Add(growingPath);
            }

            db.SaveChanges();
            
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ManageGrowingPathListViewModel();
            var currentUserId = User.Identity.GetUserId();
            model.GrowingPaths = db.GrowingPaths.Where(p => p.UserId == currentUserId).ToList();
            return View(model);
        }

    }
}