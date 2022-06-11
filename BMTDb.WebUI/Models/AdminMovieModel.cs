#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Entity;
using System.ComponentModel.DataAnnotations;

namespace BMTDb.WebUI.Models
{
    public class AdminMovieModel
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Director { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? MovieInfo { get; set; }
        public string? Trailer { get; set; }
        public string? MoviePoster { get; set; }
        public string? MovieBackdrop { get; set; }
        public string? MovieLogo { get; set; }
        public string? MovieTagline { get; set; }
        public string? MovieRatings { get; set; }
        public int? RunTime { get; set; }
        public int? Budget { get; set; }
        public string? Status { get; set; }
        public string? IMDBId { get; set; }
        public string? TMDbId { get; set; }

        public List<Studio> SelectedStudios { get; set; }
        public List<Genre> SelectedGenres { get; set; }
        public List<Person> SelectedCrews { get; set; }
    }
}
