#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.


namespace BMTDb.Entity
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Info { get; set; }
        public string? Trailer { get; set; }
        public string? Poster { get; set; }
        public string? Backdrop { get; set; }
        public string? Logo { get; set; }
        public string? Tagline { get; set; }
        public string? Original_Language { get; set; }
        public double? Ratings { get; set; }
        public int? RatingCount { get; set; }
        public int? RunTime { get; set; }
        public double? Budget { get; set; }
        public string? Status { get; set; }
        public string? IMDBId { get; set; }
        public string? TMDbId { get; set; }
        public double Popularity { get; set; } = 0;

        public List<MovieGenre> MovieGenres { get; set; }
        public List<Credit> MovieCredits { get; set; }
        public List<MovieProductionCompany> MovieProductionCompanies { get; set; }
        public List<MovieProductionCountry> MovieProductionCountries { get; set; }
    }
}