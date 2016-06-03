using System.Collections.Generic;

namespace TrainingSystem.Entities
{
    public enum GroupRole
    {
        Teacher = 1,
        Student = 2
    }

    public class GroupMember
    {
        public GroupMember()
        {

        }

        public GroupMember(TrainingGroup trainingGroup, AspNetUsers aspNetUser, bool isTeacher, bool isStudent)
        {
            TrainingGroup = trainingGroup;
            AspNetUser = aspNetUser;
            IsTeacher = isTeacher;
            IsStudent = isStudent;
        }

        public int Id { get; set; }

        public TrainingGroup TrainingGroup { get; set; }
        public AspNetUsers AspNetUser { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
    }
}