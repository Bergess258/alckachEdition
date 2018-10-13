using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koks
{
    class Program
    {
        static bool AsNeed(long[] array)
        {
            for (long i = 1; i < array.Length; i++)
            {
                if (array[i] < array[i - 1]) return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            StreamReader ForReading = new StreamReader("input.TXT");
            args = ForReading.ReadLine().Split(' ');
            long n = long.Parse(args[0]);
            long k = long.Parse(args[1]);
            StreamWriter Write = new StreamWriter("output.TXT");
            if (k != 1)
            {
                long[] arr = new long[n];
                args = ForReading.ReadLine().Split(' ');
                ForReading.Close();
                for (long i = 0; i < n; i++)
                    arr[i] = long.Parse(args[i]);
                List<List<long>> temp=new List<List<long>>();
                for (long i = 0; i < k; i++)
                    temp.Add(new List<long>());
                for (int i = 0; i < k; i ++)
                    for (long j = 0; j < n && i + j < n; j += k)
                        temp[i].Add(arr[i + j]);
                for (int i = 0; i < temp.Count; i++)
                    temp[i].Sort();
                for (int i = 0; i < k; i++)
                {
                    int g = 0;
                    for (long j = 0; j < n && i + j < n; j += k)
                        arr[i + j] = temp[i][g++];
                }
                Write.Write(AsNeed(arr) ? "YES" : "NO");
            }
            else
                Write.Write("YES");
            Write.Close();
        }
    }
}
