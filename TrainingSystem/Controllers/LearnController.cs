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

            

            return RedirectToAction("Step", "Library", new { @id = id });
        }
    }
}