using System;

class Program
{
    static void Main(string[] args)
    {
        // Создаем новый экземпляр MyArrayDeque с типом данных int
        MyArrayDeque<int> deque = new MyArrayDeque<int>();

        // Добавляем несколько чисел
        deque.Add(10);
        deque.Add(20);
        deque.Add(30);

        // Выводим содержимое очереди
        Console.WriteLine("Начальное содержимое очереди:");
        deque.Print();
        Console.WriteLine();

        // Добавляем число в начало очереди
        deque.AddFirst(5);
        Console.WriteLine("После добавления числа в начало (AddFirst):");
        deque.Print();
        Console.WriteLine();

        // Удаляем первый элемент
        deque.RemoveFirst();
        Console.WriteLine("После удаления первого элемента (RemoveFirst):");
        deque.Print();
        Console.WriteLine();

        // Добавляем число в конец очереди
        deque.AddLast(40);
        Console.WriteLine("После добавления числа в конец (AddLast):");
        deque.Print();
        Console.WriteLine();

        // Печатаем и удаляем первый элемент с помощью PollFirst
        int polledElement = deque.PollFirst();
        Console.WriteLine($"Удален первый элемент (PollFirst): {polledElement}");
        Console.WriteLine("После удаления первого элемента (PollFirst):");
        deque.Print();
        Console.WriteLine();

        // Проверка, содержит ли очередь число 20
        bool contains20 = deque.Contains(20);
        Console.WriteLine($"Содержит ли очередь число 20? {contains20}");
        Console.WriteLine();

        // Проверяем, сколько элементов в очереди
        int size = deque.Size();
        Console.WriteLine($"Размер очереди: {size}");
        Console.WriteLine();

        // Попытка удалить число, которого нет в очереди
        deque.Remove(100);
        Console.WriteLine("После попытки удалить число '100' (которого нет в очереди):");
        deque.Print();
    }
}
