using MovieApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieApp.Interfaces
{
    public interface INavigationService
    {
        Task SetMainPage(Page page);
        Task NavigateToMainPage();
        Task NavigateToHomePage();
        Task NavigateToFilmesPage(List<Filme> list, string nomeCinema);
        Task NavigateToFilmeDetalhePage(Filme filme);
        Task NavigateToCinemaListaPage(List<Cinema> list, string localidade);
        Task NavigateToCadastrarEmail();
        Task NavigateToCadastrarSenha(User user);
        Task NavigateToCadastroConclusao();

    }
}
