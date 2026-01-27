using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowEngine.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
