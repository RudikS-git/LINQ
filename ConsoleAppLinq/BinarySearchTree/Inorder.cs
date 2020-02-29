using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class Inorder<T> :Command<T> where T : IComparable
    {
        public Inorder(BinaryTreeNode<T> node, int count) : base(node, count) { }

        private int index = 0;

        override public void Execute(BinaryTreeNode<T> enode)
        {
            IEnumerator enumerator = GetEnumerator();

            while(enumerator.MoveNext())
            {
                array[index] = ((BinaryTreeNode<T>)enumerator.Current).Data;
                index++;
            }
        }

        private IEnumerator GetEnumerator()
        {
            if (_node != null)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();

                BinaryTreeNode<T> current = _node;

                bool goLeftNext = true;

                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }
    }
}
