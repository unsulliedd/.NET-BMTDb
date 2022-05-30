using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMTDb.Entity
{
    public class Crew
    {
        public int CrewId { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int TvShowId { get; set; }
        public TvShow? TvShow { get; set; }
        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
