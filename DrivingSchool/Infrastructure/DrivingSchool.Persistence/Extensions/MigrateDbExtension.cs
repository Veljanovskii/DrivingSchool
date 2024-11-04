using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using DrivingSchool.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.Persistence.Extensions;

public static class MigrateDbExtension
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        var ctx = serviceScope?.ServiceProvider.GetService<DrivingSchoolDbContext>();
        if (ctx != null)
        {
            ctx.Database.Migrate();
        }
    }
}