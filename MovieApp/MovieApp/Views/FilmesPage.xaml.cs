using MovieApp.Models;
using MovieApp.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmesPage : ContentPage
    {
        public FilmesPage(List<Filme> list, string nomeCinema)
        {
            InitializeComponent();
            BindingContext = new FilmeViewModel(list, nomeCinema);
        }
    }
}