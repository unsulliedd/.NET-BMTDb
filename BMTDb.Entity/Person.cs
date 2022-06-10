using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class Person
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Biography { get; set; }
        public string? Imdb_Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Deathday { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Job { get; set; }

        public List<MovieCrew>? MovieCrews { get; set; }
        public List<TvShowCrew>? TvCrews { get; set; }
    }
}
