using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingSystem.Entities
{
    public class TrainingGroup
    {
        protected TrainingGroup()
        {
            TrainingGroupXRoads = new List<TrainingGroupXRoad>();
            TrainingTasks = new List<TrainingTask>();
        }

        public TrainingGroup(string name, string description, string userId)
        {
            Name = name;
            Description = description;
            UserId = userId;

            TrainingGroupXRoads = new List<TrainingGroupXRoad>();
            TrainingTasks = new List<TrainingTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public IList<GroupMember> GroupMembers { get; set; }
        public IList<TrainingGroupXRoad> TrainingGroupXRoads { get; set; }
        public IList<TrainingTask> TrainingTasks { get; set; }
        public bool AutomatedTrainingIsActive { get; set; }

        public void ActivateRoad(Road road)
        {
            TrainingGroupXRoads.Add(new TrainingGroupXRoad(this, road));
        }

        public void ActivateMemberForRoad(Road road, GroupMember groupMember, bool canMakeReviews, bool canPrepareContent, bool shouldLearn)
        {
            var roadForGroup = RoadForGroup(road);
            roadForGroup.AddMember(groupMember, canMakeReviews, canPrepareContent, shouldLearn);
        }

        public TrainingGroupXRoad RoadForGroup(Road road)
        {
            return TrainingGroupXRoads.FirstOrDefault(p => p.Road.Id == road.Id);
        }

        public bool RoadIsAvailable(Road road)
        {
            return TrainingGroupXRoads.Any(p => p.Road.Id == road.Id);
        }

        public List<TrainingTask> IterateTraining()
        {
            var ret = new List<TrainingTask>();

            var groupMemberWithNeed = GetFirstGroupMemberWhoHasANeed();
            while (groupMemberWithNeed != null)
            {
                var trainingNeed = groupMemberWithNeed.AspNetUser.CalculateNeed(this);
                var solutionForNeed = DefineSolutionForNeed(trainingNeed, groupMemberWithNeed);
                TrainingTasks.Add(solutionForNeed);
                ret.Add(solutionForNeed);
                groupMemberWithNeed = GetFirstGroupMemberWhoHasANeed();
            }

            return ret;
        }

        private TrainingTask DefineSolutionForNeed(TrainingNeed trainingNeed, GroupMember groupMemberWithNeed)
        {
            if (trainingNeed.NeedsRoadStep)
            {
                if (trainingNeed.Road == null && trainingNeed.RoadMap == null)
                {
                    var availableRoadSteps = new List<RoadStep>();
                    foreach (var trainingGroupXRoad in GroupXRoadsWhereMemberCouldLearn(groupMemberWithNeed))
                    {
                        var road = trainingGroupXRoad.Road;
                        foreach (var roadXRoadStep in road.RoadXRoadSteps)
                        {
                            availableRoadSteps.Add(roadXRoadStep.RoadStep);
                            break;
                        }
                    }

                    if (availableRoadSteps.Count > 0)
                    {
                        return new TrainingTask(groupMemberWithNeed.AspNetUser, TrainingTaskType.StartOneOfRoadSteps, availableRoadSteps);
                    }
                }
            }
            else if (trainingNeed.NeedsContent)
            {
                var roadStep = trainingNeed.RoadStep;
                var teacher = FindTeacherToPrepareContent(roadStep);
                return new TrainingTask(teacher, TrainingTaskType.PrepareContent, new List<RoadStep> {roadStep});
            }
            else if (trainingNeed.NeedsExercise)
            {
                var roadStep = trainingNeed.RoadStep;
                var teacher = FindTeacherToPrepareContent(roadStep);
                return new TrainingTask(teacher, TrainingTaskType.PrepareExercise, new List<RoadStep> {roadStep});
            }
            throw new NotImplementedException();
        }

        private AspNetUsers FindTeacherToPrepareContent(RoadStep roadStep)
        {
            foreach (var trainingGroupXRoad in GroupXRoadsContainingRoadStep(roadStep))
            {
                var teacher =
                    trainingGroupXRoad.GroupMembersForRoad.FirstOrDefault(
                        p => p.CanPrepareContent && !p.HasAnotherTeachingTask);

                if (teacher != null)
                {
                    return teacher.GroupMember.AspNetUser;
                }
            }
            return null;
        }

        private IEnumerable<TrainingGroupXRoad> GroupXRoadsContainingRoadStep(RoadStep roadStep)
        {
            return TrainingGroupXRoads.Where(p => p.Road.RoadXRoadSteps.Any(u => u.RoadStep.Id == roadStep.Id));
        }

        private IEnumerable<TrainingGroupXRoad> GroupXRoadsWhereMemberCouldLearn(GroupMember groupMemberWithNeed)
        {
            return TrainingGroupXRoads.Where(p => p.GroupMembersForRoad.Any(u => u.GroupMember.Id == groupMemberWithNeed.Id && u.ShouldLearn));
        }

        private GroupMember GetFirstGroupMemberWhoHasANeed()
        {
            foreach (var groupMember in GroupMembers.Where(p => p.IsStudent))
            {
                var student = groupMember.AspNetUser.Student;
                if (student == null)
                {
                    return groupMember;
                }

                var trainingNeed = student.CalculateNeed(this);
                if (trainingNeed != null)
                {
                    return groupMember;
                }
            }

            return null;
        }
    }
}