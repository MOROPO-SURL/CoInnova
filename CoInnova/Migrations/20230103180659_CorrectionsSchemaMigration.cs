using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.UIC.CoInnova.Migrations
{
    public partial class CorrectionsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_challanges_challangeworkflows_workflowid",
                table: "challanges");

            migrationBuilder.AddColumn<Guid>(
                name: "workflowid",
                table: "ideas",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "workflowid",
                table: "challanges",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "ix_ideas_workflowid",
                table: "ideas",
                column: "workflowid");

            migrationBuilder.AddForeignKey(
                name: "fk_challanges_challangeworkflows_workflowid",
                table: "challanges",
                column: "workflowid",
                principalTable: "challangeworkflows",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ideas_ideaworkflow_workflowid",
                table: "ideas",
                column: "workflowid",
                principalTable: "ideaworkflow",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_challanges_challangeworkflows_workflowid",
                table: "challanges");

            migrationBuilder.DropForeignKey(
                name: "fk_ideas_ideaworkflow_workflowid",
                table: "ideas");

            migrationBuilder.DropIndex(
                name: "ix_ideas_workflowid",
                table: "ideas");

            migrationBuilder.DropColumn(
                name: "workflowid",
                table: "ideas");

            migrationBuilder.AlterColumn<Guid>(
                name: "workflowid",
                table: "challanges",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_challanges_challangeworkflows_workflowid",
                table: "challanges",
                column: "workflowid",
                principalTable: "challangeworkflows",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
