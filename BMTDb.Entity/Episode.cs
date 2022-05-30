using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class Episode
    {
        public int EpisodeId { get; set; }
        public string? Episode_Title { get; set; }
        public int? Episode_Number { get; set; }
        public DateTime? Episode_Date { get; set; }

        public List<SeasonEpisode>? SeasonEpisodes { get; set; }
    }
}
