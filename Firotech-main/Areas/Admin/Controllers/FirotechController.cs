using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace Firotechbd.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FirotechController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public FirotechController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
         
        public IActionResult Index()
        {
            return View();
        }

    }
}
