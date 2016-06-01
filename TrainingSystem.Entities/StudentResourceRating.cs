using System;

namespace TrainingSystem.Entities
{
    public class StudentResourceRating
    {
        public StudentResourceRating()
        {

        }

        public StudentResourceRating(StudentXRoadStep studentXRoadStep, StepResource stepResource, RatingValue ratingValue)
        {
            StudentXRoadStep = studentXRoadStep;
            CreateDate = DateTime.Now;
            StepResource = stepResource;
            RatingValue = ratingValue;
        }

        public StudentXRoadStep StudentXRoadStep { get; set; }

        public RatingValue RatingValue { get; set; }

        public StepResource StepResource { get; set; }

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        
    }
}