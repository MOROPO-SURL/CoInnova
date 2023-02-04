using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MOROPO.UIC.CoInnova.Migrations
{
    public partial class InitialSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "innovus_sapressc_core_security_common");

            migrationBuilder.EnsureSchema(
                name: "innovus_sapressc_middleware_identity");

            migrationBuilder.EnsureSchema(
                name: "innovus_sapressc_middleware_media");

            migrationBuilder.EnsureSchema(
                name: "innovus_sapressc_middleware_region");

            migrationBuilder.EnsureSchema(
                name: "innovus_sapressc_middleware_security_identity");

            migrationBuilder.CreateTable(
                name: "apikeys",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    token = table.Column<string>(nullable: false),
                    enabled = table.Column<bool>(nullable: false),
                    expirateat = table.Column<DateTime>(nullable: false),
                    host = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_apikeys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ideas",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    ispublic = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    createdat = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prototypinglevels",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    numerator = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prototypinglevels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "traces",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    type = table.Column<int>(nullable: false),
                    author = table.Column<string>(nullable: true),
                    registered = table.Column<DateTime>(nullable: false),
                    entityname = table.Column<string>(nullable: false),
                    beforedata = table.Column<string>(nullable: false),
                    afterdata = table.Column<string>(nullable: true),
                    nextid = table.Column<long>(nullable: false),
                    signature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_traces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "innovus_sapressc_core_security_common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    family = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "innovus_sapressc_core_security_common",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emailcontactitems",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    emailaddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_emailcontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phonecontactitems",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    phonenumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phonecontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "socialcontactitems",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    socialid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socialcontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "websitecontactitems",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_websitecontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "thumbnails",
                schema: "innovus_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_thumbnails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "innovus_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    isocode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "evaluations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createdat = table.Column<DateTime>(nullable: false),
                    ideaid = table.Column<Guid>(nullable: false),
                    valoration = table.Column<int>(nullable: false),
                    argument = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_evaluations", x => x.id);
                    table.ForeignKey(
                        name: "fk_evaluations_ideas_ideaid",
                        column: x => x.ideaid,
                        principalTable: "ideas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prototypes",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createdat = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    ispublic = table.Column<bool>(nullable: false),
                    levelid = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    ideaid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prototypes", x => x.id);
                    table.ForeignKey(
                        name: "fk_prototypes_ideas_ideaid",
                        column: x => x.ideaid,
                        principalTable: "ideas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prototypes_prototypinglevels_levelid",
                        column: x => x.levelid,
                        principalTable: "prototypinglevels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rolepermissionbridges",
                schema: "innovus_sapressc_core_security_common",
                columns: table => new
                {
                    roleid = table.Column<Guid>(nullable: false),
                    permissionid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rolepermissionbridges", x => new { x.roleid, x.permissionid });
                    table.ForeignKey(
                        name: "fk_rolepermissionbridges_permissions_permissionid",
                        column: x => x.permissionid,
                        principalSchema: "innovus_sapressc_core_security_common",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rolepermissionbridges_roles_roleid",
                        column: x => x.roleid,
                        principalSchema: "innovus_sapressc_core_security_common",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    thumbnailid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documents", x => x.id);
                    table.ForeignKey(
                        name: "fk_documents_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "innovus_sapressc_middleware_media",
                        principalTable: "thumbnails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "images",
                schema: "innovus_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createat = table.Column<DateTime>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true),
                    height = table.Column<int>(nullable: false),
                    width = table.Column<int>(nullable: false),
                    thumbnailid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "innovus_sapressc_middleware_media",
                        principalTable: "thumbnails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videos",
                schema: "innovus_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createat = table.Column<DateTime>(nullable: false),
                    extension = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true),
                    duration = table.Column<TimeSpan>(nullable: false),
                    format = table.Column<string>(nullable: false),
                    framesize = table.Column<string>(nullable: false),
                    colormodel = table.Column<string>(nullable: false),
                    fps = table.Column<double>(nullable: false),
                    audioformat = table.Column<string>(nullable: false),
                    audiochanneloutput = table.Column<string>(nullable: false),
                    thumbnailid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_videos", x => x.id);
                    table.ForeignKey(
                        name: "fk_videos_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "innovus_sapressc_middleware_media",
                        principalTable: "thumbnails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "states",
                schema: "innovus_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    countryid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_states", x => x.id);
                    table.ForeignKey(
                        name: "fk_states_countries_countryid",
                        column: x => x.countryid,
                        principalSchema: "innovus_sapressc_middleware_region",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ideadocumentbridges",
                columns: table => new
                {
                    ideaid = table.Column<Guid>(nullable: false),
                    documentid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideadocumentbridges", x => new { x.ideaid, x.documentid });
                    table.ForeignKey(
                        name: "fk_ideadocumentbridges_documents_documentid",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ideadocumentbridges_ideas_ideaid",
                        column: x => x.ideaid,
                        principalTable: "ideas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prototypedocumentbridges",
                columns: table => new
                {
                    prototypeid = table.Column<Guid>(nullable: false),
                    documentid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prototypedocumentbridges", x => new { x.prototypeid, x.documentid });
                    table.ForeignKey(
                        name: "fk_prototypedocumentbridges_documents_documentid",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prototypedocumentbridges_prototypes_prototypeid",
                        column: x => x.prototypeid,
                        principalTable: "prototypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prototypeimagebridges",
                columns: table => new
                {
                    prototypeid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prototypeimagebridges", x => new { x.prototypeid, x.imageid });
                    table.ForeignKey(
                        name: "fk_prototypeimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "innovus_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prototypeimagebridges_prototypes_prototypeid",
                        column: x => x.prototypeid,
                        principalTable: "prototypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationidentities",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    borndate = table.Column<DateTime>(nullable: true),
                    about = table.Column<string>(nullable: true),
                    ownerid = table.Column<Guid>(nullable: true),
                    imageid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationidentities", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizationidentities_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "innovus_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_organizationidentities_organizationidentities_ownerid",
                        column: x => x.ownerid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personidentities",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    borndate = table.Column<DateTime>(nullable: true),
                    about = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true),
                    alias = table.Column<string>(nullable: true),
                    sex = table.Column<int>(nullable: false),
                    civilstatus = table.Column<int>(nullable: false),
                    imageid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personidentities", x => x.id);
                    table.ForeignKey(
                        name: "fk_personidentities_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "innovus_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "municipalities",
                schema: "innovus_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    stateid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_municipalities", x => x.id);
                    table.ForeignKey(
                        name: "fk_municipalities_states_stateid",
                        column: x => x.stateid,
                        principalSchema: "innovus_sapressc_middleware_region",
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationemailcontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationemailcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationemailcontactitembridges_emailcontactitems_conta~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "emailcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationemailcontactitembridges_organizationidentities_~",
                        column: x => x.organizationid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationphonecontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationphonecontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationphonecontactitembridges_phonecontactitems_conta~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "phonecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationphonecontactitembridges_organizationidentities_~",
                        column: x => x.organizationid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationsocialcontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationsocialcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationsocialcontactitembridges_socialcontactitems_con~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "socialcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationsocialcontactitembridges_organizationidentities~",
                        column: x => x.organizationid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationwebsitecontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationwebsitecontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationwebsitecontactitembridges_websitecontactitems_c~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "websitecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationwebsitecontactitembridges_organizationidentitie~",
                        column: x => x.organizationid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    organizationid = table.Column<Guid>(nullable: false),
                    role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_memberships", x => new { x.organizationid, x.personid });
                    table.ForeignKey(
                        name: "fk_memberships_organizationidentities_organizationid",
                        column: x => x.organizationid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_memberships_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personemailcontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personemailcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personemailcontactitembridges_emailcontactitems_contactitem~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "emailcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personemailcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personphonecontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personphonecontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personphonecontactitembridges_phonecontactitems_contactitem~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "phonecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personphonecontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personsocialcontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personsocialcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personsocialcontactitembridges_socialcontactitems_contactit~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "socialcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personsocialcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personwebsitecontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personwebsitecontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personwebsitecontactitembridges_websitecontactitems_contact~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "websitecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personwebsitecontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "innovus_sapressc_middleware_security_identity",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    enabled = table.Column<bool>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    passwordchangerequired = table.Column<bool>(nullable: false),
                    hasfullaccess = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    lastsessiondate = table.Column<DateTime>(nullable: false),
                    failauthenticationcount = table.Column<int>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false),
                    signature = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_personidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "localities",
                schema: "innovus_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    municipalityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_localities", x => x.id);
                    table.ForeignKey(
                        name: "fk_localities_municipalities_municipalityid",
                        column: x => x.municipalityid,
                        principalSchema: "innovus_sapressc_middleware_region",
                        principalTable: "municipalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collaborators",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createdat = table.Column<DateTime>(nullable: false),
                    userid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collaborators", x => x.id);
                    table.ForeignKey(
                        name: "fk_collaborators_users_userid",
                        column: x => x.userid,
                        principalSchema: "innovus_sapressc_middleware_security_identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ideacomments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    createdat = table.Column<DateTime>(nullable: false),
                    authorid = table.Column<string>(nullable: false),
                    ideaid = table.Column<Guid>(nullable: false),
                    message = table.Column<string>(nullable: false),
                    upstreamid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideacomments", x => x.id);
                    table.ForeignKey(
                        name: "fk_ideacomments_users_authorid",
                        column: x => x.authorid,
                        principalSchema: "innovus_sapressc_middleware_security_identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ideacomments_ideas_ideaid",
                        column: x => x.ideaid,
                        principalTable: "ideas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ideacomments_ideacomments_upstreamid",
                        column: x => x.upstreamid,
                        principalTable: "ideacomments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "userrolebridges",
                schema: "innovus_sapressc_middleware_security_identity",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    roleid = table.Column<Guid>(nullable: false),
                    signature = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_userrolebridges", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "fk_userrolebridges_roles_roleid",
                        column: x => x.roleid,
                        principalSchema: "innovus_sapressc_core_security_common",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_userrolebridges_users_userid",
                        column: x => x.userid,
                        principalSchema: "innovus_sapressc_middleware_security_identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                schema: "innovus_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    discriminator = table.Column<string>(nullable: false),
                    countryid = table.Column<Guid>(nullable: true),
                    localityid = table.Column<Guid>(nullable: true),
                    municipalityid = table.Column<Guid>(nullable: true),
                    stateid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regions", x => x.id);
                    table.ForeignKey(
                        name: "fk_regions_countries_countryid",
                        column: x => x.countryid,
                        principalSchema: "innovus_sapressc_middleware_region",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_regions_localities_localityid",
                        column: x => x.localityid,
                        principalSchema: "innovus_sapressc_middleware_region",
                        principalTable: "localities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_regions_municipalities_municipalityid",
                        column: x => x.municipalityid,
                        principalSchema: "innovus_sapressc_middleware_region",
                        principalTable: "municipalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_regions_states_stateid",
                        column: x => x.stateid,
                        principalSchema: "innovus_sapressc_middleware_region",
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "evaluationcollaboratorbridges",
                columns: table => new
                {
                    evaluationid = table.Column<Guid>(nullable: false),
                    collaboratorid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_evaluationcollaboratorbridges", x => new { x.evaluationid, x.collaboratorid });
                    table.ForeignKey(
                        name: "fk_evaluationcollaboratorbridges_collaborators_collaboratorid",
                        column: x => x.collaboratorid,
                        principalTable: "collaborators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_evaluationcollaboratorbridges_evaluations_evaluationid",
                        column: x => x.evaluationid,
                        principalTable: "evaluations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ideacollaboratorbridges",
                columns: table => new
                {
                    collaboratorid = table.Column<Guid>(nullable: false),
                    ideaid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideacollaboratorbridges", x => new { x.collaboratorid, x.ideaid });
                    table.ForeignKey(
                        name: "fk_ideacollaboratorbridges_collaborators_collaboratorid",
                        column: x => x.collaboratorid,
                        principalTable: "collaborators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ideacollaboratorbridges_ideas_ideaid",
                        column: x => x.ideaid,
                        principalTable: "ideas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "postalcontactitems",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: false),
                    regionid = table.Column<Guid>(nullable: false),
                    zipcode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_postalcontactitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_postalcontactitems_regions_regionid",
                        column: x => x.regionid,
                        principalSchema: "innovus_sapressc_middleware_region",
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationpostalcontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationpostalcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationpostalcontactitembridges_postalcontactitems_con~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "postalcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationpostalcontactitembridges_organizationidentities~",
                        column: x => x.organizationid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personpostalcontactitembridges",
                schema: "innovus_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personpostalcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personpostalcontactitembridges_postalcontactitems_contactit~",
                        column: x => x.contactitemid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "postalcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personpostalcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "innovus_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "challangecollaboratorbridges",
                columns: table => new
                {
                    collaboratorid = table.Column<Guid>(nullable: false),
                    challangeid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challangecollaboratorbridges", x => new { x.collaboratorid, x.challangeid });
                    table.ForeignKey(
                        name: "fk_challangecollaboratorbridges_collaborators_collaboratorid",
                        column: x => x.collaboratorid,
                        principalTable: "collaborators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "challangecomments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    createdat = table.Column<DateTime>(nullable: false),
                    authorid = table.Column<string>(nullable: false),
                    challangeid = table.Column<Guid>(nullable: false),
                    message = table.Column<string>(nullable: false),
                    upstreamid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challangecomments", x => x.id);
                    table.ForeignKey(
                        name: "fk_challangecomments_users_authorid",
                        column: x => x.authorid,
                        principalSchema: "innovus_sapressc_middleware_security_identity",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_challangecomments_challangecomments_upstreamid",
                        column: x => x.upstreamid,
                        principalTable: "challangecomments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "challangedocumentbridges",
                columns: table => new
                {
                    challangeid = table.Column<Guid>(nullable: false),
                    documentid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challangedocumentbridges", x => new { x.challangeid, x.documentid });
                    table.ForeignKey(
                        name: "fk_challangedocumentbridges_documents_documentid",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "challangeworkflowiterationdocumentbridges",
                columns: table => new
                {
                    iterationid = table.Column<Guid>(nullable: false),
                    documentid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challangeworkflowiterationdocumentbridges", x => new { x.iterationid, x.documentid });
                    table.ForeignKey(
                        name: "fk_challangeworkflowiterationdocumentbridges_documents_documen~",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "challangeworkflowiterationsimagebridges",
                columns: table => new
                {
                    iterationid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challangeworkflowiterationsimagebridges", x => new { x.iterationid, x.imageid });
                    table.ForeignKey(
                        name: "fk_challangeworkflowiterationsimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "innovus_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "challanges",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    ispublic = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    createdat = table.Column<DateTime>(nullable: false),
                    workflowid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challanges", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "challangeworkflowsteps",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    nextid = table.Column<Guid>(nullable: false),
                    workflowid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challangeworkflowsteps", x => x.id);
                    table.ForeignKey(
                        name: "fk_challangeworkflowsteps_challangeworkflowsteps_nextid",
                        column: x => x.nextid,
                        principalTable: "challangeworkflowsteps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "challangeworkflowiterations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createdat = table.Column<DateTime>(nullable: false),
                    summary = table.Column<string>(nullable: false),
                    workflowstepid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challangeworkflowiterations", x => x.id);
                    table.ForeignKey(
                        name: "fk_challangeworkflowiterations_challangeworkflowsteps_workflow~",
                        column: x => x.workflowstepid,
                        principalTable: "challangeworkflowsteps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "challangeworkflows",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    startstepid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_challangeworkflows", x => x.id);
                    table.ForeignKey(
                        name: "fk_challangeworkflows_challangeworkflowsteps_startstepid",
                        column: x => x.startstepid,
                        principalTable: "challangeworkflowsteps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ideaworkflowiterations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createdat = table.Column<DateTime>(nullable: false),
                    summary = table.Column<string>(nullable: false),
                    workflowstepid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideaworkflowiterations", x => x.id);
                    table.ForeignKey(
                        name: "fk_ideaworkflowiterations_challangeworkflowsteps_workflowstepid",
                        column: x => x.workflowstepid,
                        principalTable: "challangeworkflowsteps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ideaworkflowiterationdocumentbridges",
                columns: table => new
                {
                    iterationid = table.Column<Guid>(nullable: false),
                    documentid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideaworkflowiterationdocumentbridges", x => new { x.iterationid, x.documentid });
                    table.ForeignKey(
                        name: "fk_ideaworkflowiterationdocumentbridges_documents_documentid",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ideaworkflowiterationdocumentbridges_ideaworkflowiterations~",
                        column: x => x.iterationid,
                        principalTable: "ideaworkflowiterations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ideaworkflowiterationimagebridges",
                columns: table => new
                {
                    iterationid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideaworkflowiterationimagebridges", x => new { x.iterationid, x.imageid });
                    table.ForeignKey(
                        name: "fk_ideaworkflowiterationimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "innovus_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ideaworkflowiterationimagebridges_ideaworkflowiterations_it~",
                        column: x => x.iterationid,
                        principalTable: "ideaworkflowiterations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ideaworkflowsteps",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    nextid = table.Column<Guid>(nullable: true),
                    workflowid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideaworkflowsteps", x => x.id);
                    table.ForeignKey(
                        name: "fk_ideaworkflowsteps_ideaworkflowsteps_nextid",
                        column: x => x.nextid,
                        principalTable: "ideaworkflowsteps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ideaworkflow",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    startstepid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ideaworkflow", x => x.id);
                    table.ForeignKey(
                        name: "fk_ideaworkflow_ideaworkflowsteps_startstepid",
                        column: x => x.startstepid,
                        principalTable: "ideaworkflowsteps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_challangecollaboratorbridges_challangeid",
                table: "challangecollaboratorbridges",
                column: "challangeid");

            migrationBuilder.CreateIndex(
                name: "ix_challangecomments_authorid",
                table: "challangecomments",
                column: "authorid");

            migrationBuilder.CreateIndex(
                name: "ix_challangecomments_challangeid",
                table: "challangecomments",
                column: "challangeid");

            migrationBuilder.CreateIndex(
                name: "ix_challangecomments_upstreamid",
                table: "challangecomments",
                column: "upstreamid");

            migrationBuilder.CreateIndex(
                name: "ix_challangedocumentbridges_documentid",
                table: "challangedocumentbridges",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_challanges_workflowid",
                table: "challanges",
                column: "workflowid");

            migrationBuilder.CreateIndex(
                name: "ix_challangeworkflowiterationdocumentbridges_documentid",
                table: "challangeworkflowiterationdocumentbridges",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_challangeworkflowiterations_workflowstepid",
                table: "challangeworkflowiterations",
                column: "workflowstepid");

            migrationBuilder.CreateIndex(
                name: "ix_challangeworkflowiterationsimagebridges_imageid",
                table: "challangeworkflowiterationsimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_challangeworkflows_startstepid",
                table: "challangeworkflows",
                column: "startstepid");

            migrationBuilder.CreateIndex(
                name: "ix_challangeworkflowsteps_nextid",
                table: "challangeworkflowsteps",
                column: "nextid");

            migrationBuilder.CreateIndex(
                name: "ix_challangeworkflowsteps_workflowid",
                table: "challangeworkflowsteps",
                column: "workflowid");

            migrationBuilder.CreateIndex(
                name: "ix_collaborators_userid",
                table: "collaborators",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_documents_thumbnailid",
                table: "documents",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_evaluationcollaboratorbridges_collaboratorid",
                table: "evaluationcollaboratorbridges",
                column: "collaboratorid");

            migrationBuilder.CreateIndex(
                name: "ix_evaluations_ideaid",
                table: "evaluations",
                column: "ideaid");

            migrationBuilder.CreateIndex(
                name: "ix_ideacollaboratorbridges_ideaid",
                table: "ideacollaboratorbridges",
                column: "ideaid");

            migrationBuilder.CreateIndex(
                name: "ix_ideacomments_authorid",
                table: "ideacomments",
                column: "authorid");

            migrationBuilder.CreateIndex(
                name: "ix_ideacomments_ideaid",
                table: "ideacomments",
                column: "ideaid");

            migrationBuilder.CreateIndex(
                name: "ix_ideacomments_upstreamid",
                table: "ideacomments",
                column: "upstreamid");

            migrationBuilder.CreateIndex(
                name: "ix_ideadocumentbridges_documentid",
                table: "ideadocumentbridges",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_ideaworkflow_startstepid",
                table: "ideaworkflow",
                column: "startstepid");

            migrationBuilder.CreateIndex(
                name: "ix_ideaworkflowiterationdocumentbridges_documentid",
                table: "ideaworkflowiterationdocumentbridges",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_ideaworkflowiterationimagebridges_imageid",
                table: "ideaworkflowiterationimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_ideaworkflowiterations_workflowstepid",
                table: "ideaworkflowiterations",
                column: "workflowstepid");

            migrationBuilder.CreateIndex(
                name: "ix_ideaworkflowsteps_nextid",
                table: "ideaworkflowsteps",
                column: "nextid");

            migrationBuilder.CreateIndex(
                name: "ix_ideaworkflowsteps_workflowid",
                table: "ideaworkflowsteps",
                column: "workflowid");

            migrationBuilder.CreateIndex(
                name: "ix_prototypedocumentbridges_documentid",
                table: "prototypedocumentbridges",
                column: "documentid");

            migrationBuilder.CreateIndex(
                name: "ix_prototypeimagebridges_imageid",
                table: "prototypeimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_prototypes_ideaid",
                table: "prototypes",
                column: "ideaid");

            migrationBuilder.CreateIndex(
                name: "ix_prototypes_levelid",
                table: "prototypes",
                column: "levelid");

            migrationBuilder.CreateIndex(
                name: "ix_prototypinglevels_numerator",
                table: "prototypinglevels",
                column: "numerator",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rolepermissionbridges_permissionid",
                schema: "innovus_sapressc_core_security_common",
                table: "rolepermissionbridges",
                column: "permissionid");

            migrationBuilder.CreateIndex(
                name: "ix_memberships_personid",
                schema: "innovus_sapressc_middleware_identity",
                table: "memberships",
                column: "personid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationemailcontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "organizationemailcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationidentities_imageid",
                schema: "innovus_sapressc_middleware_identity",
                table: "organizationidentities",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationidentities_ownerid",
                schema: "innovus_sapressc_middleware_identity",
                table: "organizationidentities",
                column: "ownerid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationphonecontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "organizationphonecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationpostalcontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "organizationpostalcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationsocialcontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "organizationsocialcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationwebsitecontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "organizationwebsitecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personemailcontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "personemailcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personidentities_imageid",
                schema: "innovus_sapressc_middleware_identity",
                table: "personidentities",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_personphonecontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "personphonecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personpostalcontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "personpostalcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personsocialcontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "personsocialcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personwebsitecontactitembridges_contactitemid",
                schema: "innovus_sapressc_middleware_identity",
                table: "personwebsitecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_postalcontactitems_regionid",
                schema: "innovus_sapressc_middleware_identity",
                table: "postalcontactitems",
                column: "regionid");

            migrationBuilder.CreateIndex(
                name: "ix_images_thumbnailid",
                schema: "innovus_sapressc_middleware_media",
                table: "images",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_videos_thumbnailid",
                schema: "innovus_sapressc_middleware_media",
                table: "videos",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_countries_isocode",
                schema: "innovus_sapressc_middleware_region",
                table: "countries",
                column: "isocode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_countries_name",
                schema: "innovus_sapressc_middleware_region",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_localities_municipalityid",
                schema: "innovus_sapressc_middleware_region",
                table: "localities",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "ix_localities_name_municipalityid",
                schema: "innovus_sapressc_middleware_region",
                table: "localities",
                columns: new[] { "name", "municipalityid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_municipalities_stateid",
                schema: "innovus_sapressc_middleware_region",
                table: "municipalities",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_municipalities_name_stateid",
                schema: "innovus_sapressc_middleware_region",
                table: "municipalities",
                columns: new[] { "name", "stateid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_regions_countryid",
                schema: "innovus_sapressc_middleware_region",
                table: "regions",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_regions_localityid",
                schema: "innovus_sapressc_middleware_region",
                table: "regions",
                column: "localityid");

            migrationBuilder.CreateIndex(
                name: "ix_regions_municipalityid",
                schema: "innovus_sapressc_middleware_region",
                table: "regions",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "ix_regions_stateid",
                schema: "innovus_sapressc_middleware_region",
                table: "regions",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_states_countryid",
                schema: "innovus_sapressc_middleware_region",
                table: "states",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_userrolebridges_roleid",
                schema: "innovus_sapressc_middleware_security_identity",
                table: "userrolebridges",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "ix_users_identityid",
                schema: "innovus_sapressc_middleware_security_identity",
                table: "users",
                column: "identityid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_challangecollaboratorbridges_challanges_challangeid",
                table: "challangecollaboratorbridges",
                column: "challangeid",
                principalTable: "challanges",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_challangecomments_challanges_challangeid",
                table: "challangecomments",
                column: "challangeid",
                principalTable: "challanges",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_challangedocumentbridges_challanges_challangeid",
                table: "challangedocumentbridges",
                column: "challangeid",
                principalTable: "challanges",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_challangeworkflowiterationdocumentbridges_challangeworkflow~",
                table: "challangeworkflowiterationdocumentbridges",
                column: "iterationid",
                principalTable: "challangeworkflowiterations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_challangeworkflowiterationsimagebridges_challangeworkflowit~",
                table: "challangeworkflowiterationsimagebridges",
                column: "iterationid",
                principalTable: "challangeworkflowiterations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_challanges_challangeworkflows_workflowid",
                table: "challanges",
                column: "workflowid",
                principalTable: "challangeworkflows",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_challangeworkflowsteps_challangeworkflows_workflowid",
                table: "challangeworkflowsteps",
                column: "workflowid",
                principalTable: "challangeworkflows",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ideaworkflowsteps_ideaworkflow_workflowid",
                table: "ideaworkflowsteps",
                column: "workflowid",
                principalTable: "ideaworkflow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_challangeworkflowsteps_challangeworkflows_workflowid",
                table: "challangeworkflowsteps");

            migrationBuilder.DropForeignKey(
                name: "fk_ideaworkflow_ideaworkflowsteps_startstepid",
                table: "ideaworkflow");

            migrationBuilder.DropTable(
                name: "apikeys");

            migrationBuilder.DropTable(
                name: "challangecollaboratorbridges");

            migrationBuilder.DropTable(
                name: "challangecomments");

            migrationBuilder.DropTable(
                name: "challangedocumentbridges");

            migrationBuilder.DropTable(
                name: "challangeworkflowiterationdocumentbridges");

            migrationBuilder.DropTable(
                name: "challangeworkflowiterationsimagebridges");

            migrationBuilder.DropTable(
                name: "evaluationcollaboratorbridges");

            migrationBuilder.DropTable(
                name: "ideacollaboratorbridges");

            migrationBuilder.DropTable(
                name: "ideacomments");

            migrationBuilder.DropTable(
                name: "ideadocumentbridges");

            migrationBuilder.DropTable(
                name: "ideaworkflowiterationdocumentbridges");

            migrationBuilder.DropTable(
                name: "ideaworkflowiterationimagebridges");

            migrationBuilder.DropTable(
                name: "prototypedocumentbridges");

            migrationBuilder.DropTable(
                name: "prototypeimagebridges");

            migrationBuilder.DropTable(
                name: "traces");

            migrationBuilder.DropTable(
                name: "rolepermissionbridges",
                schema: "innovus_sapressc_core_security_common");

            migrationBuilder.DropTable(
                name: "memberships",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationemailcontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationphonecontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationpostalcontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationsocialcontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationwebsitecontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personemailcontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personphonecontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personpostalcontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personsocialcontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personwebsitecontactitembridges",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "videos",
                schema: "innovus_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "userrolebridges",
                schema: "innovus_sapressc_middleware_security_identity");

            migrationBuilder.DropTable(
                name: "challanges");

            migrationBuilder.DropTable(
                name: "challangeworkflowiterations");

            migrationBuilder.DropTable(
                name: "evaluations");

            migrationBuilder.DropTable(
                name: "collaborators");

            migrationBuilder.DropTable(
                name: "ideaworkflowiterations");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "prototypes");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "innovus_sapressc_core_security_common");

            migrationBuilder.DropTable(
                name: "organizationidentities",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "emailcontactitems",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "phonecontactitems",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "postalcontactitems",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "socialcontactitems",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "websitecontactitems",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "innovus_sapressc_core_security_common");

            migrationBuilder.DropTable(
                name: "users",
                schema: "innovus_sapressc_middleware_security_identity");

            migrationBuilder.DropTable(
                name: "ideas");

            migrationBuilder.DropTable(
                name: "prototypinglevels");

            migrationBuilder.DropTable(
                name: "regions",
                schema: "innovus_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "personidentities",
                schema: "innovus_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "localities",
                schema: "innovus_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "images",
                schema: "innovus_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "municipalities",
                schema: "innovus_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "thumbnails",
                schema: "innovus_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "states",
                schema: "innovus_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "innovus_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "challangeworkflows");

            migrationBuilder.DropTable(
                name: "challangeworkflowsteps");

            migrationBuilder.DropTable(
                name: "ideaworkflowsteps");

            migrationBuilder.DropTable(
                name: "ideaworkflow");
        }
    }
}
