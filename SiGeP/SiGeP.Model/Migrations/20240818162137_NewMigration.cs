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
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
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
                });

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
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CUIL = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(32)", maxLength: 32, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
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
                        name: "FK_Customer_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Password", "Updated", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2024, 8, 18, 13, 21, 35, 499, DateTimeKind.Local).AddTicks(1266), "System", null, "", "admin", "admin", null, "" });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "DeletedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 18, 13, 21, 35, 498, DateTimeKind.Local).AddTicks(2103), "System", null, "", "Femenino", null, "" },
                    { 2, new DateTime(2024, 8, 18, 13, 21, 35, 498, DateTimeKind.Local).AddTicks(2104), "System", null, "", "Masculino", null, "" },
                    { 3, new DateTime(2024, 8, 18, 13, 21, 35, 498, DateTimeKind.Local).AddTicks(2106), "System", null, "", "Otro", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "BirthDate", "CUIL", "Created", "CreatedBy", "Deleted", "DeletedBy", "GenderId", "Name", "Phone", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "20123456789", new DateTime(2024, 8, 18, 13, 21, 35, 498, DateTimeKind.Local).AddTicks(1923), "System", null, "", 1, "Juan Pérez", "1234567890", null, "" },
                    { 2, new DateTime(1990, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "20234567890", new DateTime(2024, 8, 18, 13, 21, 35, 498, DateTimeKind.Local).AddTicks(1940), "System", null, "", 2, "María López", "0987654321", null, "" },
                    { 3, new DateTime(1982, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "20345678901", new DateTime(2024, 8, 18, 13, 21, 35, 498, DateTimeKind.Local).AddTicks(1942), "System", null, "", 1, "Carlos García", "1122334455", null, "" },
                    { 4, new DateTime(1995, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "20456789012", new DateTime(2024, 8, 18, 13, 21, 35, 498, DateTimeKind.Local).AddTicks(1943), "System", null, "", 2, "Ana Martínez", "6677889900", null, "" },
                    { 5, new DateTime(1978, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "20567890123", new DateTime(2024, 8, 18, 13, 21, 35, 498, DateTimeKind.Local).AddTicks(1978), "System", null, "", 1, "Miguel Fernández", "5566778899", null, "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Deleted",
                table: "Customer",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_GenderId",
                table: "Customer",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Deleted",
                table: "Gender",
                column: "Deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
