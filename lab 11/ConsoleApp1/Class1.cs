namespace MyArrayList_Class
{
    public class MyArrayList<T>
    {
        private T[] elementData;
        private int size;

        public MyArrayList()
        {
            size = 0;
            elementData = new T[0];
        }
        public MyArrayList(T[] a)
        {
            elementData = new T[a.Length];
            for (int i = 0; i < elementData.Length; i++)
            {
                elementData[i] = a[i];
            }
            size = a.Length;
        }
        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = 0;
        }
        public void Add(T e)
        {
            if (size == elementData.Length)
            {
                T[] NewElementData = new T[size + 1 + (size / 2)];
                for (int i = 0; i < size; i++)
                {
                    NewElementData[i] = elementData[i];
                }
                elementData = NewElementData;
            }
            elementData[size] = e;
            size++;
        }
        public void AddAll(T[] a)
        {
            foreach (var smth in a)
            {
                Add(smth);
            }
        }
        public void Clear()
        {
            size = 0;
        }
        public bool Contains(object o)
        {
            for (int i = 0; i < size; i++)
                if (Equals(o, elementData[i])) return true;
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            foreach (var smth in a)
                if (!Contains(smth)) return false;
            return true;
        }
        public bool isEmpty()
        {
            if (size == 0) return true;
            else return false;
        }
        public void Remove(object o)
        {
            for (int i = 0; i < size; i++)
                if (Equals(elementData[i], o))
                {
                    for (int j = i; j < size - 1; j++)
                        elementData[j] = elementData[j + 1];
                    size--;
                }
        }
        public void RemoveAll(T[] a)
        {
            for (int k = 0; k < a.Length; k++)
            {
                Remove(a[k]);
            }
        }
        public void RetainAll(T[] a)
        {
            int newSize = 0;
            T[] newElementData = new T[size];
            for (int k = 0; k < a.Length; k++)
            {
                for (int i = 0; i < size; i++)
                    if (Equals(a[k], elementData[i]))
                    {
                        newElementData[newSize] = a[k];
                        newSize++;
                    }
            }
            size = newSize;
            elementData = newElementData;
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            T[] result = new T[size];
            Array.Copy(elementData, result, size);
            return result;
        }
        public T[] ToArray(ref T[] a)
        {
            if (a == null) a = ToArray();
            Array.Copy(elementData, a, size);
            return a;
        }
        public void Add(int index, T e)
        {
            if (index > size) { Add(e); return; }
            T[] NewElementData = new T[size + 1];
            for (int i = 0; i < index; i++)
            {
                NewElementData[i] = elementData[i];
            }
            NewElementData[index] = e;
            for (int i = index + 1; i < size; i++)
            {
                NewElementData[i] = elementData[i - 1];
            }
            elementData = NewElementData;
            size++;
        }
        public void AddAll(int index, T[] a)
        {
            if (index > size) { AddAll(a); return; }
            T[] NewElementData = new T[size + a.Length];
            for (int i = 0; i < index; i++)
            {
                NewElementData[i] = elementData[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                NewElementData[i + index] = a[i];
            }
            for (int i = index; i < size; i++)
            {
                NewElementData[i + index] = elementData[i];
            }
            elementData = NewElementData;
            size += a.Length;
        }
        public T Get(int index)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException("index");
            return elementData[index];
        }
        public int IndexOf(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (Equals(o, elementData[i])) return i;
            }
            return -1;
        }
        public int LastIndexOf(object o)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (Equals(o, elementData[i])) return i;
            }
            return -1;
        }
        public T Remove(int index)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index + 1; i < size; i++)
            {
                elementData[i - 1] = elementData[i];
            }
            size--;
            return element;
        }
        public void Set(int index, T e)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException("index");
            elementData[index] = e;
        }
        public T[] SubList(int fromindex, int toindex)
        {
            if ((fromindex < 0 || fromindex > size) || (toindex < 0 || toindex > size))
                throw new ArgumentOutOfRangeException("fromindex", "toindex");
            T[] Result = new T[toindex - fromindex];
            for (int i = fromindex; i < toindex; i++)
                Result[i - fromindex] = elementData[i];
            return Result;
        }
    }
}