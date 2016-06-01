using System.Collections.Generic;

namespace TrainingSystem.Entities
{
    public class Teacher
    {
        protected Teacher()
        {

        }

        public Teacher(string parentUserId)
        {
            ParentUserId = parentUserId;
        }

        public string ParentUserId { get; set; }
        public IList<TeacherXRoadStep> TeacherXRoadSteps { get; set; }
    }
}