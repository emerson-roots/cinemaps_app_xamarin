using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp.Custom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDisplayAlertOkPage : ContentPage
    {
        private TaskCompletionSource<bool?> _taskCompletionSource;
        public Task ModalClosedTask => _taskCompletionSource.Task;

        public CustomDisplayAlertOkPage(TipoAlertOk tipoAlert, string titulo, string mensagem)
        {
            InitializeComponent();
            BindingContext = new CustomDisplayAlertOkViewModel(tipoAlert, titulo, mensagem);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _taskCompletionSource = new TaskCompletionSource<bool?>();

            // inicia em uma estala de 0.7 para simular um transição suave ao aparecer
            _alertPage.Scale = 0;

            // estica a pagina e reseta ao seu tamanho normal
            await _alertPage.ScaleTo(1.2, 100, Easing.Linear);
            await _alertPage.ScaleTo(1, 100, Easing.Linear);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _taskCompletionSource.SetResult(null);
        }
    }
}