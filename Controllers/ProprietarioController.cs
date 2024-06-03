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
    public class ProprietarioController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProprietarioController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Proprietario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proprietario>>> GetProprietarios()
        {
            return await _context.Proprietarios.ToListAsync();
        }

        // GET: api/Proprietario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proprietario>> GetProprietario(uint id)
        {
            var proprietario = await _context.Proprietarios.FindAsync(id);

            if (proprietario == null)
            {
                return NotFound();
            }

            return proprietario;
        }

        // PUT: api/Proprietario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProprietario(uint id, Proprietario proprietario)
        {
            if (id != proprietario.Id)
            {
                return BadRequest();
            }

            _context.Entry(proprietario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProprietarioExists(id))
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

        // POST: api/Proprietario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proprietario>> PostProprietario(Proprietario proprietario)
        {
            _context.Proprietarios.Add(proprietario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProprietario", new { id = proprietario.Id }, proprietario);
        }

        // DELETE: api/Proprietario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProprietario(uint id)
        {
            var proprietario = await _context.Proprietarios.FindAsync(id);
            if (proprietario == null)
            {
                return NotFound();
            }

            _context.Proprietarios.Remove(proprietario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProprietarioExists(uint id)
        {
            return _context.Proprietarios.Any(e => e.Id == id);
        }
    }
}
