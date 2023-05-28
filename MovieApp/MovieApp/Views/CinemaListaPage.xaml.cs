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
    public partial class CinemaListaPage : ContentPage
    {
        public CinemaListaPage(List<Cinema> list, string localidade)
        {
            InitializeComponent();
            BindingContext = new CinemaViewModel(list, localidade);
        }
    }
}