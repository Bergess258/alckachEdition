using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightWeekSec
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader read = new StreamReader("input.txt");
            StreamWriter writer = new StreamWriter("output.txt");
            int n = int.Parse(read.ReadLine());
            Dictionary<string, string> mm = new Dictionary<string, string>();
            Dictionary<string, int> m = new Dictionary<string, int>();
            List<string> kek = new List<string>();
            for (int i = 0; i < n; ++i)
            {
                args = read.ReadLine().Split(' ');
                switch (args[0])
                {
                    case "put":
                        if (mm.ContainsKey(args[1]))
                        {
                            mm[args[1]] = args[2];
                        }
                        else
                        {
                            m.Add(args[1], mm.Count);
                            mm.Add(args[1], args[2]);
                            kek.Add(args[1]);
                        }
                            
                        break;
                    case "get":
                        if (mm.ContainsKey(args[1]))
                            writer.WriteLine(mm[args[1]]);
                        else
                            writer.WriteLine("<none>");
                        break;
                    case "prev":
                        if (m.ContainsKey(args[1])&& m[args[1]] - 1>-1)
                            writer.WriteLine(mm[kek[m[args[1]]-1]]);
                        else
                            writer.WriteLine("<none>");
                        break;
                    case "next":
                        if (m.ContainsKey(args[1]) && m[args[1]] + 1 < kek.Count)
                            writer.WriteLine(mm[kek[m[args[1]] + 1]]);
                        else
                            writer.WriteLine("<none>");
                        break;
                    case "delete":
                        mm.Remove(args[1]);
                        kek.RemoveAt(m[args[1]]);
                        for (int j = m[args[1]]; j < kek.Count; ++j)
                            --m[kek[j]];
                        m.Remove(args[1]);
                        break;
                }
            }
            read.Close();
            writer.Close();
        }
    }
}
