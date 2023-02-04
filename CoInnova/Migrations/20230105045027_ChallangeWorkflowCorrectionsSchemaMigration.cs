using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.UIC.CoInnova.Migrations
{
    public partial class ChallangeWorkflowCorrectionsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_challangeworkflows_challangeworkflowsteps_startstepid",
                table: "challangeworkflows");

            migrationBuilder.AlterColumn<Guid>(
                name: "startstepid",
                table: "challangeworkflows",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "fk_challangeworkflows_challangeworkflowsteps_startstepid",
                table: "challangeworkflows",
                column: "startstepid",
                principalTable: "challangeworkflowsteps",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_challangeworkflows_challangeworkflowsteps_startstepid",
                table: "challangeworkflows");

            migrationBuilder.AlterColumn<Guid>(
                name: "startstepid",
                table: "challangeworkflows",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_challangeworkflows_challangeworkflowsteps_startstepid",
                table: "challangeworkflows",
                column: "startstepid",
                principalTable: "challangeworkflowsteps",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
