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
        public IList<TrainingGroup> TrainingGroups { get; set; }
        public IList<TrainingTask> TrainingTasks { get; set; }
        public List<TrainingMessage> TrainingMessages { get; set; }
    }

    public class DoReviewViewModel
    {
        public ExerciseReview Review { get; set; }
        public string ReviewContent { get; set; }
        public int ReviewId { get; set; }
        public bool HasGraduated { get; set; }
    }

    public class ManageGroupViewModel
    {
        public ManageGroupViewModel()
        {
            GroupMembers = new List<GroupMember>();
            AddGroupMember = new AddGroupMemberViewModel();
            RoadsForGroup = new List<RoadForGroup>();
        }

        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public IList<GroupMember> GroupMembers { get; set; }

        public AddGroupMemberViewModel AddGroupMember { get; set; }
        public IList<RoadForGroup> RoadsForGroup { get; set; }
    }

    public class AddGroupMemberViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
    }

    public class IterateGroupTrainingViewModel
    {
        public string Summary { get; set; }
    }

    public class RoadForGroup
    {
        public RoadForGroup(int id, string name, bool isAvailable)
        {
            Id = id;
            Name = name;
            IsAvailable = isAvailable;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class ActivateRoadForGroupViewModel
    {
        public ActivateRoadForGroupViewModel()
        {
            RoadForGroupMembers = new List<RoadForGroupMember>();
        }

        public TrainingGroup TrainingGroup { get; set; }
        public Road Road { get; set; }

        public IList<RoadForGroupMember> RoadForGroupMembers { get; set; }
        public int RoadId { get; set; }
        public int TrainingGroupId { get; set; }
    }

    public class RoadForGroupMember
    {
        public RoadForGroupMember()
        {
            
        }

        public RoadForGroupMember(int groupMemberId, string name)
        {
            GroupMemberId = groupMemberId;
            Name = name;
        }

        public int GroupMemberId { get; set; }
        public string Name { get; set; }
        public bool CanPrepareContent { get; set; }
        public bool CanMakeReviews { get; set; }
        public bool ShouldLearn { get; set; }
    }
}