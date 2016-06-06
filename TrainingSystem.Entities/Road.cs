using System.Collections.Generic;

namespace TrainingSystem.Entities
{
    public class Road
    {
        protected Road()
        {

        }

        public Road(string name, string description, string userId, RoadMap roadmap)
        {
            Name = name;
            Description = description;
            UserId = userId;

            if (roadmap != null)
            {
                RoadmapXRoads = new List<RoadmapXRoad>();
                RoadmapXRoads.Add(new RoadmapXRoad(roadmap, this));
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public IList<RoadmapXRoad> RoadmapXRoads { get; set; }
        public IList<RoadXRoadStep> RoadXRoadSteps { get; set; }

        public RoadStep GetStartingStep()
        {
            foreach (var roadXRoadStep in RoadXRoadSteps)
            {
                return roadXRoadStep.RoadStep;
            }
            return null;
        }
    }
}