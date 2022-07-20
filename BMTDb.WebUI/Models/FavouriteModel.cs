#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Entity;

namespace BMTDb.WebUI.Models
{
    public class FavouriteModel
    {
        public int FavouriteId { get; set; }
        public List<FavouriteItemModel> FavouriteItems { get; set; }
    }

    public class FavouriteItemModel
    {
        public int FavouriteItemId { get; set; }
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? MoviePoster { get; set; }
        public string? MovieBackdrop { get; set; }
        public string? MovieInfo { get; set; }
        public string? MovieTagline { get; set; }
        public string? Director { get; set; }
        public double? MovieRatings { get; set; }
        public string? Status { get; set; }
        public int? RunTime { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public List<Genre> Genres { get; set; }
        public List<ProductionCompany> ProductionCompanies { get; set; }
    }
}
