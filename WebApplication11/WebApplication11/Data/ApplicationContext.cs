using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Model;

namespace WebApplication11.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RoleAssignment> RoleAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<RoleAssignment>().ToTable("RoleAssignment");

            modelBuilder.Entity<RoleAssignment>()
                .HasKey(c => new { c.RoleID, c.UserID });


        }
    }
}
