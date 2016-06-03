namespace TrainingSystem.Entities
{
    public class TrainingNeed
    {
        public TrainingNeed(bool needsRoadStep, Road road, RoadMap roadMap)
        {
            NeedsRoadStep = needsRoadStep;
            Road = road;
            RoadMap = roadMap;
        }

        public TrainingNeed(bool needsContent, bool needsExercise, RoadStep roadStep)
        {
            NeedsContent = needsContent;
            NeedsExercise = needsExercise;
            RoadStep = roadStep;
        }

        public bool NeedsRoadStep { get; set; }
        public bool NeedsExercise { get; set; }
        public bool NeedsContent { get; set; }
        public Road Road { get; set; }
        public RoadMap RoadMap { get; set; }
        public RoadStep RoadStep { get; set; }
    }
}