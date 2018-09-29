using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thirdweek
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader ForReading = new StreamReader("input.TXT");
            string[] line = ForReading.ReadLine().Split(' ');
            int n = Convert.ToInt32(line[0]);
            int m = Convert.ToInt32(line[1]);
            int[] c = new int[m * n];
            int[] c2 = new int[m * n];
            int max = 0;
            int[] a = new int[n];
            int[] b = new int[m];
            FillMas(ref ForReading, ref a);
            FillMas(ref ForReading, ref b);
            ForReading.Close();
            {
            int lol = 0;
            int[] c1 = new int[40001];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    c2[lol] = a[i] * b[j];
                    if (max < c2[lol]) max = c2[lol];
                    c1[c2[lol++]]++;
                }
            for (int i = 1; i <= max; i++)
                c1[i] += c1[i - 1];
            NewMethod(ref c, ref c1, c2);
            }
            int res = 0;
            for (int i = 0; i < c.Length; i += 10)
                res += c[i];
            StreamWriter Write = new StreamWriter("output.TXT");
            Write.Write(res);
            Write.Close();
        }
        private static int[] countingSort(int[] arr, int min, int max)
        {
            int[] count = new int[max - min + 1];
            int z = 0;

            for (int i = 0; i < count.Length; i++)
            {
                count[i] = 0;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                count[arr[i] - min]++;
            }

            for (int i = min; i <= max; i++)
            {
                while (count[i - min]-- > 0)
                {
                    arr[z] = i;
                    z++;
                }
            }
            return arr;
        }
            private static void NewMethod(ref int[] c,ref int[] c1, int[] a)
        {
            for (int i = a.Length-1; i > 0; i--)
                c[--c1[a[i]]] = a[i];
        }

        private static void FillMas(ref StreamReader ForReading,ref int[] a)
        {
            string[] line = ForReading.ReadLine().Split(' ');
            for (int i = 0; i < a.Length; i++)
            {
                int kek = Convert.ToInt32(line[i]);
                a[i] = kek;
            }
        }
    }
}
