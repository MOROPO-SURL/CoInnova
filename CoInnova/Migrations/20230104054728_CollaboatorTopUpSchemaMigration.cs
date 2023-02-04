using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.UIC.CoInnova.Migrations
{
    public partial class CollaboatorTopUpSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "organizationidentityid",
                table: "collaborators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_collaborators_organizationidentityid",
                table: "collaborators",
                column: "organizationidentityid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_collaborators_organizationidentities_organizationidentityid",
                table: "collaborators",
                column: "organizationidentityid",
                principalSchema: "innovus_sapressc_middleware_identity",
                principalTable: "organizationidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_collaborators_organizationidentities_organizationidentityid",
                table: "collaborators");

            migrationBuilder.DropIndex(
                name: "ix_collaborators_organizationidentityid",
                table: "collaborators");

            migrationBuilder.DropColumn(
                name: "organizationidentityid",
                table: "collaborators");
        }
    }
}
