using System;

namespace TrainingSystem.Entities
{
    public class StudentExercise
    {
        public StudentExercise()
        {

        }

        public StudentExercise(StudentXRoadStep studentXRoadStep, StepExercise stepExercise)
        {
            StudentXRoadStep = studentXRoadStep;
            CreateDate = DateTime.Now;
            StepExercise = stepExercise;
        }

        public ExerciseStatus ExerciseStatus { get; set; }

        public StepExercise StepExercise { get; set; }

        public StudentXRoadStep StudentXRoadStep { get; set; }

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? StartExerciseDate { get; set; }
        public DateTime? FinishExerciseDate { get; set; }
        public string FinishExerciseFileName { get; set; }
        public string FinishExerciseComment { get; set; }
        public RatingValue? RatingValue { get; set; }

        public void StartExercise()
        {
            ExerciseStatus = ExerciseStatus.Started;
            StartExerciseDate = DateTime.Now;
        }

        public void FinishExercise(string fileName)
        {
            ExerciseStatus = ExerciseStatus.Finished;
            FinishExerciseDate = DateTime.Now;
            FinishExerciseFileName = fileName;
        }
    }
}