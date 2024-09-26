// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace InteliblueVueJsHrManagement.Server;
/// <summary>
/// Necessary for dotnet ef tools to create instances of HumanResourceDbContext 
/// at design-time, enabling commands like 'dotnet ef migrations add InitialCreate'.
/// </summary>
public class HumanResourceDbContextFactory : IDesignTimeDbContextFactory<HumanResourceDbContext>
{
    public HumanResourceDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<HumanResourceDbContext>();
        optionsBuilder.UseSqlite(configuration.GetConnectionString("Default"));

        return new HumanResourceDbContext(optionsBuilder.Options);
    }
}
