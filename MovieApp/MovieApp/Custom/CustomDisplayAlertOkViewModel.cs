using MovieApp.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.Custom
{
    public class CustomDisplayAlertOkViewModel : BaseViewModel
    {
        #region PROPERTIES
        private string _corBotao;
        public string CorBotao
        {
            get { return _corBotao; }
            set
            {
                _corBotao = value;
                OnPropertyChanged();
            }
        }

        private string _corFundo;
        public string CorFundo
        {
            get { return _corFundo; }
            set
            {
                _corFundo = value;
                OnPropertyChanged();
            }
        }

        private bool _isModalAtivado;
        public bool IsModalAtivado
        {
            get { return _isModalAtivado; }
            set
            {
                _isModalAtivado = value;
                OnPropertyChanged();
            }
        }

        private AlertBindings _telaBindings = new AlertBindings();
        public AlertBindings TelaBindings
        {
            get { return _telaBindings; }
            set
            {
                _telaBindings = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region COMMANDS
        public ICommand BotaoOk
        {
            get
            {
                return new Command<ContentPage>(async (_alertPage) =>
                {
                    IsModalAtivado = false;

                    // cresce e diminui o modal desaparecendo no centro
                    //await _alertPage.ScaleTo(1.2, 150, Easing.Linear);
                    //await _alertPage.ScaleTo(0.2, 150, Easing.CubicIn);
                    //___________________

                    // escorrega para baixo o modal
                    //await _alertPage.TranslateTo(1, 1000, 200, Easing.Linear); 

                    // escorrega para baixo o modal (dando um pulo antes de escorregar)
                    await _alertPage.TranslateTo(1, 1000, 250, Easing.SpringIn);


                    // escorrega para a lateral o modal
                    //await _alertPage.TranslateTo(1000, 1, 200, Easing.Linear); 

                    await ClosePopUp();
                    IsModalAtivado = true;
                    //await Application.Current.MainPage.Navigation.PopModalAsync(false);
                });
            }
        }

        #endregion

        public CustomDisplayAlertOkViewModel(TipoAlertOk tipoAlert, string titulo, string mensagem)
        {
            Color cor = ValidaCor(tipoAlert);
            string icone = ValidaIcone(tipoAlert);

            var obj = new AlertBindings()
            {
                Titulo = titulo,
                Mensagem = mensagem,
                CorBotao = cor,
                CorFundo = cor.MultiplyAlpha(0.3),
                Icone = icone
            };

            TelaBindings = obj;
            IsModalAtivado = true;
        }

        private async Task ClosePopUp()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(false); ;
        }

        private Color ValidaCor(TipoAlertOk tipoAlert)
        {
            switch (tipoAlert)
            {
                case TipoAlertOk.INFORMATION:
                    return Cores.Azul;
                case TipoAlertOk.WARNING:
                    return Cores.Laranja;
                case TipoAlertOk.ERROR:
                    return Cores.Vermelho;
                case TipoAlertOk.SUCCESS:
                    return Cores.Verde;
                default:
                    return Cores.Azul;
            }
        }

        private string ValidaIcone(TipoAlertOk tipoAlert)
        {
            switch (tipoAlert)
            {
                case TipoAlertOk.INFORMATION:
                    return "info_256px.png";
                case TipoAlertOk.WARNING:
                    return "warning_256px.png";
                case TipoAlertOk.ERROR:
                    return "error_256px.png";
                case TipoAlertOk.SUCCESS:
                    return "success_256px.png";
                default:
                    return "info_256px.png";
            }
        }

        private static class Cores
        {
            public static Color Azul = Color.DodgerBlue;
            public static Color Verde = Color.Green;
            public static Color Vermelho = Color.Red;
            public static Color Laranja = Color.Orange;
        }
    }

    public class AlertBindings
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public Color CorBotao { get; set; }
        public Color CorFundo { get; set; }
        public string Icone { get; set; }
    }

    public enum TipoAlertOk
    {
        INFORMATION = 1,
        WARNING = 2,
        ERROR = 3,
        SUCCESS = 4
    }


}
