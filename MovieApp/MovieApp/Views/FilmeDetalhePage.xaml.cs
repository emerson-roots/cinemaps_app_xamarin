using MovieApp.Models;
using MovieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmeDetalhePage : ContentPage
    {
        public FilmeDetalhePage(Filme filme)
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new FilmeDetalheViewModel(filme);
        }
    }
}