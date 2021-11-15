using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backtracer.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tracer");

            migrationBuilder.CreateTable(
                name: "ConstantGroups",
                schema: "tracer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstantGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstantTypes",
                schema: "tracer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstantTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constants",
                schema: "tracer",
                columns: table => new
                {
                    ConstantGroupId = table.Column<int>(type: "integer", nullable: false),
                    ConstantTypeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constants", x => new { x.ConstantTypeId, x.ConstantGroupId });
                    table.ForeignKey(
                        name: "FK_Constants_ConstantGroups_ConstantGroupId",
                        column: x => x.ConstantGroupId,
                        principalSchema: "tracer",
                        principalTable: "ConstantGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Constants_ConstantTypes_ConstantTypeId",
                        column: x => x.ConstantTypeId,
                        principalSchema: "tracer",
                        principalTable: "ConstantTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Constants_ConstantGroupId",
                schema: "tracer",
                table: "Constants",
                column: "ConstantGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Constants",
                schema: "tracer");

            migrationBuilder.DropTable(
                name: "ConstantGroups",
                schema: "tracer");

            migrationBuilder.DropTable(
                name: "ConstantTypes",
                schema: "tracer");
        }
    }
}
