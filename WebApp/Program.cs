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

            app.Map("/searchform1", SearchForm1ForGenres);
            app.Map("/searchform2", SearchForm2ForGenres);
            app.Map("/Actors", TableActors);
            app.Map("/ActorCasts", TableActorCasts);
            app.Map("/CinemaHalls", TableCinemaHalls);
            app.Map("/CountryProductions", TableCountryProductions);
            app.Map("/FilmProductions", TableАFilmProductions);
            app.Map("/Films", TableFilms);
            app.Map("/Genres", TableGenres);
            app.Map("/ListEvents", TableListEvents);
            app.Map("/Places", TablePlaces);
            app.Map("/StaffCasts", TableStaffCasts);
            app.Map("/Staffs", TableStaffs);
            app.Map("/info", Info);
            app.Run((context) =>
            {
                string responseString = "<HTML><TITLE>Главная</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>";
                responseString += "<UL>";
                responseString += "<LI><A href='/'>Главная</A></LI>";
                responseString += "<LI><A href='/Actors'>Aктеры</A></LI>";
                responseString += "<LI><A href='/ActorCasts'>Актерские группы</A></LI>";
                responseString += "<LI><A href='/CinemaHalls'>Залы кинотеатра</A></LI>";
                responseString += "<LI><A href='/CountryProductions'>Страна-производитель</A></LI>";
                responseString += "<LI><A href='/Films'>Фильмы</A></LI>";
                responseString += "<LI><A href='/Genres'>Жанры</A></LI>";
                responseString += "<LI><A href='/ListEvents'>Список мероприятий</A></LI>";
                responseString += "<LI><A href='/Places'>Места</A></LI>";
                responseString += "<LI><A href='/StaffCasts'>Группы сотрудников</A></LI>";
                responseString += "<LI><A href='/Staffs'>Сотрудники</A></LI>";
                responseString += "<LI><A href='/searchform1'>Форма #1 для таблицы жанры</A></LI>";
                responseString += "<LI><A href='/searchform2'>Форма #2 для таблицы жанры</A></LI>";
                responseString += "</UL></BODY></HTML>";
                return context.Response.WriteAsync(responseString);
            });

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
                string responseString = "<HTML><TITLE>Таблица актеры</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица актеры</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Имя</TH><TH>Фамилия</TH><TH>Отчетсво</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица актерские труппы</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица актерские труппы</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Id Фильма</TH><TH>Id Актера</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица залы кинотеатра</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица залы кинотеатра</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Номер зала</TH><TH>Максимальное количество мест</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица страна-производитель</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица страна-производитель</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Страна производитель</TH></TR>" +
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

        private static void TableАFilmProductions(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<FilmProductions> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<FilmProductions>>();
                var filmProductions = cachedClassService.GetAll("FilmProductions20");
                string responseString = "<HTML><TITLE>Таблица компания-производитель</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица компания-производитель</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Название</TH><TH>Страна производства</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица фильмы</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица фильмы</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Название</TH><TH>Id жанра</TH><TH>Id актерской группы</TH><TH>Возраст просмотра</TH><TH>Id страны-производителя</TH><TH>Длительность</TH><TH>Описание</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица жанры</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица жанры</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Название</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица мероприятия</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица мероприятия</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Название</TH><TH>Дата</TH><TH>Время начала</TH><TH>Время конца</TH><TH>Цена билета</TH><TH>Id группы сотрудников</TH><TH>Id фильма</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица места</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица места</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Id мероприятия</TH><TH>Id зала</TH><TH>Номер места</TH><TH>Место</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица группы сотрудников</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица группы сотрудников</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Id мероприятия</TH><TH>Id сотрудника</TH></TR>" +
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
                string responseString = "<HTML><TITLE>Таблица группы сотрудников</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица группы сотрудников</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Имя</TH><TH>Фамилия</TH><TH>Отчество</TH><TH>Должность</TH><TH>Опыт работы</TH></TR>" +
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
                string responseString = "<HTML><HEAD><TITLE>Информация</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><h1>Информация:</h1>";
                responseString += "<p> Сервер: " + context.Request.Host + "</p>";
                responseString += "<p> Путь: " + context.Request.PathBase + "</p>";
                responseString += "<p> Протокол: " + context.Request.Protocol + "</p>";
                responseString += "<p> Контекст: " + context.Request.HttpContext + "</p>";
                responseString += "<A href='/'>Главная</A></BODY></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }
        private static void SearchForm1ForGenres(IApplicationBuilder app)
        {
            app.UseSession();
            app.Run(async context =>
            {
                GenericCachedClassService<Genres> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Genres>>();
                var genres = cachedClassService.GetAll("Genres20");
                string keyInputField = "FieldGenre";
                string keySelectField = "SelectFieldGenre";
                string keyRadioFiled = "RadioFiledGenre";

                string responseString = "<HTML><HEAD><TITLE>Форма №1(Сессия)</TITLE></HEAD>" + "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/><BODY>" +
                "<FORM>" +
                "<BR><A href='/'>На главную</A></BR>" +
                "<BR><INPUT type='submit' value='Сохранить в сессию'/></BR>" +
                $"<BR><INPUT type='text' name='{keyInputField}' value='{context.Session.GetString(keyInputField)}'/><BR/>" +
                $"<BR><SELECT name='{keySelectField}'><option>default</options></BR>";

                foreach (var genre in genres)
                {
                    if ($"{genre.Name}" == context.Session.GetString(keySelectField))
                    {
                        responseString += $"<option selected>{genre.Name}</option>";
                    }
                    else
                    {
                        responseString += $"<option>{genre.Name}</option>";
                    }
                }
                responseString += "</SELECT></BR>";
                responseString += "<BR>";
                foreach (var genre in genres)
                {
                    if ($"{genre.Name}" == context.Session.GetString(keyRadioFiled))
                    {
                        responseString += $"<p><INPUT type='radio' checked value='{genre.Name}' name='{keyRadioFiled}'/>{genre.Name}</p>";
                    }
                    else
                    {
                        responseString += $"<p><INPUT type='radio' value='{genre.Name}' name='{keyRadioFiled}'/>{genre.Name}</p>";
                    }
                }
                responseString += "</BR>";
                string genreField = context.Request.Query[keyInputField];
                string genreSelectedOne = context.Request.Query[keySelectField];
                string genreSelected = context.Request.Query[keyRadioFiled];

                if (genreField is not null)
                {
                    context.Session.SetString(keyInputField, genreField);
                }

                if (genreSelectedOne is not null)
                {
                    context.Session.SetString(keySelectField, genreSelectedOne);
                }

                if (genreSelected is not null)
                {
                    context.Session.SetString(keyRadioFiled, genreSelected);
                }
                responseString += "</BODY></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void SearchForm2ForGenres(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                GenericCachedClassService<Genres> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Genres>>();
                var genres = cachedClassService.GetAll("Genres20");
                string keyInputField = "FieldGenre";
                string keySelectField = "SelectFieldGenre";
                string keyRadioFiled = "RadioFiledGenre";
                
                string responseString = "<HTML><HEAD><TITLE>Форма №2(Куки)</TITLE></HEAD>" + "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/><BODY>" +
                "<FORM>" +
                "<BR><A href='/'>На главную</A></BR>" + 
                "<BR><INPUT type='submit' value='Сохранить в Cookies'/></BR>" +
                $"<BR><INPUT type='text' name='{keyInputField}' value='{context.Request.Cookies[keyInputField]}'/><BR/>" + 
                $"<BR><SELECT name='{keySelectField}'><option>default</options></BR>";

                foreach(var genre in genres)
                {
                    if ($"{genre.Name}" == context.Request.Cookies[keySelectField])
                    {
                        responseString += $"<option selected>{genre.Name}</option>";
                    }
                    else
                    {
                        responseString += $"<option>{genre.Name}</option>";
                    }
                }
                responseString += "</SELECT></BR>";
                responseString += "<BR>";
                foreach (var genre in genres)
                {
                    if ($"{genre.Name}" == context.Request.Cookies[keyRadioFiled])
                    {
                        responseString += $"<p><INPUT type='radio' checked value='{genre.Name}' name='{keyRadioFiled}'/>{genre.Name}</p>";
                    }
                    else
                    {
                        responseString += $"<p><INPUT type='radio' value='{genre.Name}' name='{keyRadioFiled}'/>{genre.Name}</p>";
                    }
                }
                responseString += "</BR>";
                string genreField = context.Request.Query[keyInputField];
                string genreSelectedOne = context.Request.Query[keySelectField];
                string genreSelected = context.Request.Query[keyRadioFiled];

                if(genreField is not null)
                {
                    context.Response.Cookies.Append(keyInputField, genreField);
                }

                if (genreSelectedOne is not null)
                {
                    context.Response.Cookies.Append(keySelectField, genreSelectedOne);
                }

                if (genreSelected is not null)
                {
                    context.Response.Cookies.Append(keyRadioFiled, genreSelected);
                }
                responseString += "</BODY></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }
    }
}
