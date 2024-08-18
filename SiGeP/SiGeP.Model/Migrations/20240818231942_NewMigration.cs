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
                    StreetNumber = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: false),
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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
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
                    { 1, new DateTime(2024, 8, 18, 20, 19, 42, 81, DateTimeKind.Local).AddTicks(8967), "System", null, "", "Masculino", null, "" },
                    { 2, new DateTime(2024, 8, 18, 20, 19, 42, 81, DateTimeKind.Local).AddTicks(8969), "System", null, "", "Femenino", null, "" },
                    { 3, new DateTime(2024, 8, 18, 20, 19, 42, 81, DateTimeKind.Local).AddTicks(8970), "System", null, "", "Otro", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9766), "System", null, "", "Buenos Aires", null, "" },
                    { 2, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9768), "System", null, "", "Córdoba", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Permissions", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 18, 20, 19, 42, 85, DateTimeKind.Local).AddTicks(122), "System", null, "", "Admin", "FullAccess", null, "" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Password", "RoleId", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9707), "System", null, "", "admin", "admin", 1, null, "" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "ProvinceId", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9796), "System", null, "", "La Plata", 1, null, "" },
                    { 2, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9797), "System", null, "", "Córdoba Capital", 2, null, "" }
                });

            migrationBuilder.InsertData(
                table: "Neighborhood",
                columns: new[] { "Id", "CityId", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9828), "System", null, "", "Centro", null, "" },
                    { 2, 2, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9830), "System", null, "", "Nueva Córdoba", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "CityId", "Created", "CreatedBy", "Deleted", "DeletedBy", "NeighborhoodId", "NeighborhoodName", "ProvinceId", "StreetNumber", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9896), "System", null, "", 1, "Centro", 1, "1234", null, "" },
                    { 2, 1, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9915), "System", null, "", 1, "Centro 222", 1, "1234", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "AddressId", "BirthDate", "Created", "CreatedBy", "DNI", "Deleted", "DeletedBy", "Email", "GenderId", "LastName", "Name", "Phone", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9944), "System", "20123456789", null, "", "juan.perez@example.com", 1, "Pérez", "Juan", "1234567890", null, "" },
                    { 2, 2, new DateTime(1978, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9946), "System", "20987654321", null, "", "maria.gonzalez@example.com", 2, "González", "Dr. María", "0987654321", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "PersonId", "Specialty", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 18, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9967), "System", null, "", 2, "Cardiología", null, "" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "DoctorId", "PersonId", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 18, 20, 19, 42, 81, DateTimeKind.Local).AddTicks(8781), "System", null, "", 1, 1, null, "" });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "Address", "Created", "CreatedBy", "CustomerId", "Date", "Deleted", "DeletedBy", "Updated", "UpdatedBy" },
                values: new object[] { 1, "1234 Centro, La Plata", new DateTime(2024, 8, 18, 20, 19, 42, 85, DateTimeKind.Local).AddTicks(9), "System", 1, new DateTime(2024, 8, 19, 20, 19, 42, 84, DateTimeKind.Local).AddTicks(9992), null, "", null, "" });

            migrationBuilder.InsertData(
                table: "MedicalRecord",
                columns: new[] { "Id", "Created", "CreatedBy", "CustomerId", "Date", "Deleted", "DeletedBy", "Diagnosis", "Treatment", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 18, 20, 19, 42, 85, DateTimeKind.Local).AddTicks(37), "System", 1, new DateTime(2024, 8, 18, 20, 19, 42, 85, DateTimeKind.Local).AddTicks(30), null, "", "Hipertensión", "Dieta baja en sodio", null, "" });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "Amount", "AppointmentId", "Created", "CreatedBy", "Date", "Deleted", "DeletedBy", "Updated", "UpdatedBy" },
                values: new object[] { 1, 200.00m, 1, new DateTime(2024, 8, 18, 20, 19, 42, 85, DateTimeKind.Local).AddTicks(67), "System", new DateTime(2024, 8, 18, 20, 19, 42, 85, DateTimeKind.Local).AddTicks(62), null, "", null, "" });

            migrationBuilder.InsertData(
                table: "Reminder",
                columns: new[] { "Id", "AppointmentId", "Created", "CreatedBy", "Date", "Deleted", "DeletedBy", "SendMode", "Sent", "Updated", "UpdatedBy" },
                values: new object[] { 1, 1, new DateTime(2024, 8, 18, 20, 19, 42, 85, DateTimeKind.Local).AddTicks(92), "System", new DateTime(2024, 8, 18, 20, 19, 42, 85, DateTimeKind.Local).AddTicks(91), null, "", "Email", false, null, "" });

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
