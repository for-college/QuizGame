using System.Windows;

namespace QuizGame
{
    public partial class Rules : Window
    {
        private readonly string[] rules;
        private int counter;

        public Rules()
        {
            InitializeComponent();
            ruleButtonBefore.IsEnabled = false;

            rules = new string[]
            {
                "Каждое предложение - стих А.С. Пушкина, поэтому порядок слов и знаков крайне важен! Будьте внимательны.",
                "В качестве ответа надо ввести предложение, в котором все изображения заменены на предполагаемые вами слова.",
                "Количество времени зависит от сложности (3-5 минут). Количество пиктограм также зависит от сложности (3-5 штук). По умолчанию и того и того - 3.",
                "Вы побеждаете, если смогли восстановить не менее половины предложение. Предложение считается восстановленным, если вы ввели все слова и символы в нужном порядке.",
                "Удачной игры!"
            };

            counter = 0;
            UpdateRuleText();
        }

        private void NextRule(object sender, RoutedEventArgs e)
        {
            counter++;
            UpdateRuleText();
        }

        private void PrevRule(object sender, RoutedEventArgs e)
        {
            counter--;
            UpdateRuleText();
        }

        private void UpdateRuleText()
        {
            ruleButtonBefore.IsEnabled = counter > 0;
            ruleButtonNext.IsEnabled = counter < rules.Length - 1;
            rulesBlock.Text = $" [{counter + 1}/{rules.Length}] {rules[counter]}";
        }

        private void CloseWindow(object sender, RoutedEventArgs e) => Close();
    }
}
