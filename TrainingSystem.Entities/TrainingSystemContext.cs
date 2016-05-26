using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TrainingSystem.Entities
{
    public class TrainingSystemContext : DbContext
    {

        public TrainingSystemContext() : base("DefaultConnection")
        {
        }

        public DbSet<GrowingPath> GrowingPaths { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}