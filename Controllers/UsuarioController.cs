using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NeoImobSystem_API.Data;
using NeoImobSystem_API.DTO;
using NeoImobSystem_API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NeoImobSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CriarUsuario(CriarUsuarioDTO request)
        {
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (usuarioExistente != null)
            {
                return Conflict("Já existe um usuário com o mesmo email.");
            }

            var novoUsuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha
            };

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return Ok(novoUsuario);
        }

        [Authorize]
        [HttpGet("{UsuarioId}")]
        public async Task<ActionResult<Usuario>> ChecarUsuarioPorId(uint UsuarioId)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == UsuarioId);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(uint id, Inquilino usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificaUsuario(id))
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

        private bool VerificaUsuario(uint id)
        {
            return _context.Usuarios.Any(u => u.Id == id);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO request)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email && u.Senha == request.Senha);

            if (usuario == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtSecret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Nada específico a fazer no servidor para o logout com JWT.
            // O cliente deve simplesmente remover o token do armazenamento local.
            return Ok("Logout bem-sucedido. Remova o token do cliente.");
        }

        [HttpGet]
        public async Task<ActionResult<Usuario>> ResetarSenha(string Email)
        {

            return Ok();
        }
    }
}
