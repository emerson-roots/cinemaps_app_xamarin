using MovieApp.Custom;
using MovieApp.Interfaces;
using MovieApp.Models;
using MovieApp.Views;
using Refit;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{

    public class CadastrarSenhaViewModel : BaseViewModel
    {
        private readonly IMessageService _messageService;
        private readonly HttpClient _httpClient;

        #region Properties
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
        #endregion

        #region COMMANDS
        public ICommand IrParaConclusaoCadastroCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await FazerLogin();
                });
            }
        }
        #endregion

        public CadastrarSenhaViewModel(User user)
        {
            Credenciais = user;
            _messageService = DependencyService.Get<IMessageService>();
            _httpClient = DependencyService.Get<HttpClient>();
        }

        private async Task FazerLogin()
        {
            try
            {

                //IsActivityIndicatorEnabledBinding = true;

                var regexDigit = new Regex(@"^(?=.*\d)");
                var regexMaiustula = new Regex(@"^(?=.*[A-Z])");
                var regexMinuscula = new Regex(@"^(?=.*[a-z])");
                var regexCaracterEspecial = new Regex(@"^(?=.*[$*&@#])");

                if (string.IsNullOrEmpty(Credenciais.Password))
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Campo obrigatório.", "Preencha com uma senha para prosseguir.");
                    return;
                }
                else if (Credenciais.Password.Length < 6)
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Minimo 6 caracteres.", "Quantidade de caracteres inválida para a senha.");
                    return;
                }
                else if (!regexDigit.IsMatch(Credenciais.Password))
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Requer dígito.", "A senha deve conter pelo menos 1 dígito.");
                    return;
                }
                else if (!regexMaiustula.IsMatch(Credenciais.Password))
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Requer maiúscula.", "A senha deve conter pelo menos 1 letra maiúscula.");
                    return;
                }
                else if (!regexMinuscula.IsMatch(Credenciais.Password))
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Requer minúscula.", "A senha deve conter pelo menos 1 letra minúscula.");
                    return;
                }
                else if (!regexCaracterEspecial.IsMatch(Credenciais.Password))
                {
                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Requer carácter.", "A senha deve conter pelo menos 1 carácter especial.");
                    return;
                }

                // momentaneamente, esta setando a mesma senha pra enviar pra API
                Credenciais.ConfirmPassword = Credenciais.Password;

                var api = RestService.For<IRestApi>(_httpClient);
                var result = await api.CadastrarUsuario(Credenciais);
                if (result.Equals("Conta criada com sucesso!"))
                {
                    await DependencyService.Get<INavigationService>().SetMainPage(new ConclusaoCadastroPage());
                }
                else
                {
                    throw new Exception("Ocorreu algum erro ao enviar cadastro.");
                }

            }
            catch (ApiException apiEx)
            {
                var statusCode = apiEx.StatusCode;
                await _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR, $"Tentativa de cadastro", $"{statusCode} - Não foi possível efetuar cadastro: \n\n{apiEx.Content}");
                //await DependencyService.Get<INavigationService>().SetMainPage(new LoginPage());
            }
            catch (Exception ex)
            {
                await _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR, $"Tentativa de cadastro", $"Não foi possível efetuar o cadastro: {ex.Message}");
                //await DependencyService.Get<INavigationService>().SetMainPage(new LoginPage());
            }
            finally
            {
                //IsActivityIndicatorEnabledBinding = false;
            }
        }

    }

}