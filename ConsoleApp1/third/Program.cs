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
            int center = n / 2;
            if (n % 2 == 0) center--;
            mas[center] = n;
            int i = 1,j=1;
            while(i<n/2)
            {
                mas[center + i] = n - j++;
                mas[center - i++] = n - j++;
            }
            mas[n - 1] = 2;
            mas[0] = 1;
            StreamWriter Write = new StreamWriter("output.TXT");
            foreach (int c in mas)
                Write.Write(c + " ");
            Write.Close();
        }
    }
}
