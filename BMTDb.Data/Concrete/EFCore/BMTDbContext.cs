using BMTDb.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class BMTDbContext:DbContext
    {
        //For Movies
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Studio>? Studios { get; set; }


        //For Tv
        public DbSet<TvShow>? TvShows { get; set; }
        public DbSet<Season>? Seasons { get; set; }
        public DbSet<Episode>? Episodes { get; set; }
        public DbSet<Network>? Networks { get; set; }

        //For People
        public DbSet<Person>? Persons { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)       
        {
            /*Connection String*/
            optionsBuilder.UseSqlServer
            (@"Server=(LocalDB)\MSSQLLocalDB; 
                AttachDbFilename=C:\Users\berkk\Documents\Visual Studio 2022\Databases\BMTDb.MovieDb.mdf;
                Database=BMTDb.MovieDb;
                Integrated Security=TRUE"
            );
            /*Connection String*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)  //Fluent Api
        {
            modelBuilder.Entity<MovieCrew>()
                .HasKey(c => new { c.MovieId, c.PersonId });
            modelBuilder.Entity<TvShowCrew>()
                .HasKey(c => new { c.TvShowId, c.PersonId });
            modelBuilder.Entity<MovieGenre>()
                .HasKey(c => new { c.MovieId, c.GenreId });
            modelBuilder.Entity<MovieStudio>()
                .HasKey(c => new { c.MovieId, c.StudioId });
            modelBuilder.Entity<TvShowGenre>()
                .HasKey(c => new { c.TvShowId, c.GenreId });
            modelBuilder.Entity<TvShowNetwork>()
                .HasKey(c => new { c.TvShowId, c.NetworkId });
            modelBuilder.Entity<TvShowSeason>()
                .HasKey(c => new { c.TvShowId, c.SeasonId });
            modelBuilder.Entity<SeasonEpisode>()
                .HasKey(c => new { c.SeasonId, c.EpisodeId });
        }

    }
}
