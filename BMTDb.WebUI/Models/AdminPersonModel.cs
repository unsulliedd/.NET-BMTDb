using System.ComponentModel.DataAnnotations;

namespace BMTDb.WebUI.Models
{
    public class AdminPersonModel
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Biography { get; set; }
        [RegularExpression(@"^nm[0-9]*$", ErrorMessage = "IMDb Id must start with \"nm\" and follows with number")]
        public string? Imdb_Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Deathday { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Job { get; set; }
    }
}
