#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

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
    public class EFCoreFavouriteRepository : EFCoreGenericRepository<Favourite>, IFavouriteRepository
    {
        public EFCoreFavouriteRepository(BMTDbContext context) : base(context)
        {

        }
        private BMTDbContext BMTDbContext
        {
            get { return context as BMTDbContext; }
        }

        public Favourite GetByUserId(string userId)
        {
            return BMTDbContext.Favourites
                        .Include(i => i.FavouriteItems)
                        .ThenInclude(i => i.Movie).ThenInclude(i => i.MovieGenres).ThenInclude(i => i.Genre)
                        .Include(i => i.FavouriteItems)
                        .ThenInclude(i => i.Movie).ThenInclude(i => i.MovieProductionCompanies).ThenInclude(i => i.ProductionCompanies)
                        .FirstOrDefault(i => i.UserId == userId);
        }

        public void RemoveFromFavourite(int watchlistId, int movieId)
        {
            var cmd = @"DELETE from FavouriteItems WHERE FavouriteId=@p0 and MovieId=@p1";
            BMTDbContext.Database.ExecuteSqlRaw(cmd, watchlistId, movieId);
        }

        public override void Update(Favourite entity)
        {
            BMTDbContext.Favourites.Update(entity);
        }
    }
}