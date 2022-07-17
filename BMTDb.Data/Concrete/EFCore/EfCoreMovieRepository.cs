#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable IDE0063 // Use simple 'using' statement

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
    public class EfCoreMovieRepository : EFCoreGenericRepository<Movie, BMTDbContext>, IMovieRepository
    {
        public int GetCountbyFilter(string genre, string studio)
        {
            using (var context = new BMTDbContext())
            {
                var movies = context.Movies.AsQueryable();

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
                                .Include(i => i.MovieStudios)
                                .ThenInclude(i => i.Studios)
                                .Where(i => i.MovieStudios.Any(a => a.Studios.Studio_Name.ToLower() == studio.ToLower()));
                }

                return movies.Count();
            }
        }

        public List<Movie> GetMoviebyFilter(string name, string Studio_Name, string sortOrder, int page, int pageSize)
        {
            using var context = new BMTDbContext();
            {
                var movies = context.Movies.AsQueryable();
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
                                .Include(i => i.MovieStudios)
                                .ThenInclude(i => i.Studios)
                                .Where(i => i.MovieStudios.Any(a => a.Studios.Studio_Name.ToLower() == Studio_Name.ToLower()));
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
                        return movies.OrderBy(i => i.MovieRatings).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    case "Rating_Descending":
                        return movies.OrderByDescending(i => i.MovieRatings).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    default:
                        return movies.Skip((page - 1) * pageSize).Take(pageSize).ToList(); 
                }
            }
        }

        public Movie GetMovieDetails(int id)
        {
            using var context = new BMTDbContext();
            var cmd = @"UPDATE Movies SET Popularity = Popularity+1 WHERE MovieId=@p0";
            context.Database.ExecuteSqlRaw(cmd, id);
            return context.Movies
                            .Where(i => i.MovieId == id)
                            .Include(i => i.MovieGenres)
                            .ThenInclude(i => i.Genre)
                            .Include(i => i.MovieCrews)
                            .ThenInclude(i => i.Person)
                            .Include(i => i.MovieStudios)
                            .ThenInclude(i => i.Studios)
                            .FirstOrDefault();
        }

        public List<Movie> GetSearchResult(string searchString)
        {
            using (var context = new BMTDbContext())
            {
                var movies = context.Movies.AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
                {
                    movies = movies
                                .Include(i => i.MovieGenres)
                                .ThenInclude(i => i.Genre)
                                .Include(i => i.MovieStudios)
                                .ThenInclude(i => i.Studios)
                                .Where(i => i.Title.ToLower().Contains(searchString.ToLower())
                                    || i.MovieInfo.ToLower().Contains(searchString.ToLower())
                                    || i.MovieGenres.Any(a => a.Genre.Name.ToLower().Contains(searchString.ToLower()))
                                    || i.MovieStudios.Any(a => a.Studios.Studio_Name.ToLower().Contains(searchString.ToLower())));
                }

                return movies.ToList();
            }
        }

        public int GetMovieCount()
        {
            using (var context = new BMTDbContext())
            {
                var movies = context.Movies.AsQueryable();

                return movies.Count();
            }
        }
        public List<Movie> GetMovies(int page, int pageSize)
        {
            using var context = new BMTDbContext();
            {
                var movies = context.Movies.AsQueryable();
                
                return movies.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public void Update(Movie entity, int[] genreIds, int[] studioIds, int[] crewIds)
        {
            using (var context = new BMTDbContext())
            {
                var movies = context.Movies
                    .Include(i => i.MovieGenres)
                    .Include(i => i.MovieStudios)
                    .Include(i => i.MovieCrews)
                    .FirstOrDefault(i => i.MovieId == entity.MovieId);


                if (movies != null)
                {
                    movies.Title = entity.Title;
                    movies.Director = entity.Director;
                    movies.MovieTagline = entity.MovieTagline;
                    movies.MovieInfo = entity.MovieInfo;
                    movies.MoviePoster = entity.MoviePoster;
                    movies.MovieBackdrop = entity.MovieBackdrop;
                    movies.ReleaseDate = entity.ReleaseDate;
                    movies.RunTime = entity.RunTime;
                    movies.MovieRatings = entity.MovieRatings;
                    movies.Budget = entity.Budget;
                    movies.IMDBId = entity.IMDBId;
                    movies.TMDbId = entity.TMDbId;
                    movies.MovieLogo = entity.MovieLogo;
                    movies.Trailer = entity.Trailer;

                    movies.MovieGenres = genreIds.Select(sgenreid=> new MovieGenre()
                    {
                        MovieId=entity.MovieId,
                        GenreId=sgenreid,

                    }).ToList();

                    movies.MovieStudios = studioIds.Select(sstudioid => new MovieStudio()
                    {
                        MovieId=entity.MovieId,
                        StudioId=sstudioid,

                    }).ToList();

                    movies.MovieCrews = crewIds.Select(screwid => new MovieCrew()
                    {
                        MovieId=entity.MovieId,
                        PersonId=screwid,

                    }).ToList();

                    context.SaveChanges();
                }
            }
        }

        public List<Movie> GetUserMovielist(List<int> data,string username)
        {
            using var context = new BMTDbContext();
            {
                var movies = context.Movies.AsQueryable();
                Movie? movie = null;
                List<Movie>? result = new();
                if (data.Count > 100)
                {
                    var cmd = @"DELETE TOP(50) from UserActivities Where UserName=@p0";
                    context.Database.ExecuteSqlRaw(cmd, username);
                }
                if (data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        int id = data.ElementAt(data.Count - i - 1);

                        movie = context.Movies
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
        }
        public void RemoveFromRecentlyViewed(string username, int movieId)
        {
            using (var context = new BMTDbContext())
            {
                var cmd = @"DELETE from UserActivities WHERE UserName=@p0 and Data=@p1";
                context.Database.ExecuteSqlRaw(cmd, username, movieId);
            }
        }

        public List<Movie> GetByPopularity()
        {
            using var context = new BMTDbContext();
            {
                var movies = context.Movies.AsQueryable();

                return movies.OrderByDescending(i => i.Popularity).Take(20).ToList();
            }
        }
    }
}
