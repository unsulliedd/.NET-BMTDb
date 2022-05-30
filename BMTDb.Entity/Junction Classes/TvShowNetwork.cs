using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class TvShowNetwork
    {
        public int NetworkId { get; set; }
        public Network? Networks { get; set; }
        public int TvShowId { get; set; }
        public TvShow? TvShow { get; set; }
    }
}