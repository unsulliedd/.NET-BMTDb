namespace BMTDb.Entity
{
    public class ProductionCountry
    {
        public string? ProductionCountryId { get; set; }
        public string? Name { get; set; }

        public List<MovieProductionCountry>? MovieProductionCountries { get; set; }
    }
}