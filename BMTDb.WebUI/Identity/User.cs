using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BMTDb.WebUI.Identity
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime?  Birthday { get; set; }
        public string? ProfilePic { get; set; }
    }
}
