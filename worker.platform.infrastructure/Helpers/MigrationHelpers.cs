using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

namespace worker.platform.infrastructure.Helpers;

public static class MigrationHelpers
{
    private const string SqlScriptsFolder = "SqlFiles";
    public static void ExecuteSqlFile(MigrationBuilder migrationBuilder, string fileName)
    {
        // Get the current assembly
        var assembly = Assembly.GetExecutingAssembly();

        // Construct the resource name
        var resourceName = $"{assembly.GetName().Name}.{SqlScriptsFolder}.{fileName}";

        // Read the SQL file as an embedded resource
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
        }

        using var reader = new StreamReader(stream);
        var sql = reader.ReadToEnd();
        migrationBuilder.Sql(sql); // Execute the SQL script
    }
}
