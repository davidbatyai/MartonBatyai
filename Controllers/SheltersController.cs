using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MindenMancs.Data;
using MindenMancs.Models;

namespace MindenMancs.Controllers
{
    public class SheltersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SheltersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shelters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shelters.ToListAsync());
        }

        // GET: Shelters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelters = await _context.Shelters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelters == null)
            {
                return NotFound();
            }

            return View(shelters);
        }

        // GET: Shelters/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shelters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City,Address,Email,PhoneNumber,TaxNumber")] Shelters shelters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shelters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shelters);
        }

        // GET: Shelters/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelters = await _context.Shelters.FindAsync(id);
            if (shelters == null)
            {
                return NotFound();
            }
            return View(shelters);
        }

        // POST: Shelters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,Address,Email,PhoneNumber,TaxNumber")] Shelters shelters)
        {
            if (id != shelters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shelters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheltersExists(shelters.Id))
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
            return View(shelters);
        }

        // GET: Shelters/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelters = await _context.Shelters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelters == null)
            {
                return NotFound();
            }

            return View(shelters);
        }

        // POST: Shelters/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shelters = await _context.Shelters.FindAsync(id);
            _context.Shelters.Remove(shelters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheltersExists(int id)
        {
            return _context.Shelters.Any(e => e.Id == id);
        }
    }
}
