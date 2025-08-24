using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Courses_Platform.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class databaseChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_PaidCourses_PaidCourseId",
                table: "Episodes");

            migrationBuilder.DropTable(
                name: "PaidCourses");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_PaidCourseId",
                table: "Episodes");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "PaidCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidCourses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_PaidCourseId",
                table: "Episodes",
                column: "PaidCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_PaidCourses_PaidCourseId",
                table: "Episodes",
                column: "PaidCourseId",
                principalTable: "PaidCourses",
                principalColumn: "Id");
        }
    }
}
