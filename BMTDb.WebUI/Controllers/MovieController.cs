using BMTDb.Entity;
using BMTDb.Service.Abstract;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BMTDb.WebUI.Controllers
{
    public class MovieController(IMovieService movieService, IHttpClientFactory httpClientFactory, IConfiguration configuration) : Controller
    {
        private readonly IMovieService _movieService = movieService;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly string? _omdbApiKey = configuration["ApiKeys:OmdbApiKey"];
        private readonly string? _tmdbApiKey = configuration["ApiKeys:TmdbApiKey"];
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public IActionResult Index(string? genre, string? studio, string? sortOrder, int page = 1)
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

            var movie = _movieService.GetMovieDetails(id.Value);
            if (movie == null)
            {
                return View("Error");
            }

            var model = new MovieDetailModel
            {
                Movie = movie,
                Genres = movie.MovieGenres.Select(i => i.Genre).ToList(),
                ProductionCompanies = movie.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),
                Casts = movie.MovieCredits.Select(i => i.Casts).ToList()
            };

            if (string.IsNullOrEmpty(_omdbApiKey) || string.IsNullOrEmpty(_tmdbApiKey))
            {
                return View(model);
            }

            try
            {
                if (string.IsNullOrEmpty(movie.IMDBId) && !string.IsNullOrEmpty(movie.TMDbId))
                {
                    await HandleMissingImdbIdAsync(movie, model);
                }
                else if (!string.IsNullOrEmpty(movie.IMDBId) && !string.IsNullOrEmpty(movie.TMDbId))
                {
                    await FetchApiDataAsync(movie.IMDBId, movie.TMDbId, model);
                }
            }
            catch (HttpRequestException)
            {
                // API data unavailable, return with database data only
            }

            return View(model);
        }

        private async Task HandleMissingImdbIdAsync(Movie movie, MovieDetailModel model)
        {
            var tmdbData = await FetchTmdbMovieAsync(movie.TMDbId!);
            if (tmdbData != null && !string.IsNullOrEmpty(tmdbData.imdb_id))
            {
                movie.IMDBId = tmdbData.imdb_id;
                _movieService.Update(movie);

                model.TMDbApiMovieDetail = tmdbData;
                model.OMDBApiResponse = await FetchOmdbDataAsync(tmdbData.imdb_id);
            }
        }

        private async Task FetchApiDataAsync(string imdbId, string tmdbId, MovieDetailModel model)
        {
            var omdbTask = FetchOmdbDataAsync(imdbId);
            var tmdbTask = FetchTmdbMovieAsync(tmdbId, "append_to_response=videos,images,credits");

            await Task.WhenAll(omdbTask, tmdbTask);

            model.OMDBApiResponse = await omdbTask;
            model.TMDbApiMovieDetail = await tmdbTask;
        }

        private async Task<OMDBApiResponse?> FetchOmdbDataAsync(string imdbId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://www.omdbapi.com/?apikey={_omdbApiKey}&i={imdbId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OMDBApiResponse>(content, _jsonOptions);
        }

        private async Task<TMDbApiMovieDetail?> FetchTmdbMovieAsync(string tmdbId, string? appendToResponse = null)
        {
            var client = _httpClientFactory.CreateClient("Tmdb");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tmdbApiKey);

            var query = string.IsNullOrEmpty(appendToResponse)
                ? $"movie/{tmdbId}?language=en-US"
                : $"movie/{tmdbId}?{appendToResponse}&language=en";

            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TMDbApiMovieDetail>(content, _jsonOptions);
        }

        public IActionResult Search(string? q)
        {
            var movieViewModel = new MovieViewModel()
            {
                Movies = _movieService.GetSearchResult(q)
            };

            return View(movieViewModel);
        }
    }
}