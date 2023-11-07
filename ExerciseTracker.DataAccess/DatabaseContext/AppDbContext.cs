using Microsoft.EntityFrameworkCore;
using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.DataAccess.DatabaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Exercise> Exercises { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration for the Exercise entity
        modelBuilder.Entity<Exercise>().HasKey(e => e.Id); 
    }
}