using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TrainingSystem.Entities
{
    public class TrainingSystemContext : DbContext
    {

        public TrainingSystemContext() : base("DefaultConnection")
        {
        }

        public DbSet<RoadMap> RoadMaps { get; set; }
        public DbSet<Road> Roads { get; set; }
        public DbSet<RoadmapXRoad> RoadmapXRoads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}