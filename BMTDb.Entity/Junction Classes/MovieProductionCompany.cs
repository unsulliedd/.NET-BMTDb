#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class MovieProductionCompany
    {
        public int ProductionCompanyId { get; set; }
        public ProductionCompany ProductionCompanies { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}