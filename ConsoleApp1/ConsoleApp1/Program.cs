using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader ForReading = new StreamReader("input.TXT");
            int n = Convert.ToInt32(ForReading.ReadLine());
            string[] line = new string[n];
            line = ForReading.ReadLine().Replace('.',',').Split(' ');
            int[] mas = new int[n];
            for (int i = 0; i < n; i++)
                mas[i] = Convert.ToInt32(line[i]);
            ForReading.Close();
            StreamWriter Write = new StreamWriter("output.TXT");
            for (int i = 0; i < mas.Length; i++)
            {
                int j = i;
                int cur = i;
                while (j <n)
                {
                    if (mas[j] < mas[cur]) { cur = j; }
                    j++;
                }
                j--;
                if (cur != i) { int temp = mas[i]; mas[i] = mas[cur];mas[cur] = temp; Write.WriteLine("Swap elements at indices {0} and {1}.", i + 1, cur+1); }
            }
            Write.WriteLine("No more swaps needed.");
            foreach (int i in mas)
                Write.Write("{0} ", i);
            Write.Close();
        }
    }
}