using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checksectask
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader ForReading = new StreamReader("input.TXT");
            StreamWriter Write = new StreamWriter("output.TXT");
            long countInv = 0;
            int n = Convert.ToInt32(ForReading.ReadLine());
            string[] line = new string[n];
            line = ForReading.ReadLine().Replace('.', ',').Split(' ');
            int[] mas = new int[n];
            for (int i = 0; i < n; i++)
                mas[i] = Convert.ToInt32(line[i]);
            ForReading.Close();
            int[] tempMas = new int[n];
            int size = 1;
            while (size < n)
            {
                int[] mas1 = new int[size], mas2 = new int[size];
                for(int i = 0; i < n; i += 2 * size)
                {
                    int size2;
                    int z = 0, j = 0;
                    if (i + 2 * size > n) size2 = n - i - size;
                    else
                        size2 = size;
                    if(size2>0)
                    for (int  z1 = 0; z1 < size+size2; z1++)
                    {
                        if (z != size)
                            if (j != size2)
                                if (mas[i+z] <= mas[i+j+size]) tempMas[i+z+j] = mas[i + z++];
                                else { tempMas[i + z + j] = mas[i + size + j++]; countInv += size - z; }
                        else tempMas[i + z + j] = mas[i + z++];
                        else tempMas[i + z + j] = mas[i + size + j++];
                    }
                }
                size *= 2;
                for (int i = 0; i < n; i++)
                    mas[i] = tempMas[i];
            }
            Write.Write(countInv);
            Write.Close();
        }
    }
}