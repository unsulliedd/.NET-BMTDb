#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class Credit
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movies { get; set; }
        public int? CastId { get; set; }
        public Cast Casts { get; set; }
        public int? CrewId { get; set; }
        public Crew Crews { get; set; }
    }
}