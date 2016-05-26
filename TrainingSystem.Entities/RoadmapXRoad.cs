using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingSystem.Entities
{
    public class RoadmapXRoad
    {
        public RoadmapXRoad()
        {
            
        }

        public RoadmapXRoad(RoadMap roadMap, Road road)
        {
            RoadMap = roadMap;
            Road = road;
        }

        public int Id { get; set; }

        public RoadMap RoadMap { get; set; }
        public Road Road { get; set; }
    }
}