using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class Cast
    {
        public int CastId { get; set; }
        public string? Name { get; set; }
        public string? ProfilePicture { get; set; }
        public int? Gender { get; set; }
        public string? Character { get; set; }
        public string? Known_for_Department { get; set; }
        public string? Original_Name { get; set; }
        public double? Popularity { get; set; }
        public int? Order { get; set; }
        public bool? Adult { get; set; }

        public List<PersonCast>? PersonCasts { get; set; }
        public List<Credit>? MovieCredits { get; set; }
    }
}
