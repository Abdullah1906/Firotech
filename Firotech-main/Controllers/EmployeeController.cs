using Firotechbd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firotechbd.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly FirotechbdContext _context;
        public EmployeeController(ILogger<EmployeeController> logger, FirotechbdContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> ViewProfile(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            // Fetch the employee profile by slug
            var employeeProfile = await _context.EmployeePubProfile.FirstOrDefaultAsync(e => e.Slug == slug && e.IsActive == true);

            if (employeeProfile == null)
            {
                return NotFound();
            }

            return View(employeeProfile); // Return the profile view with the employee data
        }



        // GET: Employee/ArifulIslam
        //[Route("ArifulIslam")]
        //[Route("arifulislamakash")]
        //[Route("akash")]
        //public ActionResult ArifulIslam()
        //{
        //    return View();
        //}

        [Route("sheikhnody")]
        public ActionResult Sunjeedanody()
        {
            return View();
        }
        [Route("rajibsourov")]
        public ActionResult RajibSourov()
        {
            return View();
        }

        [Route("ahanafshoran")]
        public ActionResult Ahanafshoran()
        {
            return View();
        }
        [Route("samiulalim")]
        public ActionResult SamiulAlim()
        {
            return View();
        }
        [Route("abdullahabir")]
        public ActionResult AbdullahAbir()
        {
            return View();
        }
        [Route("jerinakter")]
        public ActionResult JerinAkter()
        {
            return View();
        }
        
    }
}
