using BMTDb.Data.Abstract;
using BMTDb.Entity;
using BMTDb.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieManager(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public void Create(Movie entity)
        {
            _movieRepository.Create(entity);
        }

        public void Delete(Movie entity)
        {
            _movieRepository.Delete(entity);
        }

        public void Update(Movie entity)
        {
            _movieRepository.Update(entity);
        }
        public void Update(Movie entity, int[] genreIds, int[] studioIds, int[] crewIds)
        {
            _movieRepository.Update(entity, genreIds, studioIds, crewIds);
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }

        public Movie GetById(int id)
        {
            return _movieRepository.GetById(id);
        }

        public Movie GetMovieDetails(int id)
        {
            return _movieRepository.GetMovieDetails(id);
        }

        public List<Movie> GetMoviebyFilter(string name, string Studio_Name, int page, int pageSize)
        {
            return _movieRepository.GetMoviebyFilter(name, Studio_Name, page, pageSize);
        }

        public List<Movie> GetMovies(int page, int pageSize)
        {
            return _movieRepository.GetMovies(page, pageSize);
        }

        public int GetCountbyFilter(string genre, string studio)
        {
            return _movieRepository.GetCountbyFilter(genre, studio);
        }
        public int GetMovieCount()
        {
            return _movieRepository.GetMovieCount();
        }

        public List<Movie> GetSearchResult(string searchString)
        {
            return _movieRepository.GetSearchResult(searchString);
        }

    }
}
