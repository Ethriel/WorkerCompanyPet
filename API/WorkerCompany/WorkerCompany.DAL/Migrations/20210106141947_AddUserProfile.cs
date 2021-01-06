using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerCompany.DAL.Migrations
{
    public partial class AddUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Worker");

            migrationBuilder.AddColumn<int>(
                name: "AppUserProfileId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarriageStatus = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProfile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppUserProfileId",
                table: "AspNetUsers",
                column: "AppUserProfileId",
                unique: true,
                filter: "[AppUserProfileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AppUserProfile_AppUserProfileId",
                table: "AspNetUsers",
                column: "AppUserProfileId",
                principalTable: "AppUserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AppUserProfile_AppUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AppUserProfile");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Worker",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
