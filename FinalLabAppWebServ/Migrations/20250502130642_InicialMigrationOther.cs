using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalLabAppWebServ.Migrations
{
    /// <inheritdoc />
    public partial class InicialMigrationOther : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    RegisterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterPasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.RegisterId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Register");
        }
    }
}
