using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppServiceLayer.Data;
using AppServiceLayer.Models;

namespace AppServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpInfosController : ControllerBase
    {
        private readonly AppServiceLayerDbContext _context;

        public EmpInfosController(AppServiceLayerDbContext context)
        {
            _context = context;
        }

        // GET: api/EmpInfos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpInfo>>> GetEmpInfo()
        {
          if (_context.EmpInfo == null)
          {
              return NotFound();
          }
            return await _context.EmpInfo.ToListAsync();
        }

        // GET: api/EmpInfos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpInfo>> GetEmpInfo(int id)
        {
          if (_context.EmpInfo == null)
          {
              return NotFound();
          }
            var empInfo = await _context.EmpInfo.FindAsync(id);

            if (empInfo == null)
            {
                return NotFound();
            }

            return empInfo;
        }

        // PUT: api/EmpInfos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpInfo(int id, EmpInfo empInfo)
        {
            if (id != empInfo.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(empInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpInfoExists(id))
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

        // POST: api/EmpInfos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpInfo>> PostEmpInfo(EmpInfo empInfo)
        {
          if (_context.EmpInfo == null)
          {
              return Problem("Entity set 'AppServiceLayerDbContext.EmpInfo'  is null.");
          }
            _context.EmpInfo.Add(empInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpInfo", new { id = empInfo.EmpId }, empInfo);
        }

        // DELETE: api/EmpInfos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpInfo(int id)
        {
            if (_context.EmpInfo == null)
            {
                return NotFound();
            }
            var empInfo = await _context.EmpInfo.FindAsync(id);
            if (empInfo == null)
            {
                return NotFound();
            }

            _context.EmpInfo.Remove(empInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpInfoExists(int id)
        {
            return (_context.EmpInfo?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }
    }
}
