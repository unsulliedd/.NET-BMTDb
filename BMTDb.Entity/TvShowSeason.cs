using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class TvShowSeason
    {
        public int SeasonId { get; set; }
        public Season? Season { get; set; }
        public int TvShowId { get; set; }
        public TvShow? TvShow { get; set; }
    }
}
