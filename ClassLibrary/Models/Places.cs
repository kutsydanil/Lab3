using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab2.Models
{
    public class Places
    {
        public int Id { get; set; }
        public int ListEventId { get; set; }
        public int CinemaHallId { get; set; }
        public int PlaceNumber { get; set; }
        public bool TakenSeat { get; set; }
    }
}
