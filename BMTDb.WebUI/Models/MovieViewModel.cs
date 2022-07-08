#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Entity;

namespace BMTDb.WebUI.Models
{
    public class PageInfo
    {
        public int TotalItem { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentGenre { get; set; }
        public string CurrentStudio { get; set; }
        public string SortOrder { get; set; }
        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItem / ItemPerPage);
        }
    }
    public class MovieViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
