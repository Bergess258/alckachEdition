using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace SixThird
{
    class Program
    {
        class AVL
        {
            
            public Node root;
            public AVL(Node te)
            {
                root = te;
            }
            public AVL()
            {
            }
            public void Add(int data)
            {
                Node newItem = new Node(data);
                if (root == null)
                {
                    root = newItem;
                }
                else
                {
                    root = RecursiveInsert(root, newItem);
                }
            }
            private Node RecursiveInsert(Node current, Node n)
            {
                if (current == null)
                {
                    current = n;
                    return current;
                }
                else if (n.data < current.data)
                {
                    current.left = RecursiveInsert(current.left, n);
                    current = balance_tree(current);
                }
                else if (n.data > current.data)
                {
                    current.right = RecursiveInsert(current.right, n);
                    current = balance_tree(current);
                }
                return current;
            }
            public Node balance_tree(Node current)
            {
                int b_factor = balance_factor(current);
                if (b_factor > 1)
                {
                    if (balance_factor(current.left) > 0)
                    {
                        current = RotateLL(current);
                    }
                    else
                    {
                        current = RotateLR(current);
                    }
                }
                else if (b_factor < -1)
                {
                    if (balance_factor(current.right) > 0)
                    {
                        current = RotateRL(current);
                    }
                    else
                    {
                        current = RotateRR(current);
                    }
                }
                return current;
            }
            public void Delete(int target)
            {//and here
                root = Delete(root, target);
            }
            private Node Delete(Node current, int target)
            {
                Node parent;
                if (current == null)
                { return null; }
                else
                {
                    //left subtree
                    if (target < current.data)
                    {
                        current.left = Delete(current.left, target);
                        if (balance_factor(current) == -2)//here
                        {
                            if (balance_factor(current.right) <= 0)
                            {
                                current = RotateRR(current);
                            }
                            else
                            {
                                current = RotateRL(current);
                            }
                        }
                    }
                    //right subtree
                    else if (target > current.data)
                    {
                        current.right = Delete(current.right, target);
                        if (balance_factor(current) == 2)
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else
                            {
                                current = RotateLR(current);
                            }
                        }
                    }
                    //if target is found
                    else
                    {
                        if (current.right != null)
                        {
                            //delete its inorder successor
                            parent = current.right;
                            while (parent.left != null)
                            {
                                parent = parent.left;
                            }
                            current.data = parent.data;
                            current.right = Delete(current.right, parent.data);
                            if (balance_factor(current) == 2)//rebalancing
                            {
                                if (balance_factor(current.left) >= 0)
                                {
                                    current = RotateLL(current);
                                }
                                else { current = RotateLR(current); }
                            }
                        }
                        else
                        {   //if current.left != null
                            return current.left;
                        }
                    }
                }
                return current;
            }
            public void Find(int key)
            {
                if (Find(key, root).data == key)
                {
                    Console.WriteLine("{0} was found!", key);
                }
                else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
            private Node Find(int target, Node current)
            {

                if (target < current.data)
                {
                    if (target == current.data)
                    {
                        return current;
                    }
                    else
                        return Find(target, current.left);
                }
                else
                {
                    if (target == current.data)
                    {
                        return current;
                    }
                    else
                        return Find(target, current.right);
                }

            }
            public void DisplayTree()
            {
                if (root == null)
                {
                    Console.WriteLine("Tree is empty");
                    return;
                }
                InOrderDisplayTree(root);
                Console.WriteLine();
            }
            private void InOrderDisplayTree(Node current)
            {
                if (current != null)
                {
                    InOrderDisplayTree(current.left);
                    Console.Write("({0}) ", current.data);
                    InOrderDisplayTree(current.right);
                }
            }
            private int max(int l, int r)
            {
                return l > r ? l : r;
            }
            private int getHeight(Node current)
            {
                int height = 0;
                if (current != null)
                {
                    int l = getHeight(current.left);
                    int r = getHeight(current.right);
                    int m = max(l, r);
                    height = m + 1;
                }
                return height;
            }
            private int balance_factor(Node current)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int b_factor = l - r;
                return b_factor;
            }
            private Node RotateRR(Node parent)
            {
                Node pivot = parent.right;
                parent.right = pivot.left;
                pivot.left = parent;
                return pivot;
            }
            private Node RotateLL(Node parent)
            {
                Node pivot = parent.left;
                parent.left = pivot.right;
                pivot.right = parent;
                return pivot;
            }
            private Node RotateLR(Node parent)
            {
                Node pivot = parent.left;
                parent.left = RotateRR(pivot);
                return RotateLL(parent);
            }
            private Node RotateRL(Node parent)
            {
                Node pivot = parent.right;
                parent.right = RotateLL(pivot);
                return RotateRR(parent);
            }
        }
        class Node
        {
            public int data;
            public Node left;
            public Node right;
            public Node(int data)
            {
                this.data = data;
            }
            public Node()
            {
            }
        }
        static Node MainT = new Node();
        static int[] nodes;
        static int[] temp;
        static int[] nodes2;
        static AVL ma = new AVL();
        static void Main()
        {
            int length;
            StreamReader input = new StreamReader("input.txt");
            StreamWriter output = new StreamWriter("output.txt");
            
            length = int.Parse(input.ReadLine());
            if (length > 0)
            {
                nodes = new int[length * 2];
                nodes2 = new int[length];
                temp = new int[length];
                for (int i = 0; i < length; ++i)
                {
                    string[] tokens;
                    tokens = input.ReadLine().Split(' ');
                    nodes[i * 2] = int.Parse(tokens[1]) - 1;
                    nodes[i * 2 + 1] = int.Parse(tokens[2]) - 1;
                    temp[i] = int.Parse(tokens[0]);
                    ma.Add(temp[i]);
                    if (nodes[i * 2] != -1) { temp[nodes[i * 2]] = i; }
                    if (nodes[i * 2 + 1] != -1) { temp[nodes[i * 2 + 1]] = i; }
                    if (nodes[i * 2] == -1 && nodes[i * 2 + 1] == -1) { nodes2[i] = 1; }
                }
                for (int i = length - 1; i > -1; --i)
                {
                    if (nodes[i * 2] != -1 && nodes2[nodes[i * 2]] + 1 > nodes2[i]) nodes2[i] = nodes2[nodes[i * 2]] + 1;
                    if (nodes[i * 2 + 1] != -1 && nodes2[nodes[i * 2 + 1]] + 1 > nodes2[i]) nodes2[i] = nodes2[nodes[i * 2 + 1]] + 1;
                }
                for (int i = 0; i < length; i++)
                {
                    int c = 0, b = 0;
                    if (nodes[i * 2] != -1) c = nodes2[nodes[i * 2]];
                    if (nodes[i * 2 + 1] != -1) b = nodes2[nodes[i * 2 + 1]];
                    nodes2[i] = c - b;
                }
                MainT=Zap(0,MainT);
                ma = new AVL(MainT);
                ma.balance_tree(ma.root);
            }
            else
                output.WriteLine(0);
            output.Close();
        }
        private static Node Zap(int i, Node tec)
        {
            tec.data = temp[i];
            if (nodes[i * 2] != -1) { tec.left = new Node(); tec.left = Zap(nodes[i * 2], tec.left); }
            if (nodes[i * 2 + 1] != -1)
            { tec.right = new Node(); tec.right = Zap(nodes[i * 2 + 1], tec.right); }
            return tec;
        }
    }
}
