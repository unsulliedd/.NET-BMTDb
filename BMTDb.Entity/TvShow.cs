using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMTDb.Entity
{
    public class TvShow
    {
        public int TvShowId { get; set; }
        public string?  Name { get; set; }
        public DateTime? Release_Date { get; set; }
        public string?  TvShow_Info { get; set; }
        public int? Number_of_Seasons { get; set; }
        public int? Number_of_Episodes { get; set; }
        public string?  TvShow_Trailer { get; set; }
        public string?  TvShow_Poster { get; set; }
        public string?  TvShow_Backdrop { get; set; }
        public string?  TvShow_Logo { get; set; }
        public string?  TvShow_Tagline { get; set; }
        public string?  TvShow_Ratings { get; set; }
        public int?     Episode_RunTime { get; set; }
        public int?     TvShow_Budget { get; set; }
        public string?  TvShow_Status { get; set; }

        public string? IMDBId { get; set; }
        public string? TMDbId { get; set; }

        public List<TvShowNetwork>? TvShowNetworks { get; set; }
        public List<TvShowSeason>? TvShowSeasons { get; set; }
        public List<TvShowGenre>? TvShowGenres { get; set; }
        public List<TvShowCrew>? TvCrews { get; set; }

    }
}
