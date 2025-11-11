using BMTDb.Service.Abstract;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace BMTDb.WebUI.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger,
        IHttpClientFactory httpClientFactory,
        IMovieService movieService,
        IConfiguration configuration) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IMovieService _movieService = movieService;
        private readonly string? _apiKey = configuration["ApiKeys:TmdbApiKey"];
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<IActionResult> IndexAsync()
        {
            var lists = new TMDBListsApiResults
            {
                Movies = _movieService.GetByPopularity()
            };

            if (string.IsNullOrEmpty(_apiKey))
            {
                _logger.LogWarning("TMDB API key not configured");
                return View(lists);
            }

            try
            {
                var client = _httpClientFactory.CreateClient("Tmdb");
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

                var tasks = new[]
                {
                    FetchAsync<TMDBApiPopular>(client, "movie/popular?language=en-US&page=1"),
                    FetchAsync<TMDBApiUpcoming>(client, "movie/upcoming?language=en-US&page=1"),
                    FetchAsync<TMDBApiAiringTodayTv>(client, "tv/airing_today?language=en-US&page=1"),
                    FetchAsync<TMDBApiOnAirTv>(client, "tv/on_the_air?language=en-US&page=1")
                };

                var results = await Task.WhenAll(tasks);

                lists.TMDBApiPopular = results[0];
                lists.TMDBApiUpcoming = results[1];
                lists.TMDBApiAiringTodayTv = results[2];
                lists.TMDBApiOnAirTv = results[3];
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to fetch data from TMDB API");
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "TMDB API request timed out");
            }

            return View(lists);
        }

        private static async Task<dynamic?> FetchAsync<T>(HttpClient client, string endpoint)
        {
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);

            return (result as dynamic)?.results;
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