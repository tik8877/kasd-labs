using System;
using System.Collections.Generic;
using TreeMapLibrary;

class Program
{
    static void Main(string[] args)
    {
        // Создаем объект MyTreeMap с типами ключей int и значений string
        MyTreeMap<int, string> treeMap = new MyTreeMap<int, string>();

        // Добавляем элементы
        treeMap.Put(10, "Ten");
        treeMap.Put(20, "Twenty");
        treeMap.Put(5, "Five");
        treeMap.Put(15, "Fifteen");
        treeMap.Put(25, "Twenty-Five");

        // Выводим все элементы (в порядке возрастания ключей)
        Console.WriteLine("TreeMap elements:");
        foreach (var entry in treeMap.EntrySet())
        {
            Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
        }

        // Проверка метода Get
        Console.WriteLine($"Value for key 15: {treeMap.Get(15)}");

        // Проверка методов FirstKey и LastKey
        Console.WriteLine($"First key: {treeMap.FirstKey()}");
        Console.WriteLine($"Last key: {treeMap.LastKey()}");

        // Проверка удаления элемента
        Console.WriteLine("Removing key 10...");
        treeMap.Remove(10);

        Console.WriteLine("TreeMap elements after removal:");
        foreach (var entry in treeMap.EntrySet())
        {
            Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
        }

        //// Проверка методов SubMap, HeadMap, TailMap
        //Console.WriteLine("SubMap (5, 20):");
        //var subMap = treeMap.SubMap(5, 20);
        //foreach (var entry in subMap.EntrySet())
        //{
        //    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
        //}

        //Console.WriteLine("HeadMap (20):");
        //var headMap = treeMap.HeadMap(20);
        //foreach (var entry in headMap.EntrySet())
        //{
        //    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
        //}

        //Console.WriteLine("TailMap (15):");
        //var tailMap = treeMap.TailMap(15);
        //foreach (var entry in tailMap.EntrySet())
        //{
        //    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
        //}

        //// Проверка методов PollFirstEntry и PollLastEntry
        //Console.WriteLine("Polling first entry:");
        //var firstEntry = treeMap.PollFirstEntry();
        //if (firstEntry.HasValue)
        //{
        //    Console.WriteLine($"Polled Key: {firstEntry.Value.Key}, Value: {firstEntry.Value.Value}");
        //}

        //Console.WriteLine("Polling last entry:");
        //var lastEntry = treeMap.PollLastEntry();
        //if (lastEntry.HasValue)
        //{
        //    Console.WriteLine($"Polled Key: {lastEntry.Value.Key}, Value: {lastEntry.Value.Value}");
        //}

        //// Вывод финального состояния дерева
        //Console.WriteLine("Final TreeMap elements:");
        //treeMap.PrintTree();

        //Console.WriteLine("TreeMap size:");
        //Console.WriteLine(treeMap.Size());
    }
}
