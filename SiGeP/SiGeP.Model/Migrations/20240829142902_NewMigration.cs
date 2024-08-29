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
                    { 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2244), "System", null, "", "Masculino", null, "" },
                    { 2, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2245), "System", null, "", "Femenino", null, "" },
                    { 3, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2246), "System", null, "", "Otro", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1903), "System", null, "", "Buenos Aires", null, "" },
                    { 2, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1904), "System", null, "", "Córdoba", null, "" },
                    { 3, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1905), "System", null, "", "Catamarca", null, "" },
                    { 4, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1906), "System", null, "", "Chaco", null, "" },
                    { 5, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1907), "System", null, "", "Chubut", null, "" },
                    { 6, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1908), "System", null, "", "Corrientes", null, "" },
                    { 7, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1909), "System", null, "", "Entre Ríos", null, "" },
                    { 8, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1910), "System", null, "", "Formosa", null, "" },
                    { 9, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1911), "System", null, "", "Jujuy", null, "" },
                    { 10, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1912), "System", null, "", "La Pampa", null, "" },
                    { 11, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1913), "System", null, "", "La Rioja", null, "" },
                    { 12, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1914), "System", null, "", "Mendoza", null, "" },
                    { 13, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1914), "System", null, "", "Misiones", null, "" },
                    { 14, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1915), "System", null, "", "Neuquén", null, "" },
                    { 15, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1916), "System", null, "", "Río Negro", null, "" },
                    { 16, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1917), "System", null, "", "Salta", null, "" },
                    { 17, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1918), "System", null, "", "San Juan", null, "" },
                    { 18, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1919), "System", null, "", "San Luis", null, "" },
                    { 19, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1920), "System", null, "", "Santa Cruz", null, "" },
                    { 20, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1921), "System", null, "", "Santa Fe", null, "" },
                    { 21, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1922), "System", null, "", "Santiago del Estero", null, "" },
                    { 22, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1923), "System", null, "", "Tierra del Fuego", null, "" },
                    { 23, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1924), "System", null, "", "Tucumán", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Permissions", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1872), "System", null, "", "Admin", "FullAccess", null, "" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Password", "RoleId", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1690), "System", null, "", "admin", "admin", 1, null, "" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "ProvinceId", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1968), "System", null, "", "Resistencia", 4, null, "" },
                    { 2, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1969), "System", null, "", "Presidencia Roque Sáenz Peña", 4, null, "" },
                    { 3, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1971), "System", null, "", "Barranqueras", 4, null, "" },
                    { 4, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1972), "System", null, "", "Villa Ángela", 4, null, "" },
                    { 5, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1973), "System", null, "", "Fontana", 4, null, "" },
                    { 6, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1978), "System", null, "", "Charata", 4, null, "" },
                    { 7, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1979), "System", null, "", "Quitilipi", 4, null, "" },
                    { 8, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1980), "System", null, "", "General San Martín", 4, null, "" },
                    { 9, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1981), "System", null, "", "Las Breñas", 4, null, "" },
                    { 10, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1982), "System", null, "", "Castelli", 4, null, "" },
                    { 11, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1983), "System", null, "", "Corzuela", 4, null, "" },
                    { 12, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1984), "System", null, "", "Machagai", 4, null, "" },
                    { 13, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1985), "System", null, "", "La Leonesa", 4, null, "" },
                    { 14, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1986), "System", null, "", "San Bernardo", 4, null, "" },
                    { 15, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1987), "System", null, "", "Las Palmas", 4, null, "" },
                    { 16, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1988), "System", null, "", "General Pinedo", 4, null, "" },
                    { 17, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1989), "System", null, "", "Puerto Tirol", 4, null, "" },
                    { 18, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1990), "System", null, "", "Margarita Belén", 4, null, "" },
                    { 19, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1991), "System", null, "", "Tres Isletas", 4, null, "" },
                    { 20, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1992), "System", null, "", "La Escondida", 4, null, "" },
                    { 21, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1993), "System", null, "", "Puerto Vilelas", 4, null, "" },
                    { 22, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1994), "System", null, "", "Puerto Bermejo", 4, null, "" },
                    { 23, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1995), "System", null, "", "Hermoso Campo", 4, null, "" },
                    { 24, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1996), "System", null, "", "Villa Berthet", 4, null, "" },
                    { 25, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1997), "System", null, "", "Colonias Unidas", 4, null, "" },
                    { 26, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1998), "System", null, "", "General Vedia", 4, null, "" },
                    { 27, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(1999), "System", null, "", "Misión Nueva Pompeya", 4, null, "" },
                    { 28, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2000), "System", null, "", "Miraflores", 4, null, "" },
                    { 29, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2001), "System", null, "", "Napenay", 4, null, "" },
                    { 30, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2002), "System", null, "", "Gancedo", 4, null, "" },
                    { 31, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2003), "System", null, "", "Samuhú", 4, null, "" },
                    { 32, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2004), "System", null, "", "Pampa del Infierno", 4, null, "" },
                    { 33, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2005), "System", null, "", "Campo Largo", 4, null, "" },
                    { 34, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2006), "System", null, "", "Fuerte Esperanza", 4, null, "" },
                    { 35, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2007), "System", null, "", "Avia Terai", 4, null, "" },
                    { 36, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2008), "System", null, "", "La Verde", 4, null, "" },
                    { 37, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2009), "System", null, "", "Colonia Elisa", 4, null, "" },
                    { 38, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2010), "System", null, "", "Capitán Solari", 4, null, "" },
                    { 39, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2011), "System", null, "", "La Tigra", 4, null, "" },
                    { 40, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2012), "System", null, "", "Enrique Urien", 4, null, "" },
                    { 41, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2013), "System", null, "", "Los Frentones", 4, null, "" },
                    { 42, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2014), "System", null, "", "Pampa del Indio", 4, null, "" },
                    { 43, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2015), "System", null, "", "Puerto Eva Perón", 4, null, "" },
                    { 44, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2016), "System", null, "", "Ciervo Petiso", 4, null, "" },
                    { 45, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2017), "System", null, "", "Formosa", 8, null, "" },
                    { 46, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2018), "System", null, "", "Clorinda", 8, null, "" },
                    { 47, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2019), "System", null, "", "Pirané", 8, null, "" },
                    { 48, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2020), "System", null, "", "El Colorado", 8, null, "" },
                    { 49, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2021), "System", null, "", "Laguna Blanca", 8, null, "" },
                    { 50, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2022), "System", null, "", "Ingeniero Juárez", 8, null, "" },
                    { 51, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2023), "System", null, "", "General Manuel Belgrano", 8, null, "" },
                    { 52, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2024), "System", null, "", "Villa Dos Trece", 8, null, "" },
                    { 53, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2025), "System", null, "", "Ibarreta", 8, null, "" },
                    { 54, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2026), "System", null, "", "Las Lomitas", 8, null, "" },
                    { 55, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2027), "System", null, "", "Comandante Fontana", 8, null, "" },
                    { 56, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2028), "System", null, "", "San Francisco de Laishí", 8, null, "" },
                    { 57, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2029), "System", null, "", "Misión Tacaaglé", 8, null, "" },
                    { 58, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2030), "System", null, "", "Herradura", 8, null, "" },
                    { 59, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2031), "System", null, "", "Estanislao del Campo", 8, null, "" },
                    { 60, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2032), "System", null, "", "Buena Vista", 8, null, "" },
                    { 61, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2033), "System", null, "", "Laguna Naick Neck", 8, null, "" },
                    { 62, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2034), "System", null, "", "Gran Guardia", 8, null, "" },
                    { 63, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2035), "System", null, "", "Tres Lagunas", 8, null, "" },
                    { 64, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2036), "System", null, "", "Riacho He Hé", 8, null, "" },
                    { 65, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2037), "System", null, "", "Laguna Yema", 8, null, "" },
                    { 66, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2038), "System", null, "", "Mayor Vicente Villafañe", 8, null, "" },
                    { 67, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2039), "System", null, "", "Subteniente Perín", 8, null, "" },
                    { 68, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2040), "System", null, "", "Misión San Martín", 8, null, "" },
                    { 69, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2041), "System", null, "", "El Espinillo", 8, null, "" },
                    { 70, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2042), "System", null, "", "Siete Palmas", 8, null, "" },
                    { 71, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2044), "System", null, "", "Palo Santo", 8, null, "" },
                    { 72, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2045), "System", null, "", "Villa Escolar", 8, null, "" },
                    { 73, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2046), "System", null, "", "Loma Monte Lindo", 8, null, "" },
                    { 74, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2049), "System", null, "", "General Lucio V. Mansilla", 8, null, "" },
                    { 75, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2051), "System", null, "", "Colonia Pastoril", 8, null, "" },
                    { 76, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2052), "System", null, "", "Fortín Lugones", 8, null, "" },
                    { 77, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2053), "System", null, "", "Pozo del Tigre", 8, null, "" },
                    { 78, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2054), "System", null, "", "Las Cañitas", 8, null, "" },
                    { 79, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2055), "System", null, "", "El Potrillo", 8, null, "" },
                    { 80, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2056), "System", null, "", "Palma Sola", 8, null, "" },
                    { 81, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2057), "System", null, "", "San Hilario", 8, null, "" },
                    { 82, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2058), "System", null, "", "Colonia Ituzaingó", 8, null, "" },
                    { 83, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2059), "System", null, "", "General Güemes", 8, null, "" },
                    { 84, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2060), "System", null, "", "Corrientes", 3, null, "" },
                    { 85, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2061), "System", null, "", "Goya", 3, null, "" },
                    { 86, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2062), "System", null, "", "Paso de los Libres", 3, null, "" },
                    { 87, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2063), "System", null, "", "Mercedes", 3, null, "" },
                    { 88, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2064), "System", null, "", "Bella Vista", 3, null, "" },
                    { 89, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2065), "System", null, "", "Santo Tomé", 3, null, "" },
                    { 90, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2066), "System", null, "", "Esquina", 3, null, "" },
                    { 91, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2067), "System", null, "", "Monte Caseros", 3, null, "" },
                    { 92, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2068), "System", null, "", "Curuzú Cuatiá", 3, null, "" },
                    { 93, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2069), "System", null, "", "Ituzaingó", 3, null, "" },
                    { 94, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2070), "System", null, "", "Mocoretá", 3, null, "" },
                    { 95, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2071), "System", null, "", "Saladas", 3, null, "" },
                    { 96, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2072), "System", null, "", "Sauce", 3, null, "" },
                    { 97, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2073), "System", null, "", "San Luis del Palmar", 3, null, "" },
                    { 98, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2074), "System", null, "", "Empedrado", 3, null, "" },
                    { 99, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2075), "System", null, "", "Santa Lucía", 3, null, "" },
                    { 100, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2076), "System", null, "", "Concepción", 3, null, "" },
                    { 101, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2077), "System", null, "", "San Roque", 3, null, "" },
                    { 102, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2078), "System", null, "", "Paso de la Patria", 3, null, "" },
                    { 103, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2079), "System", null, "", "Alvear", 3, null, "" },
                    { 104, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2080), "System", null, "", "Riachuelo", 3, null, "" },
                    { 105, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2081), "System", null, "", "San Miguel", 3, null, "" },
                    { 106, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2082), "System", null, "", "Santa Rosa", 3, null, "" },
                    { 107, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2083), "System", null, "", "San Lorenzo", 3, null, "" },
                    { 108, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2084), "System", null, "", "Colonia Carlos Pellegrini", 3, null, "" },
                    { 109, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2085), "System", null, "", "San Cosme", 3, null, "" },
                    { 110, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2086), "System", null, "", "Colonia Libertad", 3, null, "" },
                    { 111, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2087), "System", null, "", "Loreto", 3, null, "" },
                    { 112, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2088), "System", null, "", "San Carlos", 3, null, "" },
                    { 113, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2089), "System", null, "", "Yapeyú", 3, null, "" },
                    { 114, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2090), "System", null, "", "Bonpland", 3, null, "" },
                    { 115, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2091), "System", null, "", "Berón de Astrada", 3, null, "" },
                    { 116, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2092), "System", null, "", "Juan Pujol", 3, null, "" },
                    { 117, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2093), "System", null, "", "Gobernador Virasoro", 3, null, "" },
                    { 118, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2094), "System", null, "", "Itatí", 3, null, "" },
                    { 119, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2095), "System", null, "", "Chavarría", 3, null, "" },
                    { 120, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2096), "System", null, "", "Tapebicuá", 3, null, "" },
                    { 121, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2097), "System", null, "", "Parada Pucheta", 3, null, "" },
                    { 122, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2098), "System", null, "", "Perugorría", 3, null, "" },
                    { 123, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2099), "System", null, "", "Felipe Yofre", 3, null, "" },
                    { 124, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2100), "System", null, "", "Ramón Lista", 3, null, "" },
                    { 125, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2102), "System", null, "", "Villa Olivari", 3, null, "" }
                });

            migrationBuilder.InsertData(
                table: "Neighborhood",
                columns: new[] { "Id", "CityId", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2174), "System", null, "", "Centro", null, "" },
                    { 2, 2, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2176), "System", null, "", "Nueva Córdoba", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "CityId", "Created", "CreatedBy", "Deleted", "DeletedBy", "NeighborhoodId", "NeighborhoodName", "ProvinceId", "StreetNumber", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2203), "System", null, "", 1, "Centro", 1, "1234", null, "" },
                    { 2, 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2223), "System", null, "", 1, "Centro 222", 1, "1234", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "AddressId", "BirthDate", "Created", "CreatedBy", "DNI", "Deleted", "DeletedBy", "Email", "GenderId", "LastName", "Name", "Phone", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2278), "System", "20123456789", null, "", "juan.perez@example.com", 1, "Pérez", "Juan", "1234567890", null, "" },
                    { 2, 2, new DateTime(1978, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2280), "System", "20987654321", null, "", "maria.gonzalez@example.com", 2, "González", "María", "0987654321", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "PersonId", "Specialty", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2305), "System", null, "", 2, "Cardiología", null, "" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "DoctorId", "PersonId", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2324), "System", null, "", 1, 1, null, "" });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "Address", "Created", "CreatedBy", "CustomerId", "DateEnd", "DateStart", "Deleted", "DeletedBy", "Status", "Updated", "UpdatedBy" },
                values: new object[] { 1, "1234 Centro, La Plata", null, "System", 1, new DateTime(2024, 8, 30, 12, 29, 1, 647, DateTimeKind.Local).AddTicks(2349), new DateTime(2024, 8, 30, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2345), null, "", 0, null, "" });

            migrationBuilder.InsertData(
                table: "MedicalRecord",
                columns: new[] { "Id", "Created", "CreatedBy", "CustomerId", "Date", "Deleted", "DeletedBy", "Diagnosis", "Treatment", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2375), "System", 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2372), null, "", "Hipertensión", "Dieta baja en sodio", null, "" });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "Amount", "AppointmentId", "Created", "CreatedBy", "Date", "Deleted", "DeletedBy", "Updated", "UpdatedBy" },
                values: new object[] { 1, 200.00m, 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2399), "System", new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2395), null, "", null, "" });

            migrationBuilder.InsertData(
                table: "Reminder",
                columns: new[] { "Id", "AppointmentId", "Created", "CreatedBy", "Date", "Deleted", "DeletedBy", "SendMode", "Sent", "Updated", "UpdatedBy" },
                values: new object[] { 1, 1, new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2421), "System", new DateTime(2024, 8, 29, 11, 29, 1, 647, DateTimeKind.Local).AddTicks(2419), null, "", "Email", false, null, "" });

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
