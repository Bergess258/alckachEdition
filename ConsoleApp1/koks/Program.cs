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

        public static KeyValuePair<int, int>[] _heap;

        private static int _rightBoard = -1;
        static int[] mas;

        private static void CheckHeap(int index)
        {
            int min;
            if (index * 2 + 1 <= _rightBoard && _heap[index * 2 + 1].Key < _heap[index].Key)
            {
                min = index * 2 + 1;
            }
            else
            {
                min = index;
            }

            if (index * 2 + 2 <= _rightBoard && _heap[index * 2 + 2].Key < _heap[min].Key)
            {
                min = index * 2 + 2;
            }

            if (min != index)
            {
                KeyValuePair<int, int> shelf = new KeyValuePair<int, int>(_heap[min].Key, _heap[min].Value);
                _heap[min] = _heap[index];
                _heap[index] = shelf;
                CheckHeap(min);
            }
        }

        private static string RemoveMin()
        {
            if (_rightBoard > -1)
            {
                CheckHeap(0);
                int shelf = _heap[0].Key;
                _heap[0] = _heap[_rightBoard];
                --_rightBoard;
                return (shelf.ToString());
            }
            else
            {
                return "*";
            }
        }

        private static void AddElement(KeyValuePair<int, int> element)
        {
            int index = ++_rightBoard;
            _heap[index] = element;
            CheckHeap(index);
        }

        private static void ReplaceElement(int index, int newValue)
        {
            for (int i = 0; i < _heap.Length; i++)
            {
                if (_heap[i].Key == mas[index])
                {
                    _heap[i]= new KeyValuePair<int, int>(newValue, _heap[i].Value);
                    break;
                }
            }
            _heap[index] = new KeyValuePair<int, int>(index, newValue);
            CheckHeap(0);
        }

        static void Main()
        {
            StreamReader read = new StreamReader("input.txt");
            long n = long.Parse(read.ReadLine());
            _heap = new KeyValuePair<int,int>[n];
            mas = new int[n];
            StreamWriter output = new StreamWriter("output.txt");
            for (int i = 0; i < n; i++)
            {
                string[] line = read.ReadLine().Split(' ');
                switch (line[0])
                {
                    case "A":
                        {
                            int temp = int.Parse(line[1]);
                            AddElement(new KeyValuePair<int,int>(temp, i));
                            mas[i] = temp;
                            break;
                        }

                    case "D":
                        {
                            ReplaceElement(int.Parse(line[1])-1, int.Parse(line[2]));
                            break;
                        }

                    case "X":
                        {
                            output.WriteLine(RemoveMin());
                            break;
                        }
                }
            }

            output.Close();
        }
    }
}
