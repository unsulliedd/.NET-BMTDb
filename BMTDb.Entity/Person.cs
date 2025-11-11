namespace BMTDb.Entity
{
    public class Person
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? Biography { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Birthday { get; set; }
        public string? Deathday { get; set; }
        public int? Gender { get; set; }
        public string? Place_of_Birth { get; set; }
        public string? Known_for_Department { get; set; }
        public string? ImdbId { get; set; }
        public string? Homepage { get; set; }
        public double? Popularity { get; set; }
        public bool? Adult { get; set; }

        public List<PersonCast>? PersonCasts { get; set; }
        public List<PersonCrew>? PersonCrews { get; set; }
    }
}
