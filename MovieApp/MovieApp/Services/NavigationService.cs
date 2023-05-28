using MovieApp.Interfaces;
using MovieApp.Models;
using MovieApp.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieApp.Services
{
    public class NavigationService : INavigationService
    {
        public async Task SetMainPage(Page page)
        {
            Color corNavBar = Color.FromHex("#1B1D1B");
            App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = corNavBar };
            await App.Current.MainPage.Navigation.PopToRootAsync(true);
        }

        public async Task NavigateToMainPage()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync(true);
        }

        public async Task NavigateToHomePage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        public async Task NavigateToFilmesPage(List<Filme> list, string nomeCinema)
        {
            await App.Current.MainPage.Navigation.PushAsync(new FilmesPage(list, nomeCinema));
        }

        public async Task NavigateToCinemaListaPage(List<Cinema> list, string localidade)
        {
            
            await App.Current.MainPage.Navigation.PushAsync(new CinemaListaPage(list, localidade));
        }

        public async Task NavigateToFilmeDetalhePage(Filme filme)
        {
            await App.Current.MainPage.Navigation.PushAsync(new FilmeDetalhePage(filme));
        }

        public async Task NavigateToCadastrarEmail()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CadastrarEmailPage());
        }

        public async Task NavigateToCadastrarSenha(User user)
        {
            await App.Current.MainPage.Navigation.PushAsync(new CadastrarSenhaPage(user));
        }

        public async Task NavigateToCadastroConclusao()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ConclusaoCadastroPage());
        }
    }
}
