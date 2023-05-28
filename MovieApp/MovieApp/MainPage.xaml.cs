using MovieApp.Views;
using Xamarin.Forms;

namespace MovieApp
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            Color corNavBar = Color.FromHex("#1B1D1B");
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new NavigationPage(new HomePage()) { BarBackgroundColor = corNavBar };
        }
    }
}
