
using System;

namespace Task16
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание списка с использованием конструктора
            MyLinkedList<int> linkedList = new MyLinkedList<int>();

            // Добавление элементов в список
            linkedList.Add(10);
            linkedList.Add(20);
            linkedList.Add(30);
            linkedList.Add(40);
            linkedList.Add(50);

            Console.WriteLine("Список после добавления элементов:");
            PrintList(linkedList);

            // Добавление элемента по индексу
            linkedList.Add(2, 25);
            Console.WriteLine("\nСписок после добавления 25 на индекс 2:");
            PrintList(linkedList);

            // Проверка наличия элементов
            Console.WriteLine($"\nСодержит ли список 30? {linkedList.Contains(30)}");
            Console.WriteLine($"Содержит ли список 100? {linkedList.Contains(100)}");

            // Удаление элемента
            linkedList.Remove(40);
            Console.WriteLine("\nСписок после удаления 40:");
            PrintList(linkedList);

            // Получение индексов
            Console.WriteLine($"\nИндекс элемента 25: {linkedList.IndexOf(25)}");
            Console.WriteLine($"Последний индекс элемента 10: {linkedList.LastIndexOf(10)}");

            // Использование методов, работающих с первым и последним элементом
            linkedList.AddFirst(5);
            linkedList.AddLast(60);
            Console.WriteLine("\nСписок после добавления 5 в начало и 60 в конец:");
            PrintList(linkedList);

            Console.WriteLine($"\nПервый элемент: {linkedList.GetFirst()}");
            Console.WriteLine($"Последний элемент: {linkedList.GetLast()}");

            // Удаление первого и последнего элементов
            linkedList.RemoveFirst();
            linkedList.RemoveLast();
            Console.WriteLine("\nСписок после удаления первого и последнего элементов:");
            PrintList(linkedList);

            // Очистка списка
            linkedList.Clear();
            Console.WriteLine($"\nСписок пуст? {linkedList.Empty()}");
        }

        static void PrintList<T>(MyLinkedList<T> list)
        {
            for (int i = 0; i < list.Size(); i++)
            {
                Console.Write(list.Get(i) + " ");
            }
            Console.WriteLine();
        }
    }
}
