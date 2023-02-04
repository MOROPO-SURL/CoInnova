using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.UIC.CoInnova.Migrations
{
    public partial class IdeaTopUpSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "challangeid",
                table: "ideas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_ideas_challangeid",
                table: "ideas",
                column: "challangeid");

            migrationBuilder.AddForeignKey(
                name: "fk_ideas_challanges_challangeid",
                table: "ideas",
                column: "challangeid",
                principalTable: "challanges",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_ideas_challanges_challangeid",
                table: "ideas");

            migrationBuilder.DropIndex(
                name: "ix_ideas_challangeid",
                table: "ideas");

            migrationBuilder.DropColumn(
                name: "challangeid",
                table: "ideas");
        }
    }
}
