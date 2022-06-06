using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Abstract
{
    public interface IMovieRepository:IRepository<Movie>
    {
        Movie GetMovieDetails(int id);
        List<Movie> GetMoviebyFilter(string name, string Studio_Name, int page, int pageSize);
        int GetCountbyFilter(string genre, string studio);
    }
}
