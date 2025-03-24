using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeD.Infrastructure.Migrations;

public static class MigrationExtensions
{
    public static MigrationBuilder CreateClusteredIndex(this MigrationBuilder migrationBuilder, string tableName, string? schemaName = null)
    {
        if (string.IsNullOrWhiteSpace(schemaName))
        {
            migrationBuilder.Sql($"CLUSTER \"{tableName}\" USING \"UQ_{tableName}_RowId\";");
        }
        else
        {
            migrationBuilder.Sql($"CLUSTER \"{schemaName}\".\"{tableName}\" USING \"UQ_{tableName}_RowId\";");
        }
        return migrationBuilder;
    }
}