using System;

namespace BinaryTree
{
    public class Preorder<T> : Command<T> where T : IComparable
    {
        public Preorder(BinaryTreeNode<T> node, int count) : base(node, count) { }

        private int index = 0;

        override public void Execute(BinaryTreeNode<T> enode)
        {
            if (enode != null)
            {
                array[index] = enode.Data;
                index++;

                Execute(enode.Left);
                Execute(enode.Right);
            }
        }
    }
}
