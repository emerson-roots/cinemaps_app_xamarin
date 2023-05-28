using MovieApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarEmailPage : ContentPage
    {
        public CadastrarEmailPage()
        {
            InitializeComponent();
            BindingContext = new CadastrarEmailViewModel();
        }
    }
}