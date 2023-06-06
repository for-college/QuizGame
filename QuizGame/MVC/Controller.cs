using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace QuizGame
{
    public class Controller
    {
        private readonly Model model; // Модель игры
        private readonly WordImageMap wordImageMap; // Карта соответствия слов и изображений
        private readonly View view; // Представление

        public readonly GameStats gameStats; // Статистика игры

        private int iconCount; // Количество иконок
        private TimeSpan countdownTime;
        private readonly DispatcherTimer countdownTimer;

        private bool isFirstQuestionAnswered = false;

        public Controller(Model model, WordImageMap wordImageMap, View view, int iconCount)
        {
            this.model = model;
            this.wordImageMap = wordImageMap;
            this.view = view;
            this.iconCount = iconCount;

            gameStats = new GameStats
            {
                TotalQuestions = model.GetQuestionCount() // Устанавливаем общее количество вопросов в статистику
            };

            countdownTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        public void DisplayCurrentQuestion()
        {
            Question currentQuestion = model.GetCurrentQuestion();

            if (currentQuestion != null)
            {
                view.SetQuestionText(string.Empty);
                List<string> sentences = new List<string>
                {
                    currentQuestion.QuestionText
                };
                wordImageMap.ReplaceWordsWithImages(sentences, view.QuestionTextBlock, iconCount);

                if (isFirstQuestionAnswered)
                {
                    countdownTime = GetCountdownTime();
                    countdownTimer.Start();
                }
            }
            else
            {
                OpenEndGameWindow();
            }
        }

        private TimeSpan GetCountdownTime()
        {
            switch (iconCount)
            {
                case 3:
                    return TimeSpan.FromMinutes(3);
                case 4:
                    return TimeSpan.FromMinutes(4);
                case 5:
                    return TimeSpan.FromMinutes(5);
                default:
                    return TimeSpan.FromMinutes(3); // Возвращаем значение по умолчанию
            }
        }

        public void MoveToNextQuestion()
        {
            model.MoveToNextQuestion(); // Переходим к следующему вопросу в модели
        }

        public void UpdateIconCount(int count)
        {
            iconCount = count; // Обновляем количество иконок
        }

        public void OpenEndGameWindow()
        {
            (int correctAnswers, double percentCorrect) = CalculateResult(); // Вычисляем результаты игры

            bool isWin = percentCorrect >= 50; // Определяем, выиграл ли игрок

            // Создаем окно с итогами игры
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = isWin ? MessageBoxImage.Information : MessageBoxImage.Error;

            MessageBox.Show($"Количество правильных ответов: {correctAnswers}\nПроцент правильных ответов: {percentCorrect:0.00}", $"{(isWin ? "Победа" : "Поражение")}", button, icon);

            // Закрываем текущее окно игры
            Application.Current.Shutdown();
        }

        public (int, double) CalculateResult()
        {
            // Подсчитываем количество правильных ответов
            int correctAnswers = gameStats.CorrectAnswers;

            // Вычисляем процент правильных ответов
            double percentCorrect = (double)correctAnswers / gameStats.TotalQuestions * 100;

            return (correctAnswers, percentCorrect); // Возвращаем результаты
        }

        public void CheckAnswer(string answer)
        {
            Question currentQuestion = model.GetCurrentQuestion();

            if (currentQuestion != null)
            {
                if (!isFirstQuestionAnswered)
                {
                    isFirstQuestionAnswered = true;
                    countdownTime = GetCountdownTime();
                    countdownTimer.Start();
                }

                if (answer.Equals(currentQuestion.Answer, StringComparison.OrdinalIgnoreCase))
                {
                    gameStats.UpdateCorrectAnswers();
                }
                MoveToNextQuestion();
                DisplayCurrentQuestion();
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdownTime = countdownTime.Subtract(TimeSpan.FromSeconds(1));
            view.SetCountdownText(countdownTime.ToString(@"mm\:ss"));

            if (countdownTime.TotalSeconds <= 0)
            {
                TimeIsUp(); // Вызываем метод, когда время вышло
            }
        }

        public void TimeIsUp()
        {
            MessageBox.Show("Время вышло!", "Игра окончена", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Shutdown();
            countdownTimer.Stop();
        }
    }
}
