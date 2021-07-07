using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.DAL.EF;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Hotel.API
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

            services.AddDbContext<HotelContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HotelDB")));

            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IRepositoryManager, RepositoryManager>();
            services.AddTransient<IGuestService, GuestService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IPriceService, PriceService>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
