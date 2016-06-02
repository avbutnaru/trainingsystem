namespace TrainingSystem.Entities
{
    public enum ExerciseReviewStatus
    {
        WaitingForReview = 1,
        Reviewed = 2
    }

    public class ExerciseReview
    {
        protected ExerciseReview()
        {

        }

        public ExerciseReview(Teacher teacher, StudentExercise studentExercise, ExerciseReviewStatus exerciseReviewStatus)
        {
            Teacher = teacher;
            StudentExercise = studentExercise;
            ExerciseReviewStatus = exerciseReviewStatus;
        }

        public Teacher Teacher { get; set; }
        public ExerciseReviewStatus ExerciseReviewStatus { get; set; }
        public StudentExercise StudentExercise { get; set; }
        public int Id { get; set; }
        public string ReviewContent { get; set; }
        public bool HasGraduated { get; set; }

        public void FinishReview(string reviewContent, bool hasGraduated)
        {
            ReviewContent = reviewContent;
            ExerciseReviewStatus = ExerciseReviewStatus.Reviewed;
            HasGraduated = hasGraduated;

            StudentExercise.FinishExerciseReview(hasGraduated);
        }
    }
}