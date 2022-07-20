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
    public class FavouriteManager : IFavouriteService
    {
        private readonly IUnitofWork _unitofWork;

        public FavouriteManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void InitializeFavourite(string userId)
        {
            _unitofWork.Favourites.Create(new Favourite() { UserId = userId });
            _unitofWork.Save();
        }

        public Favourite GetFavouritebyUserId(string userId)
        {
            return _unitofWork.Favourites.GetByUserId(userId);
        }

        public void AddtoFavourite(string userId, int MovieId, DateTime AddedDate)
        {
            var favourite = GetFavouritebyUserId(userId);
            if (favourite != null)
            {
                var index = favourite.FavouriteItems.FindIndex(i => i.MovieId == MovieId);
                if (index < 0)
                {
                    favourite.FavouriteItems.Add(new FavouriteItem()
                    {
                        MovieId = MovieId,
                        AddedDate = AddedDate,
                        FavouriteId = favourite.Id
                    });
                    _unitofWork.Favourites.Update(favourite);
                    _unitofWork.Save();
                }
            }
        }

        public void RemoveFromFavourite(string userId, int movieId)
        {
            var favourite = GetFavouritebyUserId(userId);
            if (favourite != null)
            {
                _unitofWork.Favourites.RemoveFromFavourite(favourite.Id, movieId);
                _unitofWork.Save();
            }
        }
    }
}