using MovieApp.Custom;
using MovieApp.Interfaces;
using MovieApp.Models;
using Refit;
using System;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{

    public class LoginViewModel : BaseViewModel
    {
        private const string _iconeMostrarSenha = "eye_open_lined_120px.png";
        private const string _iconeEsconderSenha = "eye_closed_lined_120px.png";

        private readonly HttpClient _httpClient;
        private readonly IRestApi _apiRefit;
        private readonly IMessageService _messageService;

        #region Properties
        private bool _isEsconderSenhaBinding = true;
        public bool IsEsconderSenhaBinding
        {
            get { return _isEsconderSenhaBinding; }
            set
            {
                _isEsconderSenhaBinding = value;
                OnPropertyChanged();
            }
        }

        private string _iconeMostrarOuEsconderSenhaBinding;
        public string IconeMostrarOuEsconderSenhaBinding
        {
            get { return _iconeMostrarOuEsconderSenhaBinding; }
            set
            {
                _iconeMostrarOuEsconderSenhaBinding = value;
                OnPropertyChanged();
            }
        }

        private User _credenciais = new User();
        public User Credenciais
        {
            get { return _credenciais; }
            set
            {
                _credenciais = value;
                OnPropertyChanged();
            }
        }

        private bool _lembrarDeMim;
        public bool LembrarDeMim
        {
            get { return _lembrarDeMim; }
            set
            {
                IsActivityIndicatorEnabledBinding = true;
                _messageService.ShowCustomDisplayAlert(TipoAlertOk.INFORMATION,$"Não implementado", $"Funcionalidade ainda não implementada...");
                IsActivityIndicatorEnabledBinding = false;
                _lembrarDeMim = value;
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
        public ICommand FazerLoginCommand { get; set; }
        public ICommand MostrarSenhaCommand { get; set; }
        public ICommand CriarCadastroCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsActivityIndicatorEnabledBinding = true;
                    await DependencyService.Get<INavigationService>().NavigateToCadastrarEmail();
                    IsActivityIndicatorEnabledBinding = false;
                });
            }
        }

        public ICommand EsqueciMinhaSenhaCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsActivityIndicatorEnabledBinding = true;
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.INFORMATION, $"Não implementado", $"Funcionalidade ainda não implementada...");
                    IsActivityIndicatorEnabledBinding = false;
                });
            }
        }
        #endregion

        public LoginViewModel()
        {
            IconeMostrarOuEsconderSenhaBinding = _iconeMostrarSenha;

            _httpClient = DependencyService.Get<HttpClient>();
            _messageService = DependencyService.Get<IMessageService>();
            _apiRefit = RestService.For<IRestApi>(_httpClient);

            FazerLogin();
            MostrarSenha();
            Credenciais.Email = "user@example.com";
            Credenciais.Password = "Abcd123*";
            IsActivityIndicatorEnabledBinding = false;
        }

        private void FazerLogin()
        {
            FazerLoginCommand = new Command(async () =>
            {
                try
                {
                    IsActivityIndicatorEnabledBinding = true;

                    Token token = await _apiRefit.Autenticar(Credenciais);
                    if (token != null)
                        await DependencyService.Get<INavigationService>().SetMainPage(new MainPage());

                }
                catch (ApiException apiEx)
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR, $"Tentativa de login", $"Não foi possível efetuar o login: \n\n{apiEx.Content}");
                }
                catch (Exception ex)
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR,$"Tentativa de login", $"Não foi possível efetuar o login: {ex.Message}");
                }
                finally
                {
                    IsActivityIndicatorEnabledBinding = false;
                }
            });
        }

        private void MostrarSenha()
        {
            this.MostrarSenhaCommand = new Command(() =>
            {
                IsEsconderSenhaBinding = !IsEsconderSenhaBinding;
                IconeMostrarOuEsconderSenhaBinding = IsEsconderSenhaBinding ? _iconeMostrarSenha : _iconeEsconderSenha;
            });
        }

    }
}
