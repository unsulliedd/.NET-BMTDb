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
        private readonly IFavouriteRepository _favouriteRepository;
        public FavouriteManager(IFavouriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
        }

        public void InitializeFavourite(string userId)
        {
            _favouriteRepository.Create(new Favourite() { UserId = userId });
        }

        public Favourite GetFavouritebyUserId(string userId)
        {
            return _favouriteRepository.GetByUserId(userId);
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
                    _favouriteRepository.Update(favourite);
                }
            }
        }

        public void RemoveFromFavourite(string userId, int movieId)
        {
            var favourite = GetFavouritebyUserId(userId);
            if (favourite != null)
            {
                _favouriteRepository.RemoveFromFavourite(favourite.Id, movieId);
            }
        }
    }
}