using System;

namespace BinaryTree
{
    [Serializable]
    public class BinaryTreeNode <T> where T :IComparable
    {
        public T Data { get; set; }

        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
        public BinaryTreeNode<T> Parent { get; set; }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }

        public Side? CurrentSide
        { 
            get
            {
                if(Parent == null)
                {
                    return null;
                }
                else if(Parent.Left == this)
                {
                    return Side.Left;
                }
                else
                {
                    return Side.Right;
                }
            }
        }

        public int CompareTo(T dataSecond)
        {
            return Data.CompareTo(dataSecond);
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
