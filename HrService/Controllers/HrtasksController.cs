using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HrService.Models;
using Microsoft.AspNetCore.Authorization;

namespace HrService.Controllers
{
    [Authorize(Roles = "admin, hrSpecialist")]
    public class HrtasksController : Controller
    {
        private readonly HrDbContext _context;

        public HrtasksController(HrDbContext context)
        {
            _context = context;
        }

        // GET: Hrtasks
        public async Task<IActionResult> Index()
        {
            var hrDbContext = _context.Hrtasks.Include(h => h.IdHrSpecialistNavigation);
            return View(await hrDbContext.ToListAsync());
        }

        // GET: Hrtasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hrtask = await _context.Hrtasks
                .Include(h => h.IdHrSpecialistNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hrtask == null)
            {
                return NotFound();
            }

            return View(hrtask);
        }

        // GET: Hrtasks/Create
        public IActionResult Create()
        {
            ViewData["IdHrSpecialist"] = new SelectList(_context.HrSpecialists, "Id", "FullName");
            return View();
        }

        // POST: Hrtasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Task,Deadline,Completed,IdHrSpecialist")] Hrtask hrtask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hrtask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHrSpecialist"] = new SelectList(_context.HrSpecialists, "Id", "Id", hrtask.IdHrSpecialist);
            return View(hrtask);
        }

        // GET: Hrtasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hrtask = await _context.Hrtasks.FindAsync(id);
            if (hrtask == null)
            {
                return NotFound();
            }
            ViewData["IdHrSpecialist"] = new SelectList(_context.HrSpecialists, "Id", "FullName", hrtask.IdHrSpecialist);
            return View(hrtask);
        }

        // POST: Hrtasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Task,Deadline,Completed,IdHrSpecialist")] Hrtask hrtask)
        {
            if (id != hrtask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hrtask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HrtaskExists(hrtask.Id))
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
            ViewData["IdHrSpecialist"] = new SelectList(_context.HrSpecialists, "Id", "Id", hrtask.IdHrSpecialist);
            return View(hrtask);
        }

        // GET: Hrtasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hrtask = await _context.Hrtasks
                .Include(h => h.IdHrSpecialistNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hrtask == null)
            {
                return NotFound();
            }

            return View(hrtask);
        }

        // POST: Hrtasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hrtask = await _context.Hrtasks.FindAsync(id);
            _context.Hrtasks.Remove(hrtask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HrtaskExists(int id)
        {
            return _context.Hrtasks.Any(e => e.Id == id);
        }
    }
}
