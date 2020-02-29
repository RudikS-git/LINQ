using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    [Serializable]
    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable
    {
        public BinaryTreeNode<T> Head {get; private set;}
        public int Count { get; private set; }
        public CommandType Сommand { get; set; }

        public BinarySearchTree(CommandType command)
        {
            Сommand = command;
        }

        public BinarySearchTree(T item, CommandType command)
        {
            if (item == null)
            {
                throw new ArgumentException("item is null");
            }

            Insert(item);
            Сommand = command;
        }

        public BinarySearchTree(T firstItem, T secondItem, CommandType command)
        {
            if (firstItem == null)
            {
                throw new ArgumentException("firstItem is null");
            }

            if (secondItem == null)
            {
                throw new ArgumentException("secondItem is null");
            }

            Insert(firstItem);
            Insert(secondItem);
            Сommand = command;
        }

        public BinarySearchTree(CommandType command, params T[] array)
        {
            if (array == null)
            {
                throw new ArgumentException("Array is null");
            }

            foreach (T item in array)
            {
                Insert(item);
            }

            Сommand = command;
        }

        public void Insert(T data)
        {
            if(Head == null)
            {
                Head = new BinaryTreeNode<T>(data);
            }
            else
            {
                InsertTo(Head, data);
            }

            Count++;
        }

        private void InsertTo(BinaryTreeNode<T> node, T data)
        {
            if (data.CompareTo(node.Data) < 0) // меньше узла
            {
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(data);
                    node.Left.Parent = node;
                }
                else
                {
                    InsertTo(node.Left, data);
                }
            }
            else // больше или равно
            {
                if(node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(data);
                    node.Right.Parent = node;
                }
                else
                {
                    InsertTo(node.Right, data);
                }
            }
        }

        public void Remove(T data)
        {
            BinaryTreeNode<T> removableNode = Find(data);

            if (removableNode == null)
            {
                return;
            }

            if (removableNode.Left == null && removableNode.Right == null)
            {
                ChangeNode(ref removableNode, null);
            }
            else if (removableNode.Left == null)
            {
                ChangeNode(ref removableNode, removableNode.Right);

                removableNode.Right.Parent = removableNode.Parent;
            }
            else if(removableNode.Right == null)
            {
                ChangeNode(ref removableNode, removableNode.Left);

                removableNode.Left.Parent = removableNode.Parent;
            }
            else
            {
                switch(removableNode.CurrentSide)
                {
                    case Side.Left:
                        removableNode.Parent.Left = removableNode.Right;
                        removableNode.Right.Parent = removableNode.Parent;
                        InsertTo(removableNode.Right, removableNode.Left.Data);
                        break;

                    case Side.Right:
                        removableNode.Parent.Right = removableNode.Right;
                        removableNode.Right.Parent = removableNode.Parent;
                        InsertTo(removableNode.Right, removableNode.Left.Data);
                        break;

                    default:
                        var bufLeft = removableNode.Left;
                        var bufRightLeft = removableNode.Right.Left;
                        var bufRightRight = removableNode.Right.Right;
                        removableNode.Data = removableNode.Right.Data;
                        removableNode.Right = bufRightRight;
                        removableNode.Left = bufRightLeft;
                        InsertTo(removableNode, bufLeft.Data);
                        break;
                }
            }

        }

        private void ChangeNode(ref BinaryTreeNode<T> removableNode, BinaryTreeNode<T> newNode)
        {
            if (removableNode.CurrentSide == Side.Left)
            {
                removableNode.Parent.Left = newNode;
            }
            else
            {
                removableNode.Parent.Right = newNode;
            }
        }

        public BinaryTreeNode<T> Find(T data)
        {
            BinaryTreeNode<T> current = Head;

            while(current != null)
            {
                int result = current.CompareTo(data);

                if(result > 0)
                {
                    current = current.Left;
                }
                else if (result < 0)
                {
                    current = current.Right;
                }
                else
                {
                    return current;
                }
            }

            return current;
        }
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Command<T> GetEnumerator()
        {
            switch (Сommand)
            {
                case CommandType.Preorder:
                    return new Preorder<T>(Head, Count);

                case CommandType.Postorder:
                    return new Postorder<T>(Head, Count);

                case CommandType.Inorder:
                    return new Inorder<T>(Head, Count);

                default:
                    throw new Exception("Command must be of commandtype");
            }
        }

        public T [] GetArray()
        {
            return GetEnumerator().TraversalArray(Head, Count);
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

    }
}
