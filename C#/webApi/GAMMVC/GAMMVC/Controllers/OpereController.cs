using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAMMVC.Models;

namespace GAMMVC.Controllers
{
    public class OpereController : Controller
    {
        private readonly GamContext _context;

        public OpereController(GamContext context)
        {
            _context = context;
        }

        // GET: Opere

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var gamContext = _context.Opere
                .Include(o => o.IdAutoreNavigation)
                .Include(o => o.Materiali)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                gamContext = gamContext.Where(o =>
                    o.TitoloSoggetto!.Contains(searchString) ||
                    o.AmbitoCulturale!.Contains(searchString) ||
                    (o.IdAutoreNavigation != null && o.IdAutoreNavigation.Nominativo!.Contains(searchString)));
            }

            return View(await gamContext.Take(20).ToListAsync());
        }

        // GET: Opere/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opera = await _context.Opere
                .Include(o => o.IdAutoreNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opera == null)
            {
                return NotFound();
            }

            return View(opera);
        }

        // GET: Opere/Create
        public IActionResult Create()
        {
            ViewData["IdAutore"] = new SelectList(_context.Autori, "Id", "Id");
            return View();
        }

        // POST: Opere/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Inventario,IdAutore,AmbitoCulturale,Datazione,TitoloSoggetto,Immagine,Lsreferenceby")] Opera opera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutore"] = new SelectList(_context.Autori, "Id", "Id", opera.IdAutore);
            return View(opera);
        }

        // GET: Opere/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opera = await _context.Opere.FindAsync(id);
            if (opera == null)
            {
                return NotFound();
            }
            ViewData["IdAutore"] = new SelectList(_context.Autori, "Id", "Id", opera.IdAutore);
            return View(opera);
        }

        // POST: Opere/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Inventario,IdAutore,AmbitoCulturale,Datazione,TitoloSoggetto,Immagine,Lsreferenceby")] Opera opera)
        {
            if (id != opera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperaExists(opera.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutore"] = new SelectList(_context.Autori, "Id", "Id", opera.IdAutore);
            return View(opera);
        }

        // GET: Opere/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opera = await _context.Opere
                .Include(o => o.IdAutoreNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opera == null)
            {
                return NotFound();
            }

            return View(opera);
        }

        // POST: Opere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opera = await _context.Opere.FindAsync(id);
            if (opera != null)
            {
                _context.Opere.Remove(opera);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperaExists(int id)
        {
            return _context.Opere.Any(e => e.Id == id);
        }
    }
}
