using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vk_test_api.Migrations
{
    /// <inheritdoc />
    public partial class Secound : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserGroups_User_Group_Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserStates_User_State_Id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserStates");

            migrationBuilder.CreateTable(
                name: "User_Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_States", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_User_Groups_User_Group_Id",
                table: "Users",
                column: "User_Group_Id",
                principalTable: "User_Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_User_States_User_State_Id",
                table: "Users",
                column: "User_State_Id",
                principalTable: "User_States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_User_Groups_User_Group_Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_User_States_User_State_Id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "User_Groups");

            migrationBuilder.DropTable(
                name: "User_States");

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStates", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserGroups_User_Group_Id",
                table: "Users",
                column: "User_Group_Id",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserStates_User_State_Id",
                table: "Users",
                column: "User_State_Id",
                principalTable: "UserStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
