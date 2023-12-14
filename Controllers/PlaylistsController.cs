using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spotify.Models.db;
using Spotify.Models;

namespace Spotify.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly spotifyContext _context;

        public PlaylistsController(spotifyContext context)
        {
            _context = context;
        }

        // GET: Playlists
    public async Task<IActionResult> Index(int? userId)
{
    if (userId == null)
    {
        return NotFound();
    }

    var playlists = await _context.Playlists
        .Where(p => p.Idusuario == userId)
        .Include(p => p.IdusuarioNavigation)
        .ToListAsync();

    int totalSongs = await _context.Cancions.CountAsync();

    // Obtén todas las canciones
    var allSongs = await _context.Cancions.ToListAsync();

    // Baraja las canciones de manera aleatoria
    var shuffledSongs = allSongs.OrderBy(c => Guid.NewGuid()).ToList();

    // Toma las primeras 6 canciones (o menos si no hay suficientes)
    var randomSongs = shuffledSongs.Take(6).ToList();
    var viewModel = new IndexViewModel
    {
        Playlists = playlists,
        RandomSongs = randomSongs
    };

    return View(viewModel);
}

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.IdusuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }
        // GET: Playlists/Create
        public IActionResult Create()
        {
            ViewData["Idusuario"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idusuario,Nombre,Descripcion")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idusuario"] = new SelectList(_context.Usuarios, "Id", "Id", playlist.Idusuario);
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewData["Idusuario"] = new SelectList(_context.Usuarios, "Id", "Id", playlist.Idusuario);
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idusuario,Nombre,Descripcion")] Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.Id))
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
            ViewData["Idusuario"] = new SelectList(_context.Usuarios, "Id", "Id", playlist.Idusuario);
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.IdusuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Playlists == null)
            {
                return Problem("Entity set 'spotifyContext.Playlists'  is null.");
            }
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(int id)
        {
          return (_context.Playlists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // GET: Playlists/ViewSongs/5
        public async Task<IActionResult> ViewSongs(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.IdusuarioNavigation)
                .Include(p => p.Idcancions) // Incluye las canciones relacionadas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }
        [HttpPost]
    public async Task<IActionResult> AddToPlaylist(int? cancionId, int? playlistId)
    {
        if (cancionId == null || playlistId == null)
        {
            return NotFound();
        }

        var playlist = await _context.Playlists
            .Include(p => p.Idcancions)
            .FirstOrDefaultAsync(p => p.Id == playlistId);

        if (playlist == null)
        {
            return NotFound();
        }

        var cancion = await _context.Cancions.FindAsync(cancionId);
        if (cancion != null)
        {
            // Asegúrate de que la canción no esté ya en la playlist
            if (!playlist.Idcancions.Any(c => c.Id == cancionId))
            {
                playlist.Idcancions.Add(cancion);
                await _context.SaveChangesAsync();
            }
        }
        // Redirige a la página de index o a donde desees
        return RedirectToAction("Index", new { userId = playlist.Idusuario });
    }


    }
}
