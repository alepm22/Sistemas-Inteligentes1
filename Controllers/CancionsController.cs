using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spotify.Models.db;

namespace Spotify.Controllers
{
    public class CancionsController : Controller
    {
        private readonly spotifyContext _context;

        public CancionsController(spotifyContext context)
        {
            _context = context;
        }

        // GET: Cancions
        public async Task<IActionResult> Index(string searchString)
        {
            var canciones = from c in _context.Cancions select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                canciones = canciones.Where(c =>
                    c.Titulo.Contains(searchString) ||
                    c.Interprete.Contains(searchString) ||
                    c.Album.Contains(searchString) ||
                    c.Genero.Contains(searchString)
                );
            }
                return View(await canciones.ToListAsync());

        }

        // GET: Cancions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cancions == null)
            {
                return NotFound();
            }

            var cancion = await _context.Cancions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancion == null)
            {
                return NotFound();
            }

            return View(cancion);
        }

        // GET: Cancions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cancions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Interprete,Album,Genero,Anio,Portada")] Cancion cancion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cancion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cancion);
        }

        // GET: Cancions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cancions == null)
            {
                return NotFound();
            }

            var cancion = await _context.Cancions.FindAsync(id);
            if (cancion == null)
            {
                return NotFound();
            }
            return View(cancion);
        }

        // POST: Cancions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Interprete,Album,Genero,Anio,Portada")] Cancion cancion)
        {
            if (id != cancion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cancion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancionExists(cancion.Id))
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
            return View(cancion);
        }

        // GET: Cancions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cancions == null)
            {
                return NotFound();
            }

            var cancion = await _context.Cancions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancion == null)
            {
                return NotFound();
            }

            return View(cancion);
        }

        // POST: Cancions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cancions == null)
            {
                return Problem("Entity set 'spotifyContext.Cancions'  is null.");
            }
            var cancion = await _context.Cancions.FindAsync(id);
            if (cancion != null)
            {
                _context.Cancions.Remove(cancion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancionExists(int id)
        {
          return (_context.Cancions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
