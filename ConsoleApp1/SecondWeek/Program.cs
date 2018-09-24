using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondWeek
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader ForReading = new StreamReader("input.TXT");
            int countInv = 0;
            int n = Convert.ToInt32(ForReading.ReadLine());
            string[] line = new string[n];
            line = ForReading.ReadLine().Replace('.', ',').Split(' ');
            int[] mas = new int[n];
            for (int i = 0; i < n; i++)
                mas[i] = Convert.ToInt32(line[i]);
            ForReading.Close();
            StreamWriter Write = new StreamWriter("output.TXT");
            mas = Sort(mas, 0, n,Write,ref countInv);
            Write.WriteLine(countInv);
            Write.Close();
        }
        static public int[] Sort(int[] a, int l, int r, StreamWriter Write,ref int countInv)
        {
            if (a.Length == 1) return a;
            if (a.Length == 2)
            {
                if (a[0] > a[1]) { int temp = a[0]; a[0] = a[1]; a[1] = temp; countInv++; }
                return a;
            }
            int[] c;
            int Fc = a.Length / 2;
            if (a.Length % 2 == 1) Fc++;
            if (Fc % 2 == 1) Fc++;
            c = new int[Fc];
            int[] b = new int[a.Length-Fc];
            for (int i = 0; i < Fc; i++)
                c[i] = a[i];
            int Fb = 0;
            for (int i = Fc; i < a.Length; i++)
                b[Fb++] = a[i];
            c =Sort(c,l,l+Fc, Write, ref countInv);
            b = Sort(b, l + Fc, r, Write, ref countInv);
            int z = 0, j = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (z != c.Length)
                    if (j != b.Length)
                        if (c[z] < b[j]) a[i] = c[z++];
                        else { a[i] = b[j++]; countInv += c.Length-z; }
                    else a[i] = c[z++];
                else a[i] = b[j++];
            }
            return a;
        }
    }
}
