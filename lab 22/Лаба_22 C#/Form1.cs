using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using HashMapLibrary;
using TreeMapLibrary;

namespace Лаба_22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] hashMapResults = new double[3];
            double[] treeMapResults = new double[3];

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    PutOperation(hashMapResults, treeMapResults);
                    break;
                case 1:
                    GetOperation(hashMapResults, treeMapResults);
                    break;
                case 2:
                    RemoveOperation(hashMapResults, treeMapResults);
                    break;
            }
            GraphResults(hashMapResults, treeMapResults);
        }

        private void PutOperation(double[] hashMapResults, double[] treeMapResults)
        {
            int k = 0;
            for (int size = 100; size <= 10000; size *= 10)
            {
                double hashMapTotalTime = 0;
                double treeMapTotalTime = 0;

                for (int i = 0; i < 20; i++)
                {
                    var hashMap = new MyHashMap<int, int>();
                    var treeMap = new MyTreeMap<int, int>();

                    Stopwatch stopwatch = new Stopwatch();

                    // Измерение времени операции Put для hashMap
                    stopwatch.Start();
                    for (int j = 0; j < size; j++)
                    {
                        hashMap.Put(j, j);
                    }
                    stopwatch.Stop();
                    hashMapTotalTime += stopwatch.ElapsedMilliseconds;

                    // Измерение времени операции Put для treeMap
                    stopwatch.Restart();
                    for (int j = 0; j < size; j++)
                    {
                        treeMap.Put(j, j);
                    }
                    stopwatch.Stop();
                    treeMapTotalTime += stopwatch.ElapsedMilliseconds;
                }

                hashMapResults[k] = hashMapTotalTime / 20;
                treeMapResults[k] = treeMapTotalTime / 20;
                k++;
            }
        }

        private void GetOperation(double[] hashMapResults, double[] treeMapResults)
        {
            int k = 0;
            for (int size = 1000; size <= 10000; size *= 10)
            {
                // Initialize hashMap and treeMap once for each size
                var hashMap = new MyHashMap<int, int>();
                var treeMap = new MyTreeMap<int, int>();

                // Fill the maps with data
                for (int j = 0; j < size; j++)
                {
                    hashMap.Put(j, j);
                    treeMap.Put(j, j);
                }

                double hashMapTotalTime = 0;
                double treeMapTotalTime = 0;

                // Measure Get operation multiple times
                for (int i = 0; i < 20; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();

                    // Measure hashMap Get
                    stopwatch.Start();
                    for (int j = 0; j < size; j++)
                    {
                        hashMap.Get(j);
                    }
                    stopwatch.Stop();
                    hashMapTotalTime += stopwatch.ElapsedMilliseconds;

                    // Measure treeMap Get
                    stopwatch.Restart();
                    for (int j = 0; j < size; j++)
                    {
                        treeMap.Get(j);
                    }
                    stopwatch.Stop();
                    treeMapTotalTime += stopwatch.ElapsedMilliseconds;
                }

                // Store the average times
                hashMapResults[k] = hashMapTotalTime / 20;
                treeMapResults[k] = treeMapTotalTime / 20;
                k++;
            }
        }


        private void RemoveOperation(double[] hashMapResults, double[] treeMapResults)
        {
            int k = 0;
            for (int size = 100; size <= 10000; size *= 10)
            {
                // Initialize hashMap and treeMap once for each size
                var hashMap = new MyHashMap<int, int>();
                var treeMap = new MyTreeMap<int, int>();

                // Fill the maps with data
                for (int j = 0; j < size; j++)
                {
                    hashMap.Put(j, j);
                    treeMap.Put(j, j);
                }

                double hashMapTotalTime = 0;
                double treeMapTotalTime = 0;

                // Measure Remove operation multiple times
                for (int i = 0; i < 20; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();

                    // Measure hashMap Remove
                    stopwatch.Start();
                    for (int j = 0; j < size; j++)
                    {
                        hashMap.Remove(j);
                    }
                    stopwatch.Stop();
                    hashMapTotalTime += stopwatch.ElapsedMilliseconds;

                    // Measure treeMap Remove
                    stopwatch.Restart();
                    for (int j = 0; j < size; j++)
                    {
                        treeMap.Remove(j);
                    }
                    stopwatch.Stop();
                    treeMapTotalTime += stopwatch.ElapsedMilliseconds;
                }

                // Store the average times
                hashMapResults[k] = hashMapTotalTime / 20;
                treeMapResults[k] = treeMapTotalTime / 20;
                k++;
            }
        }

        private void GraphResults(double[] hashMapResults, double[] treeMapResults)
        {
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.Title.Text = "Сравнение производительности MyHashMap и MyTreeMap";
            pane.XAxis.Title.Text = "Операция";
            pane.YAxis.Title.Text = "Время (мс)";
            pane.CurveList.Clear();

            PointPairList hashMapPoints = new PointPairList();
            PointPairList treeMapPoints = new PointPairList();

            for (int i = 0; i < 3; i++)
            {
                hashMapPoints.Add(i + 1, hashMapResults[i]);
                treeMapPoints.Add(i + 1, treeMapResults[i]);
            }

            pane.AddCurve("MyHashMap", hashMapPoints, Color.Blue, SymbolType.Circle);
            pane.AddCurve("MyTreeMap", treeMapPoints, Color.Red, SymbolType.Circle);

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
        }
    }
}