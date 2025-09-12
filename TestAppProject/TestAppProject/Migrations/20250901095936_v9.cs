using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAppProject.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profilepic",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "profilepic_base64string",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profilepic_name",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profilepic_base64string",
                table: "users");

            migrationBuilder.DropColumn(
                name: "profilepic_name",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "profilepic",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
