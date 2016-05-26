namespace TrainingSystem.Entities
{
    public class GrowingPath
    {
        protected GrowingPath()
        {
            
        }

        public GrowingPath(string name, string description, string userId)
        {
            Name = name;
            Description = description;
            UserId = userId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}