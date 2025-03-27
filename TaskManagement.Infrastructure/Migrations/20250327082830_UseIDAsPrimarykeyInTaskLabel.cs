using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseIDAsPrimarykeyInTaskLabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLabels",
                table: "TaskLabels");

            migrationBuilder.AlterColumn<int>(
                name: "LabelId",
                table: "TaskLabels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TaskLabels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLabels",
                table: "TaskLabels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLabels_TaskId",
                table: "TaskLabels",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLabels",
                table: "TaskLabels");

            migrationBuilder.DropIndex(
                name: "IX_TaskLabels_TaskId",
                table: "TaskLabels");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TaskLabels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "LabelId",
                table: "TaskLabels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLabels",
                table: "TaskLabels",
                columns: new[] { "TaskId", "LabelId" });
        }
    }
}
