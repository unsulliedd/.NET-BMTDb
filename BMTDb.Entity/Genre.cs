using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMTDb.Entity
{
    public class Genre
    {

        public int GenreId { get; set; }
        public string? Name { get; set; }

        public List<MovieGenre>? MovieGenres { get; set; }
        public List<TvShowGenre>? TvShowGenres { get; set; }
    }
}
