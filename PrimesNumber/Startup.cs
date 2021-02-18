using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace PrimesNumber
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Name Program: PrimesNumber | Author: Halych Ivan");
                    logger.LogInformation("Name Program: PrimesNumber | Author: Halych Ivan");
                });
                endpoints.MapGet("/primes/{number:int}", async context =>
                {
                    if (int.TryParse((string)context.Request.RouteValues["number"], out var number))
                    {
                        var prime = number.IsPrime();
                        context.Response.StatusCode = (int)(prime ? HttpStatusCode.OK : HttpStatusCode.NotFound);
                        logger.LogInformation($"Number: {number} Prime: {prime}");
                    }
                    else
                    {
                        logger.LogError("Invalid argument");
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync("Invalid argument");
                    }
                });
                endpoints.MapGet("/primes", async context =>
                {

                    if (int.TryParse((string)context.Request.Query["to"].FirstOrDefault(), out var to)
                        && int.TryParse((string)context.Request.Query["from"].FirstOrDefault(), out var from)
                        && from <= to)
                    {
                        var primes = Enumerable.Range(from, to - from + 1).Where(i => i.IsPrime());
                        await context.Response.WriteAsync(string.Join(" ", primes));
                        logger.LogInformation($"From={from} To={to} : {string.Join(" ", primes)}");
                    }
                    else
                    {
                        logger.LogError("Invalid argument");
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync("Invalid argument");
                    }
                });
            }); 
        }
    }
}
