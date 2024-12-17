namespace MyArrayDeque
{
    public class MyArrayDeque<T>
    {
        private T[] elements;
        private int head;
        private int tail;
        public MyArrayDeque()
        {
            elements = new T[16];
            head = 0;
            tail = 0;
        }
        public MyArrayDeque(T[] array)
        {
            elements = new T[array.Length];
            head = 0;
            int i = 0;
            foreach (T item in array)
            {
                elements[i] = item;
                i++;
            }
            tail = i;
        }
        public MyArrayDeque(int numElements)
        {
            head = 0;
            tail = 0;
            if (numElements > 0)
                elements = new T[numElements];
            else throw new Exception("Отрицательная емкость");
        }
        public void Add(T e)
        {
            if (tail == elements.Length - 1)
            {
                T[] newElements = new T[elements.Length * 2];
                for (int i = head; i < elements.Length; i++)
                    newElements[i] = elements[i];
                elements = newElements;
                elements[tail++] = e;
            }
            else elements[tail++] = e;
        }
        public void AddAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Add(a[i]);
        }
        public void Clear()
        {
            tail = 0;
            head = 0;
        }
        public bool Contains(T o)
        {
            for (int i = head; i <= tail; i++)
                if (Equals(elements[i], o)) return true;
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
                if (!Contains(a[i])) return false;
            return true;
        }
        public bool IsEmpty() => tail == 0;
        public void Remove(T o)
        {
            if (Contains(o))
            {
                int index = 0;
                for (int i = head; i < tail; i++)
                {
                    if (Equals(elements[i], o)) index = i;
                }
                if (index == head)
                {
                    head++; return;
                }
                else if (index == tail)
                {
                    tail--; return;
                }
                else
                {
                    T[] newElements = new T[--tail];
                    for (int i = head; i < index; i++)
                        newElements[i] = elements[i];
                    for (int i = index; i < tail; i++)
                        newElements[i] = elements[i + 1];
                    elements = newElements;
                }
            }
            else throw new Exception("Элемент отсутствует");
        }
        public void RemoveAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Remove(array[i]);
        }
        public void RetainAll(T[] array)
        {
            int newTail = 0;
            T[] newElements = new T[elements.Length];
            for (int i = 0; i < array.Length; i++)
                for (int j = head; j <= tail; j++)
                    if (Equals(elements[j], array[i]))
                    {
                        newElements[newTail] = elements[j];
                        newTail++;
                    }
            elements = newElements;
            tail = newTail;
        }
        public int IndexOfHead() => head;
        public int Size() => tail - head;
        public T[] ToArray()
        {
            T[] array = new T[tail + 1];
            for (int i = 0; i <= tail; i++) array[i] = elements[i];
            return array;
        }
        public void ToArray(ref T[] array)
        {
            if (array == null) array = ToArray();
            Array.Copy(elements, array, tail + 1);
        }
        public T Element() => elements[head];
        public bool Offer(T obj)
        {
            Add(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public T Peek()
        {
            if (tail == 0) return default(T);
            else return elements[head];
        }
        public T Poll()
        {
            if (tail == 0) return default(T);
            else
            {
                T item = elements[head];
                head++;
                return item;
            }
        }
        public void AddFirst(T obj)
        {
            T[] newElements = new T[elements.Length + 1];
            newElements[head] = obj;
            for (int i = head; i <= tail; i++)
                newElements[i + 1] = elements[i];
            elements = newElements;
            tail++;
        }
        public void AddLast(T obj)
        {
            Add(obj);
        }
        public T GetFirst() => elements[head];
        public T GetLast() => elements[tail];
        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public bool OfferLast(T obj)
        {
            AddLast(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public T Pop()
        {
            T item = elements[head];
            head++;
            return item;
        }
        public void Push(T obj)
        {
            AddFirst(obj);
        }
        public T PeekFirst()
        {
            if (tail == 0) return default(T);
            else return elements[head];
        }
        public T PeekLast()
        {
            if (tail == 0) return default(T);
            else return elements[tail];
        }
        public T PollFirst()
        {
            if (tail == 0) return default(T);
            else
            {
                T item = elements[head];
                head++;
                return item;
            }
        }
        public T PollLast()
        {
            if (tail == 0) return default(T);
            else
            {
                T item = elements[tail];
                tail--;
                return item;
            }
        }
        public T RemoveLast()
        {
            T item = elements[tail];
            tail--;
            return item;
        }
        public T RemoveFirst()
        {
            return Pop();
        }
        public bool RemoveLastOccurrence(T obj)
        {
            for (int i = tail; i >= head; i--)
                if (Equals(elements[i], obj))
                {
                    Remove(obj);
                    return true;
                }
            return false;
        }
        public bool RemoveFirstOccurrence(T obj)
        {
            for (int i = head; i <= tail; i++)
                if (Equals(elements[i], obj))
                {
                    Remove(obj);
                    return true;
                }
            return false;
        }
        public T Get(int index)
        {
            return elements[index];
        }
    }
}