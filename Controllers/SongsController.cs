using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaLaFaMusicPortal.Data;
using LaLaFaMusicPortal.Models;

namespace LaLaFaMusicPortal.Controllers
{
    public class SongsController : Controller
    {
        private readonly MusicPortalContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SongsController(MusicPortalContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var musicPortalContext = _context.Songs.Include(s => s.Genre).Include(s => s.User);
            return View(await musicPortalContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.GenreId = new SelectList(_context.Genres, "GenreId", "Name");
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Artist,GenreId,UserId,Mood")] Song song, IFormFile songFile, IFormFile imageFile, IFormFile videoFile)
        {
            if (ModelState.IsValid)
            {
                if (songFile != null)
                {
                    song.FilePath = await SaveFileAsync(songFile);
                }

                if (imageFile != null)
                {
                    song.ImagePath = await SaveFileAsync(imageFile);
                }

                if (videoFile != null)
                {
                    song.VideoPath = await SaveFileAsync(videoFile);
                }

                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GenreId = new SelectList(_context.Genres, "GenreId", "Name", song.GenreId);
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Username", song.UserId);
            return View(song);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewBag.GenreId = new SelectList(_context.Genres, "GenreId", "Name", song.GenreId);
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Username", song.UserId);
            return View(song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongId,Title,Artist,GenreId,UserId,Mood")] Song song, IFormFile songFile, IFormFile imageFile, IFormFile videoFile)
        {
            if (id != song.SongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (songFile != null)
                    {
                        song.FilePath = await SaveFileAsync(songFile);
                    }

                    if (imageFile != null)
                    {
                        song.ImagePath = await SaveFileAsync(imageFile);
                    }

                    if (videoFile != null)
                    {
                        song.VideoPath = await SaveFileAsync(videoFile);
                    }

                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongId))
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
            ViewBag.GenreId = new SelectList(_context.Genres, "GenreId", "Name", song.GenreId);
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Username", song.UserId);
            return View(song);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Genre)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Genre)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return "/uploads/" + uniqueFileName;
        }
    }
}