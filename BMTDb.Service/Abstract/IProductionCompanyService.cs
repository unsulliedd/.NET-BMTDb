using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
