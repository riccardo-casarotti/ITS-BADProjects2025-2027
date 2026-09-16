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
    public class OrdersController : Controller
    {
        private readonly BikeStoresContext _context;

        public OrdersController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string? searchTesto, DateOnly? dataDa, DateOnly? dataA)
        {
            var lista = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTesto))
            {
                lista = lista.Where(o =>
                    o.OrderId.ToString().Contains(searchTesto) ||
                    o.Customer.FirstName.Contains(searchTesto) ||
                    o.Customer.LastName.Contains(searchTesto) ||
                    o.Store.StoreName.Contains(searchTesto));
            }

            if (dataDa.HasValue)
            {
                lista = lista.Where(o => o.OrderDate >= dataDa.Value);
            }

            if (dataA.HasValue)
            {
                lista = lista.Where(o => o.OrderDate <= dataA.Value);
            }

            ViewBag.SearchTesto = searchTesto;
            ViewBag.DataDa = dataDa;
            ViewBag.DataA = dataA;

            return View(await lista.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        
    }
}
