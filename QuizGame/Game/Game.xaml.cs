﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizGame
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public Controller controller;
        public View view; // Представление игры
        public WordImageMap wordImageMap; // Карта соответствия слов и изображений


        public int iconCount = 3; // Количество иконок по умолчанию

        public Game()
        {
            InitializeComponent();
            controller = new Controller(new Model(), wordImageMap, view, iconCount);
        }

        private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string answer = AnswerTextBox.Text;
            controller.CheckAnswer(answer); // Проверка ответа
            AnswerTextBox.Text = string.Empty; // Очистить поле ввода ответа
        }

        // Ловим вводимые символы
        private void AnswerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string allowedCharsPattern = "^[а-яА-Я,;!]*$";
            Regex regex = new Regex(allowedCharsPattern);
            if (!regex.IsMatch(e.Text) || Regex.IsMatch(AnswerTextBox.Text + e.Text, "[A-Z]{2,}"))
            {
                e.Handled = true; // Запретить ввод символа
            }
        }
    }
}
