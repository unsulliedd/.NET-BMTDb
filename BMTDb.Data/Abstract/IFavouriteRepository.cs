using BMTDb.Entity.Lists;

namespace BMTDb.Data.Abstract
{
    public interface IFavouriteRepository : IRepository<Favourite>
    {
        Favourite GetByUserId(string userId);
        void RemoveFromFavourite(int watchlistId, int movieId);
    }
}
