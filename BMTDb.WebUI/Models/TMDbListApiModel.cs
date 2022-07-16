#pragma warning disable IDE1006 // Naming Styles

using BMTDb.Entity;

namespace BMTDb.WebUI.Models
{
    public class ListsMovieResult
    {
        public bool adult { get; set; }
        public string? backdrop_path { get; set; }
        public List<int>? genre_ids { get; set; }
        public int id { get; set; }
        public string? original_language { get; set; }
        public string? original_title { get; set; }
        public string? overview { get; set; }
        public double? popularity { get; set; }
        public string? poster_path { get; set; }
        public string? release_date { get; set; }
        public string? title { get; set; }
        public bool video { get; set; }
        public double? vote_average { get; set; }
        public int? vote_count { get; set; }
    }

    public class ListTvResult
    {
        public string? backdrop_path { get; set; }
        public string? first_air_date { get; set; }
        public List<int>? genre_ids { get; set; }
        public int id { get; set; }
        public string? name { get; set; }
        public List<string>? origin_country { get; set; }
        public string? original_language { get; set; }
        public string? original_name { get; set; }
        public string? overview { get; set; }
        public double? popularity { get; set; }
        public string? poster_path { get; set; }
        public double? vote_average { get; set; }
        public int? vote_count { get; set; }
    }
    public class TMDBApiOnAirTv
    {
        public int page { get; set; }
        public List<ListTvResult>? results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
    public class TMDBApiAiringTodayTv
    {
        public int page { get; set; }
        public List<ListTvResult>? results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class TMDBApiPopular
    {
        public int page { get; set; }
        public List<ListsMovieResult>? results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class TMDBApiUpcoming
    {
        public int page { get; set; }
        public List<ListsMovieResult>? results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class TMDBApiRecommendation
    {
        public int page { get; set; }
        public List<ListsMovieResult>? results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class TMDBListsApiResults
    {
        public List<ListsMovieResult>? TMDBApiUpcoming { get; set; }
        public List<ListsMovieResult>? TMDBApiPopular { get; set; }
        public List<ListTvResult>? TMDBApiOnAirTv { get; set; }
        public List<ListTvResult>? TMDBApiAiringTodayTv { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
