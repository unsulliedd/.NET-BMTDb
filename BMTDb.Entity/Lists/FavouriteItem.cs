using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity.Lists
{
    public class FavouriteItem
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int FavouriteId { get; set; }
        public Favourite? Favourite { get; set; }
    }
}
