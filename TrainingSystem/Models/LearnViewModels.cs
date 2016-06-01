using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TrainingSystem.Entities;

namespace TrainingSystem.Models
{
    public class RateStepContentViewModel
    {
        public IList<StepResourceRating> StepResourceRatings { get; set; }
        public string Comment { get; set; }
        public RoadStep RoadStep { get; set; }
        public int RoadStepId { get; set; }
    }

    public class StepResourceRating
    {
        public StepResourceRating()
        {
            
        }

        public StepResourceRating(int id, string name, string description, RatingValue ratingValue)
        {
            Id = id;
            Name = name;
            Description = description;
            RatingValue = ratingValue;
        }

        public int Id { get; set; }
        public RatingValue RatingValue { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FinishExerciseViewModel
    {
        public StepExercise Exercise { get; set; }
        public RoadStep RoadStep { get; set; }
    }

    public class RateStepExerciseViewModel
    {
        public RatingValue RatingValue { get; set; }
        public string Comment { get; set; }
        public StepExercise StepExercise { get; set; }
        public int StepExerciseId { get; set; }
        public int RoadStepId { get; set; }
    }
}