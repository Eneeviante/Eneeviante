using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Infrastucture.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtBooking
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
        {//=)
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ITechArtBooking", Version = "v1" });
            });
            string connection = Configuration.GetConnectionString("DefaultConnection");
            //gets the options object that configures the database for the context class
            services.AddDbContext<EFBookingDBContext>(options => {
                options.UseSqlServer(connection);
            });

            services.AddTransient<IUserRepository, EFUserRepository>();          //defines a service that creates a new instance
            services.AddTransient<IRepository<Hotel>, EFHotelRepository>();            //of the EFUserRepository class
            services.AddTransient<IRepository<Category>, EFCategoryRepository>();      //every time an instance of the IUserRepository type is required
            services.AddTransient<IReviewRepository, EFReviewRepository>();
            services.AddTransient<IRoomRepository, EFRoomRepository>();
            services.AddTransient<IRepository<Booking>, EFBookingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITechArtBooking v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
