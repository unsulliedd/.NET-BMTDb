using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class ProductionCompany
    {
        public int ProductionCompanyId { get; set; }
        public string? Name { get; set; }
        public string? Logo_Path { get; set; }
        public string? Origin_Country { get; set; }

        public List<MovieProductionCompany>? ProductionCompanies { get; set; }
    }
}