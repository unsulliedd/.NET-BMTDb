using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMTDb.Entity
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Director { get; set; }
        public DateTime? Release_Date { get; set; }
        public string? MovieInfo { get; set; }
        public string? Trailer { get; set; }
        public string? MoviePoster { get; set; }
        public string? MovieBackdrop { get; set; }
        public string? MovieLogo { get; set; }
        public string? MovieTagline { get; set; }
        public string? MovieRatings { get; set; }
        public int? RunTime { get; set; }
        public int? Budget { get; set; }
        public string? Status { get; set; }

        public string? IMDBId { get; set; }
        public string? TMDbId { get; set; }


        public List<MovieStudio>? MovieStudios { get; set; }
        public List<MovieGenre>? MovieGenres { get; set; }

    }
}
