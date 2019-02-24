using System;
using System.Collections.Generic;
using BinTree;
using LinkList;
using Sorting;

namespace TestConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            var tree = new BinTree<int>();
            tree.Add(3);
            tree.Add(1);
            tree.Add(2);
            tree.Add(4);
            
            Console.WriteLine(tree);

            tree.Delete(1);
            
            Console.WriteLine(tree);
        }

        private static void TestList()
        {
            var li = new LinkList<int>();
            
            li.Add(20);

            li.Add(10);
            li.Add(30);
            
            li.DeleteAtIndex(2);
            Console.WriteLine(li);
            Console.WriteLine(li.Reverse());
            Console.WriteLine(li);
            Console.WriteLine(li.GetType());
        }

        private static void TestTree()
        {
            var tree = new BinTree<int>();
            var tree2 = new BinTree<int>(10);
            tree.Add(20);
            tree2.Add(20);
            Console.WriteLine(tree2.Contains(20));
            Console.WriteLine(tree.Contains(10));
        }
    }
}
