using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CodeD");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "CodeD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: true),
                    Key = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    RowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceProviderKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SourceItemId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SourceVersion = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "CodeD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Content = table.Column<string>(type: "text", maxLength: 2147483647, nullable: false),
                    PostStatus = table.Column<int>(type: "integer", nullable: false),
                    PublishedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: true),
                    Key = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    RowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceProviderKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SourceItemId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SourceVersion = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "CodeD",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Key",
                schema: "CodeD",
                table: "Category",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Category_RowId",
                schema: "CodeD",
                table: "Category",
                column: "RowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_CategoryId",
                schema: "CodeD",
                table: "Post",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Key",
                schema: "CodeD",
                table: "Post",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Post_RowId",
                schema: "CodeD",
                table: "Post",
                column: "RowId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post",
                schema: "CodeD");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "CodeD");
        }
    }
}
