using Cinema;
using Cinema.Context;
using Microsoft.EntityFrameworkCore;
using Cinema.Interface;
using Cinema.Models;
using Cinema.Services;

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

            app.Map("/Actors", TableActors);
            app.Map("/ActorCasts", TableActorCasts);
            app.Map("/CinemaHalls", TableCinemaHalls);
            app.Map("/CountryProductions", TableCountryProductions);
            app.Map("/FilmProductions", Table�FilmProductions);
            app.Map("/Films", TableFilms);
            app.Map("/Genres", TableGenres);
            app.Map("/ListEvents", TableListEvents);
            app.Map("/Places", TablePlaces);
            app.Map("/StaffCasts", TableStaffCasts);
            app.Map("/Staffs", TableStaffs);
            app.Map("/info", Info);
            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CinemaContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<GenericCachedClassService<Actors>>();
            services.AddTransient<GenericCachedClassService<ActorCasts>>();
            services.AddTransient<GenericCachedClassService<CinemaHalls>>();
            services.AddTransient<GenericCachedClassService<CountryProductions>>();
            services.AddTransient<GenericCachedClassService<Films>>();
            services.AddTransient<GenericCachedClassService<FilmProductions>>();
            services.AddTransient<GenericCachedClassService<Genres>>();
            services.AddTransient<GenericCachedClassService<ListEvents>>();
            services.AddTransient<GenericCachedClassService<Places>>();
            services.AddTransient<GenericCachedClassService<StaffCasts>>();
            services.AddTransient<GenericCachedClassService<Staffs>>();

            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
            services.AddSession();
        }

        private static void TableActors(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<Actors> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Actors>>();
                var actors = cachedClassService.GetAll("Actors20");
                string responseString = "<HTML><TITLE>������� ������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� ������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>���</TH><TH>�������</TH><TH>��������</TH></TR>" +
                    "</THEAD>";
                foreach (var actor in actors)
                {
                    responseString += 
                    "<TR>" + 
                        "<TD>" + actor.Id + "</TD>" +
                        "<TD>" + actor.Name + "</TD>" +
                        "<TD>" + actor.SurName + "</TD>" +
                        "<TD>" + actor.MiddleName + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableActorCasts(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<ActorCasts> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<ActorCasts>>();
                var actorCasts = cachedClassService.GetAll("ActorCasts20");
                string responseString = "<HTML><TITLE>������� ��������� ������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� ��������� ������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Id ������</TH><TH>Id ������</TH></TR>" +
                    "</THEAD>";
                foreach (var actorCast in actorCasts)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + actorCast.Id + "</TD>" +
                        "<TD>" + actorCast.FilmId + "</TD>" +
                        "<TD>" + actorCast.ActorId + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableCinemaHalls(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<CinemaHalls> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<CinemaHalls>>();
                var cinemaHalls = cachedClassService.GetAll("CinemaHalls20");
                string responseString = "<HTML><TITLE>������� ���� ����������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� ���� ����������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>����� ����</TH><TH>������������ ���������� ����</TH></TR>" +
                    "</THEAD>";
                foreach (var cinemaHall in cinemaHalls)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + cinemaHall.Id + "</TD>" +
                        "<TD>" + cinemaHall.HallNumber + "</TD>" +
                        "<TD>" + cinemaHall.MaxPlaceNumber + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableCountryProductions(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<CountryProductions> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<CountryProductions>>();
                var countryProductions = cachedClassService.GetAll("CountryProductions20");
                string responseString = "<HTML><TITLE>������� ������-�������������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� ������-�������������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>������ �������������</TH></TR>" +
                    "</THEAD>";
                foreach (var countryProduction in countryProductions)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + countryProduction.Id + "</TD>" +
                        "<TD>" + countryProduction.Name + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void Table�FilmProductions(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<FilmProductions> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<FilmProductions>>();
                var filmProductions = cachedClassService.GetAll("FilmProductions20");
                string responseString = "<HTML><TITLE>������� ��������-�������������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� ��������-�������������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>��������</TH><TH>������ ������������</TH></TR>" +
                    "</THEAD>";
                foreach (var filmProduction in filmProductions)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + filmProduction.Id + "</TD>" +
                        "<TD>" + filmProduction.Name + "</TD>" +
                        "<TD>" + filmProduction.Country + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableFilms(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<Films> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Films>>();
                var films = cachedClassService.GetAll("Films20");
                string responseString = "<HTML><TITLE>������� ������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� ������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>��������</TH><TH>Id �����</TH><TH>Id ��������� ������</TH><TH>������� ���������</TH><TH>Id ������-�������������</TH><TH>������������</TH><TH>��������</TH></TR>" +
                    "</THEAD>";
                foreach (var film in films)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + film.Id + "</TD>" +
                        "<TD>" + film.Name + "</TD>" +
                        "<TD>" + film.GenreId + "</TD>" +
                        "<TD>" + film.ActorCastId + "</TD>" +
                        "<TD>" + film.AgeLimit + "</TD>" +
                        "<TD>" + film.CountryProductionId + "</TD>" +
                        "<TD>" + film.Duration + "</TD>" +
                        "<TD>" + film.Description + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableGenres(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<Genres> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Genres>>();
                var genres = cachedClassService.GetAll("Genres20");
                string responseString = "<HTML><TITLE>������� �����</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� �����</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>��������</TH></TR>" +
                    "</THEAD>";
                foreach (var genre in genres)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + genre.Id + "</TD>" +
                        "<TD>" + genre.Name + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableListEvents(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<ListEvents> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<ListEvents>>();
                var listEvents = cachedClassService.GetAll("ListEvents20");
                string responseString = "<HTML><TITLE>������� �����������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� �����������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>��������</TH><TH>����</TH><TH>����� ������</TH><TH>����� �����</TH><TH>���� ������</TH><TH>Id ������ �����������</TH><TH>Id ������</TH></TR>" +
                    "</THEAD>";
                foreach (var listEvent in listEvents)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + listEvent.Id + "</TD>" +
                        "<TD>" + listEvent.Name + "</TD>" +
                        "<TD>" + listEvent.Date + "</TD>" +
                        "<TD>" + listEvent.StartTime + "</TD>" +
                        "<TD>" + listEvent.EndTime + "</TD>" +
                        "<TD>" + listEvent.TicketPrice + "</TD>" +
                        "<TD>" + listEvent.StaffCastId + "</TD>" +
                        "<TD>" + listEvent.FilmId + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TablePlaces(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<Places> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Places>>();
                var places = cachedClassService.GetAll("Places20");
                string responseString = "<HTML><TITLE>������� �����</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� �����</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Id �����������</TH><TH>Id ����</TH><TH>����� �����</TH><TH>�����</TH></TR>" +
                    "</THEAD>";
                foreach (var place in places)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + place.Id + "</TD>" +
                        "<TD>" + place.ListEventId + "</TD>" +
                        "<TD>" + place.CinemaHallId + "</TD>" +
                        "<TD>" + place.PlaceNumber + "</TD>" +
                        "<TD>" + place.TakenSeat + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableStaffCasts(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<StaffCasts> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<StaffCasts>>();
                var staffCasts = cachedClassService.GetAll("StaffCasts20");
                string responseString = "<HTML><TITLE>������� ������ �����������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� ������ �����������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Id �����������</TH><TH>Id ����������</TH></TR>" +
                    "</THEAD>";
                foreach (var staffCast in staffCasts)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + staffCast.Id + "</TD>" +
                        "<TD>" + staffCast.ListEventId + "</TD>" +
                        "<TD>" + staffCast.StaffId + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableStaffs(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<Staffs> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Staffs>>();
                var staffs = cachedClassService.GetAll("Staffs20");
                string responseString = "<HTML><TITLE>������� ������ �����������</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>������� ������ �����������</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>���</TH><TH>�������</TH><TH>��������</TH><TH>���������</TH><TH>���� ������</TH></TR>" +
                    "</THEAD>";
                foreach (var staff in staffs)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + staff.Id + "</TD>" +
                        "<TD>" + staff.Name + "</TD>" +
                        "<TD>" + staff.Surname + "</TD>" +
                        "<TD>" + staff.MiddleName + "</TD>" +
                        "<TD>" + staff.Post + "</TD>" +
                        "<TD>" + staff.WorkExperience + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
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

        private static void SearchFormForFilms(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string responseString = ;
                await context.Response.WriteAsync(responseString);
            });
        }
    }
}
