using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TrainingSystem.Entities;

namespace TrainingSystem.Models
{
    public class CommunityMainViewModel
    {
        public CommunityMainViewModel()
        {
            ReviewsToDo = new List<ExerciseReview>();
            ReviewsReceived = new List<ExerciseReview>();
        }

        public IList<ExerciseReview> ReviewsToDo { get; set; }
        public IList<ExerciseReview> ReviewsReceived { get; set; }
    }

    public class DoReviewViewModel
    {
        public ExerciseReview Review { get; set; }
        public string ReviewContent { get; set; }
        public int ReviewId { get; set; }
        public bool HasGraduatedRoadStep { get; set; }
    }
}