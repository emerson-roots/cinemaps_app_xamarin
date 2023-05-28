using MovieApp.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Interfaces
{
    interface IRestApi
    {

        [Get("/Quiz")]
        Task<ICollection<Pergunta>> GetTodos();

        #region Login e User
        [Post("/Auth/Autenticar")]
        Task<Token> Autenticar(User credenciais);

        [Post("/Auth/Cadastrar")]
        Task<string> CadastrarUsuario(User credenciais);

        [Post("/Auth/IsPossuiCadastro")]
        Task<bool> IsPossuiCadastro(string email);
        #endregion

        #region Localidade
        [Get("/Localidade/GetAllRegioes")]
        Task<ICollection<Regiao>> GetAllRegioes();

        [Get("/Localidade/GetAllCidades")]
        Task<ICollection<Cidade>> GetAllCidades();
        #endregion

        #region Cinemas
        [Get("/Cinema/GetCinemasPorCidade/{idCidade}")]
        Task<ICollection<Cinema>> GetCinemasPorCidade(int idCidade);

        [Get("/Cinema/GetCinemasPorRegiao/{idRegiao}")]
        Task<List<Cinema>> GetCinemasPorRegiao(int idRegiao);
        #endregion

    }
}
