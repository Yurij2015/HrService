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
    public class EmployeesController : Controller
    {
        private readonly HrDbContext _context;

        public EmployeesController(HrDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int divisionNumber)
        {
            var hrDbContext = from e in _context.Employees.Include(e => e.IdDirectorNavigation).Include(e => e.IdDivisionNavigation).Include(e => e.IdPositionNavigation).Include(e => e.Training).Include(e => e.WorkPlans) select e;

            if (divisionNumber != 0)
            {
                hrDbContext = hrDbContext.Where(s => s.IdDivision == divisionNumber);
            }
            ViewBag.divisionList = await _context.Divisions.ToListAsync();
            ViewBag.divisionSelected = _context.Divisions.Where(d => d.Id == divisionNumber);
            return View(await hrDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.IdDirectorNavigation)
                .Include(e => e.IdDivisionNavigation)
                .Include(e => e.IdPositionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.userTraining = _context.Training.Where(t => t.IdEmployee == id);
            ViewBag.userWorkPlan = _context.WorkPlans.Where(t => t.IdEmployee == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["IdDirector"] = new SelectList(_context.EmployeeDirectors, "Id", "FullName");
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Name");
            ViewData["IdPosition"] = new SelectList(_context.Positions, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SecondName,MiddleName,BirthDate,Skils,Phone,IdPosition,IdUser,IdDivision,IdDirector,Satus,Email,Password,RoleId")] Employee employee, User user)
        {
            user.RoleId = 2;
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDirector"] = new SelectList(_context.EmployeeDirectors, "Id", "FullName", employee.IdDirector);
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Name", employee.IdDivision);
            ViewData["IdPosition"] = new SelectList(_context.Positions, "Id", "Name", employee.IdPosition);



            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["IdDirector"] = new SelectList(_context.EmployeeDirectors, "Id", "Id", employee.IdDirector);
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id", employee.IdDivision);
            ViewData["IdPosition"] = new SelectList(_context.Positions, "Id", "Id", employee.IdPosition);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SecondName,MiddleName,BirthDate,Skils,Phone,Email,IdPosition,IdUser,IdDivision,IdDirector,Satus")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["IdDirector"] = new SelectList(_context.EmployeeDirectors, "Id", "Id", employee.IdDirector);
            ViewData["IdDivision"] = new SelectList(_context.Divisions, "Id", "Id", employee.IdDivision);
            ViewData["IdPosition"] = new SelectList(_context.Positions, "Id", "Id", employee.IdPosition);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.IdDirectorNavigation)
                .Include(e => e.IdDivisionNavigation)
                .Include(e => e.IdPositionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
