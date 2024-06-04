using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoImobSystem_API.Data;
using NeoImobSystem_API.DTO;
using NeoImobSystem_API.Model;
using Serilog;

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
        public async Task<ActionResult<IEnumerable<Inquilino>>> ListagemInquilinos()
        {
            return await _context.Inquilinos
                .Include(i => i.Usuario)
                .ToListAsync();
        }

        // GET: api/Inquilino/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inquilino>> ChecarInquilinoPorId(uint id)
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
        public async Task<IActionResult> EditarInquilino(uint id, Inquilino inquilino)
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
                if (!VerificaInquilino(id))
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
        [HttpPost]
        public async Task<ActionResult<Inquilino>> CriarInquilino(CriarInquilinoDTO request)
        {

            var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);

            if (usuario == null)
                return NotFound("Não existe esse usuário.");

            var inquilino = await _context.Inquilinos.Where(i => i.CPF.Equals(request.CPF)).FirstOrDefaultAsync();

            if (inquilino != null)
                return Conflict("Já existe um inquilino com o mesmo CPF.");


            var novoInquilino = new Inquilino
            {
                Nome = request.Nome,
                Email = request.Email,
                Telefone = request.Telefone,
                CPF = request.CPF,
                DataNascimento = request.DataNascimento,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                UsuarioId = request.UsuarioId
            };

            _context.Inquilinos.Add(novoInquilino);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ChecarInquilinoPorId", new { id = novoInquilino.Id }, novoInquilino);
        }

        // DELETE: api/Inquilino/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirInquilino(uint id)
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

        private bool VerificaInquilino(uint id)
        {
            return _context.Inquilinos.Any(e => e.Id == id);
        }
    }
}
