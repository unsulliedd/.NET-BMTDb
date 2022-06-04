#pragma warning disable IDE0044 // Make field readonly

using BMTDb.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.ViewComponents
{
    public class StudioViewComponent : ViewComponent
    {
        private IStudioService _studioService;

        public StudioViewComponent(IStudioService studioService)
        {
            _studioService = studioService;
        }
        public IViewComponentResult Invoke() 
        {
            return View(_studioService.GetAll());
        }
    }
}