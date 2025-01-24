using lab23;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Лаба_26
{
    class Program
    {
        static void Main(string[] args)
        {
            MyHashSet<string> set = new MyHashSet<string>();
            string[] lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                // Проверяем строки состоит ли он из символов, не пробел и не null (то есть из слов)
                if (!string.IsNullOrWhiteSpace(line))
                {
                    // Извлекаем слова, состоящие только из латинских букв
                    var words = Regex.Matches(line, @"[a-zA-Z]+");
                    foreach (Match match in words)
                    {
                        // Приводим слово к нижнему регистру и добавляем в множество
                        set.Add(match.Value.ToLower());
                    }
                }
            }

            var uniqueWords = set.ToArray();
            foreach (var word in uniqueWords)
            {
                if (!string.IsNullOrWhiteSpace(word)) // Исключаем пустые значения в множестве
                {
                    Console.WriteLine(word);
                }
            }
        }
    }
}