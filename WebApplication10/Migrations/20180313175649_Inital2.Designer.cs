﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApplication10.Data;
using WebApplication10.Model;

namespace WebApplication10.Migrations
{
    [DbContext(typeof(AplicationContext))]
    [Migration("20180313175649_Inital2")]
    partial class Inital2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication10.Model.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("RoleID");

                    b.HasKey("DepartmentID");

                    b.HasIndex("RoleID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("WebApplication10.Model.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Lavel");

                    b.Property<int>("PermissionID");

                    b.Property<int>("UserID");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("PermissionID");

                    b.HasIndex("UserID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("WebApplication10.Model.Permission", b =>
                {
                    b.Property<int>("PermissionID");

                    b.Property<int>("DepartmentID");

                    b.Property<string>("Title");

                    b.HasKey("PermissionID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("WebApplication10.Model.PermissionAssignment", b =>
                {
                    b.Property<int>("PermissionID");

                    b.Property<int>("RoleID");

                    b.HasKey("PermissionID", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("PermissionAssignment");
                });

            modelBuilder.Entity("WebApplication10.Model.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName");

                    b.HasKey("ID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("WebApplication10.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EnrollmentDate");

                    b.Property<string>("FirstMidName");

                    b.Property<string>("LastName");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebApplication10.Model.Department", b =>
                {
                    b.HasOne("WebApplication10.Model.Role", "Administrator")
                        .WithMany()
                        .HasForeignKey("RoleID");
                });

            modelBuilder.Entity("WebApplication10.Model.Enrollment", b =>
                {
                    b.HasOne("WebApplication10.Model.Permission", "Permission")
                        .WithMany("Enrollments")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication10.Model.User", "User")
                        .WithMany("Enrollments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication10.Model.Permission", b =>
                {
                    b.HasOne("WebApplication10.Model.Department", "Department")
                        .WithMany("Permissions")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication10.Model.PermissionAssignment", b =>
                {
                    b.HasOne("WebApplication10.Model.Permission", "Permission")
                        .WithMany("PermissionAssignments")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication10.Model.Role", "Role")
                        .WithMany("PermissionAssignments")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
