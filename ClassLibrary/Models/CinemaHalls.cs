using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab2.Models
{
    public class CinemaHalls
    {
        public int Id { get; set; }
        public int HallNumber { get; set; }
        public int MaxPlaceNumber { get; set; }

        public override string ToString()
        {
            return $"{Id} --- {HallNumber} -- {MaxPlaceNumber}";
        }

    }
}
