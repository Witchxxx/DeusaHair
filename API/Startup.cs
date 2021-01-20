using API.Errors;
using API.Extensions;
using API.Middleware;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddApplicationServices(); // created an extension class to reduce crowding in start up.
            services.AddSwaggerDocumentation();
            services.AddCors(opt =>
            { 
                opt.AddPolicy("CorsPolicy", policy =>
                 {
                     policy.AllowAnyHeader().AllowAnyHeader().WithOrigins("https://localhost:4200");
                 });
            }); // so our angular app is Allowed to send requests to API since it is forbidden by default.

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            app.UseMiddleware<ExceptionMiddleware>(); // our own exception checker to make consistent error responses. Also used to check for errors that may not be handled in out controllers, Like null refernce exceptions.

            app.UseStatusCodePagesWithReExecute("/errors/{0}"); //hitting error controller if no endpoint exists to handle that error. E.G return badrequest(new ApiResponse(400)) . Like when a certain endpoint doesn't exist.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles(); //so we can serve static images like our products. Always has to be here as order is important.

            app.UseCors("CorsPolicy"); // middle for enbling cors.

            app.UseAuthorization();

            app.UseSwaggerDocumentation(); //reduce crowding in startup
        }
    }
}
