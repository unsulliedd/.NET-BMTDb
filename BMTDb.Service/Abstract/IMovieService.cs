using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Abstract
{
    public interface IMovieService
    {
        Movie GetMovieDetails(int id);
        List<Movie> GetMoviebyFilter(string name, string Studio_Name);
        Movie GetById(int id);
        List<Movie> GetAll();
        void Create(Movie entity);
        void Update(Movie entity);
        void Delete(Movie entity);
    }
}