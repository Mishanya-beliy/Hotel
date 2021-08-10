using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.DAL.EF;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Repositories;
using Hotel.WEB.Additional;
using Hotel.WEB.Cookie;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using static Hotel.DAL.EF.GuestIdentity;

namespace Hotel.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Database
            services.AddDbContext<HotelKeyContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("HotelKeyProtection")));

            services.AddDbContext<HotelAuthenticationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("HotelAuthentication")));

            services.AddDbContext<HotelContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("HotelDB"));
                options.UseLazyLoadingProxies();
            });
            services.AddDatabaseDeveloperPageExceptionFilter();
            #endregion

            #region Custom services
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IRepositoryManager, RepositoryManager>();
            services.AddTransient<IGuestService, GuestService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IPriceService, PriceService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IClaimsTransformation, GuestClaimsTransformation>();
            #endregion

            #region Security
            services.AddDataProtection()
                .PersistKeysToDbContext<HotelKeyContext>()
                .SetApplicationName("HotelCoockie");

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = ClaimType.Id;
                options.ClaimsIdentity.UserNameClaimType = ClaimType.Name;
                options.ClaimsIdentity.RoleClaimType = ClaimType.Role;

                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            services.AddDefaultIdentity<GuestIdentity>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HotelAuthenticationContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "HotelCoockie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                options.LoginPath = Routes.AccountLogin;
                options.LogoutPath = Routes.AccountLogout;
                options.AccessDeniedPath = Routes.Home;
                options.SlidingExpiration = true;
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Common",
            //         policy => policy.RequireRole("Administrator", "User"));
            //});
            #endregion

            services.AddControllersWithViews();
            services.Configure<ConsoleLifetimeOptions>(opts
                    => opts.SuppressStatusMessages = Configuration["SuppressStatusMessages"] != null);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapRazorPages();

                //endpoints.MapControllerRoute(name: "room",
                // pattern: "Room/{*article}",
                // defaults: new { controller = "Blog", action = "Article" });


                //endpoints.MapGet("/sombread", async context =>
                //{
                //    var name = context.Request.RouteValues["name"];
                //    await context.Response.WriteAsync($"Hello {name}!");
                //});
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            loggerFactory.AddFile(FileLogger.FilePath);
            var logger = loggerFactory.CreateLogger("FileLogger");

            //app.Run(async (context) =>
            //{
            //    logger.LogInformation("Processing request {0}", context.Request.Path);
            //    //await context.Response.WriteAsync("Start logging");
            //});

            logger.LogInformation("Start server");
        }
    }
}
