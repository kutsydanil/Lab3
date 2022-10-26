using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Models;

namespace Cinema.Context
{
    public class CinemaContext: DbContext
    {
        public DbSet<Actors> Actors { get; set; }
        public DbSet<ActorCasts> ActorCasts { get; set; }
        public DbSet<CountryProductions> CountryProductions { get; set; }
        public DbSet<FilmProductions> FilmProductions { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Films> Films { get; set; }
        public DbSet<CinemaHalls> CinemaHalls { get; set; }
        public DbSet<Places> Places { get; set; }
        public DbSet<Staffs> Staffs { get; set; }
        public DbSet<StaffCasts> StaffCasts { get; set; }
        public DbSet<ListEvents> ListEvents { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            optionsBuilder.UseSqlServer(Connection.GetConnetion());
        }*/

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }
    }
}
