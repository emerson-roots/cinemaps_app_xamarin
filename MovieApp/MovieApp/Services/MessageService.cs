using MovieApp.Custom;
using MovieApp.Interfaces;
using MovieApp.ViewModels;
using System.Threading.Tasks;


namespace MovieApp.Services
{
    public class MessageService : IMessageService
    {
        public void Show(string tituloMensagem, string message)
        {
            App.Current.MainPage.DisplayAlert(tituloMensagem, message, "Ok");
        }

        public async Task ShowAsync(string tituloMensagem, string message)
        {
            await App.Current.MainPage.DisplayAlert(tituloMensagem, message, "Ok");
        }

        public async Task ShowCustomDisplayAlert(TipoAlertOk tipoAlert, string titulo, string mensagem)
        {
            var page = new CustomDisplayAlertOkPage((TipoAlertOk)tipoAlert, titulo, mensagem);
            await App.Current.MainPage.Navigation.PushModalAsync(page, false);

            // libera a thread apos tocar no botao do modal
            await page.ModalClosedTask;
        }

        public async Task<bool> ShowSimOuNaoAsync(string tituloMensagem, string message)
        {
            return await App.Current.MainPage.DisplayAlert(tituloMensagem, message, "Sim", "Não");
        }
    }
}
