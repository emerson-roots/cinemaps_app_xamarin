using MovieApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConclusaoCadastroPage : ContentPage
    {
        public ConclusaoCadastroPage()
        {
            InitializeComponent();
            BindingContext = new ConclusaoCadastroViewModel();
        }
    }
}