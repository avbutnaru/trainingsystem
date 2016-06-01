using System;
using System.Collections.Generic;
using System.Linq;
using TrainingSystem.Entities;

namespace TrainingSystem.Services
{
    public class ExerciseReviewerFinder
    {
        public Teacher Find(RoadStep roadStep, List<Teacher> allTeachers)
        {
            var correctTeachers = allTeachers.Where(p => p.TeacherXRoadSteps.Any(u => u.Id == roadStep.Id)).ToList();

            Random rnd = new Random();
            int r = rnd.Next(correctTeachers.Count);
            return correctTeachers[r];
        }
    }
}