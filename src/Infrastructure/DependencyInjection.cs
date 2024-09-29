using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Data;
//using CleanArchitecture.Infrastructure.Data.Interceptors;
using CleanArchitecture.Infrastructure.Event;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            //options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //services
        //    .AddDefaultIdentity<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>();

        //services.AddSingleton(TimeProvider.System);
        //services.AddTransient<IIdentityService, IdentityService>();

        //services.AddAuthorization(options =>
        //    options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        #region RabbitMq Consumer
        //    services.AddHostedService<RabbitMqConsumerService>(sp =>
        //new RabbitMqConsumerService("localhost", "queueName", sp.GetRequiredService<ILogger<IRabbitMqConsumerService>>()));

        services.AddSingleton(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMqConsumerService>>();
            var hostName = "localhost";
            var queueName = "queueName";

            return new RabbitMqConsumerService(hostName, queueName, logger);
        });
        #endregion

        #region Unit Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        return services;
    }
}
