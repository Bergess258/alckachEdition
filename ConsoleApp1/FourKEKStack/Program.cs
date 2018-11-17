using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourKEKStack
{
    class Program
    {
        class MinHeap
        {
            private List<long> array = new List<long>();

            public void Add(long p)
            {
                array.Add(p);
                int c = array.Count - 1;
                while (c > 0 && array[c] < array[c / 2])
                {
                    long tmp = array[c];
                    array[c] = array[c / 2];
                    array[c / 2] = tmp;
                    c = c / 2;
                }
            }
            public long RemoveMin()
            {
                long ret = array[0];
                array[0] = array[array.Count - 1];
                array.RemoveAt(array.Count - 1);
                int c = 0;
                while (c < array.Count)
                {
                    int min = c;
                    if (2 * c + 1 < array.Count && array[2 * c + 1].CompareTo(array[min]) == -1)
                        min = 2 * c + 1;
                    if (2 * c + 2 < array.Count && array[2 * c + 2].CompareTo(array[min]) == -1)
                        min = 2 * c + 2;

                    if (min == c)
                        break;
                    else
                    {
                        long tmp = array[c];
                        array[c] = array[min];
                        array[min] = tmp;
                        c = min;
                    }
                }
                return ret;
            }
            public void Replace(long p,long o)
            {
                int c = array.IndexOf(p);
                array[c] = o;
                while (c > 0 && array[c]<array[c / 2])
                {
                    long tmp = array[c];
                    array[c] = array[c / 2];
                    array[c / 2] = tmp;
                    c = c / 2;
                }
            }

            public long Peek()
            {
                return array[0];
            }

            public int Count
            {
                get
                {
                    return array.Count;
                }
            }
        }
        static void Main()
            {
                StreamReader read = new StreamReader("input.txt");
                long n = long.Parse(read.ReadLine());
                long[] mas = new long[n];
                MinHeap queue = new MinHeap();
                StreamWriter output = new StreamWriter("output.txt");
                for (int i = 0; i < n; ++i)
                {
                    string[] line = read.ReadLine().Split(' ');
                    switch (line[0])
                    {
                        case "A":
                            {
                                long temp = long.Parse(line[1]);
                                queue.Add(temp);
                                mas[i] = temp;
                                break;
                            }

                        case "D":
                            {
                                queue.Replace(mas[long.Parse(line[1])-1], long.Parse(line[2]));
                                break;
                            }

                        case "X":
                            {
                            if (queue.Count != 0)
                                output.WriteLine(queue.RemoveMin());
                            else
                                output.WriteLine('*');
                                break;
                            }
                    }
                }
                output.Close();
            }
        }
}
