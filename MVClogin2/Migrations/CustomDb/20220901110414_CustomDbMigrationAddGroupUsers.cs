using Microsoft.EntityFrameworkCore.Migrations;

namespace MVClogin2.Migrations.CustomDb
{
    public partial class CustomDbMigrationAddGroupUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "groupUsers",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    GroupId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupUsers", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "groupUsers");
        }
    }
}
