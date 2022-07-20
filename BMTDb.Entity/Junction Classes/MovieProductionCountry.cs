namespace BMTDb.Entity
{
    public class MovieProductionCountry
    {
        public int ProductionCountryId { get; set; }
        public ProductionCountry? ProductionCountries { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
