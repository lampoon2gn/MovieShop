using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web
{
    public class Program
    {   //create Kestral server in the background
        //create a hosting environment so ASP>NET Core can run in it.
        //it will call a class called start up

        //Middleware in ASP.NET Core
        //when you make a request in ASP.NET Core MVC/API... the request will go through some middleware
        //Request ->middleware1 -> do some processing -> m2 -> m3 -> destination
        //Response -> m3 -> m2 -> m1
        //asp.net has some built-in middlewares that every request will go through
        //developers can also create custom middlewares and plugin to the pipline of middlewares

        //routing is a pattern matching technique used to identify which controller and action method to use
            //Traditional based routing
                //will override default pattern with its owen value if provided, else, the default wil be used.

            //Attribute based routing -> most often used
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
