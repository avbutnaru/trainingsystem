using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingSystem.Entities;
using TrainingSystem.Models;
using System.Data.Entity;

namespace TrainingSystem.Controllers
{
    public class CommunityController : TrainingSystemController
    {
        public ActionResult Index()
        {
            var model = new CommunityMainViewModel();
            var currentTeacher = CurrentTeacher;
            model.ReviewsToDo =
                Db.ExerciseReviews
                .Include(p => p.StudentExercise.StepExercise)
                .Where(
                    p =>
                        p.Teacher.Id == currentTeacher.Id &&
                        p.ExerciseReviewStatus == ExerciseReviewStatus.WaitingForReview).ToList();

            return View(model);
        }

        public ActionResult DoReview(int id)
        {
            var model = new DoReviewViewModel();

            var review = Db.ExerciseReviews
                .Include(p => p.StudentExercise.StepExercise)
                .FirstOrDefault(p => p.Id == id);

            model.Review = review;
            model.ReviewId = review.Id;

            return View(model);
        }

        public ActionResult SaveReview(DoReviewViewModel model)
        {
            var review = Db.ExerciseReviews
                .FirstOrDefault(p => p.Id == model.ReviewId);

            review.FinishReview(model.ReviewContent);

            Db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}