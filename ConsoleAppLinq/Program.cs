using System;
using BinaryTree;
using System.Linq;

namespace ConsoleAppLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<Student> students;
            InitStudents(out students);

            string str, order;

            while(true)
            {
                Console.WriteLine("Чтобы получить список студентов");
                Console.WriteLine("Введите show: ...");
                Console.WriteLine("Введите orderyby: ...");
                Console.WriteLine("Пример: 'show: student, course, grade, date'");
                Console.WriteLine("Пример: 'orderby: student+'");
                Console.WriteLine("'+' - возрастание, '-' - убывание");

                Console.Write("show: ");
                str = Console.ReadLine();

                if(str == "" || str == null)
                {
                    Console.Clear();
                    Console.WriteLine("Невереный формат ввода, ориентируйтесь на пример\n");
                    continue;
                }

                break;
            }

            Console.Write("order by: ");
            order = Console.ReadLine();

            StudentOutputBuilder builder = new StudentOutputBuilder();
            var stud = builder.Init(str.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries),
                         order.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries),
                         students);

            foreach (Student item in stud)
            {
                Console.WriteLine(builder.Build(item));
            }
        }

        static void InitStudents(out BinarySearchTree<Student> students)
        {
            BinaryHelper binaryHelper = new BinaryHelper("students.dat");

            students = binaryHelper.Read();
            if(students != null)
            {
                return;
            }


            students = new BinarySearchTree<Student>(CommandType.Preorder);

            Student stud1 = new Student()
            {
                Name = "Dmitry",
            };

            stud1.testInfo.Insert(new Test()
            {
                Date = new DateTime(2015, 02, 03),
                Grade = 2,
                Course = "Math"
            });

            students.Insert(stud1);

            Student stud2 = new Student()
            {
                Name = "Egor"
            };
            
            students.Insert(stud2);

            stud2.testInfo.Insert(new Test()
            {
                Date = new DateTime(2015, 02, 03),
                Grade = 4,
                Course = "Eng"
            });

            stud2.testInfo.Insert(new Test()
            {
                Date = new DateTime(2012, 02, 03),
                Grade = 2,
                Course = "Rus"
            });

            binaryHelper.Write(new Student[] { stud1, stud2 });
           // binaryHelper.Write(stud2);
        }
    }
}
