using System;

namespace lab23
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Тестирование MyHashSet ===");

            // Создание множества
            var mySet = new MyHashSet<int>();

            // Добавление элементов
            Console.WriteLine("\nДобавляем элементы: 1, 3, 5, 7, 9");
            mySet.Add(1);
            mySet.Add(3);
            mySet.Add(5);
            mySet.Add(7);
            mySet.Add(9);
            mySet.Print();

            // Проверка Contains
            Console.WriteLine($"\nContains(3): {mySet.Contains(3)}");
            Console.WriteLine($"Contains(10): {mySet.Contains(10)}");

            // Удаление элемента
            Console.WriteLine("\nУдаляем элемент 5");
            mySet.RemoveAll(new[] { 5 });
            mySet.Print();

            // Получение первого и последнего элемента
            Console.WriteLine($"\nFirst: {mySet.First()}");
            Console.WriteLine($"Last: {mySet.Last()}");

            // Добавление массива элементов
            Console.WriteLine("\nДобавляем массив элементов: 10, 15, 20");
            mySet.AddAll(new[] { 10, 15, 20 });
            mySet.Print();

            // Подмножество
            Console.WriteLine("\nПодмножество от 3 до 15");
            var subset = mySet.SubSet(3, 15);
            Console.WriteLine("Subset: " + string.Join(", ", subset));

            // HeadSet
            Console.WriteLine("\nHeadSet (меньше 10)");
            var headset = mySet.HeadSet(10);
            Console.WriteLine("HeadSet: " + string.Join(", ", headset));

            // TailSet
            Console.WriteLine("\nTailSet (больше 10)");
            var tailset = mySet.TailSet(10);
            Console.WriteLine("TailSet: " + string.Join(", ", tailset));

            // Проверка на пустоту
            Console.WriteLine($"\nIsEmpty: {mySet.IsEmpty()}");

            // Очистка множества
            Console.WriteLine("\nОчищаем множество");
            mySet.Clear();
            Console.WriteLine($"IsEmpty после очистки: {mySet.IsEmpty()}");
        }
    }
}
