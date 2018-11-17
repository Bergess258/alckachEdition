using System.IO;
using System.Threading;

namespace SixForth
{
    class Program
    {
        private static temp[] nodes;

        public static StreamWriter output = new StreamWriter("output.txt");

        private static void BigStackMain()
        {
            if (CheckBranch(int.MinValue, int.MaxValue, 0))
            {
                output.WriteLine("YES");
            }
            else
            {
                output.WriteLine("NO");
            }

            output.Close();
        }

        private static bool CheckBranch(int minBoard, int maxBoard, int numberNode)
        {
            return (nodes[numberNode].Item2 == 0 || (nodes[nodes[numberNode].Item2 - 1].Item1 < nodes[numberNode].Item1
                                                     && nodes[nodes[numberNode].Item2 - 1].Item1 > minBoard
                                                     && CheckBranch(
                                                         minBoard,
                                                         nodes[numberNode].Item1,
                                                         nodes[numberNode].Item2 - 1)))
                   && (nodes[numberNode].Item3 == 0 || (nodes[nodes[numberNode].Item3 - 1].Item1 > nodes[numberNode].Item1
                                                        && nodes[nodes[numberNode].Item3 - 1].Item1 < maxBoard
                                                        && CheckBranch(
                                                            nodes[numberNode].Item1,
                                                            maxBoard,
                                                            nodes[numberNode].Item3 - 1)));
        }

        static void Main()
        {
            StreamReader input = new StreamReader("input.txt");
            int length = int.Parse(input.ReadLine());
            if (length > 0)
            {
                nodes = new temp[length];
                for (int i = 0; i < length; i++)
                {
                    string[] tokens = input.ReadLine().Split(' ');
                    nodes[i] = new temp(int.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2]));
                }

                input.Close();
                Thread newThread = new Thread(BigStackMain);
                newThread.Start();
            }
            else
            {
                output.WriteLine("YES");
                output.Close();
            }
        }
        class temp
        {
            public int Item1;
            public int Item2;
            public int Item3;
            public temp(int t,int f,int h)
            {
                Item1 = t;
                Item2 = f;
                Item3 = h;
            }
        }
    }
}
