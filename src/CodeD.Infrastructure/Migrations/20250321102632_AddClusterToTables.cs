using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClusterToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateClusteredIndex("Category", "CodeD");
            migrationBuilder.CreateClusteredIndex("Post", "CodeD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
