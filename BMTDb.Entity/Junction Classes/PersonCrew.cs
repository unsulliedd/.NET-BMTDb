using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{ 
    public class PersonCrew
    {
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public int CrewId { get; set; }
        public Crew? Crews { get; set; }
    }
}