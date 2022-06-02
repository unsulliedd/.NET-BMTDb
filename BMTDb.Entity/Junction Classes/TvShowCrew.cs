using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class TvShowCrew
    {
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public int TvShowId { get; set; }
        public TvShow? TvShow { get; set; }
    }
}
