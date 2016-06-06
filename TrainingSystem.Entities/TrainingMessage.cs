namespace TrainingSystem.Entities
{
    public class TrainingMessage
    {
        public string Content { get; set; }
        public AspNetUsers Sender { get; set; }
        public AspNetUsers Recipient { get; set; }
        public Road Road { get; set; }
        public RoadMap RoadMap { get; set; }
        public RoadStep RoadStep { get; set; }

        protected TrainingMessage()
        {
        }

        public TrainingMessage(string content, AspNetUsers sender, AspNetUsers recipient, Road road, RoadMap roadMap, RoadStep roadStep)
        {
            Content = content;
            Sender = sender;
            Recipient = recipient;
            Road = road;
            RoadMap = roadMap;
            RoadStep = roadStep;
        }

        public int Id { get; set; }
        
    }
}