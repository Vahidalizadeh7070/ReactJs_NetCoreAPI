using GameHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models
{
    // DbContext class
    // This class inherits from IdentityDbContext 
    // IdentityDbContext provides for us all tables that we need in our project for authentication and authorization based on the 
    // Asp.net core identity 
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
        // News DbSet
        public DbSet<News> News { get; set; }

        // Category DbSet
        public DbSet<Category> Category { get; set; }

        // Slider DbSet
        public DbSet<Slider> Slider { get; set; }

        // User DbSet
        public DbSet<User> User{ get; set; }

        // Review DbSet
        public DbSet<Review> Review{ get; set; }

        // RateList DbSet
        public DbSet<RateList> RateList{ get; set; }

        // FutureReleaseGames DbSet
        public DbSet<FutureReleaseGame> FutureReleaseGames{ get; set; }
    }
}
