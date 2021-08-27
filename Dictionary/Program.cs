using System;

namespace Dictionary
{
    class Program
    {
        private static DictionaryService _dictionaryService = new DictionaryService();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("|--ПРИЛОЖЕНИЕ АНГЛО-РУССКИЙ СЛОВАРЬ.--|");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine(" ");
                Console.WriteLine("МЕНЮ ПРОГРАММЫ:");
                Console.WriteLine("1. Создание словаря.");
                Console.WriteLine("2. Добавить слово и его перевод в уже существующий словарь.");
                Console.WriteLine("3. Замена слова.");
                Console.WriteLine("4. Замена перевода в слове.");
                Console.WriteLine("5. Удаление слова и его переводов из словаря.");
                Console.WriteLine("6. Поиск перевода слова.");
                Console.WriteLine("7. Экспорт словаря в отдельный файл.");
                Console.WriteLine("8. Выбрать словарь.");
                Console.WriteLine("9. Выход из программы.");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Создание словаря.");
                        _dictionaryService.CreateDictionary();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Добавление слова и его перевода в уже существующий словарь.");
                        _dictionaryService.WordProcessing();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Замена слова.");
                        _dictionaryService.WordProcessing(isReplace: true);
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Замена перевода в слове.");
                        _dictionaryService.WordProcessing(isReplaceTranslation: true);
                        Console.Clear();
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Удаление слова и его переводов из словаря.");
                        _dictionaryService.WordProcessing(isDelete: true);
                        Console.Clear();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Поиск перевода слова.");
                        _dictionaryService.WordProcessing(isSearch: true);
                        Console.Clear();
                        break;
                    case "7":
                        Console.Clear();
                        Console.WriteLine("Экспорт словаря в отдельный файл.");
                        _dictionaryService.WordProcessing(isExportDict: true);
                        Console.Clear();
                        break;
                    case "8":
                        Console.Clear();
                        Console.WriteLine("Выбор словаря.");
                        _dictionaryService.ChoiseDictionary();
                        Console.Clear();
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
