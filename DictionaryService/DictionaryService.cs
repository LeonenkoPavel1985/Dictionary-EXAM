using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Newtonsoft.Json;
public class DictionaryService
{
    private static List<Dictionary> _dictionaries;
    private static int _dictionaryId;

    public DictionaryService()
    {
        LoadDictionaries();
        _dictionaryId = -1;
    }

    private void LoadDictionaries() // Загружаем словарь из файла.
    {
        _dictionaries = new List<Dictionary>();

        if (File.Exists("dictionaries.json")) // Проверяем существует ли файл.
        {
            var jsonFile = File.ReadAllText("dictionaries.json"); // Открываем текстовый файл, считываем весь текст файла в строку и затем закрываем файл.
            _dictionaries = JsonConvert.DeserializeObject<List<Dictionary>>(jsonFile); // Производим десереализацию.
        }
    }
    public void CreateDictionary() // Создаем словарь.
    {
        Console.WriteLine("Введите тип словаря:");
        string type = Console.ReadLine();

        if (string.IsNullOrEmpty(type.Trim())) // Проверяем, действительно ли указанная строка является пустой строкой ("").
        {
            Console.WriteLine("Введите тип словаря:");
            return;
        }

        if (_dictionaries.Any(d => d.type == type)) // Проверяем существование хотя бы одного элемента в последовательности (типа словаря).
        {
            Console.WriteLine("Внимание !!! Указанный тип словаря уже создан.");
            return;
        }

        int i = 0;

        if (_dictionaries.Any())
        {
            i = _dictionaries.Max(d => d.id);
            i++;
        }

        _dictionaries.Add(new Dictionary() // Добавляем словарь.
        {
            id = i,
            type = type
        });

        Console.WriteLine($"Словарь {type} создан.");
        SaveDictionaries(); // Сохраняем словарь.
    }

    public void PrintDictionaries() // Печатаем словари.
    {
        foreach (var dir in _dictionaries)
        {
            Console.WriteLine($"№{dir.id} - {dir.type}");
        }
    }

    public void ChoiseDictionary() // Выбор словаря.
    {
        PrintDictionaries(); // Печатаем имеющиеся словари.
        Console.WriteLine("Введите номер словаря:");

        string value = Console.ReadLine();

        if (!int.TryParse(value, out int id)) // Проверяем существование номера словаря.
        {
            Console.WriteLine("Введено не верное значение.");
            return;
        }

        if (!_dictionaries.Any(d => d.id == id)) // Проверяем существует ли словарь с таким номером.
        {
            Console.WriteLine("Словарь не найден.");
            return;
        }

        var dict = _dictionaries.Where(d => d.id == id).FirstOrDefault(); // Возвращаем первый элемент последовательности или значение по умолчанию, если ни одного элемента не найдено (выбор словаря).
        _dictionaryId = dict.id;

        Console.WriteLine($"Выбран словарь: {dict.type}");
    }

    public void WordProcessing(bool isReplace = false, bool isReplaceTranslation = false, bool isDelete = false, bool isSearch = false, bool isExportDict = false) // Работа со словарем.
    {
        if (_dictionaryId < 0) // Проверяем выбран словарь или нет.
        {
            Console.WriteLine("Словарь не выбран.");
            return;
        }

        var dict = _dictionaries.Where(d => d.id == _dictionaryId).FirstOrDefault();

        if (dict == null)
        {
            Console.WriteLine("Словарь не выбран или удален."); // Проверяем существует словарь или нет.
        }

        if (isExportDict)
        {
            ExportDictionary(dict); // Сохраняем в файл.
            return;
        }

        Console.WriteLine("Введите слово: ");
        string word = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(word)) // Проверяем правильность ввода слова.
        {
            Console.WriteLine("Введено не верное значение.");
            return;
        }

        var w = dict?.words?.FirstOrDefault(w => w.initial == word);

        if (w != null)
        {
            if (isSearch)
            {
                Console.WriteLine($"{w.initial}");
                Console.WriteLine($"Слово {w.initial}; перевод слова: {string.Join(',', w.translation)}");
                return;
            }

            if (isDelete) // Удаляем перевод слова из словаря.
            {
                dict.words.Remove(w);
                Console.WriteLine($"Слово удалено.");
                return;
            }

            if (isReplace) //Вводим замену перевода слова.
            {
                Console.WriteLine("Введите замену слова: ");
                string newWord = Console.ReadLine().Trim(); // Удаляем все начальные и конечные символы пробела из текущей строки.

                if (string.IsNullOrEmpty(newWord)) // Проверяем корректность ввода слова.
                {
                    Console.WriteLine("Введено не верное значение.");
                    return;
                }

                w.initial = newWord;
                Console.WriteLine("Слово изменено.");
                SaveDictionaries(); // Сохрнаняем измененный словарь.
                return;
            }

            if (isReplaceTranslation) // Проверяем введен ли новый перевод слова.
            {
                var newTranslation = AddTranslation();

                if (!newTranslation.Any())
                {
                    Console.WriteLine("Не ввели ни одного значения нового перевода, перевод не изменен.");
                    return;
                }

                w.translation = newTranslation;

                Console.WriteLine("Перевод изменен.");
                SaveDictionaries(); // Сохрнаняем измененный словарь.
                return;
            }

            Console.WriteLine("Слово уже есть в словаре.");
            return;
        }

        if (isReplace || isReplaceTranslation || isDelete || isSearch) // Проверяем есть ли слово в словаре.
        {
            Console.WriteLine($"Слово \"{word}\" не найдено.");
            return;
        }

        w = new Word()
        {
            initial = word,
            translation = new List<string>()
        };

        w.translation = AddTranslation();

        if (!w.translation.Any()) // Проверяем есть у слова перевод.
        {
            Console.WriteLine($"У слова \"{w.initial}\" нет ни одного перевода, слово не добавлено.");
            return;
        }

        if (dict.words == null)
        {
            dict.words = new List<Word>();
        }

        dict.words.Add(w);
        Console.WriteLine($"Слово \"{w.initial}\" добавлено в словарь.");
        SaveDictionaries(); // Сохраняем словарь в файл.
    }

    private List<string> AddTranslation() // заносим перевод слова.
    {
        var result = new List<string>();

        while (true)
        {
            Console.WriteLine("Введите перевод или слово \"stop\" для завершения ввода.");

            string translation = Console.ReadLine().Trim();

            if (translation == "stop")
            {
                break;
            }

            if (string.IsNullOrEmpty(translation))
            {
                Console.WriteLine("Введено не верное значение.");
                continue;
            }

            result.Add(translation);
        }

        return result;
    }

    private void SaveDictionaries() // Сохраняем словарь в файл.
    {
        var json = JsonConvert.SerializeObject(_dictionaries);
        File.WriteAllText("dictionaries.json", json);
    }

    private void ExportDictionary(Dictionary dict) // Экспортируем словарь в отдельный файл.
    {
        Console.WriteLine("Введите имя файла:");
        string fileName = Console.ReadLine();

        using (StreamWriter writetext = new StreamWriter(fileName))
        {
            foreach (var d in dict.words)
            {
                writetext.WriteLine($"{dict.type}; {d.initial}; {string.Join(',', d.translation)}");
            }
        }

        Console.WriteLine("Данные экспортированны.");
    }
}

