using System.Collections.Generic;
using System;
using System.Xml.Linq;

namespace TreeMapLibrary
{
    public class MyTreeMap<K, V> where K : IComparable<K>
    {
        private class TreeNode
        {
            public K key;
            public V value;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(K key, V value)
            {
                this.key = key;
                this.value = value;
                left = null;
                right = null;
            }
        }

        private TreeNode root;
        private int size;
        private IComparer<K> comparator;

        public MyTreeMap()
        {
            comparator = Comparer<K>.Default;
            root = null;
            size = 0;
        }

        public MyTreeMap(IComparer<K> comparator)
        {
            this.comparator = comparator;
            root = null;
            size = 0;
        }

        public void Clear()
        {
            root = null;
            size = 0;
        }

        public bool ContainsKey(object key)
        {
            return GetNode((K)key) != null;
        }

        // Воспомогательный метод которы возвращает узел дерева (родителя) по указанному ключу
        private TreeNode GetNode(K key)
        {
            return GetNode(root, key);
        }

        private TreeNode GetNode(TreeNode root, K key)
        {
            if (root == null) return null;
            int comp = comparator.Compare(key, root.key); // Сравнение через компаратор

            if (comp < 0) return GetNode(root.left, key); // Ищем в левом поддереве
            else if (comp > 0) return GetNode(root.right, key); // Ищем в правом поддереве
            else return root;
        }

        public bool ContainsValue(object value)
        {
            return ContainsValue(root, value);
        }

        private bool ContainsValue(TreeNode root, object value)
        {
            if (root == null) return false;
            if (EqualityComparer<V>.Default.Equals(root.value, (V)value)) return true;
            return ContainsValue(root.left, value) || ContainsValue(root.right, value);
        }

        public HashSet<KeyValuePair<K, V>> EntrySet()
        {
            var entries = new HashSet<KeyValuePair<K, V>>();
            EntrySet(root, entries);
            return entries;
        }

        // Воспомогательный метод для рекурсивного добавления всех пар ключей в дереве в множество и в дальнейшем его вернуть
        private void EntrySet(TreeNode root, HashSet<KeyValuePair<K, V>> entries)
        {
            if (root != null)
            {
                entries.Add(new KeyValuePair<K, V>(root.key, root.value)); // Добавляем текущий узел
                EntrySet(root.left, entries);
                EntrySet(root.right, entries);
            }
        }

        public V Get(object key)
        {
            TreeNode root = GetNode((K)key);
            if (root.key.Equals(key)) return root.value;
            else return default;
        }

        public bool IsEmpty()
        {
            if (size == 0) return true;
            else return false;
        }

        public HashSet<K> KeySet()
        {
            var keys = new HashSet<K>();
            KeySet(root, keys);
            return keys;
        }

        // Воспомогательный метод для рекурсивного добавления всех ключей в дереве в множество 
        private void KeySet(TreeNode root, HashSet<K> keys)
        {
            if (root != null)
            {
                keys.Add(root.key);
                KeySet(root.left, keys);
                KeySet(root.right, keys);
            }
        }

        public void Put(K key, V value)
        {
            root = Put(root, key, value);
            size++;
        }

        private TreeNode Put(TreeNode root, K key, V value)
        {
            if (root == null) return new TreeNode(key, value);

            int comp = comparator.Compare(key, root.key);
            if (comp < 0) root.left = Put(root.left, key, value);
            else if (comp > 0) root.right = Put(root.right, key, value);
            else root.value = value; // Обновление значения, если ключ уже существует
            return root;
        }

        public bool Remove(K key)
        {
            if (ContainsKey(key))
            {
                root = Remove(root, key);
                size--;
                return true;
            }
            else return false;
        }

        private TreeNode Remove(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
            if (comp < 0) root.left = Remove(root.left, key);
            else if (comp > 0) root.right = Remove(root.right, key);
            else // Нашел узел, в котором нужно удалить дочерний с указанным ключом (ключ равен данному ключу)
            {
                // Удаление узла
                if (root.left == null) return root.right;
                if (root.right == null) return root.left;

                // Узел с двумя поддеревами (имеет и левое и правое))
                TreeNode minNode = GetMin(root.right); // Находит минимальный узел в правом поддереве
                // Заменяем узел с указанным ключом на минимальный в правом поддереве
                root.key = minNode.key;
                root.value = minNode.value;
                root.right = Remove(root.right, minNode.key); // Затем удаляем минимальный узел ибо он стал лишним
            }
            return root;
        }

        // Воспомогательный метод для получение левого поддерева (наименьшего)
        private TreeNode GetMin(TreeNode root)
        {
            while (root.left != null) root = root.left;
            return root;
        }

        // Воспомогательный метод для получение правого поддерева (наибольшего)
        private TreeNode GetMax(TreeNode root)
        {
            while (root.right != null) root = root.right;
            return root;
        }

        public int Size()
        {
            return size;
        }

        public K FirstKey()
        {
            if (IsEmpty()) throw new InvalidOperationException("TreeMap is empty.");
            return GetMin(root).key;
        }

        public K LastKey()
        {
            if (IsEmpty()) throw new InvalidOperationException("TreeMap is empty.");
            return GetMax(root).key;
        }

        public MyTreeMap<K, V> HeadMap(K end)
        {
            MyTreeMap<K, V> result = new MyTreeMap<K, V>(comparator);
            HeadMap(root, end, result);
            return result;
        }

