using System;
using ChessWeb.Application.Constants;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Persistence.Implementations;
using ChessWeb.Service.Interfaces;
using ChessWeb.Service.Services;
using ChessWeb.SignalR.Client;
using ChessWeb.SignalR.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChessWeb.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = DatabaseConnectionString;
            services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(connection));

            services.AddIdentity<User, UserRole>(opts=> {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddSignalR();

            services
                .AddAuthentication()
                .AddGoogle(options =>
                {
                    var clientId = "GoogleClientId";
                    var secret = "GoogleSecretClientCode";
                    if (Env.IsDevelopment())
                    {
                        options.ClientId = Configuration[clientId];
                        options.ClientSecret = Configuration[secret];
                    }

                    else
                    {
                        options.ClientId = Environment.GetEnvironmentVariable(clientId);
                        options.ClientSecret = Environment.GetEnvironmentVariable(secret);
                    }
                });
            
            services.AddScoped<IChessGameService, ChessGameService>();
            services.AddScoped<IGameService, GameService>();
            
            services.AddTransient<IMailSender, MailSenderService>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IGameStatusRepository, GameStatusRepository>();
            services.AddTransient<IMoveRepository, MoveRepository>();
            services.AddTransient<ISideRepository, SideRepository>();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddServerSideBlazor();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net("log4net.config");
            
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            else if (env.IsProduction())
            {
                app.UseExceptionHandler(Routes.ErrorRoute);
                app.UseHsts();
            }
 
            app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<ChessGameHub>(ChessGameClient.HubUrl);
                endpoints.MapBlazorHub("/Sandbox/_blazor");
                endpoints.MapFallbackToPage("~/Sandbox/{*clientrouts:nonfile}", "/Sandbox/_Host");
            });
        }
        
        private string GetHerokuConnectionString() 
        {
            // Get the connection string from the ENV variables
            // postgres://{user}:{password}@{hostname}:{port}/{database-name}
            var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // parse the connection string
            var databaseUri = new Uri(connectionUrl);

            var db = databaseUri.LocalPath.TrimStart('/');
            var userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }

        public string DatabaseConnectionString =>
            Env.IsDevelopment()
                ? Configuration["DbConnection"]
                : GetHerokuConnectionString();
    }
}