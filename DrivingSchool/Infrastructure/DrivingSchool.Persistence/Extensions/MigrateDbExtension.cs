using Microsoft.EntityFrameworkCore;
using DrivingSchool.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DrivingSchool.Persistence.Extensions;

public static class MigrateDbExtension
{
    public static void MigrateDatabase(this IHostApplicationBuilder builder)
    {
        using var serviceScope = builder.Services.BuildServiceProvider().CreateScope();
        var ctx = serviceScope?.ServiceProvider.GetService<DrivingSchoolDbContext>();
        if (ctx != null)
        {
            ctx.Database.Migrate();
        }
    }
}