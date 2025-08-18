using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Courses_Platform.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class firstdatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_PaidCourses_Paid_CourseId",
                table: "Episodes");

            migrationBuilder.RenameColumn(
                name: "Paid_CourseId",
                table: "Episodes",
                newName: "PaidCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Episodes_Paid_CourseId",
                table: "Episodes",
                newName: "IX_Episodes_PaidCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_PaidCourses_PaidCourseId",
                table: "Episodes",
                column: "PaidCourseId",
                principalTable: "PaidCourses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_PaidCourses_PaidCourseId",
                table: "Episodes");

            migrationBuilder.RenameColumn(
                name: "PaidCourseId",
                table: "Episodes",
                newName: "Paid_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Episodes_PaidCourseId",
                table: "Episodes",
                newName: "IX_Episodes_Paid_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_PaidCourses_Paid_CourseId",
                table: "Episodes",
                column: "Paid_CourseId",
                principalTable: "PaidCourses",
                principalColumn: "Id");
        }
    }
}
