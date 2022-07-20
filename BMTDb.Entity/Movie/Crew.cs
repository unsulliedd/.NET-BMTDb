using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class Crew
    {
        public int CrewId { get; set; }
        public string? Name { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Known_for_Department { get; set; }
        public int? Gender { get; set; }
        public string? Original_Name { get; set; }
        public string? Department { get; set; }
        public string? Job { get; set; }
        public double? Popularity { get; set; }
        public bool? Adult { get; set; }

        public List<PersonCrew>? PersonCrews { get; set; }
        public List<Credit>? MovieCredits { get; set; }
    }
}