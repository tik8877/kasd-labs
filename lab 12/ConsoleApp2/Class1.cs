using MyArrayList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayList
{
    public class MyArrayList<T>
    {
        private T[] elementData;
        private int size;
        public MyArrayList() // №1 конструктор для создания пустого динамического массива
        {
            elementData = new T[10];
            size = 0;
        }

        public MyArrayList(T[] a) // №2 конструктор для создания динамического массива из переданного массива
        {
            elementData = new T[a.Length];
            for (int i = 0; i < elementData.Length; i++) elementData[i] = a[i];
            size = a.Length;
        }

        public MyArrayList(int capacity) // №3 конструктор для создания пустого динамического массива с заданной емкостью
        {
            elementData = new T[capacity];
            size = 0;
        }

        public void Add(T e) // №4 метод для добавления элемента в конец массива
        {
            if (size == elementData.Length)
            {
                T[] NewelementData = new T[(size / 2) + size + 1];
                for (int i = 0; i < elementData.Length; i++) NewelementData[i] = elementData[i];
                elementData = NewelementData;
            }
            elementData[size] = e;
            size++;
        }

        public void AddAll(T[] a) // №5 метод для добавления элементов из другого массива
        {
            foreach (var element in a)
            {
                Add(element);
            }
        }

        public void Clear() // №6 метод для очистки массива
        {
            size = 0;
        }

        public bool Contains(object o) // №7 метод для проверки наличия элемента в массиве
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o)) return true;
            }
            return false;
        }

        public bool ContainsAll(T[] a) // №8 метод для проверки наличия всех элементов из другого массива
        {
            foreach (var element in a)
            {
                if (!Contains(element)) return false;
            }
            return true;
        }

        public bool IsEmpty() // №9 метод для проверки, пуст ли массив
        {
            if (size == 0) return true;
            else return false;
        }

        public void Remove(object o) // №10 метод для удаления указанного объекта из массива
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o))
                {
                    for (int j = i; j < size - 1; j++) elementData[j] = elementData[j + 1];
                    size--;
                }
            }
        }

        public void RemoveAll(T[] a) // №11 метод для удаления всех указанных объектов из массива
        {
            foreach (var element in a)
            {
                Remove(element);
            }
        }

        public void RetainAll(T[] a) // №12 метод для оставления в массиве только указанных объектов
        {
            int newSize = 0;
            T[] newElementData = new T[size];
            for (int k = 0; k < a.Length; k++)
            {
                for (int i = 0; i < size; i++)
                    if (elementData[i].Equals(a[k]))
                    {
                        newElementData[newSize] = a[k];
                        newSize++;
                    }
            }
            size = newSize;
            elementData = newElementData;
        }

        public int Size() // №13 метод для получения размера массива
        {
            return size;
        }

        public T[] ToArray() // №14 метод для преобразования в массив
        {
            T[] result = new T[size];
            for (int i = 0; i < size; i++) result[i] = elementData[i];
            return result;
        }

        public T[] ToArray(T[] a) // №15 метод для преобразования в массив с заданным типом
        {
            if (a == null || a.Length < size) return ToArray();
            for (int i = 0; i < size; i++) a[i] = elementData[i];
            return a;
        }

        public void Add(int index, T e) // №16 метод для добавления элемента по индексу
        {
            if (index > size)
            {
                Add(e);
                return;
            }
            T[] NewElementData = new T[size + 1];
            for (int i = 0; i < index; i++) NewElementData[i] = elementData[i];
            NewElementData[index] = e;
            for (int i = index + 1; i < size; i++) NewElementData[i] = elementData[i - 1];
            elementData = NewElementData;
            size++;
        }

        public void AddAll(int index, T[] a) // №17 метод для добавления элементов из другого массива по индексу
        {
            foreach (var element in a)
            {
                Add(index++, element);
            }
        }

        public T Get(int index) // №18 метод для получения элемента по индексу
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("Index out of range");
            return elementData[index];
        }

        public int IndexOf(object o) // №19 метод для нахождения индекса указанного объекта
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }

        public int LastIndexOf(object o) // №20 метод для нахождения последнего вхождения указанного объекта
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }

        public T Remove(int index) // №21 метод для удаления элемента по индексу
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("Index out of range");
            T removedElement = elementData[index];
            for (int i = index; i < size - 1; i++) elementData[i] = elementData[i + 1];
            size--;
            return removedElement;
        }

        public void Set(int index, T e) // №22 метод для замены элемента по индексу новым элементом
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("Index out of range");
            elementData[index] = e;
        }

        public T[] SubList(int fromIndex, int toIndex) // №23 метод для получения подсписка элементов
        {
            if (fromIndex < 0 || toIndex > size || fromIndex > toIndex) throw new ArgumentOutOfRangeException("Index out of range");
            T[] subList = new T[toIndex - fromIndex];
            int index = 0;
            for (int i = fromIndex; i < toIndex; i++)
            {
                subList[index] = elementData[i];
                index++;
            }
            return subList;
        }
    }
}

namespace MyPriorityDeque
{
    public class MyPriorityQueue<T> where T : IComparable<T>
    {
        private MyArrayList<T> queue;
        private int size;
        private Comparer<T> comparator;

