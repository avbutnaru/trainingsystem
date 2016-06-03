using System.Collections.Generic;

namespace TrainingSystem.Entities
{
    public class TrainingGroupXRoad
    {
        public TrainingGroupXRoad()
        {
            GroupMembersForRoad = new List<GroupMemberForRoad>();
        }

        public TrainingGroupXRoad(TrainingGroup trainingGroup, Road road)
        {
            TrainingGroup = trainingGroup;
            Road = road;
            GroupMembersForRoad = new List<GroupMemberForRoad>();
        }

        public int Id { get; set; }

        public TrainingGroup TrainingGroup { get; set; }
        public Road Road { get; set; }
        public IList<GroupMemberForRoad> GroupMembersForRoad { get; set; }

        public void AddMember(GroupMember groupMember, bool canMakeReviews, bool canPrepareContent, bool shouldLearn)
        {
            GroupMembersForRoad.Add(new GroupMemberForRoad(groupMember, this, canMakeReviews, canPrepareContent,
                shouldLearn));
        }
    }
}