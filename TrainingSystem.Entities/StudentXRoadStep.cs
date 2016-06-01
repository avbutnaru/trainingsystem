using System;
using System.Collections.Generic;

namespace TrainingSystem.Entities
{
    public enum LearningStatus
    {
        StudyingResources = 1,
        FinishedResources
    }

    public class StudentXRoadStep
    {
        public StudentXRoadStep()
        {
            StudentResourceRatings = new List<StudentResourceRating>();
        }

        public StudentXRoadStep(Student student, RoadStep roadStep, LearningStatus learningStatus)
        {
            RoadStep = roadStep;
            Student = student;
            CreateDate = DateTime.Now;
            LearningStatus = learningStatus;

            StudentResourceRatings = new List<StudentResourceRating>();
        }

        public int Id { get; set; }

        public RoadStep RoadStep { get; set; }
        public Student Student { get; set; }
        public LearningStatus LearningStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishResourcesDate { get; set; }
        public string FinishResourcesComment { get; set; }

        public IList<StudentResourceRating> StudentResourceRatings { get; set; }

        public void RateResource(StepResource stepResource, RatingValue ratingValue)
        {
            StudentResourceRatings.Add(new StudentResourceRating(this, stepResource, ratingValue));
        }
    }
}