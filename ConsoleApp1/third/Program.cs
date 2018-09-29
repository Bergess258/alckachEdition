using System;
using System.IO;

namespace third
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader ForReading = new StreamReader("input.TXT");
            int n = Convert.ToInt32(ForReading.ReadLine());
            int[] mas = new int[n];
            for(int i = 0; i < n; i++)
                mas[i] = i+1;
            for (int i = 2; i < n; i++)
            {
                int temp = mas[i];
                mas[i] = mas[i / 2];
                mas[i / 2] = temp;
            }
            StreamWriter Write = new StreamWriter("output.TXT");
            foreach (int c in mas)
                Write.Write(c + " ");
            Write.Close();
        }
    }
}
