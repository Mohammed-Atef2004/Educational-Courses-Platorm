using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Courses_Platform.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addpaidcourseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Courses_CourseId",
                table: "Episodes");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Episodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Courses_CourseId",
                table: "Episodes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Courses_CourseId",
                table: "Episodes");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Courses_CourseId",
                table: "Episodes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
