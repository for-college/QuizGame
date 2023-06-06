namespace QuizGame
{
    public class GameStats
    {
        public int TotalQuestions { get; set; } // Общее количество вопросов
        public int CorrectAnswers { get; set; } // Количество правильных ответов
        public double PercentCorrect => (double)CorrectAnswers / TotalQuestions * 100; // Процент правильных ответов

        public void UpdateCorrectAnswers()
        {
            CorrectAnswers++; // Увеличиваем количество правильных ответов на 1
        }
    }
}
