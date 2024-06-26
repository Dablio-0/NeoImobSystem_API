﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoImobSystem_API.Data;
using NeoImobSystem_API.DTO;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<Proprietario>>> ListagemProprietarios()
        {
            return await _context.Proprietarios
                .Include(p => p.Usuario)
                .ToListAsync();
        }

        // GET: api/Proprietario/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Proprietario>> ChecarProprietarioPorId(uint id)
        {
            var proprietario = await _context.Proprietarios.FindAsync(id);

            if (proprietario == null)
            {
                return NotFound();
            }

            return proprietario;
        }

        // PUT: api/Proprietario/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarProprietario(uint id, Proprietario proprietario)
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
                if (!VerificaProprietario(id))
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Proprietario>> CriarProprietario(CriarProprietarioDTO request)
        {
            var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);

            if (usuario == null)
                return NotFound("Não existe esse usuário.");

            var proprietario = await _context.Proprietarios.Where(i => i.CPF.Equals(request.CPF)).FirstOrDefaultAsync();

            if (proprietario != null)
                return Conflict("Já existe um proprietario com o mesmo CPF.");

            var novoProprietario = new Proprietario
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

            _context.Proprietarios.Add(novoProprietario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ChecarProprietarioPorId", new { id = novoProprietario.Id }, novoProprietario);
        }

        // DELETE: api/Proprietario/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProprietario(uint id)
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

        private bool VerificaProprietario(uint id)
        {
            return _context.Proprietarios.Any(e => e.Id == id);
        }
    }
}
