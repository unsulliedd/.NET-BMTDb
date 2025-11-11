using BMTDb.Entity.Lists;

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