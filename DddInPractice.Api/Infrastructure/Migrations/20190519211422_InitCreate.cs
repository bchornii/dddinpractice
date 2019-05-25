using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DddInPractice.Api.Infrastructure.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "slotseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "SnakMachine",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MoneyInside_OneCentCount = table.Column<int>(nullable: false),
                    MoneyInside_TenCentCount = table.Column<int>(nullable: false),
                    MoneyInside_QuarterCount = table.Column<int>(nullable: false),
                    MoneyInside_OneDollarCount = table.Column<int>(nullable: false),
                    MoneyInside_FiveDollarCount = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnakMachine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnakType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnakType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnakMachineSlot",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    SnakTypeId = table.Column<int>(nullable: true),
                    ItemPrice = table.Column<decimal>(nullable: false),
                    ItemsQuantity = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    SnackMachineId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnakMachineSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnakMachineSlot_SnakMachine_SnackMachineId",
                        column: x => x.SnackMachineId,
                        principalTable: "SnakMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnakMachineSlot_SnakType_SnakTypeId",
                        column: x => x.SnakTypeId,
                        principalTable: "SnakType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SnakMachineSlot_SnackMachineId",
                table: "SnakMachineSlot",
                column: "SnackMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_SnakMachineSlot_SnakTypeId",
                table: "SnakMachineSlot",
                column: "SnakTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnakMachineSlot");

            migrationBuilder.DropTable(
                name: "SnakMachine");

            migrationBuilder.DropTable(
                name: "SnakType");

            migrationBuilder.DropSequence(
                name: "slotseq");
        }
    }
}
