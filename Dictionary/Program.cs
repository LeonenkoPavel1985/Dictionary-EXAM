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
                        _dictionaryService.CreateDictionary();
                        break;
                    case "2":
                        _dictionaryService.WordProcessing();
                        break;
                    case "3":
                        _dictionaryService.WordProcessing(isReplace: true);
                        break;
                    case "4":
                        _dictionaryService.WordProcessing(isReplaceTranslation: true);
                        break;
                    case "5":
                        _dictionaryService.WordProcessing(isDelete: true);
                        break;
                    case "6":
                        _dictionaryService.WordProcessing(isSearch: true);
                        break;
                    case "7":
                        _dictionaryService.WordProcessing(isExportDict: true);
                        break;
                    case "8":
                        _dictionaryService.ChoiseDictionary();
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
