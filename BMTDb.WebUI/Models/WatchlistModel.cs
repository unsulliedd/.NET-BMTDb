#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Entity;

namespace BMTDb.WebUI.Models
{
    public class WatchlistModel
    {
        public int WatchlistId { get; set; }
        public List<WatchlistItemModel> WatchlistItems { get; set; }
    }

    public class WatchlistItemModel
    {
        public int WatchlistItemId { get; set; }
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? MoviePoster { get; set; }
        public string? MovieBackdrop { get; set; }
        public string? MovieInfo { get; set; }
        public string? MovieTagline { get; set; }
        public string? Director { get; set; }
        public string? MovieRatings { get; set; }
        public string? Status { get; set; }
        public int? RunTime { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public List<Genre> Genres { get; set; }
        public List<Studio> Studios { get; set; }
    }
}
