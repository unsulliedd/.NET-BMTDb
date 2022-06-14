using BMTDb.Entity;
using System.ComponentModel.DataAnnotations;

namespace BMTDb.WebUI.Models
{
    public class AdminMovieModel
    {
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Movie Title Cannot be Empty")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Director Cannot be Empty")]
        public string? Director { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Movie Info Cannot be Empty")]
        [MinLength(50, ErrorMessage = "Minumum Character More Than 50")]
        public string? MovieInfo { get; set; }
        [RegularExpression(@"^.*www.youtube.com/embed/.*$", ErrorMessage = "Url Must Contains \"embed\" link")]
        [Url (ErrorMessage = "Must be HTTPS Url")]
        public string? Trailer { get; set; }
        public string? MoviePoster { get; set; }
        public string? MovieBackdrop { get; set; }
        public string? MovieLogo { get; set; }
        public string? MovieTagline { get; set; }
        public string? MovieRatings { get; set; }
        public int? RunTime { get; set; }
        public int? Budget { get; set; }
        public string? Status { get; set; }
        [RegularExpression(@"^tt[0-9]*$", ErrorMessage ="IMDb Id must start with \"tt\" and follows with number")]
        public string? IMDBId { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "TMDb Id must be number")]
        public string? TMDbId { get; set; }
        
        public List<Studio>? SelectedStudios { get; set; }
        public List<Genre>? SelectedGenres { get; set; }
    }
}
