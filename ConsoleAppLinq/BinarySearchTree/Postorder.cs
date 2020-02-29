using System;

namespace BinaryTree
{
    public class Postorder<T> : Command<T> where T : IComparable
    {
        public Postorder(BinaryTreeNode<T> node, int count) : base(node, count) { }

        private int index = 0;

        override public void Execute(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                Execute(node.Left);
                Execute(node.Right);

                array[index] = node.Data;
                index++;
            }
        }
    }
}
