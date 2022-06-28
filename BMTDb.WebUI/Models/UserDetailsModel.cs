#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.ComponentModel.DataAnnotations;

namespace BMTDb.WebUI.Models
{
    public class UserDetailsModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Username Cannot be Empty")]
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        public string? ProfilePic { get; set; }
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [Required(ErrorMessage = "Email Cannot be Empty")]
        public string Email { get; set; }
    }
}
