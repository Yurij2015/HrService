﻿using System;
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
    public class EmployeeDirectorsController : Controller
    {
        private readonly HrDbContext _context;

        public EmployeeDirectorsController(HrDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeDirectors
        public async Task<IActionResult> Index(int divisionNumber)
        {

            var hrDbContext = from e in _context.EmployeeDirectors.Include(e => e.IdDivisionNavigation).Include(e => e.IdPositionNavigation).Include(e => e.Employees) select e;

            if (divisionNumber != 0)
            {
                hrDbContext = hrDbContext.Where(s => s.IdDivision == divisionNumber);
            }

            ViewBag.divisionList = await _context.Divisions.ToListAsync();
            ViewBag.divisionSelected = _context.Divisions.Where(d => d.Id == divisionNumber);
            return View(await hrDbContext.ToListAsync());
        }

        // GET: EmployeeDirectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDirector = await _context.EmployeeDirectors
                .Include(e => e.IdDivisionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeDirector == null)
            {
                return NotFound();
            }

            return View(employeeDirector);
        }

        // GET: EmployeeDirectors/Create
        public IActionResult Create()
        {
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id");
            return View();
        }

        // POST: EmployeeDirectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SecondName,MiddleName,BirthData,Phone,Email,IdPosition,IdDivision,IdUser")] EmployeeDirector employeeDirector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeDirector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id", employeeDirector.IdDivision);
            return View(employeeDirector);
        }

        // GET: EmployeeDirectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDirector = await _context.EmployeeDirectors.FindAsync(id);
            if (employeeDirector == null)
            {
                return NotFound();
            }
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id", employeeDirector.IdDivision);
            return View(employeeDirector);
        }

        // POST: EmployeeDirectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SecondName,MiddleName,BirthData,Phone,Email,IdPosition,IdDivision,IdUser")] EmployeeDirector employeeDirector)
        {
            if (id != employeeDirector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDirector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDirectorExists(employeeDirector.Id))
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
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id", employeeDirector.IdDivision);
            return View(employeeDirector);
        }

        // GET: EmployeeDirectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDirector = await _context.EmployeeDirectors
                .Include(e => e.IdDivisionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeDirector == null)
            {
                return NotFound();
            }

            return View(employeeDirector);
        }

        // POST: EmployeeDirectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeDirector = await _context.EmployeeDirectors.FindAsync(id);
            _context.EmployeeDirectors.Remove(employeeDirector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDirectorExists(int id)
        {
            return _context.EmployeeDirectors.Any(e => e.Id == id);
        }
    }
}
