using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QuizGame
{
    public class GameInitializer
    {
        public Game mainWindow;

        private readonly int iconCount = 3;

        private readonly string imageDirectoryPath = Path.Combine(Environment.CurrentDirectory, "..", "..", "Images"); // Картинки берём из папки Images, которая в корне проекта

        private readonly QuizData quizData = new QuizData();

        public GameInitializer(Game mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void Initialize()
        {
            WordImageMap wordImageMap = InitializeWordImageMap();
            Model model = InitializeModel();

            View view = new View(mainWindow);

            Controller controller = new Controller(model, wordImageMap, view);

            mainWindow.controller = controller;
            mainWindow.view = view;

            controller.DisplayCurrentQuestion();
        }

        private ImageSource GetImageSource(string imageName)
        {
            // Обработка ошибки, чтобы приложение не крашилось
            try
            {
                string imagePath = Path.Combine(imageDirectoryPath, imageName);
                // Если картинка существует, то создаём путь до неё и возвращаем его
                if (File.Exists(imagePath))
                {
                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();

                    return imageSource;
                }
                else
                {
                    Console.WriteLine($"Файл {imagePath} не существует");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return null;
            }
        }

        private WordImageMap InitializeWordImageMap()
        {
            WordImageMap wordImageMap = new WordImageMap();

            string[] sentencesImages = quizData.SentencesImages;

            foreach (string imageName in sentencesImages)
            {
                // Сплитим по точке и берём первый элемент (для связки с картинкой)
                string word = imageName.Split('.')[0];
                // Добавляем пару слово - имя картинки
                wordImageMap.AddWordImagePair(word, GetImageSource(imageName));
            }

            return wordImageMap;
        }

        private Model InitializeModel()
        {
            Model model = new Model();

            for (int i = 0; i < quizData.Questions.Length; i++)
            {
                model.AddQuestion(quizData.Questions[i], quizData.Answers[i]);
            }

            return model;
        }
    }
}
