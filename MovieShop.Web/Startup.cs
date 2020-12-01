using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repositories;
using MovieShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web
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
            services.AddControllersWithViews();

            //thoptions here will go to the MovieShopDbContext constructor
            services.AddDbContext<MovieShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(("MovieShopDbConnection"))));


            // authentication middleware, sets the default authentication scheme for the app
 
             services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
             {
                 options.Cookie.Name = "MovieShopAuthCookie";
                 options.ExpireTimeSpan = TimeSpan.FromHours(2);
                 options.LoginPath = "/Account/Login";
             });

             //check if the MOvieShopAuthCookie exists, and if its expired




             //=======================================================
             //.net core had built in inversion of control(DI) support
             //.net frameworkd does not, so we need to use third party solutions
             //=======================================================
             services.AddScoped<IMovieService, MovieServices>();//this will replace the interface with the implementation
             services.AddScoped<IMovieRepository, MovieRepository>();
             services.AddScoped<IGenreService, GenreServices>();
             services.AddScoped<IAsyncRepository<Genre>, EfRepository<Genre>>();
             services.AddScoped<IUserService, UserServices>();
             services.AddScoped<IUserRepository, UserRepository>();
             services.AddScoped<ICryptoService, CryptoServices> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(); //responsible for serving static files

            app.UseRouting(); //very important

            app.UseAuthentication();// for [Authorize] filter
            app.UseAuthorization(); //for authentication

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
