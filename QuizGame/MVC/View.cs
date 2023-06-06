using System.Windows.Controls;

namespace QuizGame
{
    public class View
    {
        public Game MainWindow { get; } // Главное окно представления

        public View(Game mainWindow)
        {
            MainWindow = mainWindow; // Установка главного окна представления
        }

        public TextBlock QuestionTextBlock => MainWindow.QuestionTextBlock; // Текстовый блок для отображения вопроса

        public void SetQuestionText(string text)
        {
            MainWindow.Dispatcher.Invoke(() =>
            {
                MainWindow.QuestionTextBlock.Text = text; // Установка текста вопроса в текстовый блок
            });
        }

        public void SetCountdownText(string text)
        {
            MainWindow.Dispatcher.Invoke(() =>
            {
                MainWindow.CountdownTextBlock.Text = text; // Установка текста времени обратного отсчета в текстовый блок
            });
        }
    }
}
