#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using BMTDb.Data.Abstract;
using BMTDb.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EfCoreMovieRepository : EFCoreGenericRepository<Movie>, IMovieRepository
    {
        public EfCoreMovieRepository(BMTDbContext context) : base(context)
        {

        }
        private BMTDbContext BMTDbContext
        {
            get { return context as BMTDbContext; }
        }

        public int GetCountbyFilter(string genre, string studio)
        {
            var movies = BMTDbContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies
                            .Include(i => i.MovieGenres)
                            .ThenInclude(i => i.Genre)
                            .Where(i => i.MovieGenres.Any(a => a.Genre.Name.ToLower() == genre.ToLower()));
            }

            if (studio != null)
            {
                movies = movies
                            .Include(i => i.MovieProductionCompanies)
                            .ThenInclude(i => i.ProductionCompanies)
                            .Where(i => i.MovieProductionCompanies
                                    .Any(a => a.ProductionCompanies.Name.ToLower() == studio.ToLower()));
            }

            return movies.Count();
        }
        

        public List<Movie> GetMoviebyFilter(string name, string Studio_Name, string sortOrder, int page, int pageSize)
        {
            var movies = BMTDbContext.Movies.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                movies = movies
                            .Include(i => i.MovieGenres)          
                            .ThenInclude(i => i.Genre)            
                            .Where(i => i.MovieGenres.Any(a => a.Genre.Name.ToLower() == name.ToLower()));
            }

            if (Studio_Name != null)
            {
                movies = movies
                            .Include(i => i.MovieProductionCompanies)
                            .ThenInclude(i => i.ProductionCompanies)
                            .Where(i => i.MovieProductionCompanies
                                    .Any(a => a.ProductionCompanies.Name.ToLower() == Studio_Name.ToLower()));
            }

            switch (sortOrder)
            {
                case "Name_Ascending":
                    return movies.OrderBy(i => i.Title).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                case "Name_Descending":
                    return movies.OrderByDescending(i => i.Title).Skip((page - 1) * pageSize).Take(pageSize).ToList(); 
                case "Date_Ascending":
                    return movies.OrderBy(i => i.ReleaseDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                case "Date_Descending":
                    return movies.OrderByDescending(i => i.ReleaseDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                case "Rating_Ascending":
                    return movies.OrderBy(i => i.Ratings).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                case "Rating_Descending":
                    return movies.OrderByDescending(i => i.Ratings).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                default:
                    return movies.Skip((page - 1) * pageSize).Take(pageSize).ToList(); 
            }
        }

        public Movie GetMovieDetails(int id)
        {
            var cmd = @"UPDATE Movies SET Popularity = Popularity+1 WHERE MovieId=@p0";
            BMTDbContext.Database.ExecuteSqlRaw(cmd, id);
            return BMTDbContext.Movies
                                    .Where(i => i.MovieId == id)
                                    .Include(i => i.MovieGenres)
                                    .ThenInclude(i => i.Genre)
                                    .Include(i => i.MovieCredits)
                                    .ThenInclude(i => i.Casts)
                                    .Include(i => i.MovieProductionCompanies)
                                    .ThenInclude(i => i.ProductionCompanies)
                                    .FirstOrDefault();
        }

        public List<Movie> GetSearchResult(string searchString)
        {
            var movies = BMTDbContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies
                            .Include(i => i.MovieGenres)
                            .ThenInclude(i => i.Genre)
                            .Include(i => i.MovieProductionCompanies)
                            .ThenInclude(i => i.ProductionCompanies)
                            .Where(i => i.Title.ToLower().Contains(searchString.ToLower())
                                || i.Info.ToLower().Contains(searchString.ToLower())
                                || i.MovieGenres.Any(a => a.Genre.Name.ToLower().Contains(searchString.ToLower()))
                                || i.MovieProductionCompanies
                                        .Any(a => a.ProductionCompanies.Name.ToLower().Contains(searchString.ToLower())));
            }

            return movies.ToList();
        }

        public int GetMovieCount()
        {
            var movies = BMTDbContext.Movies.AsQueryable();
            return movies.Count();
        }
        public List<Movie> GetMovies(int page, int pageSize)
        {
            var movies = BMTDbContext.Movies.AsQueryable();  
            return movies.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(Movie entity, int[] genreIds, int[] studioIds, int[] crewIds)
        {
            var movies = BMTDbContext.Movies
                .Include(i => i.MovieGenres)
                .Include(i => i.MovieProductionCompanies)
                .Include(i => i.MovieCredits)
                .FirstOrDefault(i => i.MovieId == entity.MovieId);

            if (movies != null)
            {
                movies.Title = entity.Title;
                movies.Director = entity.Director;
                movies.Tagline = entity.Tagline;
                movies.Info = entity.Info;
                movies.Poster = entity.Poster;
                movies.Backdrop = entity.Backdrop;
                movies.ReleaseDate = entity.ReleaseDate;
                movies.RunTime = entity.RunTime;
                movies.Ratings = entity.Ratings;
                movies.Budget = entity.Budget;
                movies.IMDBId = entity.IMDBId;
                movies.TMDbId = entity.TMDbId;
                movies.Logo = entity.Logo;
                movies.Trailer = entity.Trailer;

                movies.MovieGenres = genreIds.Select(sgenreid=> new MovieGenre()
                {
                    MovieId=entity.MovieId,
                    GenreId=sgenreid,

                }).ToList();

                movies.MovieProductionCompanies = studioIds.Select(sstudioid => new MovieProductionCompany()
                {
                    MovieId = entity.MovieId,
                    ProductionCompanyId = sstudioid,

                }).ToList();

                movies.MovieCredits = crewIds.Select(screwid => new Credit()
                {
                    MovieId = entity.MovieId,
                    Id = screwid,

                }).ToList();
            }
        }

        public List<Movie> GetUserMovielist(List<int> data,string username)
        {
            var movies = BMTDbContext.Movies.AsQueryable();
            Movie? movie = null;
            List<Movie>? result = new();
            if (data.Count > 100)
            {
                var cmd = @"DELETE TOP(50) from UserActivities Where UserName=@p0";
                BMTDbContext.Database.ExecuteSqlRaw(cmd, username);
            }
            if (data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    int id = data.ElementAt(data.Count - i - 1);

                    movie = BMTDbContext.Movies
                                .Where(i => i.MovieId == id).FirstOrDefault();
                    if (!result.Any(i => i.MovieId == movie.MovieId))
                    {
                        result.Add(movie);
                    }
                }
                return result.Take(15).ToList();
            }
            return null;
        }

        public void RemoveFromRecentlyViewed(string username, int movieId)
        {
            var cmd = @"DELETE from UserActivities WHERE UserName=@p0 and Data=@p1";
            BMTDbContext.Database.ExecuteSqlRaw(cmd, username, movieId);
        }

        public List<Movie> GetByPopularity()
        {
            var movies = BMTDbContext.Movies.AsQueryable();
            return movies.OrderByDescending(i => (i.Popularity)).Take(20).ToList();
        }
    }
}
