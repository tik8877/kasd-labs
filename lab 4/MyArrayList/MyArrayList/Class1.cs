using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace MyArray
{
    public class MyArrayList<T>
    {
        private T[] elementData;
        private int size;

        public MyArrayList()
        {
            size = 0;
            elementData = new T[10]; // Default capacity
        }

        public MyArrayList(T[] a)
        {
            elementData = new T[a.Length];
            Array.Copy(a, elementData, a.Length);
            size = a.Length;
        }

        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = 0;
        }

        public void Add(T e)
        {
            if (size >= elementData.Length)
            {
                Resize(size + 1 + (size / 2));
            }
            elementData[size++] = e;
        }

        private void Resize(int newCapacity)
        {
            T[] newArray = new T[newCapacity];
            Array.Copy(elementData, newArray, size);
            elementData = newArray;
        }

        public void addAll(T[] a)
        {
            foreach (var i in a)
            {
                Add(i);
            }
        }

        public void Clear()
        {
            size = 0;
            elementData = new T[10]; // Reset to default capacity
        }

        public bool Contains(object o)
        {
            for (int i = 0; i < size; i++)
                if (Equals(o, elementData[i]))
                    return true;
            return false;
        }

        public bool ContainsAll(T[] a)
        {
            foreach (var i in a)
                if (!Contains(i))
                    return false;
            return true;
        }

        public bool isEmpty() => size == 0;

        public void Remove(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (Equals(elementData[i], o))
                {
                    for (int j = i; j < size - 1; j++)
                        elementData[j] = elementData[j + 1];
                    size--;
                    return;
                }
            }
        }

        public void removeAll(T[] a)
        {
            foreach (var item in a)
                Remove(item);
        }

        public void retainAll(T[] a)
        {
            T[] newElementData = new T[size];
            int newSize = 0;
            foreach (var item in a)
            {
                if (Contains(item))
                {
                    newElementData[newSize++] = item;
                }
            }
            size = newSize;
            elementData = newElementData;
        }

        public int Size() => size;

        public T[] toArray()
        {
            T[] result = new T[size];
            Array.Copy(elementData, result, size);
            return result;
        }

        public void ToArray(T[] a)
        {
            if (a == null || a.Length < size)
            {
                a = toArray();
            }
            else
            {
                Array.Copy(elementData, a, size);
            }
        }

        public void Add(int index, T e)
        {
            if (index > size) { Add(e); return; }
            if (size >= elementData.Length) Resize(size + 1 + (size / 2));
            for (int i = size; i > index; i--)
            {
                elementData[i] = elementData[i - 1];
            }
            elementData[index] = e;
            size++;
        }

        public void AddAll(int index, T[] a)
        {
            if (index > size) { addAll(a); return; }
            if (size + a.Length > elementData.Length) Resize(size + a.Length);
            for (int i = size - 1; i >= index; i--)
            {
                elementData[i + a.Length] = elementData[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                elementData[index + i] = a[i];
            }
            size += a.Length;
        }

    
public T get(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(nameof(index));
            return elementData[index];
        }

        public int indexOf(object o)
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

        public T remove(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(nameof(index));
            T element = elementData[index];
            for (int i = index; i < size - 1; i++)
            {
                elementData[i] = elementData[i + 1];
            }
            size--;
            return element;
        }

        public void set(int index, T e)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(nameof(index));
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

        public T firstElement()
        {
            if (size == 0) throw new IndexOutOfRangeException("Array is empty");
            return elementData[0];
        }

        public T lastElement()
        {
            if (size == 0) throw new IndexOutOfRangeException("Array is empty");
            return elementData[size - 1];
        }
    }
}