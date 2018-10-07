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
            ulong n = Convert.ToUInt64(line[0]);
            ulong m = Convert.ToUInt64(line[1]);
            ulong[] a = new ulong[n];
            ulong[] b = new ulong[m];
            line = ForReading.ReadLine().Split(' ');
            for (int i = 0; i < a.Length; i++)
            {
                ulong kek = Convert.ToUInt64(line[i]);
                a[i] = kek;
            }
            line = ForReading.ReadLine().Split(' ');
            for (int i = 0; i < b.Length; i++)
            {
                ulong kek = Convert.ToUInt64(line[i]);
                b[i] = kek;
            }
            ForReading.Close();
            ulong[] c2 = new ulong[m * n];
            int lol = 0;
            for (ulong i = 0; i < n; i++)
                for (ulong j = 0; j < m; j++)
                    c2[lol++] = a[i] * b[j];
            c2 = countingSort(c2);
            ulong res = 0;
            for (int i = 0; i < c2.Length; i += 10)
                res += c2[i];
            StreamWriter Write = new StreamWriter("output.TXT");
            Write.Write(res);
            Write.Close();
        }
        private static ulong[] countingSort(ulong[] arr)
        {
            List<Class1> count = new List<Class1>();
            for (int i = 0; i < arr.Length; i++)
            {
                bool ok = false;
                ulong c = arr[i] / 1000;
                for (int j = 0; j < count.Count; j++)
                {
                    if (count[j].Numb == c)
                    {
                        count[j].mas[arr[i] % 1000]++;
                        ok = true;
                        break;
                    }
                }
                if(ok==false)
                { Class1 temp = new Class1() { Numb = c };temp.mas[arr[i] % 1000]++; count.Add(temp); }
            }
            count.Sort();
            int z = 0;
            for (int i = 0; i < count.Count; i++)
            {
                ulong c = count[i].Numb*1000;
                for (uint j=0;j<1000;j++)
                    while (count[i].mas[j]-- > 0)
                        arr[z++] = c + j;
            }
            return arr;
        }
        class Class1 : IComparable
        {
            public ulong Numb;
            public int[] mas = new int[1000];

            public Class1()
            {
            }

            public Class1(uint i)
            {
                Numb = i;
            }

            public int CompareTo(object obj)
            {
                Class1 temp = (Class1)obj;
                if (Numb < temp.Numb) return -1;
                else if (Numb > temp.Numb) return 1;
                return 0;
            }
        }
    }
}
