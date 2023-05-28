using MovieApp.Custom;
using System.Threading.Tasks;

namespace MovieApp.Interfaces
{
    public interface IMessageService
    {
        //Task ShowAsync(string tituloMensagem, string message);
        void Show(string tituloMensagem, string message);
        Task<bool> ShowSimOuNaoAsync(string tituloMensagem, string message);
        Task ShowCustomDisplayAlert(TipoAlertOk tipoAlert, string titulo, string mensagem);
    }
}
