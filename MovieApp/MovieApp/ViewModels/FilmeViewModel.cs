using MovieApp.Interfaces;
using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{
    public class FilmeViewModel : BaseViewModel
    {

        #region PROPERTIES

        private bool _isDesbloquerTela;
        public bool IsDesbloquerTela
        {
            get { return _isDesbloquerTela; }
            set
            {
                _isDesbloquerTela = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Filme> _filmesBinding = new ObservableCollection<Filme>();
        public ObservableCollection<Filme> FilmesBinding
        {
            get { return _filmesBinding; }
            set
            {
                _filmesBinding = value;
                OnPropertyChanged();
            }
        }

        private string _nomeCinema;
        public string NomeCinema
        {
            get { return _nomeCinema; }
            set
            {
                _nomeCinema = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand AppearingCommand { get; set; }

        public ICommand SelectItemCommand
        {
            get
            {
                return new Command<Filme>(async (filmeSelecionado) =>
                {
                    try
                    {
                        IsDesbloquerTela = false;
                        await DependencyService.Get<INavigationService>().NavigateToFilmeDetalhePage(filmeSelecionado);
                        IsDesbloquerTela = true;
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

        public FilmeViewModel(List<Filme> list, string nomeCinema)
        {
            IsDesbloquerTela = true;
            NomeCinema = nomeCinema;
            OnAppearing(list);
        }

        private void OnAppearing(List<Filme> list)
        {
            AppearingCommand = new Command(async () =>
            {
                if (list != null && list.Count > 0)
                    FilmesBinding = new ObservableCollection<Filme>(list.ToList());

            });
        }


    }


}
