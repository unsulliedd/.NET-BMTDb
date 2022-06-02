#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Entity;

namespace BMTDb.WebUI.Models
{
    public class MovieViewModel
    {
        public List<Movie> Movies { get; set; }
    }
}
