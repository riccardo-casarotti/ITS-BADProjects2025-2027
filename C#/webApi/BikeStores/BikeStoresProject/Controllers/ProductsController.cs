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
    public class ProductsController : Controller
    {
        private readonly BikeStoresContext _context;

        public ProductsController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? searchTesto, string sortOrder = "asc")
        {
            var lista = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTesto))
            {
                lista = lista.Where(p =>
                    p.ProductName.Contains(searchTesto) ||
                    p.Brand.BrandName.Contains(searchTesto) ||
                    p.Category.CategoryName.Contains(searchTesto));
            }

            lista = sortOrder == "desc"
                ? lista.OrderByDescending(p => p.ProductName)
                : lista.OrderBy(p => p.ProductName);

            ViewBag.SearchTesto = searchTesto;
            ViewBag.SortOrder = sortOrder;
            ViewBag.NextSortOrder = sortOrder == "asc" ? "desc" : "asc"; // per il bottone

            return View(await lista.ToListAsync());
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        
    }
}
