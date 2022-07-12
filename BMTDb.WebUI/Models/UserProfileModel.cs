#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Entity;
using BMTDb.WebUI.Identity;
using System.ComponentModel.DataAnnotations;

namespace BMTDb.WebUI.Models
{
    public class UserProfileModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string? ProfilePic { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Movie> Movie { get; set; }
    }
}
