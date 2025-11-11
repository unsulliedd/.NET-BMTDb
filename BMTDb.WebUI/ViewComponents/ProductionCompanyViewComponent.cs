#pragma warning disable IDE0044 // Make field readonly

using BMTDb.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.ViewComponents
{
    public class ProductionCompanyViewComponent : ViewComponent
    {
        private IProductionCompanyService _productionCompanyService;

        public ProductionCompanyViewComponent(IProductionCompanyService productionCompanyService)
        {
            _productionCompanyService = productionCompanyService;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["studio"] != null)
                ViewBag.SelectedStudio = RouteData?.Values["studio"];
            return View(_productionCompanyService.GetAll());
        }
    }
}