using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APPUILayer.Models;
using APPUILayer.Data;

namespace BlogTracker.Controllers
{
    public class AdminController : Controller
    {
        private readonly APPUIDbContext _context;

        public AdminController(APPUIDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        //Handling employee data
        public async Task<IActionResult> EmployeeList()
        {
            return _context.EmpInfo != null ?
                        View(await _context.EmpInfo.ToListAsync()) :
                        Problem("Entity set 'BlogTrackerDbContext.EmpInfo'  is null.");
        }
        public IActionResult SaveEmployee()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEmployee([Bind("EmpId,EmailId,Name,DateOfJoining,PassCode")] EmpInfo empInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("EmployeeList", "Admin");
        }
        public async Task<IActionResult> EditEmployee(int? id)
        {
            if (id == null || _context.EmpInfo == null)
            {
                return NotFound();
            }

            var empInfo = await _context.EmpInfo.FindAsync(id);
            if (empInfo == null)
            {
                return NotFound();
            }
            return View(empInfo);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(int id, [Bind("EmpId,EmailId,Name,DateOfJoining,PassCode")] EmpInfo empInfo)
        {
            if (id != empInfo.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (EmpInfoExists(empInfo.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("EmployeeList","Admin");
            }
            return View(empInfo);
        }

        private bool EmpInfoExists(int id)
        {
            return (_context.EmpInfo?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (id == null || _context.EmpInfo == null)
            {
                return NotFound();
            }

            var empInfo = await _context.EmpInfo
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (empInfo == null)
            {
                return NotFound();
            }

            return View(empInfo);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.EmpInfo == null)
            {
                return Problem("Entity set 'BlogTrackerDbContext.EmpInfo'  is null.");
            }
            var empInfo = await _context.EmpInfo.FindAsync(id);
            if (empInfo != null)
            {
                _context.EmpInfo.Remove(empInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("EmployeeList","Admin");
        }
    }
}
