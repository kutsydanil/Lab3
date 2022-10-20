using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab2.Models
{
    public class Actors
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string MiddleName { get; set; }

        public Actors(string Name , string SurName , string MiddleName)
        {
            this.MiddleName = MiddleName;
            this.Name = Name;  
            this.SurName = SurName;
        }

        public override string ToString()
        {
            return $"{Id} ----- {Name} ----- {SurName} ----- {MiddleName}";
        }
    }
}
