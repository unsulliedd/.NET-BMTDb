#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

namespace BMTDb.WebUI.Identity
{
    public class UserActivity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Data { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.Now;
    }
}