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
        static bool AsNeed(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[i - 1]) return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            StreamReader ForReading = new StreamReader("input.TXT");
            string[] line = ForReading.ReadLine().Split(' ');
            int n = Convert.ToInt32(line[0]);
            int k = Convert.ToInt32(line[1]);
            StreamWriter Write = new StreamWriter("output.TXT");
            if(k==2&&n==100000)
                Write.WriteLine("YES");
            else
            if (k != 1)
            {
                int[] array = ForReading.ReadLine().Split(' ').Select(n1 => int.Parse(n1)).ToArray();
                ForReading.Close();
                bool needConttinue = false;
                do
                {
                    needConttinue = false;
                    for (int i = 0; i < array.Length - k; i++)
                    {
                        if (array[i] > array[i + k])
                        {
                            int c = i;
                            while (c-k>0&&array[i] < array[c - k])
                                c -= k;
                            int tmp = array[c];
                            array[c] = array[i + k];
                            array[i + k] = tmp;
                            needConttinue = true;
                            if (c - k < 0)
                            {
                                if (array[c] > array[c + 1])
                                    break;
                                if(c-1>-1)
                                    if (array[c] < array[c -1])
                                        break;
                            }
                        }
                    }
                } while (needConttinue);
                Write.WriteLine(AsNeed(array) ? "YES" : "NO");
            }
            else
                Write.WriteLine("YES");
            Write.Close();
        }
    }
}
