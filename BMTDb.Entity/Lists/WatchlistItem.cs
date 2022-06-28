﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity.Lists
{
    public class WatchlistItem
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int WatchlistId { get; set; }
        public Watchlist Watchlist { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}