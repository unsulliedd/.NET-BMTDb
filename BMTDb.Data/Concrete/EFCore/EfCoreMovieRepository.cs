#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.

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
        public Movie GetMovieDetails(int id)
        {
            using var context = new BMTDbContext();

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
    }
}
