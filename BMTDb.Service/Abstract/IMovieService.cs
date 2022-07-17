using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Abstract
{
    public interface IMovieService : IValidator<Movie>
    {
        List<Movie> GetMoviebyFilter(string name, string Studio_Name, string sortOrder, int page, int pageSize);
        List<Movie> GetMovies(int page, int pageSize);
        List<Movie> GetSearchResult(string searchString);
        Movie GetMovieDetails(int id);
        Movie GetById(int id);
        List<Movie> GetAll();
        List<Movie> GetByPopularity();
        bool Create(Movie entity);
        void Update(Movie entity);
        bool Update(Movie entity, int[] genreIds, int[] studioIds, int[] crewIds);
        void Delete(Movie entity);
        int GetCountbyFilter(string genre, string studio);
        int GetMovieCount();
        List<Movie> GetUserMovielist(List<int> data, string username);
        void RemoveFromRecentlyViewed(string username, int movieId);
    }
}