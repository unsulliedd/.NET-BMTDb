using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class TvShowGenre
    {
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int TvShowId { get; set; }
        public TvShow? TvShow { get; set; }
    }
}
