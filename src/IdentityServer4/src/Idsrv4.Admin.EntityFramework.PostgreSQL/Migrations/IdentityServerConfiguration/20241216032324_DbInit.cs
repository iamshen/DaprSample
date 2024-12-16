using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Idsrv4.Admin.EntityFramework.PostgreSQL.Migrations.IdentityServerConfiguration
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "ids_api_resource",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    allowed_access_token_signing_algorithms = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    show_in_discovery_document = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_accessed = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    non_editable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_api_resource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ids_api_scope",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    required = table.Column<bool>(type: "boolean", nullable: false),
                    emphasize = table.Column<bool>(type: "boolean", nullable: false),
                    show_in_discovery_document = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_api_scope", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ids_client",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    client_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    protocol_type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    require_client_secret = table.Column<bool>(type: "boolean", nullable: false),
                    client_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    client_uri = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    logo_uri = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    require_consent = table.Column<bool>(type: "boolean", nullable: false),
                    allow_remember_consent = table.Column<bool>(type: "boolean", nullable: false),
                    always_include_user_claims_in_id_token = table.Column<bool>(type: "boolean", nullable: false),
                    require_pkce = table.Column<bool>(type: "boolean", nullable: false),
                    allow_plain_text_pkce = table.Column<bool>(type: "boolean", nullable: false),
                    require_request_object = table.Column<bool>(type: "boolean", nullable: false),
                    allow_access_tokens_via_browser = table.Column<bool>(type: "boolean", nullable: false),
                    front_channel_logout_uri = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    front_channel_logout_session_required = table.Column<bool>(type: "boolean", nullable: false),
                    back_channel_logout_uri = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    back_channel_logout_session_required = table.Column<bool>(type: "boolean", nullable: false),
                    allow_offline_access = table.Column<bool>(type: "boolean", nullable: false),
                    identity_token_lifetime = table.Column<int>(type: "integer", nullable: false),
                    allowed_identity_token_signing_algorithms = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    access_token_lifetime = table.Column<int>(type: "integer", nullable: false),
                    authorization_code_lifetime = table.Column<int>(type: "integer", nullable: false),
                    consent_lifetime = table.Column<int>(type: "integer", nullable: true),
                    absolute_refresh_token_lifetime = table.Column<int>(type: "integer", nullable: false),
                    sliding_refresh_token_lifetime = table.Column<int>(type: "integer", nullable: false),
                    refresh_token_usage = table.Column<int>(type: "integer", nullable: false),
                    update_access_token_claims_on_refresh = table.Column<bool>(type: "boolean", nullable: false),
                    refresh_token_expiration = table.Column<int>(type: "integer", nullable: false),
                    access_token_type = table.Column<int>(type: "integer", nullable: false),
                    enable_local_login = table.Column<bool>(type: "boolean", nullable: false),
                    include_jwt_id = table.Column<bool>(type: "boolean", nullable: false),
                    always_send_client_claims = table.Column<bool>(type: "boolean", nullable: false),
                    client_claims_prefix = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    pair_wise_subject_salt = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_accessed = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    user_sso_lifetime = table.Column<int>(type: "integer", nullable: true),
                    user_code_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    device_code_lifetime = table.Column<int>(type: "integer", nullable: false),
                    non_editable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ids_identity_resource",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    required = table.Column<bool>(type: "boolean", nullable: false),
                    emphasize = table.Column<bool>(type: "boolean", nullable: false),
                    show_in_discovery_document = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    non_editable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_identity_resource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ids_api_resource_claim",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    api_resource_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_api_resource_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_api_resource_claim_ids_api_resource_api_resource_id",
                        column: x => x.api_resource_id,
                        principalSchema: "public",
                        principalTable: "ids_api_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_api_resource_property",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    api_resource_id = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_api_resource_property", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_api_resource_property_ids_api_resource_api_resource_id",
                        column: x => x.api_resource_id,
                        principalSchema: "public",
                        principalTable: "ids_api_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_api_resource_scope",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scope = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    api_resource_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_api_resource_scope", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_api_resource_scope_ids_api_resource_api_resource_id",
                        column: x => x.api_resource_id,
                        principalSchema: "public",
                        principalTable: "ids_api_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_api_secret",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    api_resource_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    value = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    type = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_api_secret", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_api_secret_ids_api_resource_api_resource_id",
                        column: x => x.api_resource_id,
                        principalSchema: "public",
                        principalTable: "ids_api_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_api_scope_claim",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scope_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_api_scope_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_api_scope_claim_ids_api_scope_scope_id",
                        column: x => x.scope_id,
                        principalSchema: "public",
                        principalTable: "ids_api_scope",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_api_scope_property",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scope_id = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_api_scope_property", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_api_scope_property_ids_api_scope_scope_id",
                        column: x => x.scope_id,
                        principalSchema: "public",
                        principalTable: "ids_api_scope",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_claim",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_claim_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_cors_origin",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    origin = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_cors_origin", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_cors_origin_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_grant_type",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    grant_type = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_grant_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_grant_type_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_id_p_restriction",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    provider = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_id_p_restriction", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_id_p_restriction_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_post_logout_redirect_uri",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    post_logout_redirect_uri = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_post_logout_redirect_uri", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_post_logout_redirect_uri_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_property",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_property", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_property_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_redirect_uri",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    redirect_uri = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_redirect_uri", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_redirect_uri_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_scope",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scope = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_scope", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_scope_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_client_secret",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    value = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    type = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_client_secret", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_client_secret_ids_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "ids_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_identity_claim",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    identity_resource_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_identity_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_identity_claim_ids_identity_resource_identity_resource_",
                        column: x => x.identity_resource_id,
                        principalSchema: "public",
                        principalTable: "ids_identity_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ids_identity_resource_property",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    identity_resource_id = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ids_identity_resource_property", x => x.id);
                    table.ForeignKey(
                        name: "fk_ids_identity_resource_property_ids_identity_resource_identi",
                        column: x => x.identity_resource_id,
                        principalSchema: "public",
                        principalTable: "ids_identity_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ids_api_resource_name",
                schema: "public",
                table: "ids_api_resource",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ids_api_resource_claim_api_resource_id",
                schema: "public",
                table: "ids_api_resource_claim",
                column: "api_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_api_resource_property_api_resource_id",
                schema: "public",
                table: "ids_api_resource_property",
                column: "api_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_api_resource_scope_api_resource_id",
                schema: "public",
                table: "ids_api_resource_scope",
                column: "api_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_api_scope_name",
                schema: "public",
                table: "ids_api_scope",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ids_api_scope_claim_scope_id",
                schema: "public",
                table: "ids_api_scope_claim",
                column: "scope_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_api_scope_property_scope_id",
                schema: "public",
                table: "ids_api_scope_property",
                column: "scope_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_api_secret_api_resource_id",
                schema: "public",
                table: "ids_api_secret",
                column: "api_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_client_id",
                schema: "public",
                table: "ids_client",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_claim_client_id",
                schema: "public",
                table: "ids_client_claim",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_cors_origin_client_id",
                schema: "public",
                table: "ids_client_cors_origin",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_grant_type_client_id",
                schema: "public",
                table: "ids_client_grant_type",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_id_p_restriction_client_id",
                schema: "public",
                table: "ids_client_id_p_restriction",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_post_logout_redirect_uri_client_id",
                schema: "public",
                table: "ids_client_post_logout_redirect_uri",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_property_client_id",
                schema: "public",
                table: "ids_client_property",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_redirect_uri_client_id",
                schema: "public",
                table: "ids_client_redirect_uri",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_scope_client_id",
                schema: "public",
                table: "ids_client_scope",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_client_secret_client_id",
                schema: "public",
                table: "ids_client_secret",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_identity_claim_identity_resource_id",
                schema: "public",
                table: "ids_identity_claim",
                column: "identity_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_ids_identity_resource_name",
                schema: "public",
                table: "ids_identity_resource",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ids_identity_resource_property_identity_resource_id",
                schema: "public",
                table: "ids_identity_resource_property",
                column: "identity_resource_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ids_api_resource_claim",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_api_resource_property",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_api_resource_scope",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_api_scope_claim",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_api_scope_property",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_api_secret",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_claim",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_cors_origin",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_grant_type",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_id_p_restriction",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_post_logout_redirect_uri",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_property",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_redirect_uri",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_scope",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client_secret",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_identity_claim",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_identity_resource_property",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_api_scope",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_api_resource",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_client",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ids_identity_resource",
                schema: "public");
        }
    }
}
