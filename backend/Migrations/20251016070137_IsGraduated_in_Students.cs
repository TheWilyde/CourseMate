using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseMate.Migrations
{
    /// <inheritdoc />
    public partial class IsGraduated_in_Students : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGraduated",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGraduated",
                table: "Students");
        }
    }
}
