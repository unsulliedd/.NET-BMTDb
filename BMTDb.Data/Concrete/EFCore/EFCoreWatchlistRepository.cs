using BMTDb.Data.Abstract;
using BMTDb.Entity.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EFCoreWatchlistRepository : EFCoreGenericRepository<Watchlist, BMTDbContext>, IWatchlistRepository
    {
    }
}
