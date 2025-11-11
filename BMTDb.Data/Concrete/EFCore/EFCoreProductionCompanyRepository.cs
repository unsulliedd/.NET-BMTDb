using BMTDb.Data.Abstract;
using BMTDb.Entity;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EFCoreProductionCompanyRepository : EFCoreGenericRepository<ProductionCompany>, IProductionCompanyRepository
    {
        public EFCoreProductionCompanyRepository(BMTDbContext context) : base(context)
        {

        }
    }
}