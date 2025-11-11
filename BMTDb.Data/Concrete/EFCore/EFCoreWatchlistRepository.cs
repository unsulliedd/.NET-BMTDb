#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using BMTDb.Data.Abstract;
using BMTDb.Entity.Lists;
using Microsoft.EntityFrameworkCore;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EFCoreWatchlistRepository : EFCoreGenericRepository<Watchlist>, IWatchlistRepository
    {
        public EFCoreWatchlistRepository(BMTDbContext context) : base(context)
        {

        }
        private BMTDbContext BMTDbContext => context as BMTDbContext;

        public Watchlist GetByUserId(string userId)
        {
            return BMTDbContext.Watchlists
                        .Include(i => i.WatchlistItems)
                        .ThenInclude(i => i.Movie).ThenInclude(i => i.MovieGenres).ThenInclude(i => i.Genre)
                        .Include(i => i.WatchlistItems)
                        .ThenInclude(i => i.Movie).ThenInclude(i => i.MovieProductionCompanies).ThenInclude(i => i.ProductionCompanies)
                        .FirstOrDefault(i => i.UserId == userId);
        }

        public void RemoveFromWatchlist(int watchlistId, int movieId)
        {
            var cmd = @"DELETE from WatchlistItems WHERE WatchlistId=@p0 and MovieId=@p1";
            BMTDbContext.Database.ExecuteSqlRaw(cmd, watchlistId, movieId);
        }

        public override void Update(Watchlist entity)
        {
            BMTDbContext.Watchlists.Update(entity);
        }
    }
}
