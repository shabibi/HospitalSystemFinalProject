﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HospitalSystemTeamTask.Migrations
{
    /// <inheritdoc />
    public partial class inisialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchName = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "branchDepartments",
                columns: table => new
                {
                    BID = table.Column<int>(type: "integer", nullable: false),
                    DepID = table.Column<int>(type: "integer", nullable: false),
                    DepartmentCapacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branchDepartments", x => new { x.BID, x.DepID });
                    table.ForeignKey(
                        name: "FK_branchDepartments_Branches_BID",
                        column: x => x.BID,
                        principalTable: "Branches",
                        principalColumn: "BID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_branchDepartments_Departments_DepID",
                        column: x => x.DepID,
                        principalTable: "Departments",
                        principalColumn: "DepID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PID = table.Column<int>(type: "integer", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PID);
                    table.ForeignKey(
                        name: "FK_Patients_Users_PID",
                        column: x => x.PID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CID = table.Column<int>(type: "integer", nullable: false),
                    PID = table.Column<int>(type: "integer", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Staus = table.Column<bool>(type: "boolean", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Patients_PID",
                        column: x => x.PID,
                        principalTable: "Patients",
                        principalColumn: "PID");
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    CID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepID = table.Column<int>(type: "integer", nullable: false),
                    AssignDoctor = table.Column<int>(type: "integer", nullable: true),
                    BID = table.Column<int>(type: "integer", nullable: false),
                    ClincName = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    SlotDuration = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.CID);
                    table.ForeignKey(
                        name: "FK_Clinics_Branches_BID",
                        column: x => x.BID,
                        principalTable: "Branches",
                        principalColumn: "BID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clinics_Departments_DepID",
                        column: x => x.DepID,
                        principalTable: "Departments",
                        principalColumn: "DepID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DID = table.Column<int>(type: "integer", nullable: false),
                    DepId = table.Column<int>(type: "integer", nullable: false),
                    CID = table.Column<int>(type: "integer", nullable: true),
                    CurrentBrunch = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: false),
                    Degree = table.Column<string>(type: "text", nullable: false),
                    WorkingYear = table.Column<int>(type: "integer", nullable: false),
                    JoiningDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DID);
                    table.ForeignKey(
                        name: "FK_Doctors_Branches_CurrentBrunch",
                        column: x => x.CurrentBrunch,
                        principalTable: "Branches",
                        principalColumn: "BID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Clinics_CID",
                        column: x => x.CID,
                        principalTable: "Clinics",
                        principalColumn: "CID");
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepId",
                        column: x => x.DepId,
                        principalTable: "Departments",
                        principalColumn: "DepID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_DID",
                        column: x => x.DID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientRecords",
                columns: table => new
                {
                    RID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PID = table.Column<int>(type: "integer", nullable: false),
                    BID = table.Column<int>(type: "integer", nullable: false),
                    DID = table.Column<int>(type: "integer", nullable: false),
                    VisitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    VisitTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Inspection = table.Column<string>(type: "text", nullable: false),
                    Treatment = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRecords", x => x.RID);
                    table.ForeignKey(
                        name: "FK_PatientRecords_Branches_BID",
                        column: x => x.BID,
                        principalTable: "Branches",
                        principalColumn: "BID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientRecords_Doctors_DID",
                        column: x => x.DID,
                        principalTable: "Doctors",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientRecords_Patients_PID",
                        column: x => x.PID,
                        principalTable: "Patients",
                        principalColumn: "PID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CID",
                table: "Bookings",
                column: "CID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PID",
                table: "Bookings",
                column: "PID");

            migrationBuilder.CreateIndex(
                name: "IX_branchDepartments_DepID",
                table: "branchDepartments",
                column: "DepID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_BranchName",
                table: "Branches",
                column: "BranchName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_AssignDoctor",
                table: "Clinics",
                column: "AssignDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_BID",
                table: "Clinics",
                column: "BID");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_ClincName",
                table: "Clinics",
                column: "ClincName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_DepID",
                table: "Clinics",
                column: "DepID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CID",
                table: "Doctors",
                column: "CID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CurrentBrunch",
                table: "Doctors",
                column: "CurrentBrunch");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepId",
                table: "Doctors",
                column: "DepId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_BID",
                table: "PatientRecords",
                column: "BID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_DID",
                table: "PatientRecords",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_PID",
                table: "PatientRecords",
                column: "PID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clinics_CID",
                table: "Bookings",
                column: "CID",
                principalTable: "Clinics",
                principalColumn: "CID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Doctors_AssignDoctor",
                table: "Clinics",
                column: "AssignDoctor",
                principalTable: "Doctors",
                principalColumn: "DID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Clinics_CID",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "branchDepartments");

            migrationBuilder.DropTable(
                name: "PatientRecords");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
