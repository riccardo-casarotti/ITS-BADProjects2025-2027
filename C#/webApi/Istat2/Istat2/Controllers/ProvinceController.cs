using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Istat2.Models;

namespace Istat2.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly IstatContext _context;

        public ProvinceController(IstatContext context)
        {
            _context = context;
        }

        // GET: Province
        public async Task<IActionResult> Index(string? searchTesto)
        {

            var lista = _context.Province.AsQueryable();
            if (searchTesto != null)
                lista = lista
                    .Where(l => l.Denominazione!.Contains(searchTesto) || l.Sigla!.Contains(searchTesto));

            ViewBag.SearchTesto = searchTesto; // Passaggio del valore di ricerca
            return View(await lista.Take(100).ToListAsync());
        }


        // GET: Province/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincium = await _context.Province
                .Include(p => p.IdRegioneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincium == null)
            {
                return NotFound();
            }

            return View(provincium);
        }

        // GET: Province/Create
        public IActionResult Create()
        {
            ViewData["IdRegione"] = new SelectList(_context.Regioni, "Id", "Id");
            return View();
        }

        // POST: Province/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Denominazione,Sigla,CodiceCittaMetropolitana,IdRegione")] Provincium provincium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provincium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRegione"] = new SelectList(_context.Regioni, "Id", "Id", provincium.IdRegione);
            return View(provincium);
        }

        // GET: Province/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincium = await _context.Province.FindAsync(id);
            if (provincium == null)
            {
                return NotFound();
            }
            ViewData["IdRegione"] = new SelectList(_context.Regioni, "Id", "Id", provincium.IdRegione);
            return View(provincium);
        }

        // POST: Province/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Denominazione,Sigla,CodiceCittaMetropolitana,IdRegione")] Provincium provincium)
        {
            if (id != provincium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provincium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinciumExists(provincium.Id))
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
            ViewData["IdRegione"] = new SelectList(_context.Regioni, "Id", "Id", provincium.IdRegione);
            return View(provincium);
        }

        // GET: Province/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincium = await _context.Province
                .Include(p => p.IdRegioneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincium == null)
            {
                return NotFound();
            }

            return View(provincium);
        }

        // POST: Province/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provincium = await _context.Province.FindAsync(id);
            if (provincium != null)
            {
                _context.Province.Remove(provincium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinciumExists(int id)
        {
            return _context.Province.Any(e => e.Id == id);
        }
    }
}
