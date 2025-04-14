using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elena.Application.DTOs;

public class LutadorDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Nacionalidade { get; set; } = default!;
    public string EstiloDeLuta { get; set; } = default!;
    public string TierAtual { get; set; } = default!;
    public string ImagemUrl { get; set; } = default!;
}
