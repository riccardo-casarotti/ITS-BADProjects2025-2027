using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeStoresProject.Models;

namespace BikeStoresProject.Controllers
{
    public class StaffsController : Controller
    {
        private readonly BikeStoresContext _context;

        public StaffsController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index(string? searchTesto)
        {
            var lista = _context.Staffs.AsQueryable();

            if (!string.IsNullOrEmpty(searchTesto))
            {
                lista = lista.Where(s =>
                    s.FirstName.Contains(searchTesto) ||
                    s.LastName.Contains(searchTesto));
            }

            ViewBag.SearchTesto = searchTesto;

            return View(await lista.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .Include(s => s.Manager)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        
    }
}
