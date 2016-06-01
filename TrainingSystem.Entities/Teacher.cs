using System.Collections.Generic;

namespace TrainingSystem.Entities
{
    public class Teacher
    {
        protected Teacher()
        {

        }

        public Teacher(string parentUserId)
        {
            ParentUserId = parentUserId;
        }

        public int Id { get; set; }
        public string ParentUserId { get; set; }
        public IList<TeacherXRoadStep> TeacherXRoadSteps { get; set; }
        public IList<ExerciseReview> ExerciseReviews { get; set; }

        public void AddRoadStep(RoadStep roadStep)
        {
            if (TeacherXRoadSteps == null)
            {
                TeacherXRoadSteps = new List<TeacherXRoadStep>();
            }

            TeacherXRoadSteps.Add(new TeacherXRoadStep(roadStep, this));
        }

        public void PrepareForReview(RoadStep roadStep, int stepExerciseId, Student student)
        {
            if (ExerciseReviews == null)
            {
                ExerciseReviews = new List<ExerciseReview>();
            }

            var studentExercise = student.GetStudentExercise(roadStep, stepExerciseId);

            ExerciseReviews.Add(new ExerciseReview(this, studentExercise, ExerciseReviewStatus.WaitingForReview));
        }
    }
}