        private void HeadMap(TreeNode root, K end, MyTreeMap<K, V> result)
        {
            if (root == null) return;

            int comp = comparator.Compare(root.key, end);
            // Если текущий ключ меньше заданного 'end', то начинаем сортировать дерево. Иначе возвращаемся в левое поддерево
            if (comp < 0)
            {
                result.Put(root.key, root.value);
                HeadMap(root.left, end, result);
                HeadMap(root.right, end, result);
            }
            else HeadMap(root.left, end, result);
        }

        public MyTreeMap<K, V> SubMap(K start, K end)
        {
            MyTreeMap<K, V> result = new MyTreeMap<K, V>(comparator);
            SubMap(root, start, end, result);
            return result;
        }

        private void SubMap(TreeNode root, K start, K end, MyTreeMap<K, V> result)
        {
            if (root == null) return;

            int compStart = comparator.Compare(root.key, start);
            int compEnd = comparator.Compare(root.key, end);

            // Если текущий ключ удовлетворяет условиям наших заданных ключей 'start' и 'end', то добавляем этот узел в результат
            if (compStart >= 0 && compEnd < 0) result.Put(root.key, root.value);
            // Иначе совершаем обход текущего дерева
            if (compStart < 0) SubMap(root.left, start, end, result);
            if (compEnd > 0) SubMap(root.right, start, end, result);
        }

        public MyTreeMap<K, V> TailMap(K start)
        {
            MyTreeMap<K, V> result = new MyTreeMap<K, V>(comparator);
            TailMap(root, start, result);
            return result;
        }

        private void TailMap(TreeNode root, K start, MyTreeMap<K, V> result)
        {
            if (root == null) return;

            int comp = comparator.Compare(root.key, start);
            // Если текущий ключ больше заданного 'start', то начинаем сортировать дерево. Иначе возвращаемся в правое поддерево
            if (comp > 0)
            {
                result.Put(root.key, root.value);
                TailMap(root.left, start, result);
                TailMap(root.right, start, result);
            }
            else TailMap(root.right, start, result);
        }

        public KeyValuePair<K, V>? LowerEntry(K key)
        {
            return LowerEntry(root, key);
        }

        private KeyValuePair<K, V>? LowerEntry(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
            // Если текущий ключ меньше заданного, то переходим в правое поддерево и там ищем наименьшее, ибо может быть ключ еще меньше заданного.
            if (comp > 0)
            {
                var rightResult = LowerEntry(root.right, key);
                // Возвращаем подходящий узел. Если его нет, то текущий, ибо он будет наименьшим искомого
                return rightResult ?? new KeyValuePair<K, V>(root.key, root.value);
            }
            return LowerEntry(root.left, key); // Если больше или равен, то ищем в левом поддереве
        }

        public KeyValuePair<K, V>? FloorEntry(K key)
        {
            return FloorEntry(root, key);
        }

        private KeyValuePair<K, V>? FloorEntry(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
            // Если текущий ключ равен заданному, то сразу возвращаем текущий узел
            if (comp == 0) return new KeyValuePair<K, V>(root.key, root.value);
            if (comp > 0)
            {
                var rightResult = FloorEntry(root.right, key);
                return rightResult ?? new KeyValuePair<K, V>(root.key, root.value);
            }
            return FloorEntry(root.left, key);
        }

        public KeyValuePair<K, V>? HigherEntry(K key)
        {
            return HigherEntry(root, key);
        }

        private KeyValuePair<K, V>? HigherEntry(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
            if (comp < 0)
            {
                var leftResult = HigherEntry(root.left, key);
                return leftResult ?? new KeyValuePair<K, V>(root.key, root.value);
            }
            return HigherEntry(root.right, key);
        }

        public KeyValuePair<K, V>? CeilingEntry(K key)
        {
            return CeilingEntry(root, key);
        }

        private KeyValuePair<K, V>? CeilingEntry(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
            if (comp == 0) return new KeyValuePair<K, V>(root.key, root.value);
            if (comp < 0)
            {
                var leftResult = CeilingEntry(root.left, key);
                return leftResult ?? new KeyValuePair<K, V>(root.key, root.value);
            }
            return CeilingEntry(root.right, key);
        }

        public K LowerKey(K key)
        {
            var entry = LowerEntry(key);
            // Если entry имеет значение (подходящий нам элемент), то возвращает true (иначе false)
            // В этом случае возвращается ключ найденного элемента
            return entry.HasValue ? entry.Value.Key : default;
        }

        public K FloorKey(K key)
        {
            var entry = FloorEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public K HigherKey(K key)
        {
            var entry = HigherEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public K CeilingKey(K key)
        {
            var entry = CeilingEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public KeyValuePair<K, V>? PollFirstEntry()
        {
            if (IsEmpty()) return null;
            var firstEntry = FirstEntry();
            Remove(firstEntry.Key);
            return firstEntry;
        }

        public KeyValuePair<K, V>? PollLastEntry()
        {
            if (IsEmpty()) return null;
            var lastEntry = LastEntry();
            Remove(lastEntry.Key);
            return lastEntry;
        }

        public KeyValuePair<K, V> FirstEntry()
        {
            if (IsEmpty()) throw new InvalidOperationException("TreeMap is empty.");
            TreeNode minNode = GetMin(root);
            return new KeyValuePair<K, V>(minNode.key, minNode.value);
        }

        public KeyValuePair<K, V> LastEntry()
        {
            if (IsEmpty()) throw new InvalidOperationException("TreeMap is empty.");
            TreeNode maxNode = GetMax(root);
            return new KeyValuePair<K, V>(maxNode.key, maxNode.value);
        }

        public void PrintTree()
        {
            PrintTree(root);
        }

        private void PrintTree(TreeNode root)
        {
            if (root == null) return;
            PrintTree(root.left);
            Console.WriteLine(root.key);
            PrintTree(root.right);
        }
    }
}