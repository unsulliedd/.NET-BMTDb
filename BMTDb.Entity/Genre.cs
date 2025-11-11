namespace BMTDb.Entity
{
    public class Genre
    {

        public int GenreId { get; set; }
        public string? Name { get; set; }

        public List<MovieGenre>? MovieGenres { get; set; }
    }
}
