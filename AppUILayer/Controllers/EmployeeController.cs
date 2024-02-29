using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APPUILayer.Data;
using APPUILayer.Models;

namespace BlogTracker.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly APPUIDbContext _context;
        //private APPUIDbContext dbContext;

        public EmployeeController(APPUIDbContext context)//, APPUIDbContext dbContext, EmpInfo empData)
        {
           // this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
              return _context.EmpInfo != null ? 
                          View(await _context.EmpInfo.ToListAsync()) :
                          Problem("Entity set 'BlogTrackerDbContext.EmpInfo'  is null.");
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Employee/Create
        
        public async Task<IActionResult> BlogList()
        {
            string s = "Blog List For: ria@gmail.com";
            ViewBag.msg = s;
            return _context.BlogInfo != null ?
                        View(await _context.BlogInfo.ToListAsync()) :
                       Problem("Entity set 'BlogTrackerDbContext.BlogInfo'  is null.");
        }
        public IActionResult SaveBlog()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveBlog([Bind("BlogId,Title,Subject,DateOfCreation,BlogUrl,EmpEmailId")] BlogInfo blogInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("BlogList", "Employee");
            }
            return View(blogInfo);
        }
        public async Task<IActionResult> EditBlog(int? id)
        {
            if (id == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo.FindAsync(id);
            if (blogInfo == null)
            {
                return NotFound();
            }
            return View(blogInfo);
        }

        // POST: BlogInfos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBlog(int id, [Bind("BlogId,Title,Subject,DateOfCreation,BlogUrl,EmpEmailId")] BlogInfo blogInfo)
        {
            if (id != blogInfo.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogInfoExists(blogInfo.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("BlogList", "Employee");
            }
            return View(blogInfo);
        }
        private bool BlogInfoExists(int id)
        {
            return (_context.BlogInfo?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }

        // GET: BlogInfos/Delete/5
        public async Task<IActionResult> DeleteBlog(int? id)
        {
            if (id == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            return View(blogInfo);
        }

        // POST: BlogInfos/Delete/5
        [HttpPost, ActionName("DeleteBlog")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (_context.BlogInfo == null)
            {
                return Problem("Entity set 'BlogTrackerDbContext.BlogInfo'  is null.");
            }
            var blogInfo = await _context.BlogInfo.FindAsync(id);
            if (blogInfo != null)
            {
                _context.BlogInfo.Remove(blogInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("BlogList", "Employee");
        }
    }
}
