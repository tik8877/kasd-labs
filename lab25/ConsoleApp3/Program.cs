using lab23;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Лаба_25
{
    class Program
    {
        // Метод для получения общей длины слов в строке
        static int GetTotalWordLength(string line)
        {
            // Проверяем, не является ли строка пустой
            if (string.IsNullOrEmpty(line))
            {
                return 0; // Если строка пустая, возвращаем 0
            }

            // Разделяем строку на слова и суммируем их длины
            var words = line.Split(' ');
            return words.Sum(word => word.Length);
        }

        static void Main(string[] args)
        {
            MyHashSet<string> set = new MyHashSet<string>();
            string[] lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                // Проверяем строки состоит ли он из символов, не пробел и не null (то есть из слов)
                if (!string.IsNullOrWhiteSpace(line))
                {
                    set.Add(line.Trim()); // 'Trim' удаляет пробелы из начала и конца строки
                }
            }

            // Сортируем строки по общей длине слов в порядке возрастания по ключу и используем список для хранения слов
            // 'Distinct' удаляет дубликаты слов
            var sortedLines = set.ToArray().OrderBy(GetTotalWordLength).ToList();
            foreach (var line in sortedLines)
            {
                if (!string.IsNullOrWhiteSpace(line)) // Исключаем пустые значения в множестве
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}