using MovieApp.Interfaces;
using MovieApp.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{

    public class ConclusaoCadastroViewModel : BaseViewModel
    {
        public ICommand IrParaLoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await DependencyService.Get<INavigationService>().SetMainPage(new LoginPage());
                });
            }
        }

        public ConclusaoCadastroViewModel()
        {
        }

    }
}