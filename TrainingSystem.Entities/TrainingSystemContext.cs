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
        public DbSet<RoadStep> RoadSteps { get; set; }
        public DbSet<Road> Roads { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ExerciseReview> ExerciseReviews { get; set; }
        public DbSet<TrainingGroup> TrainingGroups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<GroupMemberForRoad> GroupMemberForRoads { get; set; }
        public DbSet<TrainingGroupXRoad> TrainingGroupXRoads { get; set; }
        public DbSet<StepResource> StepResources { get; set; }
        public DbSet<StepExercise> StepExercises { get; set; }
        public DbSet<RoadmapXRoad> RoadmapXRoads { get; set; }
        public DbSet<RoadXRoadStep> RoadXRoadSteps { get; set; }
        public DbSet<StudentXRoadStep> StudentXRoadSteps { get; set; }
        public DbSet<StudentResourceRating> StudentResourceRatings { get; set; }
        public DbSet<StudentExercise> StudentExercises { get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<GroupMemberForRoad>()
                .HasRequired(p =>p.TrainingGroupXRoad)
                .WithMany(p => p.GroupMembersForRoad)
                .WillCascadeOnDelete(true);
        }
    }
}