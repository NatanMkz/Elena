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

    [HttpPost("Criar")]
    public async Task<ActionResult> Criar([FromBody] CreateLutadorDto dto)
    {
        var lutador = new Elena.Domain.Entities.Lutador(
            Guid.NewGuid(),
            dto.Nome,
            dto.Nacionalidade,
            dto.EstiloDeLuta,
            dto.Historia,
            dto.TierAtual,
            dto.AlturaCm,
            dto.PesoKg,
            dto.Forca,
            dto.Velocidade,
            dto.Tecnica,
            dto.Controle,
            dto.DificuldadeDeUso,
            dto.JogoDeOrigem,
            dto.AtivoNoJogoAtual,
            dto.ImagemUrl
        );

        _context.Lutadores.Add(lutador);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Detalhes), new { id = lutador.Id }, dto);
    }


    [HttpGet("Detalhes/{id}")]
    public async Task<ActionResult<LutadorDto>> Detalhes(Guid id)
    {
        var lutador = await _context.Lutadores
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lutador == null)
            return NotFound("Lutador não encontrado.");

        return Ok(new LutadorDto
        {
            Id = lutador.Id,
            Nome = lutador.Nome,
            Nacionalidade = lutador.Nacionalidade,
            EstiloDeLuta = lutador.EstiloDeLuta,
            TierAtual = lutador.TierAtual,
            ImagemUrl = lutador.ImagemUrl
        });
    }

    [HttpPut("Atualizar/{id}")]
    public async Task<ActionResult> Atualizar(Guid id, [FromBody] CreateLutadorDto dto)
    {
        var lutador = await _context.Lutadores.FindAsync(id);

        if (lutador == null)
            return NotFound("Lutador não encontrado.");

        lutador.Nome = dto.Nome;
        lutador.Nacionalidade = dto.Nacionalidade;
        lutador.EstiloDeLuta = dto.EstiloDeLuta;
        lutador.Historia = dto.Historia;
        lutador.TierAtual = dto.TierAtual;
        lutador.AlturaCm = dto.AlturaCm;
        lutador.PesoKg = dto.PesoKg;
        lutador.Forca = dto.Forca;
        lutador.Velocidade = dto.Velocidade;
        lutador.Tecnica = dto.Tecnica;
        lutador.Controle = dto.Controle;
        lutador.DificuldadeDeUso = dto.DificuldadeDeUso;
        lutador.JogoDeOrigem = dto.JogoDeOrigem;
        lutador.AtivoNoJogoAtual = dto.AtivoNoJogoAtual;
        lutador.ImagemUrl = dto.ImagemUrl;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("Deletar/{id}")]
    public async Task<ActionResult> Deletar(Guid id)
    {
        var lutador = await _context.Lutadores.FindAsync(id);

        if (lutador == null)
            return NotFound("Lutador não encontrado.");

        _context.Lutadores.Remove(lutador);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("Ranking")]
    public async Task<ActionResult<IEnumerable<object>>> Ranking()
    {
        var tierOrdem = new Dictionary<string, int>
    {
        { "S", 1 },
        { "A", 2 },
        { "B", 3 },
        { "C", 4 }        
    };

        var lutadores = await _context.Lutadores
            .AsNoTracking()
            .ToListAsync();

        var resultado = lutadores
            .Select(l => new
            {
                l.Id,
                l.Nome,
                l.TierAtual,
                l.ImagemUrl,
                l.EstiloDeLuta,
                MediaAtributos = (l.Forca + l.Velocidade + l.Tecnica + l.Controle) / 4.0,
                OrdemTier = tierOrdem.TryGetValue(l.TierAtual, out var ordem) ? ordem : 99
            })
            .OrderBy(l => l.OrdemTier)
            .ThenByDescending(l => l.MediaAtributos)
            .ToList();

        return Ok(resultado);
    }




}

