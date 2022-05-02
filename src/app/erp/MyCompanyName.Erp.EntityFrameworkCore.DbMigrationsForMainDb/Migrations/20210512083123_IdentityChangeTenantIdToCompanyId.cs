using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.Erp.DbMigrationsForMainDb.Migrations
{
    public partial class IdentityChangeTenantIdToCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Identity_Users");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Identity_UserRoles");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Identity_UserOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Identity_UserClaims");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Identity_RoleClaims");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Identity_UserTokens",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Identity_UserLogins",
                newName: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Identity_UserTokens",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Identity_UserLogins",
                newName: "TenantId");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Identity_Users",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Identity_UserRoles",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Identity_UserOrganizationUnits",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Identity_UserClaims",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Identity_RoleClaims",
                type: "char(36)",
                nullable: true);
        }
    }
}
