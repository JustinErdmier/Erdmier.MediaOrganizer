#nullable disable

    using Microsoft.EntityFrameworkCore.Migrations;

    namespace Erdmier.MediaOrganizer.Persistence.Migrations.Application;

    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "ApplicationRoles",
                                         table => new
                                         {
                                             Id               = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                                             Name             = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                                             NormalizedName   = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                                             ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                                         },
                                         constraints: table => { table.PrimaryKey(name: "PK_ApplicationRoles", x => x.Id); });

            migrationBuilder.CreateTable(name: "ApplicationUsers",
                                         table => new
                                         {
                                             Id                   = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                                             UserName             = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                                             NormalizedUserName   = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                                             Email                = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                                             NormalizedEmail      = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                                             EmailConfirmed       = table.Column<bool>(type: "bit", nullable: false),
                                             PasswordHash         = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                             SecurityStamp        = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                             ConcurrencyStamp     = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                             PhoneNumber          = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                             PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                                             TwoFactorEnabled     = table.Column<bool>(type: "bit", nullable: false),
                                             LockoutEnd           = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                                             LockoutEnabled       = table.Column<bool>(type: "bit", nullable: false),
                                             AccessFailedCount    = table.Column<int>(type: "int", nullable: false)
                                         },
                                         constraints: table => { table.PrimaryKey(name: "PK_ApplicationUsers", x => x.Id); });

            migrationBuilder.CreateTable(name: "ApplicationRoleClaims",
                                         table => new
                                         {
                                             Id = table.Column<int>(type: "int", nullable: false)
                                                       .Annotation(name: "SqlServer:Identity", value: "1, 1"),
                                             RoleId     = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                                             ClaimType  = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                             ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                                         },
                                         constraints: table =>
                                         {
                                             table.PrimaryKey(name: "PK_ApplicationRoleClaims", x => x.Id);

                                             table.ForeignKey(name: "FK_ApplicationRoleClaims_ApplicationRoles_RoleId",
                                                              x => x.RoleId,
                                                              principalTable: "ApplicationRoles",
                                                              principalColumn: "Id",
                                                              onDelete: ReferentialAction.Cascade);
                                         });

            migrationBuilder.CreateTable(name: "ApplicationUserClaims",
                                         table => new
                                         {
                                             Id = table.Column<int>(type: "int", nullable: false)
                                                       .Annotation(name: "SqlServer:Identity", value: "1, 1"),
                                             UserId     = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                                             ClaimType  = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                             ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                                         },
                                         constraints: table =>
                                         {
                                             table.PrimaryKey(name: "PK_ApplicationUserClaims", x => x.Id);

                                             table.ForeignKey(name: "FK_ApplicationUserClaims_ApplicationUsers_UserId",
                                                              x => x.UserId,
                                                              principalTable: "ApplicationUsers",
                                                              principalColumn: "Id",
                                                              onDelete: ReferentialAction.Cascade);
                                         });

            migrationBuilder.CreateTable(name: "ApplicationUserLogins",
                                         table => new
                                         {
                                             LoginProvider       = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                                             ProviderKey         = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                                             ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                                             UserId              = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                                         },
                                         constraints: table =>
                                         {
                                             table.PrimaryKey(name: "PK_ApplicationUserLogins", x => new { x.LoginProvider, x.ProviderKey });

                                             table.ForeignKey(name: "FK_ApplicationUserLogins_ApplicationUsers_UserId",
                                                              x => x.UserId,
                                                              principalTable: "ApplicationUsers",
                                                              principalColumn: "Id",
                                                              onDelete: ReferentialAction.Cascade);
                                         });

            migrationBuilder.CreateTable(name: "ApplicationUserRoles",
                                         table => new
                                         {
                                             UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                                             RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                                         },
                                         constraints: table =>
                                         {
                                             table.PrimaryKey(name: "PK_ApplicationUserRoles", x => new { x.UserId, x.RoleId });

                                             table.ForeignKey(name: "FK_ApplicationUserRoles_ApplicationRoles_RoleId",
                                                              x => x.RoleId,
                                                              principalTable: "ApplicationRoles",
                                                              principalColumn: "Id",
                                                              onDelete: ReferentialAction.Cascade);

                                             table.ForeignKey(name: "FK_ApplicationUserRoles_ApplicationUsers_UserId",
                                                              x => x.UserId,
                                                              principalTable: "ApplicationUsers",
                                                              principalColumn: "Id",
                                                              onDelete: ReferentialAction.Cascade);
                                         });

            migrationBuilder.CreateTable(name: "ApplicationUserTokens",
                                         table => new
                                         {
                                             UserId        = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                                             LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                                             Name          = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                                             Value         = table.Column<string>(type: "nvarchar(max)", nullable: true)
                                         },
                                         constraints: table =>
                                         {
                                             table.PrimaryKey(name: "PK_ApplicationUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });

                                             table.ForeignKey(name: "FK_ApplicationUserTokens_ApplicationUsers_UserId",
                                                              x => x.UserId,
                                                              principalTable: "ApplicationUsers",
                                                              principalColumn: "Id",
                                                              onDelete: ReferentialAction.Cascade);
                                         });

            migrationBuilder.CreateIndex(name: "IX_ApplicationRoleClaims_RoleId", table: "ApplicationRoleClaims", column: "RoleId");

            migrationBuilder.CreateIndex(name: "IX_ApplicationRoles_Id", table: "ApplicationRoles", column: "Id");

            migrationBuilder.CreateIndex(name: "RoleNameIndex",
                                         table: "ApplicationRoles",
                                         column: "NormalizedName",
                                         unique: true,
                                         filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(name: "IX_ApplicationUserClaims_UserId", table: "ApplicationUserClaims", column: "UserId");

            migrationBuilder.CreateIndex(name: "IX_ApplicationUserLogins_UserId", table: "ApplicationUserLogins", column: "UserId");

            migrationBuilder.CreateIndex(name: "IX_ApplicationUserRoles_RoleId", table: "ApplicationUserRoles", column: "RoleId");

            migrationBuilder.CreateIndex(name: "EmailIndex",
                                         table: "ApplicationUsers",
                                         column: "NormalizedEmail",
                                         unique: true,
                                         filter: "[NormalizedEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(name: "UserNameIndex",
                                         table: "ApplicationUsers",
                                         column: "NormalizedUserName",
                                         unique: true,
                                         filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ApplicationRoleClaims");

            migrationBuilder.DropTable(name: "ApplicationUserClaims");

            migrationBuilder.DropTable(name: "ApplicationUserLogins");

            migrationBuilder.DropTable(name: "ApplicationUserRoles");

            migrationBuilder.DropTable(name: "ApplicationUserTokens");

            migrationBuilder.DropTable(name: "ApplicationRoles");

            migrationBuilder.DropTable(name: "ApplicationUsers");
        }
    }
