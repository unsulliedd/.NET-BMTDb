#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using BMTDb.Data.Concrete.EFCore;
using BMTDb.Entity;
using BMTDb.Service.Abstract;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BMTDb.WebUI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IConfiguration _configuration;

        public MovieController(IMovieService movieService, IConfiguration configuration)
        {
            _movieService = movieService;
            _configuration = configuration;
        }

        public IActionResult Index(string genre, string studio, string sortOrder, int page = 1)
        {
            const int pageSize = 20;
            var movieViewModel = new MovieViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItem = _movieService.GetCountbyFilter(genre, studio),
                    CurrentPage = page,
                    ItemPerPage = pageSize,
                    CurrentGenre = genre,
                    CurrentStudio = studio,
                    SortOrder = sortOrder
                },
                Movies = _movieService.GetMoviebyFilter(genre, studio, sortOrder, page, pageSize)
            };
            return View(movieViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            Movie movies = _movieService.GetMovieDetails((int)id);
            string? ImdbId = movies?.IMDBId;
            string? TmdbId = movies?.TMDbId;

            string OMDBapiKey = _configuration["ApiKeys:OmdbApiKey"];
            string TMDBapiKey = _configuration["ApiKeys:TmdbApiKey"];

            var baseAddressOMDB = new Uri("http://www.omdbapi.com/");
            var baseAddressTMDB = new Uri("http://api.themoviedb.org/3/");

            using var httpClientOMDB = new HttpClient { BaseAddress = baseAddressOMDB };
            using var httpClientTMDB = new HttpClient { BaseAddress = baseAddressTMDB };

            httpClientOMDB.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
            httpClientTMDB.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

            if (string.IsNullOrEmpty(ImdbId) && TmdbId != null)
            {
                using var responseTMDB = await httpClientTMDB.GetAsync("movie/" + TmdbId + "?api_key=" + TMDBapiKey + "&language=en-US");
                string TMDBapiResponse = await responseTMDB.Content.ReadAsStringAsync();

                TMDbApiMovieDetail? TMDBrootObject = JsonConvert.DeserializeObject<TMDbApiMovieDetail>(TMDBapiResponse);    //Deserialize TMDb

                var entity = _movieService.GetMovieDetails((int)id);
                entity.IMDBId = TMDBrootObject.imdb_id;
                _movieService.Update(entity);

                using var responseOMDB = await httpClientOMDB.GetAsync("?apikey=" + OMDBapiKey + "&i=" + TMDBrootObject.imdb_id);
                string OMDBapiResponse = await responseOMDB.Content.ReadAsStringAsync();

                OMDBApiResponse? OMDBrootObject = JsonConvert.DeserializeObject<OMDBApiResponse>(OMDBapiResponse);  //Deserialize OMdb

                return View(new MovieDetailModel
                {
                    OMDBApiResponse = OMDBrootObject,
                    TMDbApiMovieDetail = TMDBrootObject,
                    Movie = movies,
                    Genres = movies.MovieGenres.Select(i => i.Genre).ToList(),
                    ProductionCompanies = movies.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),
                    Casts = movies.MovieCredits.Select(i => i.Casts).ToList()
                });
            }
            else if (string.IsNullOrEmpty(TmdbId))
            {
                return View(new MovieDetailModel
                {
                    Movie = movies,
                    Genres = movies.MovieGenres.Select(i => i.Genre).ToList(),
                    ProductionCompanies = movies.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),
                    Casts = movies.MovieCredits.Select(i => i.Casts).ToList()
                });
            }
            else
            {
                using var responseOMDB = await httpClientOMDB.GetAsync("?apikey=" + OMDBapiKey + "&i=" + ImdbId);
                using var responseTMDB = await httpClientTMDB.GetAsync("movie/" + TmdbId + "?api_key=" + TMDBapiKey + "&append_to_response=videos,images,credits&language=en");

                string OMDBapiResponse = await responseOMDB.Content.ReadAsStringAsync();
                string TMDBapiResponse = await responseTMDB.Content.ReadAsStringAsync();

                OMDBApiResponse? OMDBrootObject = JsonConvert.DeserializeObject<OMDBApiResponse>(OMDBapiResponse);
                TMDbApiMovieDetail? TMDBrootObject = JsonConvert.DeserializeObject<TMDbApiMovieDetail>(TMDBapiResponse);

                return View(new MovieDetailModel
                {
                    OMDBApiResponse = OMDBrootObject,
                    TMDbApiMovieDetail = TMDBrootObject,
                    Movie = movies,
                    Genres = movies.MovieGenres.Select(i => i.Genre).ToList(),
                    ProductionCompanies = movies.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),
                    Casts = movies.MovieCredits.Select(i => i.Casts).ToList()
                });
            }
        }

        public IActionResult Search(string q)
        {
            var movieViewModel = new MovieViewModel()
            {
                Movies = _movieService.GetSearchResult(q)
            };

            return View(movieViewModel);
        }
    }
}