using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayerApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Title = table.Column<string>(maxLength: 250, nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: true),
                    FirstName = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "EnumItemTypes",
                columns: table => new
                {
                    EnumItemTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NormalizedName = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 250, nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: true),
                    Order = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumItemTypes", x => x.EnumItemTypeId);
                    table.ForeignKey(
                        name: "FK_EnumItemTypes_EnumItemTypes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "EnumItemTypes",
                        principalColumn: "EnumItemTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnumItems",
                columns: table => new
                {
                    EnumItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NormalizedName = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 250, nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: true),
                    Order = table.Column<int>(nullable: false),
                    Settings = table.Column<string>(nullable: true),
                    EnumItemTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumItems", x => x.EnumItemId);
                    table.ForeignKey(
                        name: "FK_EnumItems_EnumItemTypes_EnumItemTypeId",
                        column: x => x.EnumItemTypeId,
                        principalTable: "EnumItemTypes",
                        principalColumn: "EnumItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    UserId1 = table.Column<int>(nullable: true),
                    EnumItemId_AccountType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_EnumItems_EnumItemId_AccountType",
                        column: x => x.EnumItemId_AccountType,
                        principalTable: "EnumItems",
                        principalColumn: "EnumItemId");
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "RoleId", "ConcurrencyStamp", "Name", "NormalizedName", "Order", "Title" },
                values: new object[] { 1, "c4c084bb-cab3-40fe-a19e-9e12820ba302", "Všichni uživatelé", "AllUsers", 100, "Skupina všech uživatelů" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "RoleId", "ConcurrencyStamp", "IsEnabled", "Name", "NormalizedName", "Order", "Title" },
                values: new object[,]
                {
                    { 2, "8fe1ee6a-162f-429c-8fc2-932f8fcfbee1", true, "Administrator", "Admin", 200, "Administrátor- správa aplikace" },
                    { 3, "5b32df3e-bd8e-44ce-92a4-d9d0a8e4184f", true, "Prodejce", "Seller", 300, "Prodejce zboží" },
                    { 4, "866269ed-3cc5-408c-9011-3052fe69f59f", true, "Zákazník", "Customer", 400, "Zákazník" },
                    { 5, "6a194d45-c591-430e-aae1-e0349bc826b8", true, "OvěřenýZákazník", "VerifiedCustomer", 500, "Ověřený zákazník" },
                    { 6, "a95c2572-b097-4e34-8b72-317298f89f54", true, "Host", "Guest", 600, "Host aplikace s minimálními oprávněními" },
                    { 7, "edb0b8ed-0ec8-4dd4-948f-4002e6c84d14", true, "Dodavatel", "Supplier", 700, "Dodavatel zboží" }
                });

            migrationBuilder.InsertData(
                table: "EnumItemTypes",
                columns: new[] { "EnumItemTypeId", "IsEnabled", "Name", "NormalizedName", "Order", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, true, "Typ účtu", "AccountType", 100, null, "Typ účtu k přihlášení uživatele" },
                    { 2, true, "Jednotky", "Unit", 200, null, null },
                    { 3, true, "Datový typ", "DataType", 300, null, "Datový typ" },
                    { 5, true, "Měna", "Currency", 500, null, null }
                });

            migrationBuilder.InsertData(
                table: "EnumItemTypes",
                columns: new[] { "EnumItemTypeId", "IsEnabled", "Name", "NormalizedName", "Order", "ParentId", "Title" },
                values: new object[] { 4, true, "Státy", "Country", 400, 1, "Státy" });

            migrationBuilder.InsertData(
                table: "EnumItems",
                columns: new[] { "EnumItemId", "EnumItemTypeId", "IsEnabled", "Name", "NormalizedName", "Order", "Settings", "Title" },
                values: new object[,]
                {
                    { 1, 1, true, "Interní účet", "Internal", 100, null, "Účet registrovaný v systému" },
                    { 2, 1, true, "Gmail účet", "Google", 200, null, "Účet Google" },
                    { 3, 1, true, "Facebook účet", "Facebook", 300, null, "Účet na facebook" },
                    { 4, 2, true, "mililitr", "ml", 100, null, null },
                    { 5, 2, true, "litr", "l", 200, null, null },
                    { 6, 2, true, "gram", "g", 300, null, null },
                    { 7, 2, true, "kilogram", "kg", 400, null, null },
                    { 8, 2, true, "g/l", "GToL", 500, null, "gram na litr" },
                    { 9, 2, true, "%", "Percent", 600, null, "Procenta" },
                    { 10, 3, true, "String", "String", 100, null, "Řetězec" },
                    { 11, 3, true, "Integer", "Int", 200, null, "Celočíselný typ" },
                    { 12, 3, true, "Double", "Double", 300, null, "Číslo s desetinnou čárkou" },
                    { 13, 3, true, "Boolean", "Bool", 400, null, "Boolean" },
                    { 14, 3, true, "DateTime", "DateTime", 500, null, "Datum a čas" },
                    { 21, 5, true, "Kč", "CZK", 700, null, "Koruna česká" }
                });

            migrationBuilder.InsertData(
                table: "EnumItems",
                columns: new[] { "EnumItemId", "EnumItemTypeId", "IsEnabled", "Name", "NormalizedName", "Order", "Settings", "Title" },
                values: new object[,]
                {
                    { 15, 4, true, "ČR", "CZ", 100, null, "Česká republika" },
                    { 16, 4, true, "Slovensko", "SVK", 200, null, "Slovenská republika" },
                    { 17, 4, true, "Rakousko", "AU", 300, null, "Rakousko" },
                    { 18, 4, true, "Německo", "DE", 400, null, "Německo" },
                    { 19, 4, true, "Francie", "FR", 500, null, "Francie" },
                    { 20, 4, true, "Chile", "CHI", 600, null, "Chile" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_IsEnabled_NormalizedName_Unique",
                table: "AspNetRoles",
                columns: new[] { "IsEnabled", "NormalizedName" })
                .Annotation("SqlServer:Include", new[] { "Name", "Title" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_EnumItemId_AccountType",
                table: "AspNetUserLogins",
                column: "EnumItemId_AccountType");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId1",
                table: "AspNetUserLogins",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Login_UserId_UserName_EnumItemId_AccountType_Unique",
                table: "AspNetUserLogins",
                columns: new[] { "UserId", "UserName", "EnumItemId_AccountType" },
                unique: true,
                filter: "[UserName] IS NOT NULL AND [EnumItemId_AccountType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName_Unique",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnumItems_EnumItemTypeId",
                table: "EnumItems",
                column: "EnumItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnumItem_IsEnabled_EnumItemTypeId_NormalizedName_Unique",
                table: "EnumItems",
                columns: new[] { "IsEnabled", "EnumItemTypeId", "NormalizedName" },
                unique: true)
                .Annotation("SqlServer:Include", new[] { "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_EnumItemType_NormalizedName_Unique",
                table: "EnumItemTypes",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnumItemTypes_ParentId",
                table: "EnumItemTypes",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EnumItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EnumItemTypes");
        }
    }
}
