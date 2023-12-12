#pragma warning disable IDE0052 // Remove unread private members
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using BMTDb.Service.Abstract;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BMTDb.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IConfiguration configuration)
        {
            _logger = logger;
            _movieService = movieService;
            _configuration = configuration;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string apiKey = _configuration["ApiKeys:TmdbApiKey"];

            var baseAddress = new Uri("http://api.themoviedb.org/3/");
            using var httpClient = new HttpClient { BaseAddress = baseAddress };

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
            if (apiKey != null)
            {
                using var responsePopular = await httpClient.GetAsync("movie/popular?api_key=" + apiKey + "&language=en-US&page=1");
                using var responseUpcoming = await httpClient.GetAsync("movie/upcoming?api_key=" + apiKey + "&language=en-US&page=1");
                using var responseAiringTodayTv = await httpClient.GetAsync("tv/airing_today?api_key=" + apiKey + "&language=en-US&page=1");
                using var responseOnAirTv = await httpClient.GetAsync("tv/on_the_air?api_key=" + apiKey + "&language=en-US&page=2");

                string apiResponsePopular = await responsePopular.Content.ReadAsStringAsync();
                string apiResponseUpcoming = await responseUpcoming.Content.ReadAsStringAsync();
                string apiResponseAiringTodayTv = await responseAiringTodayTv.Content.ReadAsStringAsync();
                string apiResponseOnAirTv = await responseOnAirTv.Content.ReadAsStringAsync();

                TMDBApiPopular? rootObjectPopular = JsonConvert.DeserializeObject<TMDBApiPopular>(apiResponsePopular);
                TMDBApiUpcoming? rootObjectUpcoming = JsonConvert.DeserializeObject<TMDBApiUpcoming>(apiResponseUpcoming);
                TMDBApiAiringTodayTv? rootObjectAiringTodayTv = JsonConvert.DeserializeObject<TMDBApiAiringTodayTv>(apiResponseAiringTodayTv);
                TMDBApiOnAirTv? rootObjectOnAirTv = JsonConvert.DeserializeObject<TMDBApiOnAirTv>(apiResponseOnAirTv);

                TMDBListsApiResults lists = new()
                {
                    TMDBApiPopular = rootObjectPopular.results,
                    TMDBApiUpcoming = rootObjectUpcoming.results,
                    TMDBApiAiringTodayTv = rootObjectAiringTodayTv.results,
                    TMDBApiOnAirTv = rootObjectOnAirTv.results,
                    Movies = _movieService.GetByPopularity()
                };
                return View(lists);
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}