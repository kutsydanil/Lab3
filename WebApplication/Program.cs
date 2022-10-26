using Cinema;
using Cinema.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            IServiceCollection services = builder.Services;
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            ConfigureServices(services, connectionString);
            var app = builder.Build();

            //app.MapGet("/", (CinemaContext db) => db.Actors.ToList());
            app.Map("/info", Info);
            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CinemaContext>(options => options.UseSqlServer(connectionString));

            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
            services.AddSession();
        }

        private static void TableActor()
        {

        }

        private static void Info(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string responseString = "<HTML><HEAD><TITLE>����������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><h1>����������:</h1>";
                responseString += "<p> ������: " + context.Request.Host + "</p>";
                responseString += "<p> ����: " + context.Request.PathBase + "</p>";
                responseString += "<p> ��������: " + context.Request.Protocol + "</p>";
                responseString += "<p> ��������: " + context.Request.HttpContext + "</p>";
                responseString += "<A href='/'>�������</A></BODY></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }
    }
}
