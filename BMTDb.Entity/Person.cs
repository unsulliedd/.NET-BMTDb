using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class Person
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? Biography { get; set; }
        public string? Imdb_Id { get; set; }
        public string? Birthday { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Job { get; set; }
    }
}
