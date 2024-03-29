﻿using BMTDb.Entity.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Abstract
{
    public interface IWatchlistService
    {
        void InitializeWatchlist(string userId);
        Watchlist GetWatchlistbyUserId(string userId);
        void AddtoWatchlist(string userId, int MovieId, DateTime AddedDate);
        void RemoveFromWatchlist(string userId, int movieId);
    }
}