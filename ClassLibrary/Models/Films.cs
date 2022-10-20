using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab2.Models
{
    public class Films
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GenreId { get; set; }

        public int ActorCastId { get; set; }

        public int Duration { get; set; }

        public int FilmProductionId { get; set; }

        public int CountryProductionId { get; set; }

        public int AgeLimit { get; set; }

        public string Description { get; set; }
    }
}
