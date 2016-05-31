using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TrainingSystem.Entities;
using System.Data.Entity;

namespace TrainingSystem.Controllers
{
    public class TrainingSystemController : Controller
    {
        protected TrainingSystemContext Db = new TrainingSystemContext();

        protected string CurrentUserId
        {
            get { return User.Identity.GetUserId(); }
        }

        protected Student CurrentStudent
        {
            get { return Db.Students.FirstOrDefault(p => p.ParentUserId == CurrentUserId); }
        }

        protected Student CurrentStudentWithRoadSteps
        {
            get { return Db.Students
                    .Include(p => p.StudentXRoadSteps.Select(u => u.RoadStep))
                    .FirstOrDefault(p => p.ParentUserId == CurrentUserId); }
        }
    }
}