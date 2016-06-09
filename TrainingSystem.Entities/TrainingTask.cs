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
        public TrainingTask()
        {
            
        }

        public TrainingTask(AspNetUsers memberActing, TrainingTaskType taskType, IList<RoadStep> roadSteps)
        {
            MemberActing = memberActing;
            TaskType = taskType;
            TrainingTaskStatus = TrainingTaskStatus.Waiting;
            if (roadSteps != null)
            {
                if (roadSteps.Count == 1)
                {
                    RoadStep = roadSteps[0];
                    RoadStepsDescription = roadSteps[0].Name;
                }
                else if (roadSteps.Count > 1)
                {
                    RoadStepsDescription = roadSteps.Select(u => u.Name).Aggregate((a, b) => a + ", " + b);
                }
            }
        }

        public TrainingTask(AspNetUsers memberActing, TrainingTaskType taskType, AspNetUsers memberReceiving)
        {
            MemberActing = memberActing;
            TaskType = taskType;
            TrainingTaskStatus = TrainingTaskStatus.Waiting;
            MemberReceiving = memberReceiving;
        }

        public override string ToString()
        {
            if (TaskType == TrainingTaskType.StartOneOfRoadSteps)
            {
                return "Student " + MemberActing.UserName + " should start road step \"" + RoadStep.Name + "\"";
            }
            if (TaskType == TrainingTaskType.PrepareContent)
            {
                if (RoadStep == null && String.IsNullOrEmpty(RoadStepsDescription))
                {
                    return "Teacher " + MemberActing.UserName + " should prepare one road step with its content for " + MemberReceiving.UserName;
                }

                if (RoadStep != null)
                {
                    return "Teacher " + MemberActing.UserName + " should prepare the content for road step " + RoadStep.Name + " so that " + MemberReceiving.UserName + " can resume learning.";
                }

                if (!string.IsNullOrEmpty(RoadStepsDescription))
                {
                    return "Teacher " + MemberActing.UserName + " should prepare the content for any of the following road steps: " + RoadStepsDescription + " so that " + MemberReceiving.UserName + " can start learning.";
                }
            }
            if (TaskType == TrainingTaskType.PrepareExercise)
            {
                if (RoadStep != null)
                {
                    return "Teacher " + MemberActing.UserName + " should prepare an exercise for road step " + RoadStep.Name;
                }
            }

            return "No description defined.";
        }

        public AspNetUsers MemberReceiving { get; set; }

        public int Id { get; set; }
        public AspNetUsers MemberActing { get; set; }
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
                if (RoadStep != null && RoadStep.Id == trainingNeed.RoadStep.Id &&
                    TaskType == TrainingTaskType.PrepareExercise)
                {
                    return true;
                }
            }

            return false;
        }
    }
}