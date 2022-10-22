using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maffei.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Money = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kdv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KdvRatio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kdv", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KdvId = table.Column<int>(type: "int", nullable: false),
                    CurrencyUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abone_CurrencyUnit_CurrencyUnitId",
                        column: x => x.CurrencyUnitId,
                        principalTable: "CurrencyUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abone_Kdv_KdvId",
                        column: x => x.KdvId,
                        principalTable: "Kdv",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndexCalculation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AboneId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<float>(type: "real", nullable: false),
                    CalculationTypeId = table.Column<int>(type: "int", nullable: false),
                    FirstIndex = table.Column<float>(type: "real", nullable: false),
                    LastIndex = table.Column<float>(type: "real", nullable: false),
                    ConsumptionAmount = table.Column<float>(type: "real", nullable: false),
                    RecipeTotalPrice = table.Column<float>(type: "real", nullable: false),
                    TariffKdvPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexCalculation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndexCalculation_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndexCalculation_CalculationType_CalculationTypeId",
                        column: x => x.CalculationTypeId,
                        principalTable: "CalculationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abone_CurrencyUnitId",
                table: "Abone",
                column: "CurrencyUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Abone_KdvId",
                table: "Abone",
                column: "KdvId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexCalculation_AboneId",
                table: "IndexCalculation",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexCalculation_CalculationTypeId",
                table: "IndexCalculation",
                column: "CalculationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndexCalculation");

            migrationBuilder.DropTable(
                name: "Abone");

            migrationBuilder.DropTable(
                name: "CalculationType");

            migrationBuilder.DropTable(
                name: "CurrencyUnit");

            migrationBuilder.DropTable(
                name: "Kdv");
        }
    }
}
