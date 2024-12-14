
using MyPriorityDeque;
using System.Diagnostics;
namespace Lab12
{
    public class PriorityQueue : IComparable<PriorityQueue> // Добавляем для того, чтобы сравнивать объекты нашего класса между собой
    {
        int priorities { get; set; } // Свойство для хранения приоритета заявки
        int aplicationNumber { get; set; } // Свойство для хранения номера заявки
        int step { get; set; } // Свойство для хранения номера шага, на котором заявка поступила в систему

        public PriorityQueue(int priorities, int aplicationNumber, int step)
        {
            this.priorities = priorities;
            this.aplicationNumber = aplicationNumber;
            this.step = step;
        }

        // Метод для сравнения приоритета текущей заявки с приоритетом заявки 'other'
        public int CompareTo(PriorityQueue other)
        {
            return priorities.CompareTo(other.priorities);
        }

        static void Main(string[] args)
        {
            string path = "log.txt";
            MyPriorityQueue<PriorityQueue> order = new MyPriorityQueue<PriorityQueue>();
            Console.Write("Напишите количество шагов для добавления заявок: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int count = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            StreamWriter sw = new StreamWriter(path);

            for (int i = 0; i < n; i++)
            {
                Random random = new Random();
                int number = random.Next(1, 11);
                for (int j = 0; j < number; j++)
                {
                    int priorities = random.Next(1, 6);
                    PriorityQueue list1 = new PriorityQueue(priorities, j, i);
                    order.Add(list1);
                    sw.WriteLine($"ADD: {list1.priorities} {list1.aplicationNumber} {list1.step} ");
                    count++;
                }
                PriorityQueue list2 = order.Poll(); // Извлекает и удаляет заявку с наивысшим приоритетом
                sw.WriteLine($"REMOVE: {list2.priorities} {list2.aplicationNumber} {list2.step} ");
                count--;
            }

            for (int i = 0; i < count; i++)
            {
                PriorityQueue list3 = order.Peek(); // Извлекает заявку с наивысшим приоритетом
                sw.WriteLine($"REMOVE: {list3.priorities} {list3.aplicationNumber} {list3.step} ");
                order.Remove(order.Peek()); // Удаляет ее
            }

            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;
            sw.WriteLine($"Время выполнения: {time.TotalSeconds} секунд");
            sw.Close();
        }
    }
}