using DrivingSchool.Domain.Contracts;
using DrivingSchool.Persistence.Context;
using DrivingSchool.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.Persistence.Extensions;

public static class RegisterDbExtension
{
    public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DrivingSchoolDb");
        services.AddDbContext<DrivingSchoolDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        }, ServiceLifetime.Scoped);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<ITestResultRepository, TestResultRepository>();
    }
}
