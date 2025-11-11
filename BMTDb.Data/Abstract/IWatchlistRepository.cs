using BMTDb.Entity.Lists;

namespace BMTDb.Data.Abstract
{
    public interface IWatchlistRepository : IRepository<Watchlist>
    {
        Watchlist GetByUserId(string userId);
        void RemoveFromWatchlist(int watchlistId, int movieId);
    }
}
