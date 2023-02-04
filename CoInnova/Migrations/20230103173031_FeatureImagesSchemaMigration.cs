using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.UIC.CoInnova.Migrations
{
    public partial class FeatureImagesSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "featureimageid",
                table: "prototypes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "featureimageid",
                table: "ideas",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "featureimageid",
                table: "challanges",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_prototypes_featureimageid",
                table: "prototypes",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_ideas_featureimageid",
                table: "ideas",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_challanges_featureimageid",
                table: "challanges",
                column: "featureimageid");

            migrationBuilder.AddForeignKey(
                name: "fk_challanges_images_featureimageid",
                table: "challanges",
                column: "featureimageid",
                principalSchema: "innovus_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ideas_images_featureimageid",
                table: "ideas",
                column: "featureimageid",
                principalSchema: "innovus_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_prototypes_images_featureimageid",
                table: "prototypes",
                column: "featureimageid",
                principalSchema: "innovus_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_challanges_images_featureimageid",
                table: "challanges");

            migrationBuilder.DropForeignKey(
                name: "fk_ideas_images_featureimageid",
                table: "ideas");

            migrationBuilder.DropForeignKey(
                name: "fk_prototypes_images_featureimageid",
                table: "prototypes");

            migrationBuilder.DropIndex(
                name: "ix_prototypes_featureimageid",
                table: "prototypes");

            migrationBuilder.DropIndex(
                name: "ix_ideas_featureimageid",
                table: "ideas");

            migrationBuilder.DropIndex(
                name: "ix_challanges_featureimageid",
                table: "challanges");

            migrationBuilder.DropColumn(
                name: "featureimageid",
                table: "prototypes");

            migrationBuilder.DropColumn(
                name: "featureimageid",
                table: "ideas");

            migrationBuilder.DropColumn(
                name: "featureimageid",
                table: "challanges");
        }
    }
}
