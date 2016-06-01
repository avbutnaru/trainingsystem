using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingSystem.Entities
{
    public enum RatingValue
    {
        Bad = 1,
        Medium = 2,
        Good = 3
    }

    public class Student
    {
        protected Student()
        {
            StudentXRoadSteps = new List<StudentXRoadStep>();
        }

        public Student(string parentUserId)
        {
            ParentUserId = parentUserId;
            StudentXRoadSteps = new List<StudentXRoadStep>();
        }

        public int Id { get; set; }
        public string ParentUserId { get; set; }

        public IList<StudentXRoadStep> StudentXRoadSteps { get; set; }

        public void FinishLearningResources(RoadStep roadStep)
        {
            var studentRoadStep = StudentRoadStep(roadStep);
            studentRoadStep.LearningStatus = LearningStatus.FinishedResources;
            studentRoadStep.FinishResourcesDate = DateTime.Now;
        }

        private StudentXRoadStep StudentRoadStep(RoadStep roadStep)
        {
            return StudentXRoadSteps.FirstOrDefault(p => p.RoadStep.Id == roadStep.Id);
        }

        public void AddFinishedResourcesComment(RoadStep roadStep, string comment)
        {
            var studentRoadStep = StudentRoadStep(roadStep);
            studentRoadStep.FinishResourcesComment = comment;
        }

        public void RateResource(RoadStep roadStep, int stepResourceId, RatingValue ratingValue)
        {
            var stepResource = roadStep.StepResources.FirstOrDefault(p => p.Id == stepResourceId);
            var studentRoadStep = StudentRoadStep(roadStep);
            studentRoadStep.RateResource(stepResource, ratingValue);
        }
    }
}