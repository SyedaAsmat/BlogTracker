using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppServiceLayer.Data;
using AppServiceLayer.Models;

namespace AppServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogInfosController : ControllerBase
    {
        private readonly AppServiceLayerDbContext _context;

        public BlogInfosController(AppServiceLayerDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogInfos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogInfo>>> GetBlogInfo()
        {
          if (_context.BlogInfo == null)
          {
              return NotFound();
          }
            return await _context.BlogInfo.ToListAsync();
        }

        // GET: api/BlogInfos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogInfo>> GetBlogInfo(int id)
        {
          if (_context.BlogInfo == null)
          {
              return NotFound();
          }
            var blogInfo = await _context.BlogInfo.FindAsync(id);

            if (blogInfo == null)
            {
                return NotFound();
            }

            return blogInfo;
        }

        // PUT: api/BlogInfos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogInfo(int id, BlogInfo blogInfo)
        {
            if (id != blogInfo.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blogInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogInfos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogInfo>> PostBlogInfo(BlogInfo blogInfo)
        {
          if (_context.BlogInfo == null)
          {
              return Problem("Entity set 'AppServiceLayerDbContext.BlogInfo'  is null.");
          }
            _context.BlogInfo.Add(blogInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogInfo", new { id = blogInfo.BlogId }, blogInfo);
        }

        // DELETE: api/BlogInfos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogInfo(int id)
        {
            if (_context.BlogInfo == null)
            {
                return NotFound();
            }
            var blogInfo = await _context.BlogInfo.FindAsync(id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            _context.BlogInfo.Remove(blogInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogInfoExists(int id)
        {
            return (_context.BlogInfo?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}
