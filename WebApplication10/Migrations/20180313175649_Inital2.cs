using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication10.Migrations
{
    public partial class Inital2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Role_AdministratorID",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_AdministratorID",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "AdministratorID",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "InstructorID",
                table: "Department",
                newName: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Department_RoleID",
                table: "Department",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Role_RoleID",
                table: "Department",
                column: "RoleID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Role_RoleID",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_RoleID",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                table: "Department",
                newName: "InstructorID");

            migrationBuilder.AddColumn<int>(
                name: "AdministratorID",
                table: "Department",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_AdministratorID",
                table: "Department",
                column: "AdministratorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Role_AdministratorID",
                table: "Department",
                column: "AdministratorID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
