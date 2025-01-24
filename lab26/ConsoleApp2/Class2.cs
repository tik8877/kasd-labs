using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using lab18;


namespace lab23
{
    public class MyHashSet<T> where T : IComparable
    {
        MyHashMap<T, T> map = new MyHashMap<T, T>();
        int size;
        double loadfactor;

        // 1) Конструктор MyHashSet() для создания пустого множества с начальной ёмкостью 16 и коэффициентом загрузки 0,75.
        public MyHashSet()
        {
            map = new MyHashMap<T, T>(16);
            size = 0;
            loadfactor = 0.75;
        }

        // 4) Конструктор MyHashSet(int initialCapacity) для создания пустого множества с указанной начальной ёмкостью и коэффициентом загрузки 0,75.
        public MyHashSet(int initialCapacity)
        {
            map = new MyHashMap<T, T>(initialCapacity);
            size = 0;
            loadfactor = 0.75;
        }

        // 3) Конструктор MyHashSet(int initialCapacity, float loadFactor) для создания пустого множества с указанной начальной ёмкостью и коэффициентом загрузки.
        public MyHashSet(int initialCapacity, float loadFactor)
        {
            map = new MyHashMap<T, T>(initialCapacity, loadFactor);
            size = 0;
            loadfactor = loadFactor;
        }

        // Вывод элементов множества для отладки
        public void Print() => map.KeySetPrint();

        // 7) Метод clear() для удаления всех элементов из множества.
        public void Clear()
        {
            size = 0;
            map = new MyHashMap<T, T>(16);
        }

        // 5) Метод add(T e) для добавления элемента в конец множества.
        public void Add(T elem)
        {
            if (Contains(elem))
                return;
            map.Put(elem, elem);
            size = map.Size();
        }

        // 6) Метод addAll(T[] a) для добавления элементов из массива.
        public void AddAll(T[] elems)
        {
            foreach (var elem in elems)
                Add(elem);
        }

        // 8) Метод contains(object o) для проверки, находится ли указанный объект во множестве.
        public bool Contains(T elem)
        {
            var value = map.Get(elem);
            return !EqualityComparer<T>.Default.Equals(value, default);
        }

        // 9) Метод containsAll(T[] a) для проверки, содержатся ли указанные объекты во множестве.
        public bool[] ContainsAll(T[] elems)
        {
            bool[] boolElems = new bool[elems.Length];
            int i = 0;
            foreach (var elem in elems)
            {
                boolElems[i] = Contains(elem);
                i++;
            }
            return boolElems;
        }

        // 12) Метод removeAll(T[] a) для удаления указанных объектов из множества.
        public void RemoveAll(T[] elems)
        {
            foreach (var elem in elems)
            {
                if (Contains(elem))
                {
                    map.Remove(elem);
                    size = map.Size();
                }
            }
        }

        // 13) Метод retainAll(T[] a) для оставления во множестве только указанных объектов.
        public void RetainAll(T[] elems)
        {
            T[] keys = map.KeySet();
            foreach (var key in keys)
            {
                bool retain = false;
                foreach (var elem in elems)
                {
                    if (key.Equals(elem))
                    {
                        retain = true;
                        break;
                    }
                }
                if (!retain)
                {
                    map.Remove(key);
                    size = map.Size();
                }
            }
        }

        // 15) Метод toArray() для возвращения массива объектов, содержащего все элементы множества.
        public T[] ToArray() => map.KeySet();

        // 17) Метод first() для возврата первого (наименьшего) элемента множества.
        public T? First()
        {
            if (IsEmpty())
                return default;
            T[] keys = map.KeySet();
            T min = keys[0];
            foreach (var elem in keys)
            {
                if (elem.CompareTo(min) < 0)
                    min = elem;
            }
            return min;
        }

        // 18) Метод last() для возврата последнего (наивысшего) элемента множества.
        public T? Last()
        {
            if (IsEmpty())
                return default;
            T[] keys = map.KeySet();
            T max = keys[0];
            foreach (var elem in keys)
            {
                if (elem.CompareTo(max) > 0)
                    max = elem;
            }
            return max;
        }

        // 10) Метод isEmpty() для проверки, является ли множество пустым.
        public bool IsEmpty() => size == 0;

        // 14) Метод size() для получения размера множества в элементах.
        public int Size() => size;

        // 19) Метод subSet(E fromElement, E toElement) для возврата подмножества элементов из диапазона [fromElement; toElement).
        public T[] SubSet(T start, T stop)
        {
            T[] keys = map.KeySet();
            List<T> result = new List<T>();

            foreach (var elem in keys)
            {
                if (elem.CompareTo(start) >= 0 && elem.CompareTo(stop) < 0)
                    result.Add(elem);
            }

            return result.ToArray();
        }

        // 20) Метод headSet(E toElement) для возврата множества элементов, меньших чем указанный элемент.
        public T[] HeadSet(T toElement)
        {
            T[] keys = map.KeySet();
            List<T> result = new List<T>();

            foreach (var elem in keys)
            {
                if (elem.CompareTo(toElement) < 0)
                    result.Add(elem);
            }

            return result.ToArray();
        }

        // 21) Метод tailSet(E fromElement) для возврата части множества из элементов, больших или равных указанному элементу.
        public T[] TailSet(T fromElement)
        {
            T[] keys = map.KeySet();
            List<T> result = new List<T>();

            foreach (var elem in keys)
            {
                if (elem.CompareTo(fromElement) >= 0)
                    result.Add(elem);
            }

            return result.ToArray();
        }
    }
}
