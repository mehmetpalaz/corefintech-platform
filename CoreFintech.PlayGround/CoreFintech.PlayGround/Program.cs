using CoreFintech.Persistence;
using CoreFintech.Tenant;
using CoreFintech.Tenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false);
    })
    .ConfigureServices((context, services) =>
    {
        var connStr = context.Configuration.GetConnectionString("Default");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connStr).EnableSensitiveDataLogging();
        });
        
        services.AddScoped<ITenantContext, PlaygroundTenantContext>();

    })
    .Build();


using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

var customers = await db.Customers.ToListAsync();
Console.WriteLine($"Toplam customer: {customers.Count}");
