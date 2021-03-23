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
    [Authorize(Roles = "admin")]
    public class HrSpecialistsController : Controller
    {
        private readonly HrDbContext _context;

        public HrSpecialistsController(HrDbContext context)
        {
            _context = context;
        }

        // GET: HrSpecialists
        public async Task<IActionResult> Index()
        {
            var hrDbContext = _context.HrSpecialists.Include(h => h.IdDivisionNavigation).Include(h => h.IdPositionNavigation);
            return View(await hrDbContext.ToListAsync());
        }

        // GET: HrSpecialists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hrSpecialist = await _context.HrSpecialists
                .Include(h => h.IdDivisionNavigation)
                .Include(h => h.IdPositionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hrSpecialist == null)
            {
                return NotFound();
            }

            return View(hrSpecialist);
        }

        // GET: HrSpecialists/Create
        public IActionResult Create()
        {
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id");
            ViewData["IdPosition"] = new SelectList(_context.Positions, "Id", "Id");
            return View();
        }

        // POST: HrSpecialists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SecondName,MiddleName,BirthDate,Phone,Email,IdPosition,IdDivision,IdUser")] HrSpecialist hrSpecialist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hrSpecialist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id", hrSpecialist.IdDivision);
            ViewData["IdPosition"] = new SelectList(_context.Positions, "Id", "Id", hrSpecialist.IdPosition);
            return View(hrSpecialist);
        }

        // GET: HrSpecialists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hrSpecialist = await _context.HrSpecialists.FindAsync(id);
            if (hrSpecialist == null)
            {
                return NotFound();
            }
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id", hrSpecialist.IdDivision);
            ViewData["IdPosition"] = new SelectList(_context.Positions, "Id", "Id", hrSpecialist.IdPosition);
            return View(hrSpecialist);
        }

        // POST: HrSpecialists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SecondName,MiddleName,BirthDate,Phone,Email,IdPosition,IdDivision,IdUser")] HrSpecialist hrSpecialist)
        {
            if (id != hrSpecialist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hrSpecialist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HrSpecialistExists(hrSpecialist.Id))
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
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id", hrSpecialist.IdDivision);
            ViewData["IdPosition"] = new SelectList(_context.Positions, "Id", "Id", hrSpecialist.IdPosition);
            return View(hrSpecialist);
        }

        // GET: HrSpecialists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hrSpecialist = await _context.HrSpecialists
                .Include(h => h.IdDivisionNavigation)
                .Include(h => h.IdPositionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hrSpecialist == null)
            {
                return NotFound();
            }

            return View(hrSpecialist);
        }

        // POST: HrSpecialists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hrSpecialist = await _context.HrSpecialists.FindAsync(id);
            _context.HrSpecialists.Remove(hrSpecialist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HrSpecialistExists(int id)
        {
            return _context.HrSpecialists.Any(e => e.Id == id);
        }
    }
}
