using Newtonsoft.Json;

namespace MovieApp.Models
{
    public class Token
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }
    }
}
