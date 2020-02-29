using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BinaryTree;

namespace ConsoleAppLinq
{
    public class BinaryHelper
    {
        public string Path { get; set; }

        public BinaryHelper(string path)
        {
            if (path == null)
            {
                throw new ArgumentException("Path is null");
            }

            Path = path;
        }

        public void Write(Student[] student)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
               formatter.Serialize(fs, student);
            }
        }

        public BinarySearchTree<Student> Read()
        {
            BinarySearchTree<Student> students;
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                    return null;

                Student[] stud = (Student[])formatter.Deserialize(fs);
                students = new BinarySearchTree<Student>(CommandType.Preorder, stud);
            }

            return students;
        }

    }
}