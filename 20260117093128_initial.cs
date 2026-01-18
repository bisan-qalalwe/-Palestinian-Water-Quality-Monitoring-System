using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterQualityMonitoring.Migrations
{ 
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "WaterReports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PollutionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterReports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_WaterReports_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Governorate", "Password", "PhoneNumber", "RegistrationDate", "Username" },
                values: new object[] { 1, "admin@waterquality.ps", "Gaza", "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9", "0599000000", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Governorate", "Password", "PhoneNumber", "RegistrationDate", "Username" },
                values: new object[] { 2, "ahmad@gmail.com", "Rafah", "306098fa01257f8e4809cbdfca258d8c22c7fb12937cc2616ef06aa20fd8008e", "0599111111", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmad" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Governorate", "Password", "PhoneNumber", "RegistrationDate", "Username" },
                values: new object[] { 3, "sara@hotmail.com", "KhanYounis", "926b4b8a00cfab44b758450fa6bf188d4bf8541c2fd6b3d9b93d152d43a99f64", "0599222222", new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara" });

            migrationBuilder.InsertData(
                table: "WaterReports",
                columns: new[] { "ReportID", "Description", "Location", "PollutionType", "ReportDate", "SourceType", "Status", "UserID" },
                values: new object[,]
                {
                    { 1, "Unpleasant odor and strange color in drinking water", "Shuja'iyya neighborhood - Gaza", "chemical pollution", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "sewage from settlements", "Confirmed", 2 },
                    { 2, "Bacterial contamination led to intestinal diseases", "Nuseirat camp", "bacteria", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "sewage leak", "Pending", 3 },
                    { 3, "Salty water unfit for drinking", "Rafah City - Brazil Neighborhood", "high salinity", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seawater intrusion", "Confirmed", 1 },
                    { 4, "Complaints of stomach pain after drinking water", "Al-Tuffah neighborhood - Gaza", "heavy metals", new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "industrial waste", "Pending", 2 },
                    { 5, "Turbid water with sediment present", "Jabalia camp", "High turbidity", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Construction and excavation works", "Confirmed", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WaterReports_UserID",
                table: "WaterReports",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WaterReports");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
