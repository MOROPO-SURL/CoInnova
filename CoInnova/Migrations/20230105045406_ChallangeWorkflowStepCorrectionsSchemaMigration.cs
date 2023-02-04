using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.UIC.CoInnova.Migrations
{
    public partial class ChallangeWorkflowStepCorrectionsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_challangeworkflowsteps_challangeworkflowsteps_nextid",
                table: "challangeworkflowsteps");

            migrationBuilder.AlterColumn<Guid>(
                name: "nextid",
                table: "challangeworkflowsteps",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "fk_challangeworkflowsteps_challangeworkflowsteps_nextid",
                table: "challangeworkflowsteps",
                column: "nextid",
                principalTable: "challangeworkflowsteps",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_challangeworkflowsteps_challangeworkflowsteps_nextid",
                table: "challangeworkflowsteps");

            migrationBuilder.AlterColumn<Guid>(
                name: "nextid",
                table: "challangeworkflowsteps",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_challangeworkflowsteps_challangeworkflowsteps_nextid",
                table: "challangeworkflowsteps",
                column: "nextid",
                principalTable: "challangeworkflowsteps",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
