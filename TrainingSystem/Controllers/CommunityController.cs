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
            if (currentTeacher != null)
            {
                model.ReviewsToDo =
                    Db.ExerciseReviews
                        .Include(p => p.StudentExercise.StepExercise)
                        .Where(
                            p =>
                                p.Teacher.Id == currentTeacher.Id &&
                                p.ExerciseReviewStatus == ExerciseReviewStatus.WaitingForReview).ToList();
            }

            var currentStudent = CurrentStudent;
            if (currentStudent != null)
            {
                model.ReviewsReceived =
                    Db.ExerciseReviews
                        .Include(p => p.StudentExercise.StepExercise)
                        .Where(
                            p =>
                                p.StudentExercise.StudentXRoadStep.Student.Id == currentStudent.Id &&
                                p.ExerciseReviewStatus == ExerciseReviewStatus.Reviewed).ToList();
            }

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
                .Include(p => p.StudentExercise.StepExercise)
                .Include(p => p.StudentExercise.StudentXRoadStep)
                .FirstOrDefault(p => p.Id == model.ReviewId);

            review.FinishReview(model.ReviewContent, model.HasGraduated);

            Db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ViewReview(int id)
        {
            var model = new DoReviewViewModel();

            var review = Db.ExerciseReviews
                .Include(p => p.StudentExercise.StepExercise)
                .Include(p => p.StudentExercise.StudentXRoadStep)
                .FirstOrDefault(p => p.Id == id);

            model.Review = review;
            model.ReviewId = review.Id;
            model.ReviewContent = review.ReviewContent;
            model.HasGraduated = review.HasGraduated;

            return View(model);
        }
    }
}