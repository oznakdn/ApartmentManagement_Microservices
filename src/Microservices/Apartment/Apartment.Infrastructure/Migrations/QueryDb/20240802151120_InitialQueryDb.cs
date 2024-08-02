using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apartment.Infrastructure.Migrations.QueryDb
{
    /// <inheritdoc />
    public partial class InitialQueryDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SiteId = table.Column<string>(type: "text", nullable: false),
                    ManagerId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    VisitId = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<string>(type: "text", nullable: true),
                    HostName = table.Column<string>(type: "text", nullable: false),
                    HasCar = table.Column<bool>(type: "boolean", nullable: false),
                    PlateNumber = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BlockId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SiteId = table.Column<string>(type: "text", nullable: true),
                    TotalUnits = table.Column<int>(type: "integer", nullable: false),
                    //SiteQueryId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_Blocks_Sites_SiteQueryId",
                    //    column: x => x.SiteQueryId,
                    //    principalTable: "Sites",
                    //    principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<string>(type: "text", nullable: false),
                    ResidentId = table.Column<string>(type: "text", nullable: true),
                    BlockId = table.Column<string>(type: "text", nullable: true),
                    UnitNo = table.Column<int>(type: "integer", nullable: false),
                    HasCar = table.Column<bool>(type: "boolean", nullable: false),
                    //BlockQueryId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_Units_Blocks_BlockQueryId",
                    //    column: x => x.BlockQueryId,
                    //    principalTable: "Blocks",
                    //    principalColumn: "Id");
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Blocks_SiteQueryId",
            //    table: "Blocks",
            //    column: "SiteQueryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Units_BlockQueryId",
            //    table: "Units",
            //    column: "BlockQueryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
