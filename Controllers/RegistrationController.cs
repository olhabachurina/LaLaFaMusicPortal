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
    public class RegistrationController : Controller
    {
        private readonly MusicPortalContext _context;

        public RegistrationController(MusicPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequest registrationRequest)
        {
            if (ModelState.IsValid)
            {
                _context.RegistrationRequests.Add(registrationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(registrationRequest);
        }
    }
}