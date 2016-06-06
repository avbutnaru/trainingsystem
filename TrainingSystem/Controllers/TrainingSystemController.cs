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

        protected AspNetUsers CurrentUser
        {
            get { return Db.AspNetUsers.FirstOrDefault(p => p.Id == CurrentUserId); }
        }

        protected Student CurrentStudent
        {
            get { return Db.Students.FirstOrDefault(p => p.ParentUser.Id == CurrentUserId); }
        }

        protected Teacher CurrentTeacher
        {
            get { return Db.Teachers.FirstOrDefault(p => p.ParentUser.Id == CurrentUserId); }
        }

        protected Student CurrentStudentWithRoadSteps
        {
            get { return Db.Students
                    .Include(p => p.StudentXRoadSteps.Select(u => u.RoadStep))
                    .Include(p => p.StudentXRoadSteps.Select(u => u.StudentExercises.Select(w => w.StepExercise)))
                    .FirstOrDefault(p => p.ParentUser.Id == CurrentUserId); }
        }
    }
}