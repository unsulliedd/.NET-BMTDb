using BMTDb.Entity.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Abstract
{
    public interface IWatchlistRepository : IRepository<Watchlist>
    {
        Watchlist GetByUserId(string userId);
    }
}
