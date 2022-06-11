using System.ComponentModel.DataAnnotations;

namespace BMTDb.WebUI.Models
{
    public class AdminPersonModel
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Biography { get; set; }
        public string? Imdb_Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Deathday { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Job { get; set; }
    }
}
