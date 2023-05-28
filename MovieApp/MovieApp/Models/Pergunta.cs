using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MovieApp.Models
{
    public class Pergunta
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("answers")]
        public Answers Answers { get; set; }

        [JsonProperty("correct_answers")]
        public CorrectAnswers Correct_Answers { get; set; }

        [JsonProperty("explanation")]
        public string Explanation { get; set; }

        [JsonProperty("tip")]
        public object Tip { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }


        // ui
        public bool IsPermitirResponder { get; set; }
        public bool IsRespostaCorreta { get; set; }
    }

    public class Answers
    {
        public int Pergunta_Id { get; set; }
        public string Answer_A { get; set; }
        public string Answer_B { get; set; }
        public string Answer_C { get; set; }
        public string Answer_D { get; set; }
        public string Answer_E { get; set; }

        public List<bool> IsRespostasSelecionadas { get; set; }
        public List<Color> CorRepostasSelecionadas { get; set; }

    }

    public class CorrectAnswers
    {
        [JsonIgnore]
        [JsonProperty("Pergunta_Id")]
        public int Pergunta_Id { get; set; }

        [JsonProperty("answer_a_correct")]
        public bool Answer_A_Correct { get; set; }

        [JsonProperty("answer_b_correct")]
        public bool Answer_B_Correct { get; set; }

        [JsonProperty("answer_c_correct")]
        public bool Answer_C_Correct { get; set; }

        [JsonProperty("answer_d_correct")]
        public bool Answer_D_Correct { get; set; }

        [JsonProperty("answer_e_correct")]
        public bool Answer_E_Correct { get; set; }
    }
}
