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
    public class StoresController : Controller
    {
        private readonly BikeStoresContext _context;

        public StoresController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index(string? searchTesto)
        {
            var lista = _context.Stores.AsQueryable();

            if (!string.IsNullOrEmpty(searchTesto))
            {
                lista = lista.Where(s =>
                    s.City.Contains(searchTesto) ||
                    s.State.Contains(searchTesto));
            }

            ViewBag.SearchTesto = searchTesto;

            return View(await lista.ToListAsync());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

       
    }
}
