using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class PersonCast
    {
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public int CastId { get; set; }
        public Cast? Casts { get; set; }
    }
}
