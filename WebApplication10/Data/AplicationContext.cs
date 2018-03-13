using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Model;

namespace WebApplication10.Data
{
    public class AplicationContext:DbContext
    {
        public AplicationContext(DbContextOptions options) : base(options) { }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PermissionAssignment> PermissionAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<PermissionAssignment>().ToTable("PermissionAssignment");

            modelBuilder.Entity<PermissionAssignment>()
                .HasKey(c => new { c.PermissionID, c.RoleID });

        
    }
}
}
