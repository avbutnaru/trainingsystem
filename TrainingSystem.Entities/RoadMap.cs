using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingSystem.Entities
{
    public class RoadMap
    {
        protected RoadMap()
        {
        }

        public RoadMap(string name, string description, string userId)
        {
            Name = name;
            Description = description;
            UserId = userId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }

        public IList<RoadmapXRoad> RoadmapXRoads { get; set; }
    }
}