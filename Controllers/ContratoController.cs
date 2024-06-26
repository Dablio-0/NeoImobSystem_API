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
    public class ContratoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ContratoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Contrato
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contrato>>> ListagemContratos()
        {
            return await _context.Contratos
                .Include(c => c.Casa)
                .Include(c => c.ContratoInquilinos)
                .Include(c => c.Usuario)
                .ToListAsync();
        }

        // GET: api/Contrato/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Contrato>> ChecarContratoPorId(uint id)
        {
            var contrato = await _context.Contratos.FindAsync(id);

            if (contrato == null)
            {
                return NotFound();
            }

            return contrato;
        }

        // PUT: api/Contrato/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContrato(uint id, Contrato contrato)
        {
            if (id != contrato.Id)
            {
                return BadRequest();
            }

            _context.Entry(contrato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificaContrato(id))
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

        // POST: api/Contrato
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Contrato>> CriarContrato(CriarContratoDTO request)
        {
            if (request == null)
            {
                return BadRequest("O corpo da requisição não pode ser nulo");
            }

            // Verificar se o usuário existe
            var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);
            if (usuario == null)
            {
                return NotFound("Não existe esse usuário");
            }

            // Verificar se a casa existe
            var casa = await _context.Casas.FindAsync(request.CasaId);
            if (casa == null)
            {
                return NotFound("Não existe essa casa");
            }

            // Verificar se a casa já está associada a um contrato
            if (casa.ContratoId.HasValue)
            {
                return Conflict("Esta casa já pertence a outro contrato");
            }

            // Verificar se a lista de inquilinos foi fornecida e não está vazia
            if (request.InquilinosId == null || !request.InquilinosId.Any())
            {
                return BadRequest("É necessário fornecer pelo menos um inquilino para criar um contrato");
            }

            // Verificar se todos os inquilinos existem
            foreach (var inquilinoId in request.InquilinosId)
            {
                var inquilinoExists = await _context.Inquilinos.FindAsync(inquilinoId);
                if (inquilinoExists == null)
                {
                    return BadRequest("Um ou mais inquilinos não existem");
                }
            }

            // Calcular o período em dias, horas, e minutos entre Inicio e Fim
            DateTime inicio = request.Inicio;
            DateTime fim = request.Fim;
            if (fim < inicio)
            {
                return BadRequest("A data de término deve ser posterior à data de início");
            }

            TimeSpan periodoTotal = fim - inicio;

            // Criar um DateTime que representa o período a partir de uma data de referência
            DateTime periodoData = new DateTime(1, 1, 1).Add(periodoTotal);

            var novoContrato = new Contrato
            {
                Descricao = request.Descricao,
                Status = request.Status,
                Valor = request.Valor,
                Parcelas = request.Parcelas,
                Inicio = request.Inicio,
                Fim = request.Fim,
                Periodo = periodoData,
                Casa = casa,
                InquilinosId = request.InquilinosId,
                Usuario = usuario,
            };

            foreach (var inquilinoId in request.InquilinosId)
            {
                novoContrato.ContratoInquilinos.Add(new ContratoInquilino
                {
                    InquilinoId = inquilinoId
                });
            }

            _context.Contratos.Add(novoContrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ChecarContratoPorId", new { id = novoContrato.Id }, novoContrato);
        }

        // DELETE: api/Contrato/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirContrato(uint id)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }

            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VerificaContrato(uint id)
        {
            return _context.Contratos.Any(e => e.Id == id);
        }
    }
}
