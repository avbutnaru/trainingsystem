using System.Collections.Generic;

namespace TrainingSystem.Entities
{
    public class RoadStep
    {
        protected RoadStep()
        {

        }

        public RoadStep(string name, string description, string userId, Road road)
        {
            Name = name;
            Description = description;
            UserId = userId;

            if (road != null)
            {
                RoadXRoadSteps = new List<RoadXRoadStep>();
                RoadXRoadSteps.Add(new RoadXRoadStep(road, this));
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public IList<RoadXRoadStep> RoadXRoadSteps { get; set; }
        public IList<StepResource> StepResources { get; set; }
        public IList<StepExercise> StepExercises { get; set; }
    }
}