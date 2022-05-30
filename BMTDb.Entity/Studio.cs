using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Entity
{
    public class Studio
    {
        public int StudioId { get; set; }
        public string? Studio_Name { get; set; }
        public string? Studio_logo { get; set; }

        public List<MovieStudio>? MovieStudios { get; set; }

    }
}