using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TrainingSystem.Entities;

namespace TrainingSystem.Models
{
    public class CommunityMainViewModel
    {
        public IList<ExerciseReview> ReviewsToDo { get; set; }
    }

    public class DoReviewViewModel
    {
        public ExerciseReview Review { get; set; }
        public string ReviewContent { get; set; }
        public int ReviewId { get; set; }
    }
}