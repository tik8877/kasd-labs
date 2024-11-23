
using System.Numerics;
public class Project
{


    public class MyVector<T>
    {
        private T[] elementData;
        private int elementCount;
        private int capacityIncrement;
        public MyVector(int initialCapacity, int initialCapacityIncrement)
        {
            elementData = new T[initialCapacity];
            elementCount = initialCapacity;
            capacityIncrement = initialCapacityIncrement;
        }
        public MyVector(int initialCapacity)
        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector()
        {
            elementData = null;
            elementCount = 10;
            capacityIncrement = 0;
        }
        public MyVector(T[] array)
        {
            elementData = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                elementData[i] = array[i];
            elementCount = array.Length;
            capacityIncrement = 0;
        }
        public void Add(T value)
        {
            if (elementCount == elementData.Length)
                Resize();
            elementData[elementCount++] = value;
        }
        private void Resize()
        {
            int newCapacity = capacityIncrement != 0 ? elementData.Length + capacityIncrement : elementData.Length * 2;
            T[] newArray = new T[newCapacity];
            for (int i = 0; i < elementCount; i++)
                newArray[i] = elementData[i];
            elementData = newArray;
        }

        public void AddAll(params T[] array)
        {
            for (int i = 0; i < array.Length; i++) Add(array[i]);
        }
        public int Count => elementCount;
        public void Clear()
        {
            elementCount = 0;
            elementData = null;
        }
        public bool Contains(object value)
        {
            foreach (object element in elementData)
                if (element.Equals(value)) return true;
            return false;
        }
        public bool ContainsAll(T[] array)
        {
            bool[] b = new bool[array.Length];
            int index = 0;
            foreach (T item in array)
            {
                for (int i = 0; i < elementCount; i++)
                {
                    if (item.Equals(elementData[i]))
                    {
                        b[index] = true;
                        if (index != b.Length) index++;
                    }
                }
            }
            foreach (bool element in b)
            {
                if (!element) return false;
            }
            return true;
        }
        public T Get(int index) => elementData[index];
        public bool Empty()
        {
            return elementCount == 0;
        }
        public T Remove(object item)
        {
            int index = -1;
            for (int i = 0; i < elementCount; i++)
                if (item.Equals(elementData[i])) { index = i; break; }
            if (index != -1)
            {
                T element = elementData[index];
                for (int i = index; i < elementCount - 1; i++)
                {
                    elementData[i] = elementData[i + 1];
                }
                elementCount--;
                return element;
            }
            return default;
        }
        public void Remove(params T[] element)
        {
            foreach (object obj in element)
            {
                int i = 0;
                while (i < elementCount)
                {
                    if (obj.Equals(elementData[i]))
                    {
                        for (int j = i; j < elementCount - 1; j++)
                            elementData[j] = elementData[j + 1];
                        elementCount--;
                    }
                    i++;
                }
            }
        }
        public void Retain(params T[] array)
        {
            T[] newArray = new T[elementCount];
            int newSize = 0;
            for (int i = 0; i < elementCount; i++)
            {
                foreach (T obj in array)
                    if (obj.Equals(elementData[i])) newArray[newSize++] = elementData[i];
            }
            elementCount = newSize;
            elementData = newArray;
        }
        public T[] toArray()
        {
            T[] answerArray = new T[elementCount];
            for (int i = 0; i < elementCount; i++) answerArray[i] = elementData[i];
            return answerArray;
        }
        public T[] ToArray(T[] array)
        {
            if (array == null)
                array = new T[elementCount];
            for (int i = 0; i < elementCount; i++)
                array[i] = elementData[i];
            return array;
        }
        public void addAll(int index, params T[] array)
        {
            if (index > elementCount)
            {
                AddAll(array);
                return;
            }
            else
            {
                int ind = 0;
                T[] newArray = new T[elementCount + array.Length];
                for (int i = 0; i < index; i++)
                    newArray[i] = elementData[i];
                for (int i = index; i < index + array.Length; i++)
                    newArray[i] = array[ind++];
                for (int i = index + array.Length; i < elementCount; i++)
                    newArray[i] = elementData[i];
                elementData = newArray;
                elementCount = newArray.Length;
            }
        }
        public int IndexOf(T element)
        {
            for (int i = 0; i < elementCount; i++)
                if (element.Equals(elementData[i])) return i;
            return -1;
        }
        public int LastIndexOf(T element)
        {
            int index = -1;
            for (int i = 0; i < elementCount; i++)
                if (element.Equals(elementData[i])) index = i;
            return index;
        }
        public T Remove(int index)
        {
            if ((index < 0) || (index >= elementCount)) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index; i < elementCount - 1; i++)
                elementData[i] = elementData[i + 1];
            elementCount--;
            return element;
        }
        public void Set(int index, T element)
        {
            if (index >= elementCount || index < 0) throw new ArgumentOutOfRangeException("index");
            if (element == null) throw new ArgumentNullException(element.ToString());
            elementData[index] = element;
        }
        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= elementCount) throw new ArgumentOutOfRangeException("fromindex");
            if (toIndex < 0 || toIndex >= elementCount) throw new ArgumentOutOfRangeException("toindex");
            MyVector<T> newArray = new MyVector<T>(toIndex - fromIndex, 10);
            for (int i = 0; i < newArray.elementCount; i++)
                newArray.Set(i, elementData[fromIndex + i]);
            return newArray;
        }
        public T FirstElement() => elementData[0];
        public T LastElement() => elementData[elementCount - 1];
        public void RemoveElement(int position) => Remove(position);
        public void RemoveRange(int begin, int end)
        {
            if (begin < 0 || end > elementCount || begin > end)
                throw new ArgumentOutOfRangeException("Invalid range");

            int range = end - begin;
            for (int i = begin; i < elementCount - range; i++)
            {
                elementData[i] = elementData[i + range];
            }
            elementCount -= range;
        }
    }

    public class MyStack<T> : MyVector<T>
    {
        private MyVector<T> stack;
        private int top;
        public MyStack()
        {
            top = -1;
            stack = new MyVector<T>(1);
        }
        public void Push(T item)
        {
            top++;
            stack.Add(item);
        }
        public void Pop()
        {
            stack.Remove(top);
            top--;
        }
        public T Peek()
        {
            if (top == -1) throw new Exception("Stack is empty");
            else return stack.Get(top);
        }
        public bool Empty()
        {
            return stack.Count == 0;
        }
        public int Search(T item)
        {
            if (stack.IndexOf(item) == -1) return -1;
            return top - stack.IndexOf(item) + 1;
        }
        public void print()
        {
            for (int i = 0; i <= top; i++)
                Console.WriteLine(stack.Get(i));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            stack.print();
            Console.WriteLine("Top element: " + stack.Peek());

            stack.Pop();
            Console.WriteLine("Top element after pop: " + stack.Peek());

            Console.WriteLine("Stack elements:");
            stack.print();
        }
    }
}