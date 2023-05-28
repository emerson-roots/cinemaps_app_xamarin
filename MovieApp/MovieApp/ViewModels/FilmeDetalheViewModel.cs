using MovieApp.Custom;
using MovieApp.Interfaces;
using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{
    public class FilmeDetalheViewModel : BaseViewModel
    {
        private readonly IMessageService _messageService;

        #region PROPERTIES

        private Filme _filmeBinding;
        public Filme FilmeBinding
        {
            get { return _filmeBinding; }
            set
            {
                _filmeBinding = value;
                OnPropertyChanged();
            }
        }

        private List<DateTime> _datasSessoes = new List<DateTime>();
        public List<DateTime> DatasSessoes
        {
            get { return _datasSessoes; }
            set
            {
                _datasSessoes = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dataSelecionada = new DateTime();
        public DateTime DataSelecionada
        {
            get { return _dataSelecionada; }
            set
            {
                _dataSelecionada = value;

                if (value != null && value != default)
                    CarregaHorariosDataSelecionada(value);

                OnPropertyChanged();
            }
        }

        private ObservableCollection<TimeSpan> _horariosDataSelecionada = new ObservableCollection<TimeSpan>();
        public ObservableCollection<TimeSpan> HorariosDataSelecionada
        {
            get { return _horariosDataSelecionada; }
            set
            {
                _horariosDataSelecionada = value;
                OnPropertyChanged();
            }
        }

        private bool _isSessoesVisivel;
        public bool IsSessoesVisivel
        {
            get { return _isSessoesVisivel; }
            set
            {
                _isSessoesVisivel = value;
                OnPropertyChanged();
            }
        }

        #endregion


        public FilmeDetalheViewModel(Filme filme)
        {
            _messageService = DependencyService.Get<IMessageService>();

            //MockFilmes();
            FilmeBinding = filme;
            if (FilmeBinding.Sessoes != null && FilmeBinding.Sessoes.Count > 0)
            {
                ValidaDatasSessoes();
                DataSelecionada = DatasSessoes[0];

            }
        }

        private void MockFilmes()
        {
            List<Sessao> sessoes = new List<Sessao>();

            Sessao repetido = new Sessao() { DataHora = DateTime.Now.AddDays(1).AddHours(3) };
            sessoes.Add(repetido);

            Sessao repetido2 = new Sessao() { DataHora = DateTime.Now.AddDays(1).AddHours(6) };
            sessoes.Add(repetido2);

            for (int i = 2; i < 10; i++)
            {
                Sessao c1 = new Sessao() { DataHora = DateTime.Now.AddDays(i) };
                sessoes.Add(c1);
            }

            FilmeBinding = new Filme()
            {
                NomeFilme = "Os Incríveis 2",
                FaixaEtaria = 13,
                Idioma = "Português",
                Duracao = 209,
                Categoria = "Ação",
                Sessoes = sessoes,
                Sinopse = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."


            };

            ValidaDatasSessoes();
        }

        private void ValidaDatasSessoes()
        {
            try
            {


                List<Sessao> distinctItems = FilmeBinding.Sessoes.GroupBy(x => x.DataHora.Day).Select(y => y.First()).ToList();

                List<DateTime> datas = new List<DateTime>();

                foreach (var item in distinctItems)
                {
                    DatasSessoes.Add(item.DataHora);
                }
                IsSessoesVisivel = distinctItems != null && distinctItems.Count > 0;
            }
            catch (Exception ex)
            {

                _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR, "Datas de sessões", $"Ocorreu um erro ao configurar as sessões disponíveis: {ex.Message}");
            }

        }

        private void CarregaHorariosDataSelecionada(DateTime dataSelecionada)
        {
            try
            {


                HorariosDataSelecionada.Clear();
                List<Sessao> sessoes = FilmeBinding.Sessoes.Where(x => x.DataHora.Day == dataSelecionada.Day).ToList();
                foreach (var item in sessoes)
                {
                    HorariosDataSelecionada.Add(item.DataHora.TimeOfDay);
                }
            }
            catch (Exception ex)
            {

                _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR, $"Horários de sessão", $"Ocorreu um erro ao configurar os horários da data selecionada: {ex.Message}");
            }

        }



    }


}
