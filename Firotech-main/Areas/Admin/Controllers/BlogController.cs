using Firotechbd.Areas.Admin.Models;
using Firotechbd.Areas.Admin.ViewModels;
using Firotechbd.Data;
using Firotechbd.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Firotechbd.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class BlogController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly FirotechbdContext _context;
        public BlogController(SignInManager<IdentityUser> signInManager,
            FirotechbdContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _signInManager = signInManager;
            _fileUploadHelper = new FileUploadHelper(webHostEnvironment);
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("BlogLandingPageThumb")]
        public IActionResult LandPageBlogPart()
        {
            return View();
        }
        [Route("BlogList")]
        public IActionResult ListBlog()
        {
            return View();
        }
        [Route("NewBlogCreate")]
        public IActionResult CreateNewBlog()
        {
            ///var viewModel = PrepareBlogPostVM();
            return View();
        }

        [HttpPost]
        [Route("NewBlogCreate")]
        public async Task<IActionResult> CreateNewBlog(BlogPostVM blogPost)
        {
            return RedirectToAction("Index");
        }
         

        [Route("SingleBlog")]
        public IActionResult SingleBlogView(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var blogPost = _context.BlogPosts.FirstOrDefault(b => b.Id == id);

            // Check if the blog post exists
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

    }
}
