using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CheckInSKP.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IsAuthorized = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Username = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardNumber = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    PhoneNotification = table.Column<bool>(type: "boolean", nullable: false),
                    Preoccupied = table.Column<bool>(type: "boolean", nullable: false),
                    MeetingTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Staffs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeTypeId = table.Column<int>(type: "integer", nullable: false),
                    StaffId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeLogs_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeLogs_TimeTypes_TimeTypeId",
                        column: x => x.TimeTypeId,
                        principalTable: "TimeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Moderator" },
                    { 3, "Staff" },
                    { 4, "User" },
                    { 5, "Guest" },
                    { 6, "Monitor" }
                });

            migrationBuilder.InsertData(
                table: "TimeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CheckIn" },
                    { 2, "CheckOut" },
                    { 3, "BreakStart" },
                    { 4, "BreakEnd" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Name", "PasswordHash", "RoleId", "UpdatedAt", "Username" },
                values: new object[] { new Guid("9b51acfd-95af-46a7-9354-b108ac6e14be"), new DateTime(2023, 11, 14, 8, 24, 18, 39, DateTimeKind.Utc).AddTicks(1906), "System Administrator", "$2a$10$BZ1AY6lFmzN1SSn5KOAqVeFroH2AykeZ7cGYtl3hQC2pFeiDy2zkO", 1, null, "sysadmin" });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Id",
                table: "Devices",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Id",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CardNumber",
                table: "Staffs",
                column: "CardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_PhoneNumber",
                table: "Staffs",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_Id",
                table: "TimeLogs",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_StaffId",
                table: "TimeLogs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_TimeTypeId",
                table: "TimeLogs",
                column: "TimeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTypes_Id",
                table: "TimeTypes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTypes_Name",
                table: "TimeTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "TimeLogs");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "TimeTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
