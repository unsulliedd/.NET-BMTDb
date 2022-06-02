#pragma warning disable IDE0044 // Make field readonly

using BMTDb.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.ViewComponents
{
    public class GenreViewComponent : ViewComponent
    {
        private IGenreService _genreService;

        public GenreViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public IViewComponentResult Invoke() 
        {
            if (RouteData.Values["genre"] != null)
                ViewBag.SelectedGenre = RouteData?.Values["genre"];
            return View(_genreService.GetAll());
        }
    }
}
