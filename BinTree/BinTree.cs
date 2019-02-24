using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BinTree
{
    public class BinTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public BinTree()
        {
            root = null;
        }

        public BinTree(T initialValue)
        {
            root = new TreeNode<T>
            {
                Value = initialValue,
                Left = null,
                Right = null
            }; 
        }

        public bool IsEmpty => root == null;

        public void Add(T value)
        {
            if (IsEmpty)
            {
                root = new TreeNode<T>
                {
                    Value = value
                };
                return;
            }
            AddToTree(value, root);
        }

        public bool Delete(T value)
        {
            var (current, parent) = FindNodeAndParent(value, null, root);

            if (current == null)
            {
                return false;
            }

            if (current.Right == null)
            {
                if (parent == null)
                {
                    root = current.Left;
                }
                else
                {
                    var result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    root = current.Right;
                }
                else
                {
                    var result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            else
            {
                var leftMost = current.Right.Left;
                var leftMostParent = current.Right;

                while (leftMost.Left != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }

                leftMostParent.Left = leftMost.Right;
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (parent == null)
                {
                    root = leftMost;
                }
                else
                {
                    var result = parent.Value.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = leftMost;
                    }
                    else if (result < 0)
                    {
                        parent.Right = leftMost;
                    }
                }
            }
            return true;
        }
        public bool Contains(T value) => !IsEmpty && FindInTree(value, root);

        private static void AddToTree(T value, TreeNode<T> currentNode)
        {
            if (currentNode.Value.CompareTo(value) == 0)
            {
                return;
            }

            if (currentNode.Value.CompareTo(value) > 0)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new TreeNode<T>
                    {
                        Value = value
                    };
                }
                else
                {
                    AddToTree(value, currentNode.Left);
                }
            }
            else
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = new TreeNode<T>
                    {
                        Value = value
                    };
                }
                else
                {
                    AddToTree(value, currentNode.Right);
                }
            }
        }

        private static bool FindInTree(T value, TreeNode<T> currentNode)
        {
            var compRes = currentNode.Value.CompareTo(value);
            if (compRes == 0) return true;
            if (compRes > 0)
            {
                return currentNode.Left != null && FindInTree(value, currentNode.Left);
            }

            return currentNode.Right != null && FindInTree(value, currentNode.Right);
        }

        private static (TreeNode<T>, TreeNode<T>) FindNodeAndParent(T value, TreeNode<T> parentNode, TreeNode<T> currentNode)
        {    
            if (currentNode.Value.CompareTo(value) == 0)
            {
                return (currentNode, parentNode);
            }

            if (currentNode.Value.CompareTo(value) > 0)
            {
                return currentNode.Left == null 
                    ? (null, currentNode) 
                    : FindNodeAndParent(value, currentNode, currentNode.Left);
            }
            
            return currentNode.Right == null 
                ? (null, currentNode) 
                : FindNodeAndParent(value, currentNode, currentNode.Right);
        }

        public override string ToString()
        {
            string Iter(TreeNode<T> currentNode, StringBuilder sb)
            {
                if (currentNode.Left != null)
                {
                    Iter(currentNode.Left, sb);
                }

                sb.Append($"{currentNode.Value}, ");
                if (currentNode.Right != null)
                {
                    Iter(currentNode.Right, sb);
                }

                return sb.ToString();
            }

            return Iter(root, new StringBuilder());
        }
    }
}
