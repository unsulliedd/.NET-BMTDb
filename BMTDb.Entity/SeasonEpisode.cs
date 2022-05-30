using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class SeasonEpisode
    {
        public int SeasonId { get; set; }
        public Season? Season { get; set; }
        public int EpisodeId { get; set; }
        public Episode? Episode { get; set; }

    }
}
