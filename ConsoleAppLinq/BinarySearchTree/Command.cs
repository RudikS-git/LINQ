using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    [Serializable]
    public abstract class Command<T> : IEnumerator<T> where T : IComparable
    {
        public readonly int Count;

        private int position = -1;

        protected BinaryTreeNode<T> _node;
        protected T[] array;

        public Command(BinaryTreeNode<T> node, int count)
        {
            _node = node;
            Count = count;

            array = new T[Count];
            Execute(_node);
        }

        public T Current
        {
            get
            {
                if (position == -1 || position >= Count)
                {
                    throw new InvalidOperationException();
                }

                return array[position];
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() { }

        public bool MoveNext()
        {
            if (position < Count - 1)
            {
                position++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            position = -1;
        }

        public T[] TraversalArray(BinaryTreeNode<T> node, int count)
        {
            if (count == 0)
            {
                return null;
            }

            List<T> list = new List<T>();

            ArrayRecord(node, ref list);

            return list.ToArray();
        }

        private void ArrayRecord(BinaryTreeNode<T> node, ref List<T> list)
        {
            if (node != null)
            {
                list.Add(node.Data);

                ArrayRecord(node.Left, ref list);
                ArrayRecord(node.Right, ref list);

            }
        }

        public abstract void Execute(BinaryTreeNode<T> enode);
    }
}
