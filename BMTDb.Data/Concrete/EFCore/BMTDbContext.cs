using BMTDb.Entity;
using BMTDb.Entity.Lists;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class BMTDbContext : DbContext
    {
        public BMTDbContext(DbContextOptions<BMTDbContext> options) : base(options)
        {

        }

        //Movies
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Credit>? Credits { get; set; }
        public DbSet<Cast>? Casts { get; set; }
        public DbSet<Crew>? Crews { get; set; }
        public DbSet<ProductionCompany>? ProductionCompanies { get; set; }
        public DbSet<ProductionCountry>? ProductionCountries { get; set; }

        //People
        public DbSet<Person>? Persons { get; set; }

        //Lists
        public DbSet<Watchlist>? Watchlists { get; set; }
        public DbSet<WatchlistItem>? WatchlistItems { get; set; }
        public DbSet<Favourite>? Favourites { get; set; }
        public DbSet<FavouriteItem>? FavouriteItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                .HasKey(c => new { c.MovieId, c.GenreId });
            modelBuilder.Entity<MovieProductionCompany>()
                .HasKey(c => new { c.MovieId, c.ProductionCompanyId });
            modelBuilder.Entity<MovieProductionCountry>()
                .HasKey(c => new { c.MovieId, c.ProductionCountryId });
            modelBuilder.Entity<Credit>()
                .HasKey(c => new { c.MovieId, c.Id });
            modelBuilder.Entity<Credit>()
                .HasKey(c => new {c.CastId, c.Id});
            modelBuilder.Entity<Credit>()
                .HasKey(c => new { c.CrewId, c.Id });
            modelBuilder.Entity<PersonCast>()
                .HasKey(c => new { c.PersonId, c.CastId });
            modelBuilder.Entity<PersonCrew>()
                .HasKey(c => new { c.PersonId, c.CrewId });
        }
    }
}