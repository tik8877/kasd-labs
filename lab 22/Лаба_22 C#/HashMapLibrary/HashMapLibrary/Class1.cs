using System.Collections.Generic;
using System;

namespace HashMapLibrary
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

        private Entry[] table;
        private int size;
        private float loadFactor;
        private int threshold;

        // Получение хеш-кода, то есть индификатора содержимого объекта
        private int HashCode(K key)
        {
            return Math.Abs(key.GetHashCode()) % table.Length;
        }

        private int HashCode(V value)
        {
            return Math.Abs(value.GetHashCode()) % table.Length;
        }

        public MyHashMap()
        {
            table = new Entry[16];
            size = 16;
            loadFactor = 0.75f;
            threshold = (int)(table.Length * loadFactor);
        }

        public MyHashMap(int initialCapacity)
        {
            table = new Entry[initialCapacity];
            size = 0;
            loadFactor = 0.75f;
            threshold = (int)(table.Length * loadFactor);
        }

        public MyHashMap(int initialCapacity, float loadFactor)
        {
            table = new Entry[initialCapacity];
            size = 0;
            this.loadFactor = loadFactor;
            threshold = (int)(table.Length * loadFactor);
        }

        public void Clear()
        {
            size = 0;
        }

        public bool ContainsKey(object key)
        {
            int i = HashCode((K)key);
            Entry current = table[i]; // Текущий ключ

            while (current != null)
            {
                if (current.key.Equals(key)) return true;
                current = current.next;
            }
            return false;
        }

        public bool ContainsValue(object value)
        {
            int i = HashCode((V)value);
            Entry current = table[i]; // Текущее значение

            while (current != null)
            {
                if (current.value.Equals(value)) return true;
                current = current.next;
            }
            return false;
        }

        public HashSet<object> EntrySet()
        {
            HashSet<object> retval = new HashSet<object>();
            foreach (Entry entry in table)
            {
                Entry current = entry;
                while (current != null)
                {
                    retval.Add(current);
                }
            }
            return retval;
        }

        public V Get(object key)
        {
            int i = HashCode((K)key);
            Entry current = table[i];

            while (current != null)
            {
                if (current.key.Equals(key)) return current.value;
                current = current.next;
            }
            return default;
        }

        public bool IsEmpty()
        {
            if (table == null) return true;
            else return false;
        }

        public K[] KeySet()
        {
            K[] retval = new K[size];
            int i = 0;
            foreach (Entry entry in table)
            {
                Entry current = entry;
                while (current != null)
                {
                    retval[i] = entry.key;
                    i++;
                    current = current.next;
                }
            }
            return retval;
        }

        public void Put(K key, V value)
        {
            int i = HashCode(key);
            Entry entry = table[i];

            while (entry != null)
            {
                if (entry.key.Equals(key))
                {
                    entry.value = value; // Обновление значения, если ключ уже существует
                    return;
                }
                entry = entry.next;
            }

            // Если ключ не найден, добавляем новый элемент
            Entry newEntry = new Entry(key, value);
            newEntry.next = table[i];
            table[i] = newEntry;
            size++;
        }

        public void Remove(object key)
        {
            int i = HashCode((K)key);
            if (table[i] == null) return;
            if (table[i].key.Equals(key))
            {
                table[i] = table[i].next;
                size--;
                return;
            }

            Entry current = table[i];
            Entry previous = null; // Предыдущий
            while (current != null)
            {
                if (current.key.Equals(key))
                {
                    previous.next = current.next;
                    size--;
                    return;
                }
                previous = current;
                current = current.next;
            }
        }

        public int Size()
        {
            return size;
        }
    }
}