        public MyPriorityQueue() // конструктор для создания пустой очереди с приоритетами
        {
            queue = new MyArrayList<T>(11);
            size = 0;
            comparator = Comparer<T>.Default;
        }

        public void Queuelify(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int parent = index;

            if (left < size && comparator.Compare(queue.Get(left), queue.Get(parent)) > 0)
            {
                parent = left;
            }

            if (right < size && comparator.Compare(queue.Get(right), queue.Get(parent)) > 0)
            {
                parent = right;
            }

            if (parent != index)
            {
                Swap(index, parent);
                Queuelify(parent);
            }
        }

        public void Swap(int index1, int index2) // конструктор  для создания пустой очереди с приоритетами
        {
            T temp1 = queue.Get(index1);
            T temp2 = queue.Get(index2);
            queue.Set(index2, temp1);
            queue.Set(index1, temp2);
        }

        public MyPriorityQueue(T[] a) // конструктор для создания очереди с приоритетами
        {
            queue = new MyArrayList<T>(a.Length);
            size = a.Length;
            comparator = Comparer<T>.Default;
            for (int i = 0; i < size; i++)
            {
                queue.Add(a[i]);
            }
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }

        public MyPriorityQueue(int initialCcapacity) // конструктор для создания пустой очереди с приоритетами с указанной начальной ёмкостью
        {
            queue = new MyArrayList<T>(initialCcapacity);
            size = 0;
            comparator = Comparer<T>.Default;
        }

        public MyPriorityQueue(int initialCcapacity, Comparer<T> comparator) // конструктор для создания пустой очереди с приоритетами с указанной начальной ёмкостью и компаратором
        {
            queue = new MyArrayList<T>(initialCcapacity);
            size = initialCcapacity;
            this.comparator = comparator;
        }

        public MyPriorityQueue(MyPriorityQueue<T> c) // конструктор для создания очереди с приоритетами, содержащей элементы указанной очереди с приоритетами
        {
            queue = new MyArrayList<T>(c.size);
            size = c.size;
            comparator = c.comparator;
            for (int i = 0; i < c.size; i++)
            {
                queue.Add(c.queue.Get(i));
            }
            size = c.size;

            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }

        public void Add(T e) // метод для добавления элемента в конец очереди с приоритетами
        {
            queue.Add(e);
            size++;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }

        public void AddAll(T[] a) // метод для добавления элементов из массива
        {
            queue.AddAll(a);
            size += a.Length;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }

        public void Clear() // метод для удаления всех элементов из очереди с приоритетами
        {
            queue.Clear();
            size = 0;
        }

        public bool Contains(object o) // метод для проверки, находится ли указанный объект в очереди с приоритетами
        {
            return queue.Contains(o);
        }

        public bool ContainsAll(T[] a) // метод для  проверки, содержатся ли указанные объекты в очереди с приоритетами
        {
            return queue.ContainsAll(a);
        }

        public bool IsEmpty() // метод для проверки, является ли очередь с приоритетами пустой
        {
            return queue.IsEmpty();
        }

        public void Remove(object o) // метод для удаления указанного объекта из очереди с приоритетами, если он есть там
        {
            queue.Remove(o);
            size--;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }

        public void RemoveAll(T[] a) // метод для удаления указанных объектов из очереди с приоритетами
        {
            queue.RemoveAll(a);
            size -= a.Length;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }

        public void RetainAll(T[] a) // метод для оставления в очереди с приоритетами только указанных объектов
        {
            queue.RetainAll(a);
            size = a.Length;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
        }

        public int Size() // метод для получения размера очереди с приоритетами в элементах.
        {
            return queue.Size();
        }

        public T[] ToArray() // метод для возвращения массива объектов, содержащего все элементы очереди с приоритетами
        {
            return queue.ToArray();
        }

        public T[] ToArray(T[] a) // метод для возвращения массива объектов, содержащего все элементы очереди с приоритетами

        {
            return queue.ToArray(a);
        }

        public T Element() // метод для возвращения элемента из головы очереди с приоритетами без его удаления
        {
            return queue.Get(0);
        }

        public bool Offer(T obj) // метод для попытки добавления элемента obj в очередь с приоритетами. Возвращает true если добавлен и false если нет
        {
            queue.Add(obj);
            size++;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }

            bool f = true;
            if (queue.Contains(obj)) return f;
            else return f == false;
        }

        public T Peek() // метод для возврата элемента из головы очереди с приоритетами без его удаления
        {
            if (size == 0) throw new ArgumentOutOfRangeException();
            else return queue.Get(0);
        }

        public T Poll() // метод для удаления и возврата элемента из головы очереди с приоритетами
        {
            if (size == 0) throw new ArgumentOutOfRangeException();
            else
            {
                T element = queue.Get(0);
                queue.Remove(element);
                size--;
                for (int i = size / 2; i >= 0; i--)
                {
                    Queuelify(i);
                }
                return element;
            }
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
                Console.WriteLine(queue.Get(i));
            Console.WriteLine();
        }
    }
}