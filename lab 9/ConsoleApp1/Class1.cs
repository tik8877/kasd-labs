using System;

namespace Vector
{
    internal class MyVector<T>
    {
        protected T[] elementData;
        protected int elementCount;
        protected int capacityIncrement;

        // Конструкторы
        public MyVector(int initialCapacity, int capacityIncrement)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity), "Capacity must be non-negative.");

            elementData = new T[initialCapacity];
            elementCount = 0;
            this.capacityIncrement = capacityIncrement;
        }

        public MyVector(int initialCapacity) : this(initialCapacity, 0) { }

        public MyVector()
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
        }

        public MyVector(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            elementData = new T[array.Length];
            Array.Copy(array, elementData, array.Length);
            elementCount = array.Length;
            capacityIncrement = 0;
        }

        // Методы
        public void Add(T element)
        {
            EnsureCapacity(elementCount + 1);
            elementData[elementCount++] = element;
        }

        public void AddAll(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            EnsureCapacity(elementCount + array.Length);
            Array.Copy(array, 0, elementData, elementCount, array.Length);
            elementCount += array.Length;
        }

        public void Clear()
        {
            elementCount = 0;
            elementData = new T[10];
        }

        public bool Contains(T element)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (Equals(elementData[i], element))
                    return true;
            }
            return false;
        }

        public bool IsEmpty() => elementCount == 0;

        public void Remove(T element)
        {
            int index = IndexOf(element);
            if (index >= 0)
                RemoveAt(index);
        }

        public void RemoveAll(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            foreach (var item in array)
                Remove(item);
        }

        public int IndexOf(T element)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (Equals(elementData[i], element))
                    return i;
            }
            return -1;
        }

        public T Get(int index)
        {
            ValidateIndex(index);
            return elementData[index];
        }

        public void Set(int index, T element)
        {
            ValidateIndex(index);
            elementData[index] = element;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);
            for (int i = index; i < elementCount - 1; i++)
                elementData[i] = elementData[i + 1];

            elementData[elementCount - 1] = default;
            elementCount--;
        }

        public void RemoveRange(int begin, int end)
        {
            if (begin < 0 || end > elementCount || begin >= end)
                throw new ArgumentOutOfRangeException("Invalid range specified.");

            int range = end - begin;
            for (int i = begin; i < elementCount - range; i++)
                elementData[i] = elementData[i + range];

            Array.Clear(elementData, elementCount - range, range);
            elementCount -= range;
        }

        public int Size() => elementCount;

        public T[] ToArray()
        {
            T[] result = new T[elementCount];
            Array.Copy(elementData, result, elementCount);
            return result;
        }

        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > elementCount || fromIndex >= toIndex)
                throw new ArgumentOutOfRangeException("Invalid sublist range.");

            T[] subArray = new T[toIndex - fromIndex];
            Array.Copy(elementData, fromIndex, subArray, 0, toIndex - fromIndex);
            return new MyVector<T>(subArray);
        }

        // Приватные методы
        private void EnsureCapacity(int minCapacity)
        {
            if (elementData.Length < minCapacity)
            {
                int newCapacity = capacityIncrement > 0
                    ? elementData.Length + capacityIncrement
                    : elementData.Length * 2;

                if (newCapacity < minCapacity)
                    newCapacity = minCapacity;

                Array.Resize(ref elementData, newCapacity);
            }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }
    }

    public class MyStack<T>
    {
        private MyVector<T> stack;

        public MyStack()
        {
            stack = new MyVector<T>();
        }

        public void Push(T item) => stack.Add(item);

        public T Pop()
        {
            if (stack.IsEmpty())
                throw new InvalidOperationException("Stack is empty.");

            T item = stack.Get(stack.Size() - 1);
            stack.RemoveAt(stack.Size() - 1);
            return item;
        }

        public T Peek()
        {
            if (stack.IsEmpty())
                throw new InvalidOperationException("Stack is empty.");

            return stack.Get(stack.Size() - 1);
        }

        public bool IsEmpty() => stack.IsEmpty();

        public int Search(T item)
        {
            int index = stack.IndexOf(item);
            return index >= 0 ? stack.Size() - index : -1;
        }

        public void Print()
        {
            for (int i = stack.Size() - 1; i >= 0; i--)
                Console.WriteLine(stack.Get(i));
        }
    }
}
