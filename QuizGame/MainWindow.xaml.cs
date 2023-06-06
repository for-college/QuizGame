using System.Windows;
using System.Windows.Controls;

namespace QuizGame
{
    public partial class MainWindow : Window
    {
        public Controller controller; // Контроллер игры
        public View view; // Представление игры

        public int iconCount = 3; // Количество иконок по умолчанию

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game(); // Создание окна с игрой
            game.ShowDialog();
        }

        private void Button_Rules(object sender, RoutedEventArgs e)
        {
            Rules rules = new Rules(); // Создание окна с правилами игры
            rules.ShowDialog();
        }

        private void DifficultyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.IsChecked == true)
            {
                // Обновление значения iconCount в зависимости от выбранной радиокнопки
                switch (radioButton.Name)
                {
                    case "ThreeIconsRadioButton":
                        iconCount = 3;
                        break;
                    case "FourIconsRadioButton":
                        iconCount = 4;
                        break;
                    case "FiveIconsRadioButton":
                        iconCount = 5;
                        break;
                }

                // Обновление значения iconCount в контроллере
                QuizData.IconsCount = iconCount;
            }
        }
    }
}
