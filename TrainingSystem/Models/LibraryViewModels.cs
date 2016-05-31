using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TrainingSystem.Entities;

namespace TrainingSystem.Models
{
    public class LibraryMainViewModel
    {
        public IList<RoadMap> RoadMaps { get; set; }
    }

    public class RoadMapMainViewModel
    {
        public RoadMap RoadMap { get; set; }
    }

    public class RoadMainViewModel
    {
        public Road Road { get; set; }
    }

    public class RoadStepMainViewModel
    {
        public RoadStep RoadStep { get; set; }
        public StudentXRoadStep StudentXRoadStep { get; set; }
        public string Message { get; set; }
    }
}