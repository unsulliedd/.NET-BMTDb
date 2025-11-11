#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

namespace BMTDb.Entity.Lists
{
    public class WatchlistItem
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int WatchlistId { get; set; }
        public Watchlist Watchlist { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}