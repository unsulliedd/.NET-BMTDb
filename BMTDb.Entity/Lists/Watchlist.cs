﻿#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity.Lists
{
    public class Watchlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<WatchlistItem> WatchlistItems { get; set; }
    }
}