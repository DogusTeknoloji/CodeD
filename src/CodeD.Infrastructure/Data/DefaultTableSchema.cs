using Microsoft.Extensions.Configuration;

namespace CodeD.Infrastructure.Data
{
    public class DefaultTableSchema(IConfiguration _configuration)
        : IDefaultTableSchema
    {
        public string SchemaName
            => _configuration.GetConnectionString("DefaultSchema")
            ?? throw new DefaultSchemaNotFoundException("DefaultSchema is not found.");
    }
}