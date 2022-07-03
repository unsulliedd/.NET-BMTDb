using BMTDb.Entity.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Abstract
{
    public interface IFavouriteRepository : IRepository<Favourite>
    {
        Favourite GetByUserId(string userId);
        void RemoveFromFavourite(int watchlistId, int movieId);
    }
}
