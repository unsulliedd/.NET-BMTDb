using BMTDb.Data.Abstract;
using BMTDb.Entity;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EfCoreGenreRepository : EFCoreGenericRepository<Genre>, IGenreRepository
    {
        public EfCoreGenreRepository(BMTDbContext context) : base(context)
        {

        }
    }
}