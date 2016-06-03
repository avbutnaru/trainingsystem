namespace TrainingSystem.Entities
{
    public class GroupMemberForRoad
    {
        public GroupMemberForRoad()
        {

        }

        public GroupMemberForRoad(GroupMember groupMember, TrainingGroupXRoad trainingGroupXRoad, bool canMakeReviews, bool canPrepareContent, bool shouldLearn)
        {
            GroupMember = groupMember;
            TrainingGroupXRoad = TrainingGroupXRoad;
            CanMakeReviews = canMakeReviews;
            CanPrepareContent = canPrepareContent;
            ShouldLearn = shouldLearn;
        }

        public int Id { get; set; }

        public GroupMember GroupMember { get; set; }
        public TrainingGroupXRoad TrainingGroupXRoad { get; set; }
        public bool CanMakeReviews { get; set; }
        public bool CanPrepareContent { get; set; }
        public bool ShouldLearn { get; set; }
    }
}