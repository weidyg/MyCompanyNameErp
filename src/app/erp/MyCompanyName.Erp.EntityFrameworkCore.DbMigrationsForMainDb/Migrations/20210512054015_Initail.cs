using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.Erp.DbMigrationsForMainDb.Migrations
{
    public partial class Initail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identity_Companys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity_LinkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    SourceUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SourceTenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TargetUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TargetTenantId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_LinkUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity_OrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Code = table.Column<string>(type: "varchar(95) CHARACTER SET utf8mb4", maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(type: "varchar(128) CHARACTER SET utf8mb4", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_OrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_OrganizationUnits_Identity_OrganizationUnits_Parent~",
                        column: x => x.ParentId,
                        principalTable: "Identity_OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identity_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Name = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsStatic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPublic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity_SecurityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ApplicationName = table.Column<string>(type: "varchar(96) CHARACTER SET utf8mb4", maxLength: 96, nullable: true),
                    Identity = table.Column<string>(type: "varchar(96) CHARACTER SET utf8mb4", maxLength: 96, nullable: true),
                    Action = table.Column<string>(type: "varchar(96) CHARACTER SET utf8mb4", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    TenantName = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: true),
                    ClientId = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: true),
                    ClientIpAddress = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "varchar(512) CHARACTER SET utf8mb4", maxLength: 512, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_SecurityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: true),
                    Surname = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: false),
                    IsExternal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<string>(type: "varchar(16) CHARACTER SET utf8mb4", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ExtraProperties = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant_Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: false),
                    ExtraProperties = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity_CompanyLinkTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ClientType = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_CompanyLinkTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_CompanyLinkTenants_Identity_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Identity_Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_OrganizationUnitRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_OrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Identity_OrganizationUnitRoles_Identity_OrganizationUnits_Or~",
                        column: x => x.OrganizationUnitId,
                        principalTable: "Identity_OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identity_OrganizationUnitRoles_Identity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ClaimType = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "varchar(1024) CHARACTER SET utf8mb4", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_RoleClaims_Identity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ClaimType = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "varchar(1024) CHARACTER SET utf8mb4", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_UserClaims_Identity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_UserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ProviderKey = table.Column<string>(type: "varchar(196) CHARACTER SET utf8mb4", maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "varchar(128) CHARACTER SET utf8mb4", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_UserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_Identity_UserLogins_Identity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_UserOrganizationUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_UserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Identity_UserOrganizationUnits_Identity_OrganizationUnits_Or~",
                        column: x => x.OrganizationUnitId,
                        principalTable: "Identity_OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identity_UserOrganizationUnits_Identity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Identity_UserRoles_Identity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identity_UserRoles_Identity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "varchar(128) CHARACTER SET utf8mb4", maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Value = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Identity_UserTokens_Identity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenant_ConnectionStrings",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(64) CHARACTER SET utf8mb4", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "varchar(1024) CHARACTER SET utf8mb4", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant_ConnectionStrings", x => new { x.TenantId, x.Name });
                    table.ForeignKey(
                        name: "FK_Tenant_ConnectionStrings_Tenant_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant_Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_CompanyLinkTenants_CompanyId_TenantId_ClientType",
                table: "Identity_CompanyLinkTenants",
                columns: new[] { "CompanyId", "TenantId", "ClientType" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Companys_Name",
                table: "Identity_Companys",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_LinkUsers_SourceUserId_SourceTenantId_TargetUserId_~",
                table: "Identity_LinkUsers",
                columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identity_OrganizationUnitRoles_RoleId_OrganizationUnitId",
                table: "Identity_OrganizationUnitRoles",
                columns: new[] { "RoleId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_OrganizationUnits_Code",
                table: "Identity_OrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_OrganizationUnits_ParentId",
                table: "Identity_OrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_RoleClaims_RoleId",
                table: "Identity_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Roles_NormalizedName",
                table: "Identity_Roles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_SecurityLogs_TenantId_Action",
                table: "Identity_SecurityLogs",
                columns: new[] { "TenantId", "Action" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_SecurityLogs_TenantId_ApplicationName",
                table: "Identity_SecurityLogs",
                columns: new[] { "TenantId", "ApplicationName" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_SecurityLogs_TenantId_Identity",
                table: "Identity_SecurityLogs",
                columns: new[] { "TenantId", "Identity" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_SecurityLogs_TenantId_UserId",
                table: "Identity_SecurityLogs",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_UserClaims_UserId",
                table: "Identity_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_UserLogins_LoginProvider_ProviderKey",
                table: "Identity_UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_UserOrganizationUnits_UserId_OrganizationUnitId",
                table: "Identity_UserOrganizationUnits",
                columns: new[] { "UserId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_UserRoles_RoleId_UserId",
                table: "Identity_UserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Users_Email",
                table: "Identity_Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Users_NormalizedEmail",
                table: "Identity_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Users_NormalizedUserName",
                table: "Identity_Users",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Users_UserName",
                table: "Identity_Users",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Tenants_Name",
                table: "Tenant_Tenants",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identity_CompanyLinkTenants");

            migrationBuilder.DropTable(
                name: "Identity_LinkUsers");

            migrationBuilder.DropTable(
                name: "Identity_OrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "Identity_RoleClaims");

            migrationBuilder.DropTable(
                name: "Identity_SecurityLogs");

            migrationBuilder.DropTable(
                name: "Identity_UserClaims");

            migrationBuilder.DropTable(
                name: "Identity_UserLogins");

            migrationBuilder.DropTable(
                name: "Identity_UserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "Identity_UserRoles");

            migrationBuilder.DropTable(
                name: "Identity_UserTokens");

            migrationBuilder.DropTable(
                name: "Tenant_ConnectionStrings");

            migrationBuilder.DropTable(
                name: "Identity_Companys");

            migrationBuilder.DropTable(
                name: "Identity_OrganizationUnits");

            migrationBuilder.DropTable(
                name: "Identity_Roles");

            migrationBuilder.DropTable(
                name: "Identity_Users");

            migrationBuilder.DropTable(
                name: "Tenant_Tenants");
        }
    }
}
