using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoImobSystem_API.Data;
using NeoImobSystem_API.Model;

namespace NeoImobSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquilinoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public InquilinoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Inquilino
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inquilino>>> GetInquilinos()
        {
            return await _context.Inquilinos.ToListAsync();
        }

        // GET: api/Inquilino/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inquilino>> GetInquilino(uint id)
        {
            var inquilino = await _context.Inquilinos.FindAsync(id);

            if (inquilino == null)
            {
                return NotFound();
            }

            return inquilino;
        }

        // PUT: api/Inquilino/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInquilino(uint id, Inquilino inquilino)
        {
            if (id != inquilino.Id)
            {
                return BadRequest();
            }

            _context.Entry(inquilino).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InquilinoExists(id))
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

        // POST: api/Inquilino
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inquilino>> PostInquilino(Inquilino inquilino)
        {
            _context.Inquilinos.Add(inquilino);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInquilino", new { id = inquilino.Id }, inquilino);
        }

        // DELETE: api/Inquilino/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInquilino(uint id)
        {
            var inquilino = await _context.Inquilinos.FindAsync(id);
            if (inquilino == null)
            {
                return NotFound();
            }

            _context.Inquilinos.Remove(inquilino);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InquilinoExists(uint id)
        {
            return _context.Inquilinos.Any(e => e.Id == id);
        }
    }
}
