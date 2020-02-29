using System;

namespace ConsoleAppLinq
{
    [Serializable]
    public class Test : IComparable
    {
        public string Course { get; set; }
        public DateTime Date { get; set; }
        public int Grade { get; set; }

        public int CompareTo(object obj)
        {
            var test = (Test)obj;
            return Grade.CompareTo(test.Grade);
        }
    }
}
