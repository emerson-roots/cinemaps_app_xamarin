using MovieApp.Models;
using MovieApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarSenhaPage : ContentPage
    {
        public CadastrarSenhaPage(User user)
        {
            InitializeComponent();
            BindingContext = new CadastrarSenhaViewModel(user);
        }
    }
}