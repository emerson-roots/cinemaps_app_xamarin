using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace MovieApp.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class Regiao
    {
        public int ID { get; set; }

        public string Descricao { get; set; }

        public List<Cidade> Cidades { get; set; }

    }

    public class Cidade
    {

        public int ID { get; set; }

        //public int RegiaoId { get; set; }

        public string Estado { get; set; }

        public string Descricao { get; set; }

    }

    public class Cinema
    {
        public int ID { get; set; }

        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }

        public string NomeCinema { get; set; }

        public string Endereco { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double NotaAvaliacao { get; set; }
        public double DistanciaLocalizacao { get; set; }

        public List<Filme> Filmes { get; set; }
        public List<FotoCinema> FotosCinema { get; set; }

    }

    public class FotoCinema
    {
        public int ID { get; set; }
        public int CinemaId { get; set; }
        public string Foto { get; set; }

        private ImageSource _fotoImageSource;
        public ImageSource FotoImageSource
        {
            get
            {

                if (!string.IsNullOrEmpty(Foto))
                {
                    byte[] Base64Stream = Convert.FromBase64String(Foto);
                    _fotoImageSource = ImageSource.FromStream(() => new MemoryStream(Base64Stream));

                }

                // limpa a string para nao pesar na memória
                Foto = string.Empty;
                return _fotoImageSource;
            }
        }


    }

    public class Filme
    {
        public int ID { get; set; }

        public int CinemaId { get; set; }
        public int Duracao { get; set; }
        public double NotaAvaliacao { get; set; }
        public string NomeFilme { get; set; }
        public string Idioma { get; set; }
        public string Diretor { get; set; }
        public string Sinopse { get; set; }
        public string FotoCapaFilme { get; set; }
        public string Categoria { get; set; }

        private TimeSpan _duracaoFormatada;
        public TimeSpan DuracaoFormatada
        {
            get
            {
                _duracaoFormatada = TimeSpan.FromMinutes(Duracao);
                return _duracaoFormatada;
            }
        }


        private ImageSource _fotoImageSource;
        public ImageSource FotoImageSource
        {
            get
            {
                if (!string.IsNullOrEmpty(FotoCapaFilme))
                {
                    byte[] Base64Stream = Convert.FromBase64String(FotoCapaFilme);
                    _fotoImageSource = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                }
                FotoCapaFilme = string.Empty;
                return _fotoImageSource;
            }
        }


        public int FaixaEtaria { get; set; }

        public List<Sessao> Sessoes { get; set; }

    }

    public class Sessao
    {
        public int ID { get; set; }

        public int FilmeId { get; set; }

        public DateTime DataHora { get; set; }

    }
}
