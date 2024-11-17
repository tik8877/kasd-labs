using Vector;
using System;
using System.IO;
using System.Numerics;

namespace laba7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ar = { 1, 2, 3, 4, 5 };
            MyVector<int> mv= new MyVector<int>(ar);
            mv.Add(3,4);
            for (int i=0;i<mv.Size(); i++)
            {
                Console.WriteLine(mv.Get(i));
            }


            string[] s = File.ReadAllLines("input.txt");
            var validIPs = new MyVector<string>(10);
            foreach (string line in s)
            {
                string ss = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '.' || (line[i] >= '0' && line[i] <= '9'))
                    {
                        ss += line[i];
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ss))
                        {
                            if (Check(ss)) validIPs.Add(ss);
                            ss = "";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ss) && Check(ss)) validIPs.Add(ss);
            }
            File.WriteAllLines("output.txt", validIPs.ToArray());
        }

        static bool Check(string ss)
        {
            bool f = true;
            string[] sss = ss.Split('.');
            if (sss.Length != 4) return false;
            foreach (string part in sss)
            {
                if (!int.TryParse(part, out int num) || num < 0 || num > 255)
                {
                    f = false;
                    break;
                }
            }
            return f;
        }
    }
}