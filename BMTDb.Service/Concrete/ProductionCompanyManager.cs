using BMTDb.Data.Abstract;
using BMTDb.Entity;
using BMTDb.Service.Abstract;

namespace BMTDb.Service.Concrete
{
    public class ProductionCompanyManager : IProductionCompanyService
    {
        private readonly IUnitofWork _unitofWork;

        public ProductionCompanyManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void Create(ProductionCompany entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductionCompany entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductionCompany entity)
        {
            throw new NotImplementedException();
        }

        public List<ProductionCompany> GetAll()
        {
            return _unitofWork.ProductionCompanies.GetAll();
        }

        public ProductionCompany GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
