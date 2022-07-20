using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Abstract
{
    public interface IUnitofWork : IDisposable
    {
        IMovieRepository Movies { get; }
        IGenreRepository Genres { get; }
        IProductionCompanyRepository ProductionCompanies { get; }
        IPersonRepository Persons { get; }
        IWatchlistRepository Watchlists { get; }
        IFavouriteRepository Favourites { get; }
        void Save();
    }
}