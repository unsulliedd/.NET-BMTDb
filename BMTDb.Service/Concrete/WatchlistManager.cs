using BMTDb.Data.Abstract;
using BMTDb.Entity.Lists;
using BMTDb.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Concrete
{
    public class WatchlistManager : IWatchlistService
    {
        private readonly IWatchlistRepository _watchlistRepository;
        public WatchlistManager(IWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public void InitializeWatchlist(string userId)
        {
            _watchlistRepository.Create(new Watchlist() { UserId = userId });
        }

        public Watchlist GetWatchlistbyUserId(string userId)
        {
            return _watchlistRepository.GetByUserId(userId);
        }

        public void AddtoWatchlist(string userId, int MovieId, DateTime AddedDate)
        {
            var watchlist = GetWatchlistbyUserId(userId);
            if(watchlist != null)
            {
                var index = watchlist.WatchlistItems.FindIndex(i => i.MovieId == MovieId);
                if(index < 0)
                {
                    watchlist.WatchlistItems.Add(new WatchlistItem()
                    {
                        MovieId = MovieId,
                        AddedDate = AddedDate,
                        WatchlistId = watchlist.Id
                    });
                    _watchlistRepository.Update(watchlist);
                }                
            }
        }
    }
}