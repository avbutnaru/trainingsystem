using System;

namespace TrainingSystem.Entities
{
    public enum LearningStatus
    {
        StudyingResources = 1
    }

    public class StudentXRoadStep
    {
        public StudentXRoadStep()
        {

        }

        public StudentXRoadStep(Student student, RoadStep roadStep, LearningStatus learningStatus)
        {
            RoadStep = roadStep;
            Student = student;
            CreateDate = DateTime.Now;
            LearningStatus = learningStatus;
        }

        public int Id { get; set; }

        public RoadStep RoadStep { get; set; }
        public Student Student { get; set; }
        public LearningStatus LearningStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}