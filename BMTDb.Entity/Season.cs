using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class Season
    {
        public int SeasonId { get; set; }
        public string? Air_Date{ get; set; }
        public string? Name { get; set; }
        public string? Season_poster { get; set; }
        public int? Season_Number { get; set; }
    }
}