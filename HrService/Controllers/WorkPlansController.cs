using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HrService.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HrService.Controllers
{
    //[Authorize(Roles = "admin")]
    public class WorkPlansController : Controller
    {
        private readonly HrDbContext _context;

        public WorkPlansController(HrDbContext context)
        {
            _context = context;
        }

        // GET: WorkPlans
        [Authorize(Roles = "admin, hrSpecialist, userManager")]
        public async Task<IActionResult> Index()
        {
            var hrDbContext = _context.WorkPlans.Include(w => w.IdEmployeeNavigation);
            return View(await hrDbContext.ToListAsync());
        }

        // GET: WorkPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPlan = await _context.WorkPlans
                .Include(w => w.IdEmployeeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workPlan == null)
            {
                return NotFound();
            }

            return View(workPlan);
        }

        // GET: WorkPlans/Create
        public IActionResult Create()
        {
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "Id", "FullName");
            return View();
        }

        // POST: WorkPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkTask,Deadline,Completed,Comment,IdEmployee")] WorkPlan workPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "Id", "FullName", workPlan.IdEmployee);
            return View(workPlan);
        }

        // GET: WorkPlans/Edit/5
        [Authorize(Roles = "admin, user, hrSpecialist, userManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPlan = await _context.WorkPlans.FindAsync(id);
            if (workPlan == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "Id", "FullName", workPlan.IdEmployee);
            return View(workPlan);
        }

        // POST: WorkPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin, user, hrSpecialist, userManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkTask,Deadline,Completed,Comment,IdEmployee")] WorkPlan workPlan)
        {
            if (id != workPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkPlanExists(workPlan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                if (role == "user")
                {
                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

                //return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "Employee");

            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "Id", "Id", workPlan.IdEmployee);
            return View(workPlan);
        }

        // GET: WorkPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workPlan = await _context.WorkPlans
                .Include(w => w.IdEmployeeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workPlan == null)
            {
                return NotFound();
            }

            return View(workPlan);
        }

        // POST: WorkPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workPlan = await _context.WorkPlans.FindAsync(id);
            _context.WorkPlans.Remove(workPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkPlanExists(int id)
        {
            return _context.WorkPlans.Any(e => e.Id == id);
        }
    }
}
