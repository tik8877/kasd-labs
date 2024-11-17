using MyArray;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Laba_5
{
    internal class Program
    {
        static string file = "input.txt";
        static StreamReader sr = new StreamReader(file);
        MyArrayList<string> GetTeg()
        {
            bool isOpen = false;
            bool isClose = false;
            string line = sr.ReadLine();
            if (line == null) { throw new Exception("Пустая строка"); }
            var arrayTeg = new MyArrayList<string>(10);
            string teg = "";
            while (line != null)
            {
                isOpen = false;
                teg = "";
                for(int i = 0; i < line.Length; i++)
                {
                    if (line[i]=='<' && i + 1 != line.Length)
                    {
                        if ((line[i + 1] == '/' || char.IsLetter(line[i + 1])) && !isOpen) { isOpen = true; }
                        else
                        {
                            i++;
                            while (i < line.Length)
                            {
                                if (line[i] == '<') break;
                                i++;
                            }
                            teg = "";
                        }
                        if (i == line.Length) break;
                    }
                    if (line[i]=='>' && isOpen) { isOpen = false; teg += line[i];isClose = true; }
                    if (isOpen && (line[i] == '<' || line[i] == '>' || line[i] == '/' || line[i] == '/' || char.IsLetter(line[i]))) teg += line[i];
                    if (isClose) { arrayTeg.Add(teg); teg = ""; isClose = false; }
                }
                line = sr.ReadLine();
            }

            return arrayTeg;
        }
        static MyArrayList<string> Delete(MyArrayList<string> Array)
         {
            MyArrayList<string> result = new MyArrayList<string>(10);
            MyArrayList<string> allLowerTegs = new MyArrayList<string>(10);
            string dublicate;
            string dublicateLower;
            for (int i = 0; i < Array.Size(); i++)
            {
                dublicate = Array.get(i);
                dublicateLower = dublicate.ToLower();
                allLowerTegs.Add(dublicateLower);
            }

            for (int i = 0; i < allLowerTegs.Size(); i++)
            {
                string teg1 = allLowerTegs.get(i);
                for (int j = i + 1; j < allLowerTegs.Size(); j++)
                {
                    bool flag = true;
                    string teg2 = allLowerTegs.get(j);
                    if (Math.Abs(teg2.Length - teg1.Length) > 1) continue;
                    if (teg2.Length > teg1.Length && teg2[1] == '/')
                    {
                        for (int k = 1; k < teg1.Length; k++)
                            if (teg1[k] != teg2[k + 1]) { flag = false; break; }
                    }
                    else if (teg1.Length > teg2.Length && teg1[1] == '/')
                    {
                        for (int k = 1; k < teg2.Length; k++)
                            if (teg2[k] != teg1[k + 1]) { flag = false; break; }
                    }
                    else if (teg2.Length == teg1.Length)
                    {
                        for (int k = 1; k < teg1.Length; k++)
                            if (teg1[k] != teg2[k]) { flag = false; break; }
                    }
                    else continue;
                    if (flag) allLowerTegs.set(j, "false");
                }
            }
            for (int i = 0; i < allLowerTegs.Size(); i++)
            {
                if (allLowerTegs.get(i) == "false") continue;
                result.Add(Array.get(i));
            }
            return result;
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            var array = program.GetTeg();
            var teg = Delete(array);
            for (int i = 0; i < teg.Size(); i++)
            {
                Console.WriteLine(teg.get(i));
            }
        }
    }
}
