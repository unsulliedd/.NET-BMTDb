#pragma warning disable IDE1006 // Naming Styles

namespace BMTDb.WebUI.Models
{
    public class TMDbApiPersonModel
    {
        public bool? adult { get; set; }
        public List<string>? also_known_as { get; set; }
        public string? biography { get; set; }
        public string? birthday { get; set; }
        public string? deathday { get; set; }
        public int? gender { get; set; }
        public string? homepage { get; set; }
        public int? id { get; set; }
        public string? imdb_id { get; set; }
        public string? known_for_department { get; set; }
        public string? name { get; set; }
        public string? place_of_birth { get; set; }
        public double? popularity { get; set; }
        public string? profile_path { get; set; }
    }

    public class Cast
    {
        public bool? adult { get; set; }
        public int? gender { get; set; }
        public int id { get; set; }
        public string? known_for_department { get; set; }
        public string? name { get; set; }
        public string? original_name { get; set; }
        public double? popularity { get; set; }
        public string? profile_path { get; set; }
        public int cast_id { get; set; }
        public string? character { get; set; }
        public string? credit_id { get; set; }
        public int? order { get; set; }
    }

    public class Root
    {
        public List<Cast>? cast { get; set; }
        public List<Crew>? crew { get; set; }
        public int id { get; set; }
    }

    public class Crew
    {
        public bool? adult { get; set; }
        public int? gender { get; set; }
        public int id { get; set; }
        public string? known_for_department { get; set; }
        public string? name { get; set; }
        public string? original_name { get; set; }
        public double? popularity { get; set; }
        public string? profile_path { get; set; }
        public string? credit_id { get; set; }
        public string? department { get; set; }
        public string? job { get; set; }
    }

    public class IdList
    {
        public bool? adult { get; set; }
        public int id { get; set; }
        public string? name { get; set; }
        public double? popularity { get; set; }
    }
}
