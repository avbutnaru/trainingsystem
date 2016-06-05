using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingSystem.Entities
{
    public class AspNetUsers
    {
        public AspNetUsers()
        {
            TrainingTasks = new List<TrainingTask>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }

        public Student Student { get; set; }
        public IList<TrainingTask> TrainingTasks { get; set; }

        public TrainingNeed CalculateNeed(TrainingGroup trainingGroup)
        {
            if (Student == null)
            {
                return new TrainingNeed(true, null, null);
            }

            return Student.CalculateNeed(trainingGroup);
        }

        public TrainingTask AddTask(TrainingTaskType taskType, AspNetUsers student)
        {
            var trainingTask = new TrainingTask(this, taskType, student);
            TrainingTasks.Add(trainingTask);
            return trainingTask;
        }

        public TrainingTask AddTask(TrainingTaskType taskType, IList<RoadStep> roadSteps)
        {
            var trainingTask = new TrainingTask(this, taskType, roadSteps);
            TrainingTasks.Add(trainingTask);
            return trainingTask;
        }
    }
}