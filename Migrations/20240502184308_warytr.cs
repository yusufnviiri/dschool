using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace victors.Migrations
{
    /// <inheritdoc />
    public partial class warytr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamCache_Courses_CourseId",
                table: "ExamCache");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamCache_Students_StudentId",
                table: "ExamCache");

            migrationBuilder.DropIndex(
                name: "IX_ExamCache_CourseId",
                table: "ExamCache");

            migrationBuilder.DropIndex(
                name: "IX_ExamCache_StudentId",
                table: "ExamCache");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "ExamCache",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "ExamCache",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ExamSession",
                table: "ExamCache",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamSession",
                table: "ExamCache");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "ExamCache",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "ExamCache",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCache_CourseId",
                table: "ExamCache",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCache_StudentId",
                table: "ExamCache",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamCache_Courses_CourseId",
                table: "ExamCache",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamCache_Students_StudentId",
                table: "ExamCache",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
