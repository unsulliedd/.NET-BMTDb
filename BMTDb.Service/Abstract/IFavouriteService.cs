using BMTDb.Entity.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Abstract
{
    public interface IFavouriteService
    {
        void InitializeFavourite(string userId);
        Favourite GetFavouritebyUserId(string userId);
        void AddtoFavourite(string userId, int MovieId, DateTime AddedDate);
        void RemoveFromFavourite(string userId, int movieId);
    }
}
