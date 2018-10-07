using System;
using System.IO;

namespace Fourth
{
    class Program
    {
        static int k1, k2;
        static void Main(string[] args)
        {
            StreamReader read = new StreamReader("input.txt");
            string[] line = read.ReadLine().Split(' ');
            int n = Convert.ToInt32(line[0]);
            k1 = Convert.ToInt32(line[1]) - 1; k2 = Convert.ToInt32(line[2]) - 1;
            line = read.ReadLine().Split(' ');
            int[] mas = new int[n];
            int A = Convert.ToInt32(line[0]), B = Convert.ToInt32(line[1]), C = Convert.ToInt32(line[2]);
            mas[0] = Convert.ToInt32(line[3]);
            mas[1] = Convert.ToInt32(line[4]);
            read.Close();
            for (int i = 2; i < n; i++)
                mas[i] = A * mas[i - 2] + C + B * mas[i - 1];
            sorting(mas, 0, n - 1);
            StreamWriter writer = new StreamWriter("output.txt");
            for (int i = k1; i < k2 + 1; i++)
                writer.Write(mas[i] + " ");
            writer.Close();
        }
        public static void sorting(int[] arr, long first, long last)
        {
            int p = arr[(last - first) / 2 + first];
            int temp;
            long i = first, j = last;
            while (i <= j)
            {
                while (arr[i] < p && i <= last) ++i;
                while (arr[j] > p && j >= first) --j;
                if (i <= j)
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    ++i; --j;
                }
            }
            if (j > first && ((k1 >= first && k1 <= j) || (k2 >= first && k2 <= j)||(k1<=first&&k2>=j))) sorting(arr, first, j);
            if (i < last && ((k1 >= i && k1 <= last) || (k2 >= i && k2 <= last) || (k1 <= i && k2 >= last))) sorting(arr, i, last);
        }
    }
}
