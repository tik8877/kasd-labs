using System.Collections.Generic;

namespace Lab18
{
    public class MyHashMap<K, V>
    {
        private class Entry
        {
            public K key { get; set; }
            public V value { get; set; }
            public Entry next { get; set; }
            public Entry(K key, V value)
            {
                this.key = key;
                this.value = value;
                next = null;
            }
        }
        Entry[] table;
        int size;
        double loadFactor;
        // Конструктор класса, инициализирует массив таблицы
        public MyHashMap()
        {
            table = new Entry[16];
            size = 0;
            this.loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity)
        {
            table = new Entry[initialCapacity];
            size = 0;
            this.loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity, double loadFactor)
        {
            table = new Entry[initialCapacity];
            size = 0;
            this.loadFactor = loadFactor;
        }
        public void Clear()
        {
            Array.Clear(table);
            size = 0;
        }
        //Проверяет наличие указанного ключа в хэш-карте.
        public bool ContainsKey(K key)
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;
            Entry step = table[index];
            while (step != null)
            {
                if (Equals(step.key, key)) return true;
                step = step.next;
            }
            return false;
        }
        //Проверяет наличие указанного значения в хэш-карте.
        public bool ContainsValue(V value)
        {
            for (int index = 0; index < table.Length; index++)
            {
                Entry step = table[index];
                while (step != null)
                {
                    if (Equals(step.value, value)) return true;
                    step = step.next;
                }
            }
            return false;
        }
        //Возвращает набор всех пар ключ-значение в виде HashSet.
        public HashSet<object> EntrySet()
        {
            HashSet<object> entries = new HashSet<object>();
            for (int index = 0; index < table.Length; index++)
            {
                Entry step = table[index];
                while (step != null)
                {
                    entries.Add(step);
                    step = step.next;
                }
            }
            return entries;
        }
        //Возвращает значение по указанному ключу.Если ключ не найден, выбрасывает исключение.
        public V Get(K key)
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;
            Entry step = table[index];
            while (step != null)
            {
                if (Equals(step.key, key)) return step.value;
                step = step.next;
            }
            throw new Exception("Ключ не найден");
        }
        public bool IsEmpty() => size == 0;
        //Проверяет, пуста ли хэш-карта.
        public K[] KeySet()
        {
            K[] array = new K[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Entry step = table[i];

                    while (step != null)
                    {
                        array[index] = step.key;
                        index++;
                        step = step.next;
                    }
                }
            }
            return array;
        }
        // Метод для добавления или обновления значения по ключу
        public void Put(K key, V value)
        {
            double count = (size + 1) / table.Length;
            if (count >= loadFactor)
            {
                Entry[] newArray = new Entry[table.Length * 3];
                size = 0;

                for (int i = 0; i < table.Length; i++)
                {
                    if (table[i] != null)
                    {
                        Entry val = table[i];
                        while (val != null)
                        {
                            int index = Math.Abs(val.key.GetHashCode()) % newArray.Length;
                            PutInArray(newArray, val.key, val.value);
                            val = val.next;
                        }
                    }
                }
                table = newArray;
            }
            int index1 = Math.Abs(key.GetHashCode()) % table.Length;
            Entry step = table[index1];
            if (step != null)
            {
                int f = 1;
                while (step.next != null)
                {
                    if (Equals(step.key, key))
                    {
                        step.value = value;
                        f = 0;
                    }
                    step = step.next;
                }
                if (f == 1)
                {
                    Entry newNode = new Entry(key, value);
                    step.next = newNode;
                    size++;
                }
            }
            else
            {
                Entry newNode = new Entry(key, value);
                table[index1] = newNode;
                size++;
            }
        }
        // Метод для добавления элемента в указанный массив (используется при увеличении размера)
        private void PutInArray(Entry[] array, K key, V value)
        {
            int index = Math.Abs(key.GetHashCode()) % array.Length;
            Entry newNode = new Entry(key, value);
            if (array[index] != null)
            {
                Entry step = array[index];
                while (step.next != null)
                    step = step.next;
                step.next = newNode;
            }
            else
                array[index] = newNode;
            size++;
        }
        // Метод для удаления элемента по ключу
        public void Remove(K key)
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;
            if (table[index] == null)
                return;
            if (Equals(key, table[index].key))
            {
                table[index] = table[index].next;
                size--;
                return;
            }
            Entry step = table[index];
            Entry pred = null;
            while (step != null)
            {
                if (Equals(step.key, key))
                {
                    pred.next = step.next;
                    size--;
                    return;
                }
                pred = step;
                step = step.next;
            }
        }
        public int Size() => size;
        public void Print()
        {
            for (int index = 0; index < table.Length; index++)
            {
                Entry step = table[index];
                while (step != null)
                {
                    Console.WriteLine(step.value);
                    step = step.next;
                }
            }
        }
    }
    
}