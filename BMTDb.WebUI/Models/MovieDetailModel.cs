#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using BMTDb.Entity;

namespace BMTDb.WebUI.Models
{
    public class MovieDetailModel
    {
        public Movie Movie { get; set; }
        public OMDBApiResponse OMDBApiResponse { get; set; }
        public TMDbApiMovieDetail TMDbApiMovieDetail { get; set; }
        public List<Genre> Genres { get; set; }
        public List<ProductionCompany> ProductionCompanies { get; set; }
        public List<Cast> Casts { get; set; }
    }
}