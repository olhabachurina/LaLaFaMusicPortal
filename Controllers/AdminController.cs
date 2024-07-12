using LaLaFaMusicPortal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaLaFaMusicPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly MusicPortalContext _context;

        public AdminController(MusicPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> ManageGenres()
        {
            return View(await _context.Genres.ToListAsync());
        }

        public async Task<IActionResult> ManageSongs()
        {
            return View(await _context.Songs.Include(s => s.Genre).Include(s => s.User).ToListAsync());
        }
    }
}