using MovieApp.Custom;
using MovieApp.Interfaces;
using MovieApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{

    public class HomeViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly IMessageService _messageService;

        #region Properties
        private string _selectedRadioButtonBinding;
        public string SelectedRadioButtonBinding
        {
            get { return _selectedRadioButtonBinding; }
            set
            {
                _selectedRadioButtonBinding = value;
                ValidaPickerVisivel(value);
                OnPropertyChanged();
            }
        }

        private bool _isPickerCidadeVisivel;
        public bool IsPickerCidadeVisivel
        {
            get { return _isPickerCidadeVisivel; }
            set
            {
                _isPickerCidadeVisivel = value;
                OnPropertyChanged();
            }
        }

        private bool _isPickerRegiaoVisivel;
        public bool IsPickerRegiaoVisivel
        {
            get { return _isPickerRegiaoVisivel; }
            set
            {
                _isPickerRegiaoVisivel = value;
                OnPropertyChanged();
            }
        }

        private bool _isActivityIndicatorEnabledBinding = false;
        public bool IsActivityIndicatorEnabledBinding
        {
            get { return _isActivityIndicatorEnabledBinding; }
            set
            {
                _isActivityIndicatorEnabledBinding = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Cidade> _cidades = new ObservableCollection<Cidade>();
        public ObservableCollection<Cidade> Cidades
        {
            get { return _cidades; }
            set
            {
                _cidades = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Regiao> _regioes = new ObservableCollection<Regiao>();
        public ObservableCollection<Regiao> Regioes
        {
            get { return _regioes; }
            set
            {
                _regioes = value;
                OnPropertyChanged();
            }
        }

        private Regiao _regiaoSelecionada;
        public Regiao RegiaoSelecionada
        {
            get { return _regiaoSelecionada; }
            set
            {
                _regiaoSelecionada = value;
                OnPropertyChanged();
            }
        }

        private Cidade _cidadeSelecionada;
        public Cidade CidadeSelecionada
        {
            get { return _cidadeSelecionada; }
            set
            {
                _cidadeSelecionada = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand IrParaCinemasPageCommand { get; set; }
        public List<Cinema> Cinemas { get; set; }

        public HomeViewModel()
        {
            _httpClient = DependencyService.Get<HttpClient>();
            _messageService = DependencyService.Get<IMessageService>();

            IrParaCinemasPage();
            SelectedRadioButtonBinding = "Cidade";
            //ValidaPickerVisivel(SelectedRadioButtonBinding);
        }

        private void IrParaCinemasPage()
        {
            IrParaCinemasPageCommand = new Command(async () =>
            {
                if (CidadeSelecionada is null && RegiaoSelecionada is null)
                {
                    await _messageService.ShowCustomDisplayAlert(MovieApp.Custom.TipoAlertOk.INFORMATION,"Selecione um item...", $"Selecione uma cidade ou região para prosseguir");
                    //await _messageService.ShowAsync("Selecione um item...", $"Selecione uma cidade ou região para prosseguir");
                    return;
                }

                string localidadeSelecionada = CidadeSelecionada != null ? CidadeSelecionada.Descricao : RegiaoSelecionada.Descricao;
                Cinemas = null;

                try
                {
                    IsActivityIndicatorEnabledBinding = true;
                    if (SelectedRadioButtonBinding.Equals("Cidade"))
                    {
                        var api = RestService.For<IRestApi>(_httpClient);
                        ICollection<Cinema> list = await api.GetCinemasPorCidade(CidadeSelecionada.ID);
                        Cinemas = new ObservableCollection<Cinema>(list).ToList();
                    }
                    else
                    {
                        var api = RestService.For<IRestApi>(_httpClient);
                        ICollection<Cinema> list = await api.GetCinemasPorRegiao(RegiaoSelecionada.ID);
                        Cinemas = new ObservableCollection<Cinema>(list).ToList();
                    }

                    await DependencyService.Get<INavigationService>().NavigateToCinemaListaPage(Cinemas, localidadeSelecionada);

                }
                catch (Refit.ApiException exRefit) when (exRefit.StatusCode == HttpStatusCode.NoContent)
                {
                    // Refit com retorno status code 204 causa exception
                    // fazer tratamento aqui para esta possibilidade
                    // https://github.com/reactiveui/refit/issues/1128
                    await DependencyService.Get<INavigationService>().NavigateToCinemaListaPage(Cinemas, localidadeSelecionada);
                }
                catch (Exception ex)
                {

                    await _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR,"Erro ao carregar cinemas", $"Não foi possível carregar os cinemas: {ex}");
                }
                finally
                {
                    IsActivityIndicatorEnabledBinding = false;
                }
            });
        }

        private async Task ValidaPickerVisivel(string radioButtonSelecionado)
        {
            try
            {

                if (SelectedRadioButtonBinding.Equals("Cidade"))
                {

                    IsPickerCidadeVisivel = true;
                    IsPickerRegiaoVisivel = false;


                    var api = RestService.For<IRestApi>(_httpClient);
                    ICollection<Cidade> list = await api.GetAllCidades();
                    Cidades = new ObservableCollection<Cidade>(list);
                }
                else
                {
                    IsPickerCidadeVisivel = false;
                    IsPickerRegiaoVisivel = true;

                    var api = RestService.For<IRestApi>(_httpClient);
                    ICollection<Regiao> list = await api.GetAllRegioes();
                    Regioes = new ObservableCollection<Regiao>(list);
                }

                CidadeSelecionada = null;
                RegiaoSelecionada = null;
            }
            catch (Exception ex)
            {
                var stacktrace = ex.ToString();
                await _messageService.ShowCustomDisplayAlert(TipoAlertOk.ERROR, "Erro ao carregar localidades", $"Não foi possível carregar as informações de cidades ou regiões:\n\n{stacktrace}");
            }
        }
    }
}
