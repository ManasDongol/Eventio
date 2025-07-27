using Microsoft.EntityFrameworkCore;
using Eventing.ApiService.Data.Entities;
using Eventing.ApiService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Eventing.ApiService.Data.Entities
{
    public class EventingDbContext(DbContextOptions<EventingDbContext> options) : IdentityDbContext<Users, IdentityRole<Guid>,Guid>(options)
    {
        
        public DbSet<Events> Events { get; set; }
        public DbSet<Attendees> Attendees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Events>().ToTable("events");
            modelBuilder.Entity<Attendees>().ToTable("attendees");
        }
    }
}