using System.Collections.Generic;

namespace TrainingSystem.Entities
{
    public class Student
    {
        protected Student()
        {
            StudentXRoadSteps = new List<StudentXRoadStep>();
        }

        public Student(string parentUserId)
        {
            ParentUserId = parentUserId;
            StudentXRoadSteps = new List<StudentXRoadStep>();
        }

        public int Id { get; set; }
        public string ParentUserId { get; set; }

        public IList<StudentXRoadStep> StudentXRoadSteps { get; set; }
    }
}