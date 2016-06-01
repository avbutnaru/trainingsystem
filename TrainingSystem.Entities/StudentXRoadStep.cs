using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingSystem.Entities
{
    public enum LearningStatus
    {
        StudyingResources = 1,
        FinishedResources
    }

    public enum ExerciseStatus
    {
        Started = 1,
        Finished
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
        public IList<StudentExercise> StudentExercises { get; set; }

        public void RateResource(StepResource stepResource, RatingValue ratingValue)
        {
            StudentResourceRatings.Add(new StudentResourceRating(this, stepResource, ratingValue));
        }

        public void StartExercise(StepExercise exercise)
        {
            var studentExercise = new StudentExercise(this, exercise);
            studentExercise.StartExercise();
            StudentExercises.Add(studentExercise);
        }

        public bool CanStartExercise(StepExercise stepExercise)
        {
            return StudentExercises.All(p => p.StepExercise.Id != stepExercise.Id);
        }

        public bool CanFinishExercise(StepExercise stepExercise)
        {
            return StudentExercises.Any(p => p.StepExercise.Id == stepExercise.Id && p.ExerciseStatus == ExerciseStatus.Started);
        }

        public void FinishExercise(StepExercise exercise, string fileName)
        {
            var studentExercise = GetStudentExercise(exercise);
            studentExercise.FinishExercise(fileName);
        }

        public StudentExercise GetStudentExercise(StepExercise exercise)
        {
            return StudentExercises.FirstOrDefault(p => p.Id == exercise.Id);
        }
    }
}