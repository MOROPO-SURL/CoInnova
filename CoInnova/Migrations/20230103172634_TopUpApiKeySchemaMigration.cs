using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.UIC.CoInnova.Migrations
{
    public partial class TopUpApiKeySchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apikeypermissionbridge",
                columns: table => new
                {
                    apikeyid = table.Column<Guid>(nullable: false),
                    permissionid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_apikeypermissionbridge", x => new { x.apikeyid, x.permissionid });
                    table.ForeignKey(
                        name: "fk_apikeypermissionbridge_apikeys_apikeyid",
                        column: x => x.apikeyid,
                        principalTable: "apikeys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_apikeypermissionbridge_permissions_permissionid",
                        column: x => x.permissionid,
                        principalSchema: "innovus_sapressc_core_security_common",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_apikeypermissionbridge_permissionid",
                table: "apikeypermissionbridge",
                column: "permissionid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apikeypermissionbridge");
        }
    }
}
