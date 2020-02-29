using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BinaryTree;

namespace ConsoleAppLinq
{
    [Serializable]
    public class Student : IComparable
    {
        public string Name { get; set; }
        public BinarySearchTree<Test> testInfo { get; private set; } = new BinarySearchTree<Test>(CommandType.Preorder);

        public double AverageGrade(BinarySearchTree<Test> test)
        {
            if(test.Count == 0)
            {
                return 0;
            }

            return test.Average(it => it.Grade);
        }

        public int CompareTo(object obj)
        {
            var stud = (Student)obj;
            
            return AverageGrade(testInfo).CompareTo(AverageGrade(stud.testInfo));
        }
    }
}
