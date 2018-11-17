using System.IO;
using System;
using System.Globalization;

namespace SixSec
{
    class Program
    {
        static Tree tree;
        static void Main(string[] args)
        {
            var sr = new StreamReader("input.txt");
            var sw = new StreamWriter("output.txt");

            int n = int.Parse(sr.ReadLine());

            for (int i = 0; i < n; i++)
            {
                args = sr.ReadLine().Split();

                tree = Tree.Insert(tree, int.Parse(args[0]));
            }

            //Tree.Print(tree);

            tree = Tree.InsertWithBalance(tree, int.Parse(sr.ReadLine()));

            //tree = Tree.Balance(tree);

            var numElements = Tree.GetElementsNum(tree);

            var array = new TreeStruct[numElements];

            Tree.ToArray(tree, array);

            //Console.WriteLine();
            //Tree.Print(tree);

            sw.WriteLine(numElements);

            for (int i = 0; i < numElements; i++)
            {
                sw.WriteLine(array[i].ToString());
            }

            sr.Close();
            sw.Close();
        }
    }
    public class TreeStruct
    {
        public int Key { get; set; }

        public int Left { get; set; }

        public int Right { get; set; }

        public TreeStruct() { }

        public TreeStruct(int key)
        {
            Key = key;
        }

        public TreeStruct(int key, int left, int right)
        {
            Key = key;
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            return $"{Key} {Left} {Right}";
        }
    }

    public class Tree
    {
        private int key;
        private int height;

        private Tree left;
        private Tree right;

        private static int curIndex;

        public Tree() { }

        public Tree(int key)
        {
            this.key = key;
            height = 1;
        }

        public static Tree Insert(Tree tree, int key)
        {
            if (tree == null)
                return new Tree(key);

            if (key < tree.key)
                tree.left = Insert(tree.left, key);
            else if (key > tree.key)
                tree.right = Insert(tree.right, key);
            else
                throw new ArgumentException();

            FixHeight(tree);

            return tree;
            //return Balance(tree);
        }

        public static Tree InsertWithBalance(Tree tree, int key)
        {
            if (tree == null)
                return new Tree(key);

            if (key < tree.key)
                tree.left = InsertWithBalance(tree.left, key);
            else if (key > tree.key)
                tree.right = InsertWithBalance(tree.right, key);
            else
                throw new ArgumentException();

            FixHeight(tree);

            return Balance(tree);
        }

        public static int GetHeight(Tree tree)
        {
            return tree != null ? tree.height : 0;
        }

        public static int BalanceFactor(Tree tree)
        {
            return GetHeight(tree.right) - GetHeight(tree.left);
        }

        public static void FixHeight(Tree tree)
        {
            int leftHeight = GetHeight(tree.left);
            int rightHeight = GetHeight(tree.right);

            tree.height = (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;
        }

        public static Tree Balance(Tree tree)
        {
            FixHeight(tree);

            if (BalanceFactor(tree) == 2)
            {
                if (BalanceFactor(tree.right) < 0)
                    tree.right = RotateRight(tree.right);

                return RotateLeft(tree);
            }

            if (BalanceFactor(tree) == -2)
            {
                if (BalanceFactor(tree.left) > 0)
                    tree.left = RotateLeft(tree.left);

                return RotateRight(tree);
            }

            return tree;
        }

        private static Tree RotateRight(Tree p)
        {
            var q = p.left;
            p.left = q.right;
            q.right = p;

            FixHeight(p);
            FixHeight(q);

            return q;
        }

        private static Tree RotateLeft(Tree q)
        {
            var p = q.right;
            q.right = p.left;
            p.left = q;

            FixHeight(q);
            FixHeight(p);

            return p;
        }

        public static Tree Remove(Tree tree, int key)
        {
            if (tree == null)
                return null;

            if (key < tree.key)
                tree.left = Remove(tree.left, key);
            else if (key > tree.key)
                tree.right = Remove(tree.right, key);
            else
            {
                var left = tree.left;
                var right = tree.right;
                tree = null;

                if (right == null)
                    return left;

                var min = FindMin(right);
                min.right = RemoveMin(right);
                min.left = left;

                return Balance(min);
            }

            return Balance(tree);
        }

        private static Tree FindMin(Tree tree)
        {
            return tree.left != null ? FindMin(tree.left) : tree;
        }

        private static Tree RemoveMin(Tree tree)
        {
            if (tree.left == null)
                return tree.right;

            tree.left = RemoveMin(tree.left);

            return Balance(tree);
        }


        // debug
        public static void Print(Tree tree, int level = 0)
        {
            if (tree.right != null)
                Print(tree.right, level + 1);

            for (int i = 0; i < level; i++)
                Console.Write("  ");

            Console.WriteLine(tree.key);

            if (tree.left != null)
                Print(tree.left, level + 1);
        }

        public static int GetElementsNum(Tree tree)
        {
            if (tree == null)
                return 0;

            int leftCount = 0;
            int rightCount = 0;

            if (tree.right != null)
                rightCount = GetElementsNum(tree.right);

            if (tree.left != null)
                leftCount = GetElementsNum(tree.left);

            return rightCount + leftCount + 1;
        }

        public static void ToArray(Tree tree, TreeStruct[] array)
        {
            curIndex = 0;
            ToArray(tree, array, false, -1);
        }

        public static void ToArray(Tree tree, TreeStruct[] array, bool isLeft, int parentIndex)
        {
            if (parentIndex != -1)
            {
                if (isLeft)
                    array[parentIndex].Left = curIndex + 1;
                else
                    array[parentIndex].Right = curIndex + 1;
            }

            parentIndex = curIndex;

            array[curIndex++] = new TreeStruct(tree.key);

            if (tree.left != null)
                ToArray(tree.left, array, true, parentIndex);

            if (tree.right != null)
                ToArray(tree.right, array, false, parentIndex);
        }
    }
}
