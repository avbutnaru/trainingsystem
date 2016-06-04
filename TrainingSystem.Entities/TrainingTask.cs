using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingSystem.Entities
{
    public enum TrainingTaskType
    {
        StartOneOfRoadSteps = 1,
        PrepareContent = 2,
        PrepareExercise = 3,
        ReviewExercise = 4
    }

    public enum TrainingTaskStatus
    {
        Waiting = 1,
        InProgress = 2,
        Done = 3
    }

    public class TrainingTask
    {
        public TrainingTask(AspNetUsers aspNetUser, TrainingTaskType taskType, List<RoadStep> roadSteps)
        {
            AspNetUsers = aspNetUser;
            TaskType = taskType;
            RoadStepsDescription = roadSteps.Select(u => u.Name).Aggregate((a, b) => a + ", " + b);
            TrainingTaskStatus = TrainingTaskStatus.Waiting;
            if (roadSteps != null && roadSteps.Count == 1)
            {
                RoadStep = roadSteps[0];
            }
        }

        public AspNetUsers AspNetUsers { get; set; }
        public TrainingTaskType TaskType { get; set; }
        public string RoadStepsDescription { get; set; }
        public TrainingTaskStatus TrainingTaskStatus { get; set; }
        public RoadStep RoadStep { get; set; }

        public bool IsTeachingTask
        {
            get
            {
                return TaskType == TrainingTaskType.PrepareContent || TaskType == TrainingTaskType.PrepareExercise ||
                       TaskType == TrainingTaskType.ReviewExercise;
            }
        }

        public bool SolvesNeed(TrainingNeed trainingNeed)
        {
            if (trainingNeed.NeedsRoadStep)
            {
                if (trainingNeed.RoadMap == null && trainingNeed.Road == null)
                {
                    return false;
                }
            }
            else if (trainingNeed.NeedsContent)
            {
                if (RoadStep != null && RoadStep.Id == trainingNeed.Road.Id &&
                    TaskType == TrainingTaskType.PrepareContent)
                {
                    return true;
                }
            }
            else if (trainingNeed.NeedsExercise)
            {
                if (RoadStep != null && RoadStep.Id == trainingNeed.Road.Id &&
                    TaskType == TrainingTaskType.PrepareExercise)
                {
                    return true;
                }
            }

            return false;
        }
    }
}