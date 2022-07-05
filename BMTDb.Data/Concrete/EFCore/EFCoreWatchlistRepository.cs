#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable IDE0063 // Use simple 'using' statement

using BMTDb.Data.Abstract;
using BMTDb.Entity.Lists;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EFCoreWatchlistRepository : EFCoreGenericRepository<Watchlist, BMTDbContext>, IWatchlistRepository
    {
        public Watchlist GetByUserId(string userId)
        {
            using (var context = new BMTDbContext())
            {
                return context.Watchlists
                                .Include(i => i.WatchlistItems)
                                .ThenInclude(i => i.Movie).ThenInclude(i => i.MovieGenres).ThenInclude(i => i.Genre)
                                .Include(i => i.WatchlistItems)
                                .ThenInclude(i => i.Movie).ThenInclude(i => i.MovieStudios).ThenInclude(i => i.Studios)
                                .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void RemoveFromWatchlist(int watchlistId, int movieId)
        {
            using (var context = new BMTDbContext())
            {
                var cmd = @"DELETE from WatchlistItems WHERE WatchlistId=@p0 and MovieId=@p1";
                context.Database.ExecuteSqlRaw(cmd, watchlistId, movieId);
            }
        }

        public override void Update(Watchlist entity)
        {
            using (var context = new BMTDbContext())
            {
                context.Watchlists.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
