using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Sorting;
using ZedGraph;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using ArraysGenerationLibrary;
using System.Diagnostics.Eventing.Reader;

namespace Task3
{
    public partial class Form1 : Form
    {
        public void InsertPath()
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            pathToArray = appDir + @"\array.txt";
            pathToTime = appDir + @"\time.txt";
        }
        string pathToArray;
        string pathToTime;
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива (шт)";
            pane.YAxis.Title.Text = "Время выполнения (мс)";
            pane.Title.Text = "Зависимости времени выполнения от размера массива";

        }
        int flag = 0;
        int selectedGroupIndex = -1;
        int selectedArrayTypeIndex = -1;

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public void SortingTime(Func<int, int[]> Generate, int length, params Func<int[], int[]>[] SortingMethods)
        {
            InsertPath();
            double[] sumSpeed = new double[SortingMethods.Length];
            int[][] arrays = new int[SortingMethods.Length][];

            // Открываем файлы один раз
            using (StreamWriter swArray = new StreamWriter(pathToArray, true))
            using (StreamWriter swTime = new StreamWriter(pathToTime, true))
            {
                for (int i = 0; i < 20; i++)
                {
                    int[] originalArray = Generate(length);
                    swArray.WriteLine("Unsorted array: " + string.Join(" ", originalArray));

                    int index = 0;
                    Stopwatch timer = new Stopwatch(); // Создаём таймер один раз

                    foreach (Func<int[], int[]> Method in SortingMethods)
                    {
                        // Копируем массив для каждой сортировки
                        if (arrays[index] == null || arrays[index].Length != originalArray.Length)
                            arrays[index] = new int[originalArray.Length];

                        Array.Copy(originalArray, arrays[index], originalArray.Length);

                        // Запускаем и останавливаем таймер
                        timer.Reset();
                        timer.Start();
                        Method(arrays[index]);
                        timer.Stop();

                        sumSpeed[index] += timer.ElapsedMilliseconds;
                        index++;
                    }

                    swArray.WriteLine("Sorted array: " + string.Join(" ", arrays[0])); // Записываем только первый отсортированный массив
                    swArray.WriteLine();
                }

                // Записываем среднее время выполнения
                for (int i = 0; i < sumSpeed.Length; i++)
                {
                    if (i > 0) swTime.Write(" ");
                    swTime.Write((sumSpeed[i] / 20).ToString());
                }
                swTime.WriteLine();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            zedGraph.GraphPane.CurveList.Clear(); // Очистить кривые
            zedGraph.GraphPane.GraphObjList.Clear(); // Очистить объекты графика (если есть)
            zedGraph.AxisChange(); // Обновить оси
            zedGraph.Invalidate();
            InsertPath();
            File.WriteAllText(pathToTime, string.Empty);
            File.WriteAllText(pathToArray, string.Empty);
            int pow = 4;
            switch (selectedArrayTypeIndex)
            {

                //Рандомные числа
                case -1:
                    textBox1.Text = "выбери группу";
                    break;

                case 0:
                    switch (selectedGroupIndex)
                    {
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, pow); length *= 10)
                                SortingTime(ArraysGeneration.GenerateFirstGroup, length, SortingMethods.BubbleSort, SortingMethods.InsertionSort, SortingMethods.SelectionSort, SortingMethods.ShakerSort, SortingMethods.GnomeSort);
                            flag = 1;
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, pow + 1); length *= 10)
                                SortingTime(ArraysGeneration.GenerateFirstGroup, length, SortingMethods.BitonicSort, SortingMethods.ShellSort, SortingMethods.TreeSort);
                            flag = 2;
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, pow + 2); length *= 10)
                                SortingTime(ArraysGeneration.GenerateFirstGroup, length, SortingMethods.CombSort, SortingMethods.HeapSort, SortingMethods.QuickSort, SortingMethods.CountingSort, SortingMethods.MergeSort, SortingMethods.RadixSort);
                            flag = 3;
                            break;
                    }
                    break;

                //Разбитые на подмассивы
                case 1:
                    switch (selectedGroupIndex)
                    {
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, pow); length *= 10)
                                SortingTime(ArraysGeneration.GenerateSecondGroup, length, SortingMethods.BubbleSort, SortingMethods.InsertionSort, SortingMethods.SelectionSort, SortingMethods.ShakerSort, SortingMethods.GnomeSort);
                            flag = 1;
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, pow + 1); length *= 10)
                                SortingTime(ArraysGeneration.GenerateSecondGroup, length, SortingMethods.BitonicSort, SortingMethods.ShellSort, SortingMethods.TreeSort);
                            flag = 2;
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, pow + 2); length *= 10)
                                SortingTime(ArraysGeneration.GenerateSecondGroup, length, SortingMethods.CombSort, SortingMethods.HeapSort, SortingMethods.QuickSort, SortingMethods.CountingSort, SortingMethods.MergeSort, SortingMethods.RadixSort);
                            flag = 3;
                            break;
                    }
                    break;

                //Отсортированные с перестановками
                case 2:
                    switch (selectedGroupIndex)
                    {
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, pow); length *= 10)
                                SortingTime(ArraysGeneration.GenerateThirdGroup, length, SortingMethods.BubbleSort, SortingMethods.InsertionSort, SortingMethods.SelectionSort, SortingMethods.ShakerSort, SortingMethods.GnomeSort);
                            flag = 1;
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, pow + 1); length *= 10)
                                SortingTime(ArraysGeneration.GenerateThirdGroup, length, SortingMethods.BitonicSort, SortingMethods.ShellSort, SortingMethods.TreeSort);
                            flag = 2;
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, pow + 2); length *= 10)
                                SortingTime(ArraysGeneration.GenerateThirdGroup, length, SortingMethods.CombSort, SortingMethods.HeapSort, SortingMethods.QuickSort, SortingMethods.CountingSort, SortingMethods.MergeSort, SortingMethods.RadixSort);
                            flag = 3;
                            break;
                    }
                    break;

                //Отсортированные с перестановками и повторами
                case 3:
                    switch (selectedGroupIndex)
                    {
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, pow); length *= 10)
                                SortingTime(ArraysGeneration.GenerateForthGroup, length, SortingMethods.BubbleSort, SortingMethods.InsertionSort, SortingMethods.SelectionSort, SortingMethods.ShakerSort, SortingMethods.GnomeSort);
                            flag = 1;
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, pow + 1); length *= 10)
                                SortingTime(ArraysGeneration.GenerateForthGroup, length, SortingMethods.BitonicSort, SortingMethods.ShellSort, SortingMethods.TreeSort);
                            flag = 2;
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, pow + 2); length *= 10)
                                SortingTime(ArraysGeneration.GenerateForthGroup, length, SortingMethods.CombSort, SortingMethods.HeapSort, SortingMethods.QuickSort, SortingMethods.CountingSort, SortingMethods.MergeSort, SortingMethods.RadixSort);
                            flag = 3;
                            break;
                    }
                    break;
            }
            pictureBox1.BackColor = Color.Green;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGroupIndex = comboBox1.SelectedIndex;
        }


        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedArrayTypeIndex = comboBox2.SelectedIndex;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            InsertPath();
            File.WriteAllText(pathToTime, string.Empty);
            File.WriteAllText(pathToArray, string.Empty);
            pictureBox1.BackColor = Color.Red;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            InsertPath();
            List<List<double>> time = new List<List<double>>();
            try
            {
                StreamReader sr = new StreamReader(pathToTime);
                string line = sr.ReadLine();

                while (line != null)
                {
                    List<double> speed = new List<double>();
                    string[] lineArray = line.Split(' ');
                    foreach (string str in lineArray) speed.Add(Convert.ToDouble(str));
                    time.Add(speed);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива (шт)";
            pane.YAxis.Title.Text = "Время выполнения (мс)";
            pane.Title.Text = "Зависимости времени выполнения от размера массива";
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
                            pane.XAxis.Scale.Max = 10000;
                            pane.AddCurve("BubbleSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.XAxis.Scale.Max = 100000;
                            pane.AddCurve("BitonicSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.XAxis.Scale.Max = 1000000;
                            pane.AddCurve("CombSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 1:
                        if (flag == 1) pane.AddCurve("InsertionSort: " + i, pointList, Color.Green, SymbolType.Default);
                        if (flag == 2) pane.AddCurve("ShellSort: " + i, pointList, Color.Green, SymbolType.Default);
                        if (flag == 3) pane.AddCurve("MergeSort: " + i, pointList, Color.Green, SymbolType.Default);
                        break;
                    case 2:
                        if (flag == 1) pane.AddCurve("SelectionSort: " + i, pointList, Color.Blue, SymbolType.Default);
                        if (flag == 2) pane.AddCurve("TreeSort: " + i, pointList, Color.Blue, SymbolType.Default);
                        if (flag == 3) pane.AddCurve("CountingSort: " + i, pointList, Color.Blue, SymbolType.Default);
                        break;
                    case 3:
                        if (flag == 1) pane.AddCurve("ShakerSort: " + i, pointList, Color.Pink, SymbolType.Default);
                        if (flag == 3) pane.AddCurve("RadixSort: " + i, pointList, Color.Pink, SymbolType.Default);
                        break;
                    case 4:
                        if (flag == 1) pane.AddCurve("GnomeSort: " + i, pointList, Color.Purple, SymbolType.Default);
                        //if (flag == 3) pane.AddCurve("CountingSort: " + i, pointList, Color.Purple, SymbolType.Default);
                        break;
                        //case 5:
                        //    if (flag == 3) pane.AddCurve("RadixSort: " + i, pointList, Color.SandyBrown, SymbolType.Default);
                        //    break;
                }
                zedGraph.AxisChange();
                zedGraph.Invalidate();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}



