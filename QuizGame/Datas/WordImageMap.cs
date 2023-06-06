using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

// Класс для связывания слов с их изображениями
public class WordImageMap
{
    private readonly Dictionary<string, ImageSource> wordImagePairs;

    public WordImageMap()
    {
        wordImagePairs = new Dictionary<string, ImageSource>();
    }

    // Добавление новой пары "слово - изображение" в словарь
    public void AddWordImagePair(string word, ImageSource imageSource)
    {
        wordImagePairs[word] = imageSource;
    }

    // Замена слов на изображения в заданных предложениях и выведение результата в заданный TextBlock 
    public void ReplaceWordsWithImages(List<string> sentences, TextBlock textBlock, int iconCount)
    {
        textBlock.Inlines.Clear(); // Очистка содержимого TextBlock

        Random random = new Random(); // Создание экземпляра класса Random

        foreach (string sentence in sentences)
        {
            List<Inline> inlines = ReplaceWordsWithImagesInSentence(sentence, iconCount, random); // Замена слов на изображения в каждом предложении

            // Добавляем элементы в TextBlock.Inlines в перемешанном порядке
            foreach (Inline inline in inlines)
            {
                textBlock.Inlines.Add(inline);
            }

            textBlock.Inlines.Add(new LineBreak()); // Добавление переноса строки после каждого предложения
        }
    }

    // Замена слов на изображения в заданном предложении
    private List<Inline> ReplaceWordsWithImagesInSentence(string sentence, int iconCount, Random random)
    {
        string[] words = sentence.Split(' '); // Разбиение предложения на слова
        List<Inline> inlines = new List<Inline>(); // Список для хранения результатов замены
        int totalLength = 0; // Переменная для отслеживания общей длины текста с изображениями
        int imageCount = 0; // Переменная для отслеживания количества изображений в тексте

        // Перемешивание элементов в массиве words
        words = words.OrderBy(x => random.Next()).ToArray();

        foreach (string word in words)
        {
            if (word.StartsWith("[") && word.EndsWith("]"))
            {
                // Удаление "[]" из слова
                string wordWithoutBrackets = word.Trim('[', ']');

                if (imageCount < iconCount)
                {
                    if (wordImagePairs.TryGetValue(wordWithoutBrackets, out ImageSource imageSource))
                    {
                        // Создание элемента изображения
                        Image image = new Image
                        {
                            Source = imageSource
                        };

                        // Добавление изображения в список результатов
                        InlineUIContainer container = new InlineUIContainer(image);
                        inlines.Add(container);

                        // Увеличение счетчика изображений
                        imageCount++;
                    }
                    else
                    {
                        // Если для слова не найдено изображение, добавляем русский перевод как текст
                        string russianWord = Dictionary.TranslateWord(wordWithoutBrackets);
                        inlines.Add(new Run(russianWord + " "));
                    }

                    // Вычисление общей длины текста с изображениями
                    totalLength += wordWithoutBrackets.Length;
                }
                else
                {
                    // Если лимит изображений достигнут, добавляем русский перевод как текст
                    string russianWord = Dictionary.TranslateWord(wordWithoutBrackets);
                    inlines.Add(new Run(russianWord + " "));

                    // Вычисление общей длины текста с изображениями
                    totalLength += russianWord.Length;
                }

                // Ограничение общей длины текста с изображениями
                if (totalLength >= sentence.Length - (iconCount - 1))
                {
                    break;
                }
            }
            else
            {
                // Если слово не заключено в квадратные скобки, добавляем его как текст
                inlines.Add(new Run(word + " "));

                // Вычисление общей длины текста с изображениями
                totalLength += word.Length;
            }
        }

        return inlines;
    }
}
