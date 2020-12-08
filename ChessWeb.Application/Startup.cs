using ChessWeb.Application.Constants;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Persistence.Implementations;
using ChessWeb.Service.Interfaces;
using ChessWeb.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ChessWeb.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                // .AddDbContext<ApplicationDbContext>(options => 
                // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                .AddDbContext<ApplicationDbContext>();
            
            services.AddIdentity<User, UserRole>(opts=> {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            
            services.AddScoped<IChessGameService, ChessGameService>();
            services.AddScoped<IGameService, GameService>();
            
            // services.Configure<SmtpOptions>(Configuration.GetSection(SmtpOptions.SectionName));
            services.AddTransient<IMailSender, MailSenderService>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IGameStatusRepository, GameStatusRepository>();
            services.AddTransient<IMoveRepository, MoveRepository>();
            services.AddTransient<ISideRepository, SideRepository>();

            services.AddRazorPages().AddRazorRuntimeCompilation();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}