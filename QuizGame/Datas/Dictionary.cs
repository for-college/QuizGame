﻿using System.Collections.Generic;

internal static class Dictionary
{
    // Словарь ключ - значение для перевода слов на русский язык.
    // Пояснение: если лимит по кол-ву картинок в тексте, то слова для картинок необходимо перевести
    private static readonly Dictionary<string, string> wordPairs = new Dictionary<string, string>()
    {
        { "you", "твоя" },
        { "web", "сеть" },
        { "carry", "несет" },
        { "enemy", "врагам" },
        { "death", "гибель" },

        { "star", "звезда" },
        { "shine", "светит" },
        { "bright", "ярко" },

        { "win", "победы" },
        { "one", "один" },
        { "fight", "бой" },
        { "us", "нас" },
    };
    // С помощью TryGetValue находим пару и переводим
    public static string TranslateWord(string englishWord)
    {
        if (wordPairs.TryGetValue(englishWord, out string russianWord))
        {
            return russianWord;
        }

        // Если нет пары, то возвращаем исходное слово
        return englishWord;
    }
}