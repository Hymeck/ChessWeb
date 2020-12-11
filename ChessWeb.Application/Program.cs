using System;
using System.Diagnostics;
using ChessWeb.Application;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                .UseStartup<Startup>()
                .UseUrls("http://*:" + BuildData.HostPort);
        });

var host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var userManager = services.GetRequiredService<UserManager<User>>();
    var rolesManager = services.GetRequiredService<RoleManager<UserRole>>();
    var gameRepository = services.GetRequiredService<IGameRepository>();
    await DataInitializer.InitializeAsync(userManager, rolesManager, gameRepository);
}
catch (Exception )
{
    Debug.WriteLine("Something goes wrong with data seeding");
}

await host.RunAsync();