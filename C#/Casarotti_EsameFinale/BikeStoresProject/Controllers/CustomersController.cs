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
    public class CustomersController : Controller
    {
        private readonly BikeStoresContext _context;

        public CustomersController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string? searchTesto)
        {
            var lista = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchTesto))
            {
                lista = lista.Where(c =>
                    c.FirstName.Contains(searchTesto) ||
                    c.LastName.Contains(searchTesto));
            }

            ViewBag.SearchTesto = searchTesto;

            return View(await lista.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

       
    }
}
