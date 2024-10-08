using Microsoft.AspNetCore.Mvc;

namespace Firotechbd.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ILogger<PortfolioController> _logger;

        public PortfolioController(ILogger<PortfolioController> logger)
        {
            _logger = logger;
        }
        public IActionResult Ecommerce()
        {
            return View();
        }
        
        public IActionResult Pos()
        {
            return View();
        }
        
        public IActionResult Inventory()
        {
            return View();
        }
        public IActionResult Website1()
        {
            return View();
        }
        public IActionResult WebsiteRestaurant()
        {
            return View();
        }
    }
}
