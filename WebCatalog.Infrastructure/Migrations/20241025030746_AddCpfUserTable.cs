using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "AspNetUsers");
        }
    }
}
