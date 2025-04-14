using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elena.Application.DTOs;

public class CreateLutadorDto
{
    public string Nome { get; set; } = default!;
    public string Nacionalidade { get; set; } = default!;
    public string EstiloDeLuta { get; set; } = default!;
    public string Historia { get; set; } = default!;
    public string TierAtual { get; set; } = default!;
    public double AlturaCm { get; set; }
    public double PesoKg { get; set; }
    public int Forca { get; set; }
    public int Velocidade { get; set; }
    public int Tecnica { get; set; }
    public int Controle { get; set; }
    public string DificuldadeDeUso { get; set; } = default!;
    public string JogoDeOrigem { get; set; } = default!;
    public bool AtivoNoJogoAtual { get; set; }
    public string ImagemUrl { get; set; } = default!;
}

