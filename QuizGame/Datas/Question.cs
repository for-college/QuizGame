namespace QuizGame
{
    public class Question
    {
        public string QuestionText { get; set; } // Текст вопроса
        public string Answer { get; set; } // Ответ на вопрос
        public bool IsAnswered { get; set; } // Флаг, указывающий, был ли данный вопрос отвечен

        public Question(string questionText, string answer)
        {
            QuestionText = questionText; // Установка текста вопроса
            Answer = answer; // Установка ответа на вопрос
            IsAnswered = false; // По умолчанию вопрос не отвечен
        }
    }
}
