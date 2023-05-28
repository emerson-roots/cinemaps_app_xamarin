using MovieApp.Custom;
using MovieApp.Interfaces;
using MovieApp.Models;
using Refit;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{

    public class CadastrarEmailViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly IMessageService _messageService;

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

        public ICommand IrParaCadastrarSenhaCommand
        {
            get
            {
                return new Command(async () =>
                {

                    try
                    {
                        if (string.IsNullOrEmpty(Credenciais.Email))
                        {
                            await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "Campo obrigatório.", "Preencha com um e-mail para prosseguir.");
                            return;
                        }


                        string email = Credenciais.Email;
                        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        Match match = regex.Match(email);
                        if (!match.Success)
                        {
                            await _messageService.ShowCustomDisplayAlert(TipoAlertOk.WARNING, "E-mail inválido.", "Digite um e-mail valido para prosseguir.");
                            return;
                        }

                        // chama API
                        var api = RestService.For<IRestApi>(_httpClient);
                        var result = await api.IsPossuiCadastro(Credenciais.Email);
                        if (!result)
                        {
                            await DependencyService.Get<INavigationService>().NavigateToCadastrarSenha(Credenciais);
                        }
                        else
                        {
                            await _messageService.ShowCustomDisplayAlert(TipoAlertOk.INFORMATION, "Tente outro e-mail.", "Este e-mail já possui cadastro.");
                            return;
                        }


                    }
                    catch (ApiException apiEx)
                    {
                        var statusCode = apiEx.StatusCode;
                        await _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR, $"Tentativa de cadastro", $"{statusCode} - Não foi possível efetuar cadastro: \n\n{apiEx.Content}");
                    }
                    catch (Exception ex)
                    {
                        await _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR, $"Tentativa de cadastro", $"Não foi possível efetuar o cadastro: {ex.Message}");
                    }
                    finally
                    {
                        //IsActivityIndicatorEnabledBinding = false;
                    }


                });
            }
        }

        public CadastrarEmailViewModel()
        {
            _httpClient = DependencyService.Get<HttpClient>();
            _messageService = DependencyService.Get<IMessageService>();

        }

    }
}
