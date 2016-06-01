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

        public void RateExercise(RoadStep roadStep, int stepExerciseId, string comment, RatingValue ratingValue)
        {
            var studentExercise = GetStudentExercise(roadStep, stepExerciseId);
            studentExercise.FinishExerciseComment = comment;
            studentExercise.RatingValue = ratingValue;
        }

        public void RateResource(RoadStep roadStep, int stepResourceId, RatingValue ratingValue)
        {
            var stepResource = roadStep.StepResources.FirstOrDefault(p => p.Id == stepResourceId);
            var studentRoadStep = StudentRoadStep(roadStep);
            studentRoadStep.RateResource(stepResource, ratingValue);
        }

        public void StartExercise(RoadStep roadStep, int exerciseId)
        {
            var exercise = roadStep.StepExercises.FirstOrDefault(p => p.Id == exerciseId);
            var studentRoadStep = StudentRoadStep(roadStep);
            studentRoadStep.StartExercise(exercise);
        }

        public void FinishExercise(RoadStep roadStep, int exerciseId, string fileName)
        {
            var exercise = roadStep.StepExercises.FirstOrDefault(p => p.Id == exerciseId);
            var studentRoadStep = StudentRoadStep(roadStep);
            studentRoadStep.FinishExercise(exercise, fileName);
        }

        public StudentExercise GetStudentExercise(RoadStep roadStep, int exerciseId)
        {
            var studentRoadStep = StudentRoadStep(roadStep);
            var exercise = roadStep.StepExercises.FirstOrDefault(p => p.Id == exerciseId);
            return studentRoadStep.GetStudentExercise(exercise);
        }
    }
}