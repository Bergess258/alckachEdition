using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader read = new StreamReader("input.txt");
            int length = int.Parse(read.ReadLine());
            int[] mas = new int[length];
            args = read.ReadLine().Split(' ');
            for (int i = 0; i < length; i++)
                mas[i] = int.Parse(args[i]);
            int len = int.Parse(read.ReadLine());
            int[] le = new int[len];
            args = read.ReadLine().Split(' ');
            for (int i = 0; i < len; i++)
                le[i] = int.Parse(args[i]);
            foreach (int item in le)
            {
                int l = -1;
                int r = length;
                int m;
                while (r > l + 1)
                {
                m = (l + r) / 2;
                    if (mas[m] < item)
                        l = m;
                    else
                        r = m;
                }
                Console.WriteLine();
            }
        }
    }
}
