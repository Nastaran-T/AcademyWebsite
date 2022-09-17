using Microsoft.EntityFrameworkCore.Migrations;

namespace Academy.DataLayer.Migrations
{
    public partial class mig_UserCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userCourses",
                columns: table => new
                {
                    UC_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userCourses", x => x.UC_Id);
                    table.ForeignKey(
                        name: "FK_userCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userCourses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userCourses_CourseId",
                table: "userCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_userCourses_UserId",
                table: "userCourses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userCourses");
        }
    }
}
