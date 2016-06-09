using System.Collections.Generic;
using System.Linq.Expressions;

namespace TrainingSystem.Entities
{
    public class TrainingSystemInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TrainingSystemContext>
    {
        protected override void Seed(TrainingSystemContext context)
        {
            var teacherUserId1 = "0fef0fe8-8327-4020-bd00-25590bb4ef07";
            var teacherUserId2 = "0fef0fe8-8327-4020-bd00-25590bb4ef11";
            var student1UserId = "0fef0fe8-8327-4020-bd00-25590bb4ef05";
            var student2UserId = "0fef0fe8-8327-4020-bd00-25590bb4ef09";
            var student3UserId = "0fef0fe8-8327-4020-bd00-25590bb4ef14";
            var teacher1 = new AspNetUsers
            {
                Id = teacherUserId1,
                Email = "senior1@companyx.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "senior1@companyx.com"
            };

            var teacher2 = new AspNetUsers
            {
                Id = teacherUserId2,
                Email = "senior2@companyx.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "senior2@companyx.com"
            };

            context.AspNetUsers.Add(teacher1);
            context.AspNetUsers.Add(teacher2);

            var student1 = new AspNetUsers
            {
                Id = student1UserId,
                Email = "midlevel1@companyx.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "midlevel1@companyx.com",
            };
            var student2 = new AspNetUsers
            {
                Id = student2UserId,
                Email = "midlevel2@companyx.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "midlevel2@companyx.com"
            };
            var student3 = new AspNetUsers
            {
                Id = student3UserId,
                Email = "junior1@companyx.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "junior1@companyx.com"
            };
            context.AspNetUsers.Add(student1);
            context.AspNetUsers.Add(student2);
            context.AspNetUsers.Add(student3);

            context.SaveChanges();

            var trainingGroup = new TrainingGroup("Software development team at Company X", "training group for the software dev team at company X", teacherUserId1);
            trainingGroup.GroupMembers = new List<GroupMember>();
            trainingGroup.GroupMembers.Add(new GroupMember(trainingGroup, teacher1, true, false));
            trainingGroup.GroupMembers.Add(new GroupMember(trainingGroup, teacher2, true, false));
            trainingGroup.GroupMembers.Add(new GroupMember(trainingGroup, student1, false, true));
            trainingGroup.GroupMembers.Add(new GroupMember(trainingGroup, student2, false, true));
            trainingGroup.GroupMembers.Add(new GroupMember(trainingGroup, student3, false, true));
            context.TrainingGroups.Add(trainingGroup);

            context.SaveChanges();



            var roadMap = new RoadMap("Becoming a strong .Net (C#) backend software developer", "To start this journey, you need to have at least some basic experience with .Net and Visual Studio.", teacherUserId1);
            context.RoadMaps.Add(roadMap);

            var road = new Road("OOP, modelling classes and interactions between them in .Net (C#)", "", teacherUserId1, roadMap);
            context.Roads.Add(road);

            var roadStep = new RoadStep("Quick OOP basics in .Net (C#)", "", teacherUserId1, road);
            context.RoadSteps.Add(roadStep);

            var teacherRole = new Teacher(teacher1);
            context.Teachers.Add(teacherRole);
            teacherRole.AddRoadStep(roadStep);

            var resource1 = new StepResource("https://msdn.microsoft.com/en-us/library/mt656686.aspx", "Concise explanation of many OOP concepts in C#. For every concept there are links to other MSDN articles which are focused on that concept, which can be pretty helpful for complicated ones.", teacherUserId1, roadStep);
            context.StepResources.Add(resource1);
            var resource2 = new StepResource("http://www.blackwasp.co.uk/csharpobjectoriented.aspx", "This is like a book with 22 short chapters, covering all aspects of OOP with C#. It goes into a bit too much detail, but it’s a good resource.", teacherUserId1, roadStep);
            context.StepResources.Add(resource2);
            var resource3 = new StepResource("http://www.c-sharpcorner.com/UploadFile/84c85b/object-oriented-programming-using-C-Sharp-net/", "Concise explanation of many OOP concepts in C#. ", teacherUserId1, roadStep);
            context.StepResources.Add(resource3);
            var resource4 = new StepResource("http://www.c-sharpcorner.com/UploadFile/mkagrahari/introduction-to-object-oriented-programming-concepts-in-C-Sharp/", "Concise explanation of only the most important OOP concepts in C#.", teacherUserId1, roadStep);
            context.StepResources.Add(resource4);
            var resource5 = new StepResource("http://zetcode.com/lang/csharp/oopi/", "Concise explanation of only the most important OOP concepts in C#.", teacherUserId1, roadStep);
            context.StepResources.Add(resource5);
            var resource6 = new StepResource("https://www.youtube.com/watch?v=e7Yj6vLyYOI", "A 3 hours video about OOP in C#. It covers a lot of stuff, and goes beyond basics. You can see the developer actually working on the code, which is pretty cool.", teacherUserId1, roadStep);
            context.StepResources.Add(resource6);

            //var exercise = new StepExercise("444", "444", teacherUserId1, roadStep);
            //context.StepExercises.Add(exercise);

            //var student = new Student(student1);
            //context.Students.Add(student);

            //var studentRoadStep = new StudentXRoadStep(student, roadStep, LearningStatus.StudyingResources);
            //context.StudentXRoadSteps.Add(studentRoadStep);
        }
    }
}