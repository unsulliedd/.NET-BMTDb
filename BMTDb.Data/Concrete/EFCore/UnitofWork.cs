#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Data.Abstract;

namespace BMTDb.Data.Concrete.EFCore
{
    public class UnitofWork : IUnitofWork
    {
        public readonly BMTDbContext _context;
        public UnitofWork(BMTDbContext context)
        {
            _context = context;
        }

        private EfCoreMovieRepository _movieRepository;
        private EfCoreGenreRepository _genreRepository;
        private EFCoreProductionCompanyRepository _productionCompanyRepository;
        private EFCorePersonRepository _personRepository;
        private EFCoreWatchlistRepository _watchlistRepository;
        private EFCoreFavouriteRepository _favouriteRepository;

        public IMovieRepository Movies =>
            _movieRepository ??= new EfCoreMovieRepository(_context);

        public IGenreRepository Genres =>
            _genreRepository ??= new EfCoreGenreRepository(_context);

        public IProductionCompanyRepository ProductionCompanies =>
            _productionCompanyRepository ??= new EFCoreProductionCompanyRepository(_context);

        public IPersonRepository Persons =>
            _personRepository ??= new EFCorePersonRepository(_context);

        public IWatchlistRepository Watchlists =>
            _watchlistRepository ??= new EFCoreWatchlistRepository(_context);

        public IFavouriteRepository Favourites =>
            _favouriteRepository ??= new EFCoreFavouriteRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
