using System;
using System.Collections.Generic;
using System.IO;

namespace ForSec
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader read = new StreamReader("input.txt");
            string[] line = read.ReadLine().Split(' ');
            int n=int.Parse(line[0]), m= int.Parse(line[1]), k= int.Parse(line[2]);
            
            string letters = "abcdefghijklmnopqrstuvwxyz";
            //for (int i = 0; i <n; ++i)
            //    mas.Push(read.ReadLine());
            read.Close();
            for (int j = 0; j < letters.Length; ++j)
                for (int i = 0; i < letters.Length; ++i)
                {
                    if (i != j)
                    {
                        Stack<string> mas = new Stack<string>(n);
                        string str = letters[j].ToString() + letters[i].ToString();
                        mas.Push(str);
                        int[] pri = new int[m];
                        for (int h = 0; h < m; ++h)
                            pri[h] = h;
                        pri = radixsort(mas, m, k, pri);
                        Console.Write(str+" ");
                        for (int  g = 0;g <m;++g)
                        {
                            Console.Write(pri[g]);
                        }
                        Console.WriteLine();
                    }
                }
            StreamWriter writer = new StreamWriter("output.txt");
            //for (int i = 0; i < m; ++i)
                //writer.Write((pri[i]+1) + " ");
            writer.Close();
        }
        static int[] radixsort(Stack<string> Arr, int m, int CountIter, int[] print)
        {
            int[] koks = new int[m];
            for (int g = 0; g < CountIter; ++g)
            {
                string first = Arr.Pop();
                if (g != 0)
                {
                    string[] temp = new string[m];
                    for (int i = 0; i < m; ++i)
                    {
                        temp[i] = first[print[i]].ToString();
                    }
                    first = "";
                    for (int i = 0; i < m; i++)
                    {
                        first += temp[i];
                    }
                }
                long[] c = new long[26];
                for (int j = 0; j < m; ++j)
                {
                    c[first[j] - 97]++;
                }
                for (int j = 1; j < 26; ++j)
                {
                    c[j] += c[j - 1];
                }
                for (int j = m - 1; j > -1; --j)
                {
                    koks[--c[first[j] - 97]] = print[j];
                }
                for (int j = 0; j < m; ++j)
                {
                    print[j] = koks[j];
                }
            }
            return print;
        }
    }
}
