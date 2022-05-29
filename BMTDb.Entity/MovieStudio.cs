using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class MovieStudio
    {
        public int StudioId { get; set; }
        public Studio? Studios { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}