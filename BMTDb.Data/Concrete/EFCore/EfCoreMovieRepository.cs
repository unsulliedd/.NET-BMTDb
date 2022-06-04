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
    public class EfCoreMovieRepository : EFCoreGenericRepository<Movie, BMTDbContext>, IMovieRepository
    {
        public List<Movie> GetMoviebyFilter(string name, string? Studio_Name)
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

                return movies.ToList();
            }
        }

        public Movie GetMovieDetails(string Title)
        {
            using var context = new BMTDbContext();

            return context.Movies
                            .Where(i => i.Title == Title)
                            .Include(i => i.MovieGenres)
                            .ThenInclude(i => i.Genre)
                            .Include(i => i.MovieCrews)
                            .ThenInclude(i => i.Person)
                            .Include(i => i.MovieStudios)
                            .ThenInclude(i => i.Studios)
                            .FirstOrDefault();
        }
    }
}
