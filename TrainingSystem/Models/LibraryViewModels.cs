using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TrainingSystem.Entities;

namespace TrainingSystem.Models
{
    public class LibraryMainViewModel
    {
        public IList<RoadMap> RoadMaps { get; set; }
    }

    public class PeopleMainViewModel
    {
        public IList<AspNetUsers> Users { get; set; }
    }

    public class RoadMapMainViewModel
    {
        public RoadMap RoadMap { get; set; }
    }

    public class ProfileViewModel
    {
        public IList<ExerciseReview> ReviewsReceived { get; set; }
        public IList<ExerciseReview> ReviewsGiven { get; set; }
        public IList<GroupMember> GroupMembers { get; set; }
        public IList<StudentExercise> StudentExercises { get; set; }
        public IList<StudentXRoadStep> RoadSteps { get; set; }
        public AspNetUsers User { get; set; }
    }

    public class RoadMainViewModel
    {
        public Road Road { get; set; }
    }

    public class RoadStepMainViewModel
    {
        public RoadStep RoadStep { get; set; }
        public StudentXRoadStep StudentXRoadStep { get; set; }
        public string Message { get; set; }
    }

    public class SendMessageViewModel
    {
        public RoadMap RoadMap { get; set; }
        public Road Road { get; set; }
        public RoadStep RoadStep { get; set; }
        public string Content { get; set; }
        public string Message { get; set; }
        public int? RoadStepId { get; set; }
        public int? RoadId { get; set; }
        public int? RoadMapId { get; set; }
    }
}