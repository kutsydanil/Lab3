using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab2.Models
{
    public class ActorCasts
    {
        
        public int Id { get; set; }


        public int ActorId { get; set; }


        public int FilmId { get; set; }

        public ActorCasts(int ActorId , int FilmId)
        {
            this.ActorId = ActorId;
            this.FilmId = FilmId;  
        }

        public override string ToString()
        {
            return $"{Id} ----- {ActorId} ----- {FilmId}";
        }

    }
}
