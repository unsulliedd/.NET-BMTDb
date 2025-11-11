using BMTDb.Entity;

namespace BMTDb.Data.Abstract
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie entity, int[] genreIds, int[] studioIds, int[] crewIds);
        Movie GetMovieDetails(int id);
        List<Movie> GetMoviebyFilter(string name, string Studio_Name, string sortOrder, int page, int pageSize);
        List<Movie> GetMovies(int page, int pageSize);
        List<Movie> GetSearchResult(string searchString);
        int GetCountbyFilter(string genre, string studio);
        int GetMovieCount();
        List<Movie> GetUserMovielist(List<int> data, string username);
        void RemoveFromRecentlyViewed(string username, int movieId);
        List<Movie> GetByPopularity();
    }
}
