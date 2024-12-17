using System.Collections.Generic;
using System.Data;


public class MyLinkedList<T>
{
    class Node<T>
    {
        public T value;
        public Node<T>? next;
        public Node<T>? pred;
        public Node(T element)
        {
            next = null;
            pred = null;
            value = element;
        }
    }
    Node<T>? first;
    Node<T>? last;
    int size;
    public MyLinkedList()
    {
        first = null;
        last = null;
        size = 0;
    }
    public MyLinkedList(T[] a)
    {
        foreach (T el in a)
        {
            Add(el);
        }
    }
    public void Add(T el)
    {
        Node<T> newNode = new Node<T>(el);
        if (size == 0)
        {
            first = newNode;
            last = newNode;
        }
        else
        {
            last.next = newNode;
            newNode.pred = last;
            last = newNode;
        }
        size++;
    }
    public void AddAll(T[] array)
    {
        foreach (T e in array)
            Add(e);
    }
    public void Clear()
    {
        first = null;
        last = null;
        size = 0;
    }
    public bool Contains(object o)
    {
        Node<T>? step = first;
        while (step != null)
        {
            if (Equals(o, step.value))
                return true;
            step = step.next;
        }
        return false;
    }
    public bool ContainsAll(T[] array)
    {
        foreach (T e in array)
        {
            if (!Contains(e)) return false;
        }
        return true;
    }
    public bool Empty() => size == 0;
    public void Remove(T obj)
    {
        if (Contains(obj))
        {
            if (Equals(obj, first.value))
            {
                first = first.next;
                size--;
                return;
            }
            if (Equals(obj, last.value))
            {
                last = last.pred;
                size--;
                return;
            }
            Node<T>? step = first;
            while (step.next != null)
            {
                if (Equals(obj, step.next.value))
                {
                    step.next = step.next.next;
                    step.next.pred = step;
                    size--;
                    return;
                }
                else step = step.next;
            }
            //    while (p->next != NULL)
            //    {
            //        if (p->next->info < 0)
            //        {
            //            list* r = p->next;
            //            p->next = r->next;
            //            delete r;
            //        }
            //        else p = p->next;
            //    }
        }
    }
    public void RemoveAll(T[] array)
    {
        foreach (T e in array)
            Remove(e);
    }
    public void RetainAll(T[] array)
    {
        for (int index = 0; index < size; index++)
        {
            int f = 0;
            for (int j = 0; j < array.Length; j++)
            {
                if (Equals(array[j], Get(index)))
                {
                    f = 0;
                    break;
                }
                else f = 1;
            }
            if (f == 1)
                Remove(Get(index));
        }
    }
    public int Size() => size;
    public T[] ToArray()
    {
        T[] array = new T[size];
        for (int index = 0; index < size; index++)
            array[index] = Get(index);
        return array;
    }
    public T[] ToArray(ref T[] array)
    {
        if (array == null) return ToArray();
        else
        {
            T[] newArray = new T[array.Length + size];
            for (int i = 0; i < array.Length; i++)
                newArray[i] = array[i];
            for (int i = array.Length; i < newArray.Length; i++)
                newArray[i] = Get(i);
            return newArray;
        }
    }
    public void Add(int index, T e)
    {
        if (index <= 0)
        {
            Node<T> step = new Node<T>(e);
            step.next = first;
            first.pred = step;
            first = step;
            return;
        }
        else if (index >= size)
        {
            Node<T> step = new Node<T>(e);
            step.pred = last;
            last.next = step;
            last = step;
            return;
        }
        else
        {
            int i = 0;
            Node<T> step = new Node<T>(e);
            step = first;
            while (i != index)
            {
                step = step.next;
                i++;
            }
            Node<T> el = new Node<T>(e);
            el.next = step;
            el.pred = step.pred;
            step.pred.next = el;
            step.pred = el;
        }
    }
    public void AddAll(int index, T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            Add(index, array[i]);
    }
    public T Get(int index)
    {
        int curIndex = 0;
        if (index >= size)
            throw new Exception();
        if (index == size - 1)
            return last.value;
        if (index == 0)
            return first.value;
        Node<T>? step = first;
        while (curIndex != index)
        {
            step = step.next;
            curIndex++;
        }
        return step.value;
    }
    public int IndexOf(T o)
    {
        Node<T> step = new Node<T>(o);
        step = first;
        int index = 0;
        while (step != null)
        {
            if (step.value.Equals(o))
                return index;
            index++;
            step = step.next;
        }
        return -1;
    }
    public int LastIndexOf(T o)
    {
        Node<T> step = new Node<T>(o);
        step = last;
        int index = size - 1;
        while (step != null)
        {
            if (Equals(o, step.value))
                return index;
            index--;
            step = step.pred;
        }
        return -1;
    }
    public T RemoveIndex(int index)
    {
        T temp = Get(index);
        Remove(temp);
        return temp;
    }
    public void Set(int index, T e)
    {
        Node<T> step = new Node<T>(e);
        step = first;
        int i = 0;
        while (i != index)
        {
            i++;
            step = step.next;
        }
        step.value = e;
    }
    public T[] SubList(int fromIndex, int toIndex)
    {
        if ((fromIndex < 0 || toIndex > size) || (toIndex < fromIndex))
            throw new Exception();
        else
        {
            T[] array = new T[toIndex - fromIndex + 1];
            Node<T> step = new Node<T>(first.value);
            step = first;
            int index = 0;
            while (index != fromIndex)
            {
                step = step.next;
                index++;
            }
            int indexOfArray = 0;
            while (index <= toIndex)
            {
                array[indexOfArray] = step.value;
                indexOfArray++;
                index++;
                step = step.next;
            }
            return array;
        }
    }
    public T Element() => first.value;
    public bool Offer(T o)
    {
        Add(o);
        if (Contains(o)) return true;
        return false;
    }
    public T Peek()
    {
        if (first == null)
            return default(T);
        return first.value;
    }
    public T Poll()
    {
        T obj = first.value;
        Remove(first.value);
        return obj;
    }
    public void AddFirst(T obj)
    {
        Add(0, obj);
    }
    public void AddLast(T obj)
    {
        Add(size, obj);
    }
    public T GetFirst()
    {
        if (first == null)
            throw new Exception();
        return first.value;
    }
    public T GetLast()
    {
        if (last == null)
            throw new Exception();
        return last.value;
    }
    public bool OfferFirst(T obj)
    {
        AddFirst(obj);
        if (Contains(obj)) return true;
        return false;
    }
    public bool OfferLast(T obj)
    {
        AddLast(obj);
        if (Contains(obj)) return true;
        return false;
    }
    public T Pop()
    {
        T obj = first.value;
        Remove(first.value);
        return obj;
    }
    public void Push(T obj)
    {
        AddFirst(obj);
    }
    public T PeekFirst()
    {
        if (size == 0)
            return default(T);
        return first.value;
    }
    public T PeekLast()
    {
        if (size == 0)
            return default(T);
        return first.value;
    }
    public T PollFirst()
    {
        T obj = first.value;
        Remove(first.value);
        return obj;
    }
    public T PollLast()
    {
        T obj = last.value;
        Remove(last.value);
        return obj;
    }
    public T RemoveFirst()
    {
        T obj = first.value;
        Remove(first.value);
        return obj;
    }
    public T RemoveLast()
    {
        T obj = last.value;
        Remove(last.value);
        return obj;
    }
    public bool RemoveLastOccurrence(T obj)
    {
        int ind = LastIndexOf(obj);
        if (ind != -1)
        {
            RemoveIndex(ind);
            return true;
        }
        return false;
    }
    public bool RemoveFirstOccurrence(T obj)
    {
        int index = IndexOf(obj);
        if (index != -1)
        {
            RemoveIndex(index);
            return true;
        }
        return false;
    }
}
