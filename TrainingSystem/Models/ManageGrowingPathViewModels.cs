using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TrainingSystem.Entities;

namespace TrainingSystem.Models
{
    public class ManageRoadMapViewModel
    {
        public ManageRoadMapViewModel()
        {
            Roads = new List<Road>();
        }

        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public IList<Road> Roads { get; set; }
    }

    public class ManageRoadMapListViewModel
    {
        public IList<RoadMap> RoadMaps { get; set; }
    }

    public class ManageRoadViewModel
    {
        public int? Id { get; set; }
        public int? RoadmapId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}