#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
#pragma warning disable IDE0063 // Use simple 'using' statement

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
        private readonly IMovieRepository _movieRepository;

        public string ErrorMessage {get; set;}
        public bool Validation(Movie entity)
        {
            var isValid = true;

            return isValid;
        }

        public MovieManager(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public bool Create(Movie entity)
        {
            if (Validation(entity))
            {
                using (var context = new BMTDbContext())
                {
                    var movies = context.Movies;
                    if ((movies.Any(i => i.Title == entity.Title)) && (movies.Any(i => i.ReleaseDate == entity.ReleaseDate)))
                    {
                        ErrorMessage = "Movie is Already Exist";
                        return false;
                    }
                    _movieRepository.Create(entity);
                    return true;
                }
            }
            return false;
        }

        public void Delete(Movie entity)
        {
            _movieRepository.Delete(entity);
        }

        public void Update(Movie entity)
        {
            _movieRepository.Update(entity);
        }

        public bool Update(Movie entity, int[] genreIds, int[] studioIds, int[] crewIds)
        {
            if (Validation(entity))
            {
                if (studioIds.Length >= 0 && studioIds.Length <= 1)
                {
                    _movieRepository.Update(entity, genreIds, studioIds, crewIds);
                    return true;
                }
                ErrorMessage = "Select Only One Studio";
                return false;
            }
            return false;
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