using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightWeek
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader read = new StreamReader("input.txt");
            StreamWriter writer = new StreamWriter("output.txt");
            int n = int.Parse(read.ReadLine());
            HT mm = new HT();
            for (int i = 0; i < n; ++i)
            {
                args = read.ReadLine().Split(' ');
                switch (args[0])
                {
                    case "put":
                        mm.Add(args[1], args[2]);
                        break;
                    case "get":
                        writer.WriteLine(mm.Search(args[1]));
                        break;
                    case "prev":
                        writer.WriteLine(mm.Prev(args[1]));
                        break;
                    case "next":
                        writer.WriteLine(mm.Next(args[1]));
                        break;
                    case "delete":
                        mm.Delete(args[1]);
                        break;
                }
            }
            read.Close();
            writer.Close();
        }
        
        class HT
        {
            class mas
            {
                public string key;
                public string value;
                public int hash;
            }
            private static int length=2000;
            private static List<int>[] bush = new List<int>[2000];
            private static List<mas> mass = new List<mas>();
            public HT()
            {
                length = 2000;
                bush = new List<int>[2000];
                for (int i = 0; i < length; ++i)
                {
                    bush[i] = new List<int>();
                }
                mass = new List<mas>();
            }
            public static int GetHash(string adres)
            {
                int hashcode = 0;
                double a = 0.6180339887;
                foreach (char s in adres) hashcode += (int)s;
                var p = Math.Truncate(hashcode * a);
                var t = hashcode * a - p;
                hashcode = (int)(length * t) % length;
                return hashcode;
            }
            public void Add(string k,string v)
            {
                int hash = GetHash(k);
                if (1.0*mass.Count / length >= 0.7)
                {
                    length += 1000;
                    bush = new List<int>[length];
                    for (int i = 0; i < length; ++i)
                    {
                        bush[i] = new List<int>();
                    }
                    for (int i=0;i<mass.Count;++i)
                    {
                        int temph = GetHash(mass[i].key);
                        mass[i].hash = temph;
                        bush[temph].Add(i);
                    }
                }
                foreach (int i in bush[hash])
                {
                    if (mass[i].key == k) { mass[i].value = v; return; }
                }
                bush[hash].Add(mass.Count);
                mass.Add(new mas() {key = k, value = v, hash=hash });
            }
            public string Search(string s)
            {
                int hash = GetHash(s);
                if (bush[hash].Count == 0) return "<none>";
                foreach (int i in bush[hash])
                {
                    if (mass[i].key == s) return mass[i].value;
                }
                return "<none>";
            }
            public string Prev(string s)
            {
                int hash = GetHash(s);
                if (bush[hash].Count == 0) return "<none>";
                foreach (int i in bush[hash])
                {
                    if (mass[i].key == s) if(i-1>-1)return mass[i-1].value; else return "<none>";
                }
                return "<none>";
            }
            public string Next(string s)
            {
                int hash = GetHash(s);
                if (bush[hash].Count == 0) return "<none>";
                foreach (int i in bush[hash])
                {
                    if (mass[i].key == s) if (i+1<mass.Count) return mass[i + 1].value; else return "<none>";
                }
                return "<none>";
            }
            public void Delete(string s)
            {
                int hash = GetHash(s);
                if (bush[hash].Count == 0) return;
                for (int i = 0; i < bush[hash].Count; i++)
                {
                    if (mass[bush[hash][i]].key == s) { Remove(bush[hash][i]); break; }
                }
            }
            private void Remove(int i)
            {
                int t = i++;
                for (; i < mass.Count; ++i)
                {
                    for (int j = 0; j < bush[mass[i].hash].Count; ++j)
                    {
                        if (bush[mass[i].hash][j] == i) { --bush[mass[i].hash][j]; break; }
                    }
                }
                mass.RemoveAt(t);
            }
        }
    }
}
