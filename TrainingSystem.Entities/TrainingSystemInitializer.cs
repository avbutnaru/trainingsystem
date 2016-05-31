namespace TrainingSystem.Entities
{
    public class TrainingSystemInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TrainingSystemContext>
    {
        protected override void Seed(TrainingSystemContext context)
        {
            var userId = "0fef0fe8-8327-4020-bd00-25590bb4ef07";
            context.AspNetUsers.Add(new AspNetUsers
            {
                Id = userId,
                Email = "rollerblade1138@yahoo.com",
                EmailConfirmed = false,
                PasswordHash = "AJeirg3g2RPE32a3Df8x8e7UdCWT3375oVryE2xMlnRMaKvNy6Rc7ovLs1s3wMdmvw==",
                SecurityStamp = "58debd59-dc2e-4cda-aa6c-e7f1d5f23982",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "rollerblade1138@yahoo.com"
            });
            context.SaveChanges();



            var roadMap = new RoadMap("1111", "11111", userId);
            context.RoadMaps.Add(roadMap);

            var road = new Road("2222", "2222", userId, roadMap);
            context.Roads.Add(road);

            var roadStep = new RoadStep("333", "333", userId, road);
            context.RoadSteps.Add(roadStep);

            var resource = new StepResource("444", "444", userId, roadStep);
            context.StepResources.Add(resource);
            var exercise = new StepExercise("444", "444", userId, roadStep);
            context.StepExercises.Add(exercise);

            var student = new Student(userId);
            context.Students.Add(student);

            var studentRoadStep = new StudentXRoadStep(student, roadStep, LearningStatus.StudyingResources);
            context.StudentXRoadSteps.Add(studentRoadStep);

            context.SaveChanges();
        }
    }
}