using System.Collections.Generic;
using System.IO;

namespace Fourth
{
    class Program
    {
        static void Main()
        {
            StreamReader read = new StreamReader("input.txt");
            long n = long.Parse(read.ReadLine());
            List<long> mas = new List<long>();
            long[] ma = new long[n];
            StreamWriter output = new StreamWriter("output.txt");
            for (long i = 0; i < n; ++i)
            {
                string[] line = read.ReadLine().Split(' ');
                switch (line[0])
                {
                    case "A":
                        {
                            long temp = long.Parse(line[1]);
                            mas.Add(temp);
                            ma[i] = --temp;
                            mas.Sort();
                            break;
                        }

                    case "D":
                        {
                            int c = mas.IndexOf(ma[long.Parse(line[1])]);
                            mas[c] = long.Parse(line[2]);
                            mas.Sort();
                            break;
                        }

                    case "X":
                        {
                            if (mas.Count != 0)
                            {
                                output.WriteLine(mas[0]);
                                mas.RemoveAt(0);
                                mas.Sort();
                            }
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
