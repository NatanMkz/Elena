using Microsoft.AspNetCore.Mvc;
using Elena.Application.DTOs;
using Elena.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Elena.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LutadoresController : ControllerBase
{
    private readonly ElenaContext _context;

    public LutadoresController(ElenaContext context)
    {
        _context = context;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<IEnumerable<LutadorDto>>> Listar()
    {
        var lutadores = await _context.Lutadores
            .AsNoTracking()
            .Select(l => new LutadorDto
            {
                Id = l.Id,
                Nome = l.Nome,
                Nacionalidade = l.Nacionalidade,
                EstiloDeLuta = l.EstiloDeLuta,
                TierAtual = l.TierAtual,
                ImagemUrl = l.ImagemUrl
            })
            .ToListAsync();

        return Ok(lutadores);
    }

    [HttpGet("Procurar")]
    public async Task<ActionResult<IEnumerable<LutadorDto>>> Procurar(
        [FromQuery] string? nome,
        [FromQuery] string? nacionalidade,
        [FromQuery] string? estilo,
        [FromQuery] string? tier)
    {
        var query = _context.Lutadores.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(l => l.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(nacionalidade))
            query = query.Where(l => l.Nacionalidade.Contains(nacionalidade));

        if (!string.IsNullOrWhiteSpace(estilo))
            query = query.Where(l => l.EstiloDeLuta.Contains(estilo));

        if (!string.IsNullOrWhiteSpace(tier))
            query = query.Where(l => l.TierAtual == tier);

        var resultado = await query
            .AsNoTracking()
            .Select(l => new LutadorDto
            {
                Id = l.Id,
                Nome = l.Nome,
                Nacionalidade = l.Nacionalidade,
                EstiloDeLuta = l.EstiloDeLuta,
                TierAtual = l.TierAtual,
                ImagemUrl = l.ImagemUrl
            })
            .ToListAsync();

        return Ok(resultado);
    }
}

