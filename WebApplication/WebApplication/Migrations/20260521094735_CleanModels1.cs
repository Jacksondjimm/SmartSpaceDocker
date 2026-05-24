using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesApp.Migrations
{
    /// <inheritdoc />
    public partial class CleanModels1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hum",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "num",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "temp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "SensorData_05");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SensorData_05");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "SensorData_04");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SensorData_04");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "SensorData_03");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SensorData_03");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "SensorData_02");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SensorData_02");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "SensorData_01");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SensorData_01");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "hum",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "num",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "temp",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "SensorData_05",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SensorData_05",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "SensorData_04",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SensorData_04",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "SensorData_03",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SensorData_03",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "SensorData_02",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SensorData_02",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "SensorData_01",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SensorData_01",
                type: "text",
                nullable: true);
        }
    }
}
