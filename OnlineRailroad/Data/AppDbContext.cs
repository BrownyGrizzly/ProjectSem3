using Microsoft.EntityFrameworkCore;
using OnlineRailroad.Models;

namespace OnlineRailroad.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PassengerDetail> PassengerDetails { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<FareRule> FareRules { get; set; }
        public DbSet<Distance> Distances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User and PassengerDetail relationship
            modelBuilder.Entity<PassengerDetail>()
                .HasOne(pd => pd.User)
                .WithMany(u => u.PassengerDetails)
                .HasForeignKey(pd => pd.UserID); // Ensure this property exists in PassengerDetail

            modelBuilder.Entity<PassengerDetail>()
                .HasOne(pd => pd.Train)
                .WithMany()
                .HasForeignKey(pd => pd.TrainNo);

            // Train and Schedule relationship
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Train)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TrainNo);

            // Station and Schedule relationship
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Station)
                .WithMany(st => st.Schedules)
                .HasForeignKey(s => s.StationID);

            // Route and Schedule relationship
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Route)
                .WithMany(r => r.Schedules)
                .HasForeignKey(s => s.RouteID);

            // Route and FareRule relationship
            modelBuilder.Entity<FareRule>()
                .HasOne(fr => fr.Route)
                .WithMany(r => r.FareRules)
                .HasForeignKey(fr => fr.RouteID);

            // Distance and Station relationship (two navigations)
            modelBuilder.Entity<Distance>()
                .HasOne(d => d.StationA)
                .WithMany()
                .HasForeignKey(d => d.StationAId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes if needed

            modelBuilder.Entity<Distance>()
                .HasOne(d => d.StationB)
                .WithMany()
                .HasForeignKey(d => d.StationBId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes if needed

            // Configure any additional options or constraints here
        }
    }
}
