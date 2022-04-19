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
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Animals.ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animals = await _context.Animals
                .FirstOrDefaultAsync(m => m.ChipID == id);
            if (animals == null)
            {
                return NotFound();
            }

            return View(animals);
        }

        // GET: Animals/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChipID,Name,Species,Breed,Sex,Born,Weight,Height,Colour,Coat,Description,Image,ShelterId")] Animals animals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animals);
        }

        // GET: Animals/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animals = await _context.Animals.FindAsync(id);
            if (animals == null)
            {
                return NotFound();
            }
            return View(animals);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChipID,Name,Species,Breed,Sex,Born,Weight,Height,Colour,Coat,Description,Image,ShelterId")] Animals animals)
        {
            if (id != animals.ChipID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalsExists(animals.ChipID))
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
            return View(animals);
        }

        // GET: Animals/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animals = await _context.Animals
                .FirstOrDefaultAsync(m => m.ChipID == id);
            if (animals == null)
            {
                return NotFound();
            }

            return View(animals);
        }

        // POST: Animals/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animals = await _context.Animals.FindAsync(id);
            _context.Animals.Remove(animals);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalsExists(int id)
        {
            return _context.Animals.Any(e => e.ChipID == id);
        }
    }
}
