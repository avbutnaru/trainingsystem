namespace TrainingSystem.Entities
{
    public class StepExercise
    {
        protected StepExercise()
        {

        }

        public StepExercise(string name, string description, string userId, RoadStep roadStep)
        {
            Name = name;
            Description = description;
            UserId = userId;

            if (roadStep != null)
            {
                RoadStep = roadStep;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public RoadStep RoadStep { get; set; }
    }
}