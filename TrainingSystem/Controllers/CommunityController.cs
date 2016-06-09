using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingSystem.Entities;
using TrainingSystem.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

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

            model.TrainingGroups =
                Db.TrainingGroups.Where(p => p.UserId == CurrentUserId).ToList();

            model.TrainingTasks =
                Db.TrainingTasks
                .Include(p => p.MemberReceiving)
                .Include(p => p.MemberActing)
                .Include(p => p.RoadStep)
                .Where(p => p.MemberActing.Id == CurrentUserId).ToList();

            model.TrainingMessages =
                Db.TrainingMessages
                .Include(p => p.RoadStep)
                .Include(p => p.RoadMap)
                .Include(p => p.Road)
                .Include(p => p.Sender)
                .Where(p => p.Recipient.Id == CurrentUserId).ToList();

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



        public ActionResult EditGroup(int? id)
        {
            var model = new ManageGroupViewModel();
            if (id != null)
            {
                var group = Db.TrainingGroups
                    .Include(x => x.GroupMembers.Select(p => p.AspNetUser))
                    .Include(x => x.TrainingGroupXRoads.Select(p => p.Road))
                    .FirstOrDefault(p => p.Id == id);

                if (group != null)
                {
                    model.Id = group.Id;
                    model.Name = group.Name;
                    model.Description = group.Description;
                    model.GroupMembers = group.GroupMembers.ToList();

                    var roads = Db.Roads.ToList();
                    foreach (var road in roads)
                    {
                        var isAvailable = group.RoadIsAvailable(road);
                        model.RoadsForGroup.Add(new RoadForGroup(road.Id, road.Name, isAvailable));
                    }
                }
            }

            return View(model);
        }

        public ActionResult RemoveGroupMember(int id, int trainingGroupId)
        {
            var groupMember = Db.GroupMembers.FirstOrDefault(p => p.Id == id);
            Db.GroupMembers.Remove(groupMember);
            Db.SaveChanges();

            return RedirectToAction("EditGroup", new {@id = trainingGroupId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(ManageGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TrainingGroup trainingGroup = null;
            if (model.Id != null)
            {
                trainingGroup = Db.TrainingGroups.FirstOrDefault(p => p.Id == model.Id);
                if (trainingGroup != null)
                {
                    trainingGroup.Name = model.Name;
                    trainingGroup.Description = model.Description;
                }
            }
            else
            {
                var currentUserId = User.Identity.GetUserId();

                trainingGroup = new TrainingGroup(model.Name, model.Description, currentUserId);
                Db.TrainingGroups.Add(trainingGroup);
            }

            Db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGroupMember(ManageGroupViewModel model)
        {
            var trainingGroup = Db.TrainingGroups.FirstOrDefault(p => p.Id == model.Id);
            var user = Db.AspNetUsers.FirstOrDefault(p => p.Email == model.AddGroupMember.Email);
            var newGroupMember = new GroupMember(trainingGroup, user, model.AddGroupMember.IsTeacher, model.AddGroupMember.IsStudent);
            Db.GroupMembers.Add(newGroupMember);
            Db.SaveChanges();

            return RedirectToAction("EditGroup", new { @id = model.Id });
        }

        public ActionResult ActivateRoadForGroup(int id, int trainingGroupId)
        {
            var model = new ActivateRoadForGroupViewModel();
            var road = Db.Roads.FirstOrDefault(p => p.Id == id);
            var trainingGroup = Db.TrainingGroups
                .Include(p => p.GroupMembers.Select(u => u.AspNetUser))
                .FirstOrDefault(p => p.Id == trainingGroupId);
            model.Road = road;
            model.RoadId = road.Id;
            model.TrainingGroup = trainingGroup;
            model.TrainingGroupId = trainingGroup.Id;
            foreach (var groupMember in trainingGroup.GroupMembers)
            {
                model.RoadForGroupMembers.Add(new RoadForGroupMember(groupMember.Id, groupMember.AspNetUser.UserName));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateRoadForGroup(ActivateRoadForGroupViewModel model)
        {
            var road = Db.Roads.FirstOrDefault(p => p.Id == model.RoadId);
            var trainingGroup = Db.TrainingGroups
                .Include(p => p.GroupMembers.Select(u => u.AspNetUser))
                .FirstOrDefault(p => p.Id == model.TrainingGroupId);

            trainingGroup.ActivateRoad(road);
            foreach (var roadForGroupMember in model.RoadForGroupMembers)
            {
                var groupMember = Db.GroupMembers.FirstOrDefault(p => p.Id == roadForGroupMember.GroupMemberId);
                trainingGroup.ActivateMemberForRoad(road, groupMember, roadForGroupMember.CanMakeReviews,
                    roadForGroupMember.CanPrepareContent, roadForGroupMember.ShouldLearn);
            }
            Db.SaveChanges();

            return RedirectToAction("EditGroup", new { @id = trainingGroup.Id });
        }

        public ActionResult DeactivateRoadForGroup(int id, int trainingGroupId)
        {
            var road = Db.Roads.FirstOrDefault(p => p.Id == id);
            var trainingGroup = Db.TrainingGroups
                .Include(p => p.TrainingGroupXRoads.Select(u => u.GroupMembersForRoad))
                .FirstOrDefault(p => p.Id == trainingGroupId);

            var roadForGroup = trainingGroup.RoadForGroup(road);
            Db.TrainingGroupXRoads.Remove(roadForGroup);
            Db.SaveChanges();

            return RedirectToAction("EditGroup", new { @id = trainingGroup.Id });
        }

        public ActionResult StartTrainingGroup(int id)
        {
            var trainingGroup = Db.TrainingGroups
                .FirstOrDefault(p => p.Id == id);

            trainingGroup.AutomatedTrainingIsActive = true;
            Db.SaveChanges();

            return RedirectToAction("IterateGroupTraining", new { @id = trainingGroup.Id });
        }

        public ActionResult IterateGroupTraining(int id)
        {
            var trainingGroup = Db.TrainingGroups
                .Include(p => p.TrainingGroupXRoads.Select(u => u.GroupMembersForRoad.Select(a => a.GroupMember)))
                .Include(p => p.TrainingGroupXRoads.Select(u => u.Road).Select(a => a.RoadXRoadSteps.Select(b => b.RoadStep)))
                .Include(p => p.GroupMembers.Select(u => u.AspNetUser).Select(a => a.Student).Select(b => b.StudentXRoadSteps.Select(c => c.RoadStep).Select(d => d.StepExercises)))
                .Include(p => p.GroupMembers.Select(u => u.AspNetUser).Select(a => a.TrainingTasks.Select(b => b.MemberActing).Select(c => c.Student)))
                .FirstOrDefault(p => p.Id == id);

            IList<TrainingTask> trainingTasks = trainingGroup.IterateTraining();

            Db.SaveChanges();

            var summary = string.Empty;
            foreach (var trainingTask in trainingTasks)
            {
                summary += trainingTask + "<br />";
            }

            var model = new IterateGroupTrainingViewModel();
            model.Summary = summary;
            return View(model);
        }

        public ActionResult TaskIsStarted(int id)
        {
            var task = Db.TrainingTasks.FirstOrDefault(p => p.Id == id);
            task.TrainingTaskStatus = TrainingTaskStatus.InProgress;
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TaskIsDone(int id)
        {
            var task = Db.TrainingTasks.FirstOrDefault(p => p.Id == id);
            task.TrainingTaskStatus = TrainingTaskStatus.Done;
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}