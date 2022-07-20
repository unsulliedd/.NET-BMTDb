#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Data.Abstract;
using BMTDb.Data.Concrete.EFCore;
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
        private readonly IUnitofWork _unitofWork;

        public MovieManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public string ErrorMessage { get; set; }

        public bool Validation(Movie entity)
        {
            var isValid = true;

            return isValid;
        }

        public bool Create(Movie entity)
        {
            if (Validation(entity))
            {
                var movies = _unitofWork.Movies.GetAll();
                if ((movies.Any(i => i.Title == entity.Title)) && (movies.Any(i => i.ReleaseDate == entity.ReleaseDate)))
                {
                    ErrorMessage = "Movie is Already Exist";
                    return false;
                }
                _unitofWork.Movies.Create(entity);
                _unitofWork.Save();
                return true;
            }
            return false;
        }

        public void Delete(Movie entity)
        {
            _unitofWork.Movies.Delete(entity);
            _unitofWork.Save();
        }

        public void Update(Movie entity)
        {
            _unitofWork.Movies.Update(entity);
            _unitofWork.Save();
        }

        public bool Update(Movie entity, int[] genreIds, int[] studioIds, int[] crewIds)
        {
            if (Validation(entity))
            {
                if (studioIds.Length >= 0 && studioIds.Length <= 1)
                {
                    _unitofWork.Movies.Update(entity, genreIds, studioIds, crewIds);
                    _unitofWork.Save();
                    return true;
                }
                ErrorMessage = "Select Only One Studio";
                return false;
            }
            return false;
        }

        public List<Movie> GetAll()
        {
            return _unitofWork.Movies.GetAll();
        }

        public Movie GetById(int id)
        {
            return _unitofWork.Movies.GetById(id);
        }

        public List<Movie> GetByPopularity()
        {
            return _unitofWork.Movies.GetByPopularity();
        }

        public Movie GetMovieDetails(int id)
        {
            return _unitofWork.Movies.GetMovieDetails(id);
        }

        public List<Movie> GetMoviebyFilter(string name, string Studio_Name, string sortOrder, int page, int pageSize)
        {
            return _unitofWork.Movies.GetMoviebyFilter(name, Studio_Name, sortOrder, page, pageSize);
        }

        public List<Movie> GetMovies(int page, int pageSize)
        {
            return _unitofWork.Movies.GetMovies(page, pageSize);
        }

        public int GetCountbyFilter(string genre, string studio)
        {
            return _unitofWork.Movies.GetCountbyFilter(genre, studio);
        }
        public int GetMovieCount()
        {
            return _unitofWork.Movies.GetMovieCount();
        }

        public List<Movie> GetSearchResult(string searchString)
        {
            return _unitofWork.Movies.GetSearchResult(searchString);
        }

        public List<Movie> GetUserMovielist(List<int> data, string username)
        {
            return _unitofWork.Movies.GetUserMovielist(data, username);
        }
        public void RemoveFromRecentlyViewed(string username, int movieId)
        {
            _unitofWork.Movies.RemoveFromRecentlyViewed(username, movieId);
        }
    }
}