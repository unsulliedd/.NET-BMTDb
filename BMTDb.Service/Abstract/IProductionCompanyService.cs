using BMTDb.Entity;

namespace BMTDb.Service.Abstract
{
    public interface IProductionCompanyService
    {
        ProductionCompany GetById(int id);
        List<ProductionCompany> GetAll();
        void Create(ProductionCompany entity);
        void Update(ProductionCompany entity);
        void Delete(ProductionCompany entity);
    }
}
