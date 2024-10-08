using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firotechbd.Areas.Admin.Models;
using Firotechbd.Data;
using Firotechbd.Areas.Admin.ViewModels;
using Firotechbd.Services;
using System.Security.Claims;

namespace Firotechbd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LookupController : Controller
    {
        private readonly FirotechbdContext _context;

        public LookupController(FirotechbdContext context)
        {
            _context = context;
        }

        // GET: Admin/Lookup
        public async Task<IActionResult> Index()
        {
            return _context.Lookup != null ?
                        View(await _context.Lookup.ToListAsync()) :
                        Problem("Entity set 'FirotechbdContext.Lookup'  is null.");
        }

        // GET: Admin/Lookup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lookup == null)
            {
                return NotFound();
            }

            var lookup = await _context.Lookup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lookup == null)
            {
                return NotFound();
            }

            return View(lookup);
        }

        // GET: Admin/Lookup/Create

        public IActionResult Create()
        {

            return View();
        }
        //lookup
        [HttpGet]
        public IActionResult CreateLookup()
        {
            ViewBag.List = _context.Lookup
                .Where(x => x.Id > 0)
                .OrderBy(x => x.Serial)
                .ToList();

            return View("CreateLookup");
        }

        [HttpPost]
        public IActionResult CreateLookup(LookupVM model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = PopupMessage.error });
            }
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = Guid.NewGuid();
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = Guid.NewGuid();

            Lookup data = new Lookup();

            data.Type = model.Type;
            data.Name = model.Name;
            data.Value = model.Value;
            data.Serial = model.Serial;
            data.IsActive = model.IsActive;
            data.CreatedBy = model.CreatedBy;
            data.UpdatedBy = model.UpdatedBy;
            data.CreatedAt = model.CreatedAt;
            data.UpdatedAt = model.UpdatedAt;
            try
            {
                _context.Lookup.Add(data);
                _context.SaveChanges();

                var Sdata = _context.Lookup.Where(x => x.Id == data.Id).ToList();
                return Json(new { success = true, message = PopupMessage.success, data = Sdata });
            }
            catch (Exception)
            {

                return Json(new { success = false, message = PopupMessage.error });
            }

        }

        [HttpPost]
        public IActionResult DeleteLookup(int Id)
        {
            if (Id <= 0)
                return Json(new { success = false, message = PopupMessage.error });


            var data = _context.Lookup.Where(x => x.Id == Id).FirstOrDefault();

            if (data == null)
                return Json(new { success = false, message = PopupMessage.error });


            try
            {
                _context.Lookup.Remove(data);// for Lookup table
                _context.SaveChanges();
                return Json(new { success = true, message = PopupMessage.success });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = PopupMessage.error });
            }



        }
        // POST: Admin/Lookup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lookup lookup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lookup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lookup);
        }


    }
}
