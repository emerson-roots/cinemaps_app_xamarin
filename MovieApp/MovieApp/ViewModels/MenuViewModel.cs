using MovieApp.Custom;
using MovieApp.Interfaces;
using MovieApp.Models;
using MovieApp.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{

    public class MenuViewModel : BaseViewModel
    {

        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;

        #region Properties
        private bool _isMenuAtivado;
        public bool IsMenuAtivado
        {
            get { return _isMenuAtivado; }
            set
            {
                _isMenuAtivado = value;
                OnPropertyChanged();
            }
        }

        private bool _isActivityIndicatorEnabledBinding = false;
        public bool IsActivityIndicatorEnabledBinding
        {
            get { return _isActivityIndicatorEnabledBinding; }
            set
            {
                _isActivityIndicatorEnabledBinding = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region COMMANDS
        public ICommand LogoutUsuarioCommand { get; set; }
        public ICommand IrParaHomePageCommand { get; set; }
        public ICommand IrParaQuizCommand { get; set; }
        public ICommand IrParaFilmesCommand { get; set; }
        public ICommand IrParaCinemasCommand { get; set; }
        #endregion

        public MenuViewModel()
        {
            IsMenuAtivado = false;
            _messageService = DependencyService.Get<IMessageService>();
            _navigationService = DependencyService.Get<INavigationService>();

            Logout();
            IrParaHomePage();
            IrParaQuiz();
            IrParaFilmes();
            IrParaCinemas();
            IsMenuAtivado = true;
        }

        private void Logout()
        {
            this.LogoutUsuarioCommand = new Command(async () =>
            {
                try
                {
                    IsMenuAtivado = false;
                    await _navigationService.SetMainPage(new LoginPage());

                }
                catch (Exception ex)
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Erro", $"Erro ao efetuar logout: { ex.Message }"); ;
                }
                finally
                {
                    IsMenuAtivado = true;
                }
            });

        }

        private void IrParaHomePage()
        {
            IrParaHomePageCommand = new Command(async () =>
            {
                IsMenuAtivado = false;
                await _navigationService.NavigateToHomePage();
                IsMenuAtivado = true;
            });
        }

        private void IrParaQuiz()
        {
            IrParaQuizCommand = new Command(async () =>
            {
                IsMenuAtivado = false;
                //await _navigationService.NavigateToHomePage();
                await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Quiz tester", $"QUIZ OK!"); ;
                IsMenuAtivado = true;
            });
        }

        private void IrParaFilmes()
        {
            IrParaFilmesCommand = new Command(async () =>
            {
                IsMenuAtivado = false;
                await _navigationService.NavigateToFilmesPage(new List<Filme>(),"teste nulo");
                IsMenuAtivado = true;
            });
        }

        private void IrParaCinemas()
        {
            IrParaCinemasCommand = new Command(async () =>
            {
                IsMenuAtivado = false;
                //await _navigationService.NavigateToCinemaListaPage();
                IsMenuAtivado = true;
            });
        }
    }
}
