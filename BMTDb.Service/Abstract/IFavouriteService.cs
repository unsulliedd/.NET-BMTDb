using BMTDb.Entity.Lists;

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
