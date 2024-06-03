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
        public async Task<ActionResult<IEnumerable<Casa>>> PegarTodasCasas()
        {
            return await _context.Casas.ToListAsync();
        }

        // GET: api/Casa/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Casa>> ChecarCasaPorId(uint id)
        {
            var casa = await _context.Casas.FindAsync(id);

            if (casa == null)
            {
                return NotFound();
            }

            return casa;
        }

        // PUT: api/Casa/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCasaPorId(uint id, Casa casa)
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
                if (!VerificaCasaExiste(id))
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
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == request.UsuarioId);
            if (usuario == null)
                return NotFound("Não existe esse usuário");


            if (request.ContratoId != null)
            {
                var contrato = await _context.Contratos.FirstOrDefaultAsync(u => u.Id == request.ContratoId);
                if (contrato == null)
                    return BadRequest("Contrato não encontrado.");

                var novaCasa = new Casa
                {
                    Endereco = request.Endereco,
                    NumeroSalas = request.NumeroSalas,
                    Tipo = request.Tipo,
                    CEP = request.CEP,
                    Contrato = contrato,
                    Usuario = usuario,
                };

                _context.Casas.Add(novaCasa);

                await _context.SaveChangesAsync();

                return CreatedAtAction("ChecarCasaPorId", new { id = novaCasa.Id }, novaCasa);
            }
            else
            {

                var novaCasa = new Casa
                {
                    Endereco = request.Endereco,
                    NumeroSalas = request.NumeroSalas,
                    Tipo = request.Tipo,
                    CEP = request.CEP,
                    Usuario = usuario,
                };

                _context.Casas.Add(novaCasa);

                await _context.SaveChangesAsync();

                return CreatedAtAction("ChecarCasaPorId", new { id = novaCasa.Id }, novaCasa);
            }
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

        private bool VerificaCasaExiste(uint id)
        {
            return _context.Casas.Any(e => e.Id == id);
        }
    }
}
