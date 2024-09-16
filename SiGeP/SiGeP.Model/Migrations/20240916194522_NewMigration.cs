using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SiGeP.Model.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Permissions = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Neighborhood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighborhood_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: true),
                    NeighborhoodName = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: true),
                    StreetNumber = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Neighborhood_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalTable: "Neighborhood",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Address_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    DNI = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnosis = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Treatment = table.Column<string>(type: "VARCHAR(512)", maxLength: 512, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reminder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendMode = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    Sent = table.Column<bool>(type: "bit", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    DeletedBy = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminder_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2866), "System", null, "", "Masculino", null, "" },
                    { 2, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2868), "System", null, "", "Femenino", null, "" },
                    { 3, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2869), "System", null, "", "Otro", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2331), "System", null, "", "Buenos Aires", null, "" },
                    { 2, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2332), "System", null, "", "Córdoba", null, "" },
                    { 3, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2334), "System", null, "", "Catamarca", null, "" },
                    { 4, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2335), "System", null, "", "Chaco", null, "" },
                    { 5, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2335), "System", null, "", "Chubut", null, "" },
                    { 6, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2336), "System", null, "", "Corrientes", null, "" },
                    { 7, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2337), "System", null, "", "Entre Ríos", null, "" },
                    { 8, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2338), "System", null, "", "Formosa", null, "" },
                    { 9, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2339), "System", null, "", "Jujuy", null, "" },
                    { 10, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2340), "System", null, "", "La Pampa", null, "" },
                    { 11, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2341), "System", null, "", "La Rioja", null, "" },
                    { 12, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2342), "System", null, "", "Mendoza", null, "" },
                    { 13, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2343), "System", null, "", "Misiones", null, "" },
                    { 14, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2344), "System", null, "", "Neuquén", null, "" },
                    { 15, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2345), "System", null, "", "Río Negro", null, "" },
                    { 16, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2346), "System", null, "", "Salta", null, "" },
                    { 17, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2347), "System", null, "", "San Juan", null, "" },
                    { 18, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2348), "System", null, "", "San Luis", null, "" },
                    { 19, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2349), "System", null, "", "Santa Cruz", null, "" },
                    { 20, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2350), "System", null, "", "Santa Fe", null, "" },
                    { 21, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2351), "System", null, "", "Santiago del Estero", null, "" },
                    { 22, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2352), "System", null, "", "Tierra del Fuego", null, "" },
                    { 23, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2352), "System", null, "", "Tucumán", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Permissions", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2294), "System", null, "", "Admin", "FullAccess", null, "" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Password", "RoleId", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2041), "System", null, "", "admin", "admin", 1, null, "" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "ProvinceId", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2455), "System", null, "", "Resistencia", 4, null, "" },
                    { 2, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2457), "System", null, "", "Presidencia Roque Sáenz Peña", 4, null, "" },
                    { 3, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2458), "System", null, "", "Barranqueras", 4, null, "" },
                    { 4, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2459), "System", null, "", "Villa Ángela", 4, null, "" },
                    { 5, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2461), "System", null, "", "Fontana", 4, null, "" },
                    { 6, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2462), "System", null, "", "Charata", 4, null, "" },
                    { 7, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2463), "System", null, "", "Quitilipi", 4, null, "" },
                    { 8, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2466), "System", null, "", "General San Martín", 4, null, "" },
                    { 9, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2468), "System", null, "", "Las Breñas", 4, null, "" },
                    { 10, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2469), "System", null, "", "Castelli", 4, null, "" },
                    { 11, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2470), "System", null, "", "Corzuela", 4, null, "" },
                    { 12, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2471), "System", null, "", "Machagai", 4, null, "" },
                    { 13, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2473), "System", null, "", "La Leonesa", 4, null, "" },
                    { 14, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2474), "System", null, "", "San Bernardo", 4, null, "" },
                    { 15, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2475), "System", null, "", "Las Palmas", 4, null, "" },
                    { 16, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2476), "System", null, "", "General Pinedo", 4, null, "" },
                    { 17, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2478), "System", null, "", "Puerto Tirol", 4, null, "" },
                    { 18, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2479), "System", null, "", "Margarita Belén", 4, null, "" },
                    { 19, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2480), "System", null, "", "Tres Isletas", 4, null, "" },
                    { 20, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2481), "System", null, "", "La Escondida", 4, null, "" },
                    { 21, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2483), "System", null, "", "Puerto Vilelas", 4, null, "" },
                    { 22, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2484), "System", null, "", "Puerto Bermejo", 4, null, "" },
                    { 23, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2485), "System", null, "", "Hermoso Campo", 4, null, "" },
                    { 24, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2486), "System", null, "", "Villa Berthet", 4, null, "" },
                    { 25, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2488), "System", null, "", "Colonias Unidas", 4, null, "" },
                    { 26, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2489), "System", null, "", "General Vedia", 4, null, "" },
                    { 27, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2491), "System", null, "", "Misión Nueva Pompeya", 4, null, "" },
                    { 28, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2492), "System", null, "", "Miraflores", 4, null, "" },
                    { 29, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2493), "System", null, "", "Napenay", 4, null, "" },
                    { 30, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2494), "System", null, "", "Gancedo", 4, null, "" },
                    { 31, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2496), "System", null, "", "Samuhú", 4, null, "" },
                    { 32, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2497), "System", null, "", "Pampa del Infierno", 4, null, "" },
                    { 33, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2498), "System", null, "", "Campo Largo", 4, null, "" },
                    { 34, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2500), "System", null, "", "Fuerte Esperanza", 4, null, "" },
                    { 35, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2501), "System", null, "", "Avia Terai", 4, null, "" },
                    { 36, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2502), "System", null, "", "La Verde", 4, null, "" },
                    { 37, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2503), "System", null, "", "Colonia Elisa", 4, null, "" },
                    { 38, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2505), "System", null, "", "Capitán Solari", 4, null, "" },
                    { 39, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2506), "System", null, "", "La Tigra", 4, null, "" },
                    { 40, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2507), "System", null, "", "Enrique Urien", 4, null, "" },
                    { 41, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2508), "System", null, "", "Los Frentones", 4, null, "" },
                    { 42, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2510), "System", null, "", "Pampa del Indio", 4, null, "" },
                    { 43, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2511), "System", null, "", "Puerto Eva Perón", 4, null, "" },
                    { 44, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2512), "System", null, "", "Ciervo Petiso", 4, null, "" },
                    { 45, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2513), "System", null, "", "Formosa", 8, null, "" },
                    { 46, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2515), "System", null, "", "Clorinda", 8, null, "" },
                    { 47, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2516), "System", null, "", "Pirané", 8, null, "" },
                    { 48, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2517), "System", null, "", "El Colorado", 8, null, "" },
                    { 49, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2519), "System", null, "", "Laguna Blanca", 8, null, "" },
                    { 50, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2520), "System", null, "", "Ingeniero Juárez", 8, null, "" },
                    { 51, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2521), "System", null, "", "General Manuel Belgrano", 8, null, "" },
                    { 52, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2522), "System", null, "", "Villa Dos Trece", 8, null, "" },
                    { 53, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2524), "System", null, "", "Ibarreta", 8, null, "" },
                    { 54, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2525), "System", null, "", "Las Lomitas", 8, null, "" },
                    { 55, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2526), "System", null, "", "Comandante Fontana", 8, null, "" },
                    { 56, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2528), "System", null, "", "San Francisco de Laishí", 8, null, "" },
                    { 57, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2529), "System", null, "", "Misión Tacaaglé", 8, null, "" },
                    { 58, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2530), "System", null, "", "Herradura", 8, null, "" },
                    { 59, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2531), "System", null, "", "Estanislao del Campo", 8, null, "" },
                    { 60, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2535), "System", null, "", "Buena Vista", 8, null, "" },
                    { 61, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2537), "System", null, "", "Laguna Naick Neck", 8, null, "" },
                    { 62, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2539), "System", null, "", "Gran Guardia", 8, null, "" },
                    { 63, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2540), "System", null, "", "Tres Lagunas", 8, null, "" },
                    { 64, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2543), "System", null, "", "Riacho He Hé", 8, null, "" },
                    { 65, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2544), "System", null, "", "Laguna Yema", 8, null, "" },
                    { 66, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2545), "System", null, "", "Mayor Vicente Villafañe", 8, null, "" },
                    { 67, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2546), "System", null, "", "Subteniente Perín", 8, null, "" },
                    { 68, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2547), "System", null, "", "Misión San Martín", 8, null, "" },
                    { 69, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2582), "System", null, "", "El Espinillo", 8, null, "" },
                    { 70, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2584), "System", null, "", "Siete Palmas", 8, null, "" },
                    { 71, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2585), "System", null, "", "Palo Santo", 8, null, "" },
                    { 72, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2586), "System", null, "", "Villa Escolar", 8, null, "" },
                    { 73, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2587), "System", null, "", "Loma Monte Lindo", 8, null, "" },
                    { 74, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2588), "System", null, "", "General Lucio V. Mansilla", 8, null, "" },
                    { 75, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2589), "System", null, "", "Colonia Pastoril", 8, null, "" },
                    { 76, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2590), "System", null, "", "Fortín Lugones", 8, null, "" },
                    { 77, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2591), "System", null, "", "Pozo del Tigre", 8, null, "" },
                    { 78, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2592), "System", null, "", "Las Cañitas", 8, null, "" },
                    { 79, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2593), "System", null, "", "El Potrillo", 8, null, "" },
                    { 80, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2594), "System", null, "", "Palma Sola", 8, null, "" },
                    { 81, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2595), "System", null, "", "San Hilario", 8, null, "" },
                    { 82, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2596), "System", null, "", "Colonia Ituzaingó", 8, null, "" },
                    { 83, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2597), "System", null, "", "General Güemes", 8, null, "" },
                    { 84, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2598), "System", null, "", "Corrientes", 3, null, "" },
                    { 85, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2599), "System", null, "", "Goya", 3, null, "" },
                    { 86, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2600), "System", null, "", "Paso de los Libres", 3, null, "" },
                    { 87, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2601), "System", null, "", "Mercedes", 3, null, "" },
                    { 88, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2602), "System", null, "", "Bella Vista", 3, null, "" },
                    { 89, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2603), "System", null, "", "Santo Tomé", 3, null, "" },
                    { 90, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2604), "System", null, "", "Esquina", 3, null, "" },
                    { 91, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2605), "System", null, "", "Monte Caseros", 3, null, "" },
                    { 92, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2606), "System", null, "", "Curuzú Cuatiá", 3, null, "" },
                    { 93, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2607), "System", null, "", "Ituzaingó", 3, null, "" },
                    { 94, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2608), "System", null, "", "Mocoretá", 3, null, "" },
                    { 95, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2609), "System", null, "", "Saladas", 3, null, "" },
                    { 96, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2610), "System", null, "", "Sauce", 3, null, "" },
                    { 97, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2611), "System", null, "", "San Luis del Palmar", 3, null, "" },
                    { 98, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2612), "System", null, "", "Empedrado", 3, null, "" },
                    { 99, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2613), "System", null, "", "Santa Lucía", 3, null, "" },
                    { 100, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2614), "System", null, "", "Concepción", 3, null, "" },
                    { 101, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2615), "System", null, "", "San Roque", 3, null, "" },
                    { 102, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2616), "System", null, "", "Paso de la Patria", 3, null, "" },
                    { 103, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2617), "System", null, "", "Alvear", 3, null, "" },
                    { 104, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2619), "System", null, "", "Riachuelo", 3, null, "" },
                    { 105, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2620), "System", null, "", "San Miguel", 3, null, "" },
                    { 106, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2621), "System", null, "", "Santa Rosa", 3, null, "" },
                    { 107, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2622), "System", null, "", "San Lorenzo", 3, null, "" },
                    { 108, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2623), "System", null, "", "Colonia Carlos Pellegrini", 3, null, "" },
                    { 109, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2624), "System", null, "", "San Cosme", 3, null, "" },
                    { 110, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2625), "System", null, "", "Colonia Libertad", 3, null, "" },
                    { 111, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2626), "System", null, "", "Loreto", 3, null, "" },
                    { 112, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2627), "System", null, "", "San Carlos", 3, null, "" },
                    { 113, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2628), "System", null, "", "Yapeyú", 3, null, "" },
                    { 114, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2629), "System", null, "", "Bonpland", 3, null, "" },
                    { 115, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2630), "System", null, "", "Berón de Astrada", 3, null, "" },
                    { 116, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2631), "System", null, "", "Juan Pujol", 3, null, "" },
                    { 117, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2632), "System", null, "", "Gobernador Virasoro", 3, null, "" },
                    { 118, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2633), "System", null, "", "Itatí", 3, null, "" },
                    { 119, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2634), "System", null, "", "Chavarría", 3, null, "" },
                    { 120, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2635), "System", null, "", "Tapebicuá", 3, null, "" },
                    { 121, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2636), "System", null, "", "Parada Pucheta", 3, null, "" },
                    { 122, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2637), "System", null, "", "Perugorría", 3, null, "" },
                    { 123, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2638), "System", null, "", "Felipe Yofre", 3, null, "" },
                    { 124, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2639), "System", null, "", "Ramón Lista", 3, null, "" },
                    { 125, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2640), "System", null, "", "Villa Olivari", 3, null, "" }
                });

            migrationBuilder.InsertData(
                table: "Neighborhood",
                columns: new[] { "Id", "CityId", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2776), "System", null, "", "Centro", null, "" },
                    { 2, 2, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2779), "System", null, "", "Nueva Córdoba", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "CityId", "Created", "CreatedBy", "Deleted", "DeletedBy", "NeighborhoodId", "NeighborhoodName", "ProvinceId", "StreetNumber", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2819), "System", null, "", 1, "Centro", 1, "1234", null, "" },
                    { 2, 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2838), "System", null, "", 1, "Centro 222", 1, "1234", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "AddressId", "BirthDate", "Created", "CreatedBy", "DNI", "Deleted", "DeletedBy", "Email", "GenderId", "LastName", "Name", "Phone", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2907), "System", "20123456789", null, "", "juan.perez@example.com", 1, "Pérez", "Juan", "1234567890", null, "" },
                    { 2, 2, new DateTime(1978, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2910), "System", "20987654321", null, "", "maria.gonzalez@example.com", 2, "González", "María", "0987654321", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "PersonId", "Specialty", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2942), "System", null, "", 2, "Cardiología", null, "" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "DoctorId", "PersonId", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2970), "System", null, "", 1, 1, null, "" });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "Address", "Created", "CreatedBy", "CustomerId", "DateEnd", "DateStart", "Deleted", "DeletedBy", "Status", "Updated", "UpdatedBy" },
                values: new object[] { 1, "1234 Centro, La Plata", null, "System", 1, new DateTime(2024, 9, 17, 17, 45, 22, 349, DateTimeKind.Local).AddTicks(3005), new DateTime(2024, 9, 17, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(2998), null, "", 0, null, "" });

            migrationBuilder.InsertData(
                table: "MedicalRecord",
                columns: new[] { "Id", "Created", "CreatedBy", "CustomerId", "Date", "Deleted", "DeletedBy", "Diagnosis", "Treatment", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(3032), "System", 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(3029), null, "", "Hipertensión", "Dieta baja en sodio", null, "" });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "Amount", "AppointmentId", "Created", "CreatedBy", "Date", "Deleted", "DeletedBy", "Updated", "UpdatedBy" },
                values: new object[] { 1, 200.00m, 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(3066), "System", new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(3062), null, "", null, "" });

            migrationBuilder.InsertData(
                table: "Reminder",
                columns: new[] { "Id", "AppointmentId", "Created", "CreatedBy", "Date", "Deleted", "DeletedBy", "SendMode", "Sent", "Updated", "UpdatedBy" },
                values: new object[] { 1, 1, new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(3098), "System", new DateTime(2024, 9, 16, 16, 45, 22, 349, DateTimeKind.Local).AddTicks(3096), null, "", "Email", false, null, "" });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_Deleted",
                table: "Address",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Address_NeighborhoodId",
                table: "Address",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProvinceId",
                table: "Address",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CustomerId",
                table: "Appointment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Deleted",
                table: "Appointment",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_RoleId",
                table: "AppUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_City_Deleted",
                table: "City",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Deleted",
                table: "Customer",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DoctorId",
                table: "Customer",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PersonId",
                table: "Customer",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Deleted",
                table: "Doctor",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_PersonId",
                table: "Doctor",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Deleted",
                table: "Gender",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_CustomerId",
                table: "MedicalRecord",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_Deleted",
                table: "MedicalRecord",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhood_CityId",
                table: "Neighborhood",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhood_Deleted",
                table: "Neighborhood",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_AppointmentId",
                table: "Payment",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Deleted",
                table: "Payment",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AddressId",
                table: "Person",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Deleted",
                table: "Person",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderId",
                table: "Person",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_Deleted",
                table: "Province",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_AppointmentId",
                table: "Reminder",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_Deleted",
                table: "Reminder",
                column: "Deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Reminder");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Neighborhood");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
