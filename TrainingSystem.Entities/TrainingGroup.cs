using System.Collections.Generic;
using System.Linq;

namespace TrainingSystem.Entities
{
    public class TrainingGroup
    {
        protected TrainingGroup()
        {
            TrainingGroupXRoads = new List<TrainingGroupXRoad>();
        }

        public TrainingGroup(string name, string description, string userId)
        {
            Name = name;
            Description = description;
            UserId = userId;

            TrainingGroupXRoads = new List<TrainingGroupXRoad>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public IList<GroupMember> GroupMembers { get; set; }
        public IList<TrainingGroupXRoad> TrainingGroupXRoads { get; set; }

        public void ActivateRoad(Road road)
        {
            TrainingGroupXRoads.Add(new TrainingGroupXRoad(this, road));
        }

        public void ActivateMemberForRoad(Road road, GroupMember groupMember, bool canMakeReviews, bool canPrepareContent, bool shouldLearn)
        {
            var roadForGroup = RoadForGroup(road);
            roadForGroup.AddMember(groupMember, canMakeReviews, canPrepareContent, shouldLearn);
        }

        public TrainingGroupXRoad RoadForGroup(Road road)
        {
            return TrainingGroupXRoads.FirstOrDefault(p => p.Road.Id == road.Id);
        }

        public bool RoadIsAvailable(Road road)
        {
            return TrainingGroupXRoads.Any(p => p.Road.Id == road.Id);
        }
    }
}
