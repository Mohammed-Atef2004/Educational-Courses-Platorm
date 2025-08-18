using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Courses_Platform.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class firstdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "Paid_CourseId",
                table: "Episodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaidCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidCourses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_Paid_CourseId",
                table: "Episodes",
                column: "Paid_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_PaidCourses_Paid_CourseId",
                table: "Episodes",
                column: "Paid_CourseId",
                principalTable: "PaidCourses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_PaidCourses_Paid_CourseId",
                table: "Episodes");

            migrationBuilder.DropTable(
                name: "PaidCourses");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_Paid_CourseId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Paid_CourseId",
                table: "Episodes");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Courses",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Courses",
                type: "float",
                nullable: true);
        }
    }
}
