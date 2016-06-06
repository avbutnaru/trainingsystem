using System.Collections.Generic;
using System.Linq.Expressions;

namespace TrainingSystem.Entities
{
    public class TrainingSystemInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TrainingSystemContext>
    {
        protected override void Seed(TrainingSystemContext context)
        {
            var teacherUserId = "0fef0fe8-8327-4020-bd00-25590bb4ef07";
            var student1UserId = "0fef0fe8-8327-4020-bd00-25590bb4ef05";
            var student2UserId = "0fef0fe8-8327-4020-bd00-25590bb4ef09";
            var teacher = new AspNetUsers
            {
                Id = teacherUserId,
                Email = "teacher1@gmail.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "teacher1@gmail.com"
            };
            context.AspNetUsers.Add(teacher);

            var student1 = new AspNetUsers
            {
                Id = student1UserId,
                Email = "student1@gmail.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "student1@gmail.com"
            };
            var student2 = new AspNetUsers
            {
                Id = student2UserId,
                Email = "student2@gmail.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "student2@gmail.com"
            };
            context.AspNetUsers.Add(student1);
            context.AspNetUsers.Add(student2);

            context.SaveChanges();

            var trainingGroup = new TrainingGroup("learners", "learn .Net", teacherUserId);
            trainingGroup.GroupMembers = new List<GroupMember>();
            trainingGroup.GroupMembers.Add(new GroupMember(trainingGroup, teacher, true, false));
            trainingGroup.GroupMembers.Add(new GroupMember(trainingGroup, student1, false, true));
            trainingGroup.GroupMembers.Add(new GroupMember(trainingGroup, student2, false, true));
            context.TrainingGroups.Add(trainingGroup);

            context.SaveChanges();

            var roadMap = new RoadMap("1111", "11111", teacherUserId);
            context.RoadMaps.Add(roadMap);

            var road = new Road("2222", "2222", teacherUserId, roadMap);
            context.Roads.Add(road);

            var roadStep = new RoadStep("333", "333", teacherUserId, road);
            context.RoadSteps.Add(roadStep);

            var teacherRole = new Teacher(teacher);
            context.Teachers.Add(teacherRole);
            teacherRole.AddRoadStep(roadStep);

            var resource = new StepResource("444", "444", teacherUserId, roadStep);
            context.StepResources.Add(resource);
            var exercise = new StepExercise("444", "444", teacherUserId, roadStep);
            context.StepExercises.Add(exercise);

            var student = new Student(student1);
            context.Students.Add(student);

            var studentRoadStep = new StudentXRoadStep(student, roadStep, LearningStatus.StudyingResources);
            context.StudentXRoadSteps.Add(studentRoadStep);
        }
    }
}