using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector

{
    internal class MyVector<T>
    {

        private T[] elementData;
        private int elementCount; 
        private int capacityIncrement; 

        public  MyVector(int initialCapacity, int initialCapacityIncrement)
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
        public MyVector(T[] a)
        {
            elementData = new T[a.Length];
            for(int i = 0; i < a.Length; i++)
            {
                elementData[i] = a[i];
            }
            elementCount = a.Length;
            capacityIncrement = 0;
        }
        public void Add(T e)
        {
            if (elementData.Length == elementCount) Resize();
            elementData[elementCount++] = e;
            elementCount++;
        }
        private void Resize()
        {
            T[] newArray;
            if (capacityIncrement != 0) newArray = new T[(int)(elementData.Length * capacityIncrement)];
            else newArray = new T[(int)(elementData.Length * 2)];
            for (int i = 0; i < elementCount; i++)
                newArray[i] = elementData[i];
            elementData = newArray;
        }

        public void AddAll(T[] a)
        {
            for(int i = 0; i < a.Length; i++)Add(a[i]);
            
        }
        public void clear() { elementCount = 0; elementData = null; }
        public bool Contains(object e)
        {
            for(int i = 0; i < elementData.Length; i++)
            {
                if ((object)elementData[i]==e) return true;
            }
            return false;
        }
        public bool AllContains(T[] a)
        {
            int k = 0;
            for(int i = 0; i < a.Length; i++)
            {
                if (Contains(a[i]) == true) k++;
            }
            if (k == a.Length) return true;
            return false;
        }
        public bool IsEmpty() {
            if ((elementCount == 0) && (elementData == null))return true;
            else return false;
        }
        public void Remove(object o)
        {
            if (Contains(o) == true)
            {
                for(int i = 0; i < elementData.Length; i++)
                {
                    if ((object)elementData[i] == o)
                    {
                        for(int j = i; j < elementData.Length; j++)
                        {
                            elementData[i] = elementData[i + 1];

                        }
                        elementCount--; 
                    }
                }
            }
        }
        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Remove(a[i]);
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
        public int Size() { return elementCount; }
        public T[] ToArray()
        {
            T[] newArray = new T[elementCount];
            for (int i = 0; i < elementCount; i++) newArray[i] = elementData[i];
            return newArray;

        }
        public T[] ToArray(T[] a)
        {
            if (a == null) a = new T[elementCount];
            for (int i = 0; i < elementCount; i++) a[i] = elementData[i];
            return a;
        }
        public void Add(int index, T e) 
        {
            if (index > elementCount)
            {
                Add(e);
                return;
            }  
            T[] NewElementData = new T[elementData.Length];
            if (elementCount == elementData.Length)
            {
                if (capacityIncrement == 0) NewElementData = new T[elementCount * 2];
                else NewElementData = new T[elementCount + capacityIncrement];
            }
            for (int i = 0; i < index; i++)
            {
                NewElementData[i] = elementData[i];
            }
            NewElementData[index] = e;
            for (int i = index + 1; i < elementCount; i++)
            {
                NewElementData[i] = elementData[i - 1];
            }
            elementData = NewElementData;
            elementCount++;
        }
        public void AddAll(int index, T[] array)
        {
            if (index > elementCount)
            {
                AddAll(array);
                return;
            }
            int ind = 0;
            T[] newArray = new T[elementCount + array.Length];
            for (int i = 0; i < index; i++) newArray[i] = elementData[i];
            for (int i = index; i < array.Length; i++) newArray[i] = array[ind++];
            for (int i = index + array.Length; i < elementCount; i++) newArray[i] = elementData[i];
            elementData = newArray;
            elementCount = newArray.Length;
        }
        public T Get(int index) => elementData[index];
        public int indexOf(object o)
        {
          
            for (int i = 0; i < elementCount; i++)
            {
                if ((object)elementData[i] == o) { return i;  }
            }
            return -1;
        }
        public int LastIndexOf(object o)
        {
            int k = 0;
            for (int i = 0; i < elementCount; i++)
            {
                if ((object)elementData[i] == o) { k=i; }
            }
            if (k == 0) return -1;
            else return k;
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
        public int Count => elementCount;

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
            if ((begin < 0) || (begin >= elementCount)) throw new ArgumentOutOfRangeException("begin out of range");
            if ((end < 0) || (end >= elementCount)) throw new ArgumentOutOfRangeException("end out of range");
            T[] newArray = new T[elementCount - (end - begin)];
            int index = 0;
            for (int i = begin; i < end; i++)
            {
                newArray[index++] = elementData[i];
            }
            this.Remove(newArray);
        }
    }
   
}

