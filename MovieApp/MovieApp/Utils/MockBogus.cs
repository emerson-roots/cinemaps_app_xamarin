using Bogus;
using MovieApp.Models;
using System;
using System.Collections.Generic;

namespace MovieApp.Utils
{
    class MockBogus
    {

        public static List<Pergunta> RandomPerguntas()
        {

            var questions = new Faker<Pergunta>("pt_BR")
                .RuleFor(p => p.Id, p => RandomInt(1, 20))
                .RuleFor(p => p.Question, p => $"{ p.Lorem.Paragraph(1) }?")
                .RuleFor(p => p.Description, p => p.Lorem.Sentence(3))
                .RuleFor(p => p.Explanation, p => p.Lorem.Sentence(3))

                .Generate(10);

            foreach (var pergunta in questions)
            {
                pergunta.Answers = GetAnswers(pergunta.Id);
                pergunta.Correct_Answers = GetCorrectAnswers(pergunta.Id);
            }

            return questions;
        }

        private static Answers GetAnswers(int perguntaId)
        {
            var answer = new Faker<Answers>("pt_BR")
               .RuleFor(p => p.Pergunta_Id, perguntaId)
               .RuleFor(p => p.Answer_A, p => p.Lorem.Sentence(3))
               .RuleFor(p => p.Answer_B, p => p.Lorem.Sentence(3))
               .RuleFor(p => p.Answer_C, p => p.Lorem.Sentence(3))
               .RuleFor(p => p.Answer_D, p => p.Lorem.Sentence(3))
               .RuleFor(p => p.Answer_E, p => p.Lorem.Sentence(3))
               .Generate();

            return answer;
        }

        private static CorrectAnswers GetCorrectAnswers(int perguntaId)
        {
            var answer = new Faker<CorrectAnswers>("pt_BR")
               .RuleFor(p => p.Pergunta_Id, perguntaId)
               //.RuleFor(p => p.Answer_A_Correct, p => p.Random.Bool())
               //.RuleFor(p => p.Answer_B_Correct, p => p.Random.Bool())
               //.RuleFor(p => p.Answer_C_Correct, p => p.Random.Bool())
               //.RuleFor(p => p.Answer_D_Correct, p => p.Random.Bool())
               //.RuleFor(p => p.Answer_E_Correct, p => p.Random.Bool())
               .Generate();

            answer.Answer_A_Correct = true;
            return answer;
        }

        private static decimal RandomDecimal(double minimum, double maximum)
        {
            Random random = new Random();
            double valor = random.NextDouble() * (maximum - minimum) + minimum;
            valor = Math.Round(valor, 1);
            return new decimal(valor);
        }

        private static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
