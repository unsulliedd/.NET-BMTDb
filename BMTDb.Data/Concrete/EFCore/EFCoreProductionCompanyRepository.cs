﻿using BMTDb.Data.Abstract;
using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EFCoreProductionCompanyRepository : EFCoreGenericRepository<ProductionCompany>,IProductionCompanyRepository
    {
        public EFCoreProductionCompanyRepository(BMTDbContext context) : base(context)
        {

        }
    }
}