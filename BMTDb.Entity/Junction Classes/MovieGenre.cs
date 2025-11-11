#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

namespace BMTDb.Entity
{
    public class MovieGenre
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
