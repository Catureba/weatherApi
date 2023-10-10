using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace weatherApi.Migrations
{
    /// <inheritdoc />
    public partial class reafactorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Max_temperature = table.Column<float>(type: "real", nullable: false),
                    Min_temperature = table.Column<float>(type: "real", nullable: false),
                    Weather_day = table.Column<string>(type: "text", nullable: false),
                    Weather_night = table.Column<string>(type: "text", nullable: false),
                    Precipitation = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<string>(type: "real", nullable: false),
                    Wind_speed = table.Column<string>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
