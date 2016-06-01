namespace TrainingSystem.Entities
{
    public class TeacherXRoadStep
    {
        public TeacherXRoadStep()
        {

        }

        public TeacherXRoadStep(RoadStep roadStep, Teacher teacher)
        {
            RoadStep = roadStep;
            Teacher = teacher;
        }

        public int Id { get; set; }

        public RoadStep RoadStep { get; set; }
        public Teacher Teacher { get; set; }
    }
}