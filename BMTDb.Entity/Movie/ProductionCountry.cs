using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class ProductionCountry
    {
        public string? ProductionCountryId { get; set; }
        public string? Name { get; set; }
    
        public List<MovieProductionCountry>? MovieProductionCountries { get; set; }
    }
}