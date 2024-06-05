using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoImobSystem_API.Data;
using NeoImobSystem_API.DTO;
using NeoImobSystem_API.Model;

namespace NeoImobSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasaController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CasaController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Casa
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Casa>>> ListagemCasas()
        {
            return await _context.Casas
                .Include(c => c.Contrato)
                .Include(c => c.CasaProprietarios)
                .Include(c => c.Usuario)
                .ToListAsync();
        }

        // GET: api/Casa/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Casa>> ChecarCasaPorId(uint id)
        {
            var casa = await _context.Casas
                .Include(c => c.Contrato)
                .Include(c => c.CasaProprietarios)
                .ThenInclude(cp => cp.Proprietario)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (casa == null)
            {
                return NotFound();
            }

            return casa;
        }

        // PUT: api/Casa/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCasa(uint id, Casa casa)
        {
            if (id != casa.Id)
            {
                return BadRequest();
            }

            _context.Entry(casa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await VerificaCasa(id))
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

        // POST: api/Casa
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Casa>> CriarCasa(CriarCasaDTO request)
        {
            var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            Casa novaCasa;

            if (request.ContratoId != 0)
            {
                var contrato = await _context.Contratos.FindAsync(request.ContratoId);
                if (contrato == null)
                    return BadRequest("Contrato não encontrado.");

                novaCasa = new Casa
                {
                    Endereco = request.Endereco,
                    NumeroSalas = request.NumeroSalas,
                    Tipo = request.Tipo,
                    CEP = request.CEP,
                    ProprietariosId = request.ProprietariosId,
                    Contrato = contrato,
                    Usuario = usuario,
                };
            }
            else
            {
                novaCasa = new Casa
                {
                    Endereco = request.Endereco,
                    NumeroSalas = request.NumeroSalas,
                    Tipo = request.Tipo,
                    CEP = request.CEP,
                    Usuario = usuario,
                };
            }

            foreach (var proprietarioId in request.ProprietariosId)
            {
                novaCasa.CasaProprietarios.Add(new CasaProprietario
                {
                    ProprietarioId = proprietarioId
                });
            }

            _context.Casas.Add(novaCasa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ChecarCasaPorId", new { id = novaCasa.Id }, novaCasa);
        }

        // DELETE: api/Casa/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCasa(uint id)
        {
            var casa = await _context.Casas.FindAsync(id);
            if (casa == null)
            {
                return NotFound();
            }

            _context.Casas.Remove(casa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> VerificaCasa(uint id)
        {
            return await _context.Casas.AnyAsync(e => e.Id == id);
        }
    }
}
