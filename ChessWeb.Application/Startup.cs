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
            // var connection = DatabaseConnectionString;
            var connection = GetHerokuConnectionString();
            if (Env.IsProduction())
            {
                services
                    .AddEntityFrameworkNpgsql()
                    .AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(connection));
            }

            else
            {
                // services
                //     // .AddDbContext<ApplicationDbContext>(options =>
                //     //     options.UseSqlServer(connection));
                // .AddDbContext<ApplicationDbContext>();
                
                services
                    .AddEntityFrameworkNpgsql()
                    .AddDbContext<ApplicationDbContext>();
            }

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
                endpoints.MapHub<SandboxHub>(SandboxClient.HubUrl);
                endpoints.MapBlazorHub("/Blazor/_blazor");
                endpoints.MapFallbackToPage("~/Blazor/{*clientrouts:nonfile}", "/Blazor/_Host");
            });
        }
        
        private string GetHerokuConnectionString() 
        {
            // Get the connection string from the ENV variables
            // postgres://{user}:{password}@{hostname}:{port}/{database-name}
            // var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var connectionUrl =
                "postgres://uikbdgoiuzaabs:f01c116697df4f7800d93686721c13c64e813cc8411de5f80af89b05d11f7135@ec2-54-246-67-245.eu-west-1.compute.amazonaws.com:5432/d29cpkqqe1m7i0";

            // parse the connection string
            var databaseUri = new Uri(connectionUrl);

            var db = databaseUri.LocalPath.TrimStart('/');
            var userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }

        public string DatabaseConnectionString =>
            Env.IsDevelopment()
                ? Configuration.GetConnectionString("DefaultConnection")
                : GetHerokuConnectionString();
    }
}