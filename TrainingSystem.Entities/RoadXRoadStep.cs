namespace TrainingSystem.Entities
{
    public class RoadXRoadStep
    {
        public RoadXRoadStep()
        {

        }

        public RoadXRoadStep(Road road, RoadStep roadStep)
        {
            RoadStep = roadStep;
            Road = road;
        }

        public int Id { get; set; }

        public RoadStep RoadStep { get; set; }
        public Road Road { get; set; }
    }
}