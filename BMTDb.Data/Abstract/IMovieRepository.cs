using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Abstract
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie entity, int[] genreIds, int[] studioIds, int[] crewIds);
        Movie GetMovieDetails(int id);
        List<Movie> GetMoviebyFilter(string name, string Studio_Name, int page, int pageSize);
        List<Movie> GetMovies(int page, int pageSize);
        List<Movie> GetSearchResult(string searchString);
        int GetCountbyFilter(string genre, string studio);
        int GetMovieCount();
        
    }
}
