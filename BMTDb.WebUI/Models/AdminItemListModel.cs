#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Entity;

namespace BMTDb.WebUI.Models
{
        public class AdminPageInfo
        {
            public int TotalAdminItem { get; set; }
            public int AdminItemPerPage { get; set; }
            public int AdminCurrentPage { get; set; }
            public int TotalAdminPages()
            {
                return (int)Math.Ceiling((decimal)TotalAdminItem / AdminItemPerPage);
            }
        }
    public class AdminItemListModel
    {
        public List<Movie> Movies { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Studio> Studios { get; set; }
        public List<Person> Persons { get; set; }
        public AdminPageInfo AdminPageInfo { get; set; }
    }
}