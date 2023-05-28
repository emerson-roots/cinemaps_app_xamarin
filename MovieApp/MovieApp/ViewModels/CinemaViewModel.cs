using MovieApp.Interfaces;
using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{
    public class CinemaViewModel : BaseViewModel
    {
        private readonly IMessageService _messageService;

        #region PROPERTIES
        private ObservableCollection<Cinema> _cinemasBinding = new ObservableCollection<Cinema>();
        public ObservableCollection<Cinema> CinemasBinding
        {
            get { return _cinemasBinding; }
            set
            {
                _cinemasBinding = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectItemCommand
        {
            get
            {
                return new Command<Cinema>(async (cinemaSelecionado) =>
                {
                    try
                    {
                        IsBusy = true;
                        await DependencyService.Get<INavigationService>().NavigateToFilmesPage(cinemaSelecionado.Filmes, cinemaSelecionado.NomeCinema);
                        IsBusy = false;
                    }
                    catch (Exception ex)
                    {
                        var stack = ex.ToString();
                        await DependencyService.Get<IMessageService>().ShowCustomDisplayAlert(Custom.TipoAlertOk.ERROR, "Erro ao selecionar", "Erro ao navegar para os detalhes do filme.");
                    }
                    finally
                    {
                        //FilmeSelecionado = null;
                    }

                });
            }
        }

        private string _complementoTituloPagina;
        public string ComplementoTituloPagina
        {
            get { return _complementoTituloPagina; }
            set
            {
                _complementoTituloPagina = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public CinemaViewModel(List<Cinema> list, string localidade)
        {
            ComplementoTituloPagina = localidade;
            _messageService = DependencyService.Get<IMessageService>();

            if (list != null)
                CinemasBinding = new ObservableCollection<Cinema>(list);

        }
    }


}
