using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToTaskLabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TaskLabels",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Color",
                value: "#ffb6c1");

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Color",
                value: "#ecdac5");

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: "#e198b5");

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 4,
                column: "Color",
                value: "#ecc5e0");

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 5,
                column: "Color",
                value: "#d8fdc1");

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 6,
                column: "Color",
                value: "#a08bda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "TaskLabels");

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 4,
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 5,
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 6,
                column: "Color",
                value: null);
        }
    }
}
