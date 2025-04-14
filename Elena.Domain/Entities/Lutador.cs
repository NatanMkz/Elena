using System;

namespace Elena.Domain.Entities
{
    public class Lutador
    {
        private Guid _id;
        private string _nome;
        private string _nacionalidade;
        private string _estiloDeLuta;
        private string _historia;
        private string _tierAtual;
        private double _alturaCm;
        private double _pesoKg;
        private int _forca;
        private int _velocidade;
        private int _tecnica;
        private int _controle;
        private string _dificuldadeDeUso;
        private string _jogoDeOrigem;
        private bool _ativoNoJogoAtual;
        private string _imagemUrl;

        public Lutador() { }

        public Lutador(
            Guid id,
            string nome,
            string nacionalidade,
            string estiloDeLuta,
            string historia,
            string tierAtual,
            double alturaCm,
            double pesoKg,
            int forca,
            int velocidade,
            int tecnica,
            int controle,
            string dificuldadeDeUso,
            string jogoDeOrigem,
            bool ativoNoJogoAtual,
            string imagemUrl)
        {
            Id = id;
            Nome = nome;
            Nacionalidade = nacionalidade;
            EstiloDeLuta = estiloDeLuta;
            Historia = historia;
            TierAtual = tierAtual;
            AlturaCm = alturaCm;
            PesoKg = pesoKg;
            Forca = forca;
            Velocidade = velocidade;
            Tecnica = tecnica;
            Controle = controle;
            DificuldadeDeUso = dificuldadeDeUso;
            JogoDeOrigem = jogoDeOrigem;
            AtivoNoJogoAtual = ativoNoJogoAtual;
            ImagemUrl = imagemUrl;
        }

        public Guid Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Nacionalidade { get => _nacionalidade; set => _nacionalidade = value; }
        public string EstiloDeLuta { get => _estiloDeLuta; set => _estiloDeLuta = value; }
        public string Historia { get => _historia; set => _historia = value; }
        public string TierAtual { get => _tierAtual; set => _tierAtual = value; }
        public double AlturaCm { get => _alturaCm; set => _alturaCm = value; }
        public double PesoKg { get => _pesoKg; set => _pesoKg = value; }
        public int Forca { get => _forca; set => _forca = value; }
        public int Velocidade { get => _velocidade; set => _velocidade = value; }
        public int Tecnica { get => _tecnica; set => _tecnica = value; }
        public int Controle { get => _controle; set => _controle = value; }
        public string DificuldadeDeUso { get => _dificuldadeDeUso; set => _dificuldadeDeUso = value; }
        public string JogoDeOrigem { get => _jogoDeOrigem; set => _jogoDeOrigem = value; }
        public bool AtivoNoJogoAtual { get => _ativoNoJogoAtual; set => _ativoNoJogoAtual = value; }
        public string ImagemUrl { get => _imagemUrl; set => _imagemUrl = value; }
    }
}
