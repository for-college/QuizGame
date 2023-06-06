using System.Collections.Generic;

namespace QuizGame
{
    public class Model
    {
        private readonly List<Question> questions; // Список вопросов
        private int currentQuestionIndex; // Индекс текущего вопроса

        public Model()
        {
            questions = new List<Question>(); // Инициализация списка вопросов
            currentQuestionIndex = 0; // Установка начального индекса текущего вопроса
        }

        public void AddQuestion(string questionText, string answer)
        {
            Question question = new Question(questionText, answer); // Создание нового вопроса
            questions.Add(question); // Добавление вопроса в список
        }

        public Question GetCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count) // Проверка, не превышен ли индекс текущего вопроса
            {
                return questions[currentQuestionIndex]; // Возвращаем текущий вопрос
            }

            return null; // Если индекс выходит за пределы списка вопросов, возвращаем null
        }

        public void MoveToNextQuestion()
        {
            currentQuestionIndex++; // Переходим к следующему вопросу, увеличивая индекс
        }

        public int GetQuestionCount()
        {
            return questions.Count; // Возвращаем количество вопросов в списке
        }
    }
}
