using MovieApp.Helper;
using MovieApp.Interfaces;
using MovieApp.Services;
using MovieApp.Views;
using Refit;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace MovieApp
{
    public partial class App : Application
    {
        private readonly Color _corNavBar = Color.FromHex("#1B1D1B");
        //private static readonly HttpClient _httpClient = new HttpClient(DisableSSL());
        private static readonly HttpClient _httpClient = RestService.CreateHttpClient(EndPoints.BaseUrl, null);

        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton(_httpClient);
            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<INavigationService, NavigationService>();

            MainPage = new NavigationPage() { BarBackgroundColor = _corNavBar };

            //DependencyService.Get<INavigationService>().SetMainPage(new MainPage());
            DependencyService.Get<INavigationService>().SetMainPage(new LoginPage());
            //DependencyService.Get<INavigationService>().SetMainPage(new FilmesPage());
            //DependencyService.Get<INavigationService>().SetMainPage(new CinemaListaPage());


            // DependencyService.Get<INavigationService>().NavigateToMainPage();
            //_httpClient = RestService.CreateHttpClient(EndPoints.BaseUrl, null);

            if (_httpClient.BaseAddress == null)
            {
                var endPointUri = new Uri($"{EndPoints.BaseUrl}");
                _httpClient.BaseAddress = endPointUri;
                _httpClient.Timeout = TimeSpan.FromSeconds(35);
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private static HttpClientHandler DisableSSL()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslpolicyerrors) =>
            {
                return true;
            };

            return httpClientHandler;
        }
    }
}
