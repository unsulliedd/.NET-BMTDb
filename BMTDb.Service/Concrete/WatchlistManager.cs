using BMTDb.Data.Abstract;
using BMTDb.Entity.Lists;
using BMTDb.Service.Abstract;

namespace BMTDb.Service.Concrete
{
    public class WatchlistManager : IWatchlistService
    {
        private readonly IUnitofWork _unitofWork;

        public WatchlistManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void InitializeWatchlist(string userId)
        {
            _unitofWork.Watchlists.Create(new Watchlist() { UserId = userId });
            _unitofWork.Save();
        }

        public Watchlist GetWatchlistbyUserId(string userId)
        {
            return _unitofWork.Watchlists.GetByUserId(userId);
        }

        public void AddtoWatchlist(string userId, int MovieId, DateTime AddedDate)
        {
            var watchlist = GetWatchlistbyUserId(userId);
            if (watchlist != null)
            {
                var index = watchlist.WatchlistItems.FindIndex(i => i.MovieId == MovieId);
                if (index < 0)
                {
                    watchlist.WatchlistItems.Add(new WatchlistItem()
                    {
                        MovieId = MovieId,
                        AddedDate = AddedDate,
                        WatchlistId = watchlist.Id
                    });
                    _unitofWork.Watchlists.Update(watchlist);
                    _unitofWork.Save();
                }
            }
        }

        public void RemoveFromWatchlist(string userId, int movieId)
        {
            var watchlist = GetWatchlistbyUserId(userId);
            if (watchlist != null)
            {
                _unitofWork.Watchlists.RemoveFromWatchlist(watchlist.Id, movieId);
                _unitofWork.Save();
            }
        }
    }
}