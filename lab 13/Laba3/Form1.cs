using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using SortingAndGenerationLibrary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Lab3
{
    public partial class Form1 : Form
    {

        int flag = 0;
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива,млрд шт";
            pane.YAxis.Title.Text = "Время выполнения,мс";
            pane.Title.Text = "Зависимость времени выполнения от размера массива";
        }

        // Подводка к файлам
        public void SetPath()
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            pathToArray = appDir + @"\array.txt";
            pathToTime = appDir + @"\time.txt";
        }
        string pathToArray;
        string pathToTime;

        // Запись массивов и времени работы сортировок в файл
        // Перегрузка для массивов int[]
        private void SpeedTime(Func<int, int[]> Generate, int length, bool reserve, params Func<int[], bool, int[]>[] SortMethods)
        {
            SetPath();
            double[] sumSpeed = new double[SortMethods.Length];
            for (int i = 0; i < 20; i++)
            {
                int[] array = Generate(length);
                try
                {
                    using (StreamWriter sw = File.AppendText(pathToArray))
                    {
                        sw.WriteLine("Unsorted array: ");
                        sw.WriteLine(string.Join(" ", array));

                        int[] sortedArray = null;
                        for (int index = 0; index < SortMethods.Length; index++)
                        {
                            var method = SortMethods[index];
                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            sortedArray = method(array, reserve);
                            timer.Stop();
                            sumSpeed[index] += timer.ElapsedMilliseconds;

                            sw.WriteLine($"Sorted array using {method.Method.Name}: ");
                            sw.WriteLine(string.Join(" ", sortedArray));
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            try
            {
                using (StreamWriter sw = File.AppendText(pathToTime))
                {
                    for (int i = 0; i < sumSpeed.Length; i++)
                    {
                        sw.Write((sumSpeed[i] / 20).ToString() + (i < sumSpeed.Length - 1 ? " " : ""));
                    }
                    sw.WriteLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        // Перегрузка для массивов double[]
        private void SpeedTime(Func<int, double[]> Generate, int length, bool reserve, params Func<double[], bool, double[]>[] SortMethods)
        {
            SetPath();
            double[] sumSpeed = new double[SortMethods.Length];
            for (int i = 0; i < 20; i++)
            {
                double[] array = Generate(length);
                try
                {
                    using (StreamWriter sw = File.AppendText(pathToArray))
                    {
                        sw.WriteLine("Unsorted array: ");
                        sw.WriteLine(string.Join(" ", array));

                        double[] sortedArray = null;
                        for (int index = 0; index < SortMethods.Length; index++)
                        {
                            var method = SortMethods[index];
                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            sortedArray = method(array, reserve);
                            timer.Stop();
                            sumSpeed[index] += timer.ElapsedMilliseconds;

                            sw.WriteLine($"Sorted array using {method.Method.Name}: ");
                            sw.WriteLine(string.Join(" ", sortedArray));
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            try
            {
                using (StreamWriter sw = File.AppendText(pathToTime))
                {
                    for (int i = 0; i < sumSpeed.Length; i++)
                    {
                        sw.Write((sumSpeed[i] / 20).ToString() + (i < sumSpeed.Length - 1 ? " " : ""));
                    }
                    sw.WriteLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        // Перегрузка для массивов string[]
        private void SpeedTime(Func<int, string[]> Generate, int length, bool reserve, params Func<string[], bool, string[]>[] SortMethods)
        {
            SetPath();
            double[] sumSpeed = new double[SortMethods.Length];
            for (int i = 0; i < 20; i++)
            {
                string[] array = Generate(length);
                try
                {
                    using (StreamWriter sw = File.AppendText(pathToArray))
                    {
                        sw.WriteLine("Unsorted array: ");
                        sw.WriteLine(string.Join(" ", array));

                        string[] sortedArray = null;
                        for (int index = 0; index < SortMethods.Length; index++)
                        {
                            var method = SortMethods[index];
                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            sortedArray = method(array, reserve);
                            timer.Stop();
                            sumSpeed[index] += timer.ElapsedMilliseconds;

                            sw.WriteLine($"Sorted array using {method.Method.Name}: ");
                            sw.WriteLine(string.Join(" ", sortedArray));
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            try
            {
                using (StreamWriter sw = File.AppendText(pathToTime))
                {
                    for (int i = 0; i < sumSpeed.Length; i++)
                    {
                        sw.Write((sumSpeed[i] / 20).ToString() + (i < sumSpeed.Length - 1 ? " " : ""));
                    }
                    sw.WriteLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: // Первая группа
                    flag = 1;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0: // Целые числа
                            for (int length = 10; length <= 10000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomNumbers, length, true,
                                    SortingsAndGenerations.BubbleSort,
                                    SortingsAndGenerations.InsertionSort,
                                    SortingsAndGenerations.SelectionSort,
                                    SortingsAndGenerations.ShakerSort,
                                    SortingsAndGenerations.GnomeSort);
                            break;
                        case 1: // Дробные числа
                            for (int length = 10; length <= 10000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomDoubles, length, true,
                                    SortingsAndGenerations.BubbleSortDouble,
                                    SortingsAndGenerations.InsertionSortDouble,
                                    SortingsAndGenerations.SelectionSortDouble,
                                    SortingsAndGenerations.ShakerSortDouble,
                                    SortingsAndGenerations.GnomeSortDouble);
                            break;
                        case 2: // Строки
                            for (int length = 10; length <= 1000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomStrings, length, true,
                                    SortingsAndGenerations.BubbleSortString,
                                    SortingsAndGenerations.InsertionSortString,
                                    SortingsAndGenerations.SelectionSortString,
                                    SortingsAndGenerations.ShakerSortString,
                                    SortingsAndGenerations.GnomeSortString);
                            break;
                    }
                    break;

                case 1: // Вторая группа
                    flag = 2;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 10000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomNumbers, length, true,
                                    SortingsAndGenerations.ShellSort,
                                    SortingsAndGenerations.BitonicSort,
                                    SortingsAndGenerations.TreeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 10000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomDoubles, length, true,
                                    SortingsAndGenerations.ShellSortDouble,
                                    SortingsAndGenerations.BitonicSortDouble,
                                    SortingsAndGenerations.TreeSortDouble);
                            break;
                        case 2:
                            for (int length = 10; length <= 1000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomStrings, length, true,
                                    SortingsAndGenerations.ShellSortString,
                                    SortingsAndGenerations.BitonicSortString,
                                    SortingsAndGenerations.TreeSortString);
                            break;
                    }
                    break;

                case 2: // Третья группа
                    flag = 3;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 10000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomNumbers, length, true,
                                    SortingsAndGenerations.CombSort,
                                    SortingsAndGenerations.HeapSort,
                                    SortingsAndGenerations.QuickSort,
                                    SortingsAndGenerations.MergeSort,
                                    SortingsAndGenerations.CountSort,
                                    SortingsAndGenerations.RadixSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 10000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomDoubles, length, true,
                                    SortingsAndGenerations.CombSortDouble,
                                    SortingsAndGenerations.HeapSortDouble,
                                    SortingsAndGenerations.QuickSortDouble,
                                    SortingsAndGenerations.MergeSortDouble,
                                    SortingsAndGenerations.CountSortDouble,
                                    SortingsAndGenerations.RadixSortDouble);
                            break;
                        case 2:
                            for (int length = 10; length <= 1000; length *= 10)
                                SpeedTime(SortingsAndGenerations.RandomStrings, length, true,
                                    SortingsAndGenerations.CombSortString,
                                    SortingsAndGenerations.HeapSortString,
                                    SortingsAndGenerations.QuickSortString,
                                    SortingsAndGenerations.MergeSortString,
                                    SortingsAndGenerations.CountSortString,
                                    SortingsAndGenerations.RadixSortString);
                            break;
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время выполнения";
            pane.Title.Text = "Исследование скорости работы сортировок";
            SetPath();
            List<List<double>> time = new List<List<double>>();
            try
            {
                StreamReader sr = new StreamReader(pathToTime);
                string line = sr.ReadLine();

                while (line != null)
                {
                    List<double> speed = new List<double>();
                    string[] lineArray = line.Split(' ');
                    foreach (string str in lineArray)
                    {
                        speed.Add(Convert.ToDouble(str));
                    }
                    time.Add(speed);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            for (int i = 0; i < time[0].Count(); i++)
            {
                PointPairList pointList = new PointPairList();
                int x = 10;

                for (int j = 0; j < time.Count(); j++)
                {

                    pointList.Add(x, time[j][i]);
                    x *= 10;
                }
                switch (i)
                {
                    case 0:
                        if (flag == 1)
                        {
                            pane.AddCurve("Пузырек: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Ракушка: " + i, pointList, Color.Gray, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Расческа: " + i, pointList, Color.Green, SymbolType.Default);
                        }
                        break;
                    case 1:
                        if (flag == 1)
                        {
                            pane.AddCurve("Вставками: " + i, pointList, Color.Red, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Деревом: " + i, pointList, Color.Yellow, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Кучей: " + i, pointList, Color.Blue, SymbolType.Default);
                        }
                        break;
                    case 2:
                        if (flag == 1)
                        {
                            pane.AddCurve("Выбором: " + i, pointList, Color.Pink, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Битон: " + i, pointList, Color.Violet, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Быстрая: " + i, pointList, Color.Brown, SymbolType.Default);
                        }
                        break;
                    case 3:
                        if (flag == 1)
                        {
                            pane.AddCurve("Шейкерная: " + i, pointList, Color.Yellow, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Слияние: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 4:
                        if (flag == 1)
                        {
                            pane.AddCurve("Гномья: " + i, pointList, Color.Green, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Подсчетом: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 5:
                        pane.AddCurve("Разрядная: " + i, pointList, Color.Aquamarine, SymbolType.Default);
                        break;
                }
            }
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}