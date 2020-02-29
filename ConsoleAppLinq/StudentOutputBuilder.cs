using BinaryTree;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleAppLinq
{
    public class StudentOutputBuilder
    {
        private List<Action<StringBuilder, Test>> funcs = new List<Action<StringBuilder, Test>>();
        private List<Action<IEnumerable<Test>>> orderTest = new List<Action<IEnumerable<Test>>>();

        private IEnumerable<Student> order;

        private void OrderStudentName(bool typeOrder = true)
        {
            if(typeOrder)
            {
                order = from it in order
                        orderby it.Name
                        select it;
            }
            else
            {
                order = from it in order
                        orderby it.Name descending
                        select it;
            }
        }

        private void OrderDate(bool typeOrder = true)
        {
            if(typeOrder)
            {
                orderTest.Add(tests =>
                {
                    tests = from item in tests
                            orderby item.Date
                            select item;
                });
            }
            else
            {
                orderTest.Add(tests =>
                {
                    tests = from item in tests
                            orderby item.Date descending
                            select item;
                });
            }
        }

        private void OrderCourse(bool typeOrder = true)
        {
            if (typeOrder)
            {
                orderTest.Add(tests =>
                {
                    tests = from item in tests
                            orderby item.Course
                            select item;
                });
            }
            else
            {
                orderTest.Add(tests =>
                {
                    tests = from item in tests
                            orderby item.Course descending
                            select item;
                });
            }
        }

        private void OrderGrade(bool typeOrder = true)
        {
            if (typeOrder)
            {
                orderTest.Add(tests =>
                {
                    tests = from item in tests
                            orderby item.Grade
                            select item;
                });
            }
            else
            {
                orderTest.Add(tests =>
                {
                    tests = from item in tests
                            orderby item.Grade descending
                            select item;
                });
            }
        }

        private void InitOrder(string[] order)
        {
            if (order.Length == 0)
            {
                return;
            }

            for (int i = 0; i < order.Length; i++)
            {
                switch (order[i].ToLower())
                {
                    case "student+":
                        OrderStudentName();
                        break;

                    case "student-":
                        OrderStudentName(false);
                        break;

                    case "course+":
                        OrderCourse();
                        break;

                    case "course-":
                        OrderCourse(false);
                        break;

                    case "grade+":
                        OrderGrade();
                        break;

                    case "grade-":
                        OrderGrade(false);
                        break;

                    case "date+":
                        OrderDate();
                        break;

                    case "date-":
                        OrderDate(false);
                        break;

                    default: 
                        throw new ArgumentException("Invalid order by input format");
                }
            }
        }

        public IEnumerable<Student> Init(string[] output, string[] orderText, BinarySearchTree<Student> students)
        {
            order = students;

            InitActions(output);
            InitOrder(orderText);

            return order;
        }

        private void InitActions(string [] str)
        {
            if (funcs.Count != 0) 
                return;

            if(str.Length > 4 || str.Length == 0)
            {
                throw new ArgumentException("Invalid string data format");
            }

            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i].ToLower())
                {
                    case "student":
                        break;

                    case "course":
                        SetCourse();
                        break;

                    case "grade":
                        SetGrade();
                        break;

                    case "date":
                        SetDate();
                        break;

                    default: 
                        throw new ArgumentException("Invalid show input format");
                }
            }
        }

        private void SetName(StringBuilder stringBuilder, Student student)
        {
            stringBuilder.Append("Имя студента - ");
            stringBuilder.Append(student.Name);
            stringBuilder.Append('\n');
        }

        private void SetDate()
        {
            funcs.Add((stringBuilder, test) =>
            {
                stringBuilder.Append("Дата - ");
                stringBuilder.Append(test.Date.ToString());
                stringBuilder.Append('\n');
            });
        }

        private void SetGrade()
        {
            funcs.Add((stringBuilder, test) =>
            {
                stringBuilder.Append("Оценка - ");
                stringBuilder.Append(test.Grade.ToString());
                stringBuilder.Append('\n');
            });
        }

        private void SetCourse()
        {
            funcs.Add((stringBuilder, test) =>
            {
                stringBuilder.Append("Тест - ");
                stringBuilder.Append(test.Course.ToString());
                stringBuilder.Append('\n');
            });
        }

        public string Build(Student student)
        {
            if(student == null)
            {
                throw new ArgumentException("student is null");
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('\n');
            SetName(stringBuilder, student);

            if(orderTest.Count != 0)
            {
                foreach (var item in orderTest)
                {
                    item.Invoke(student.testInfo);
                }
            }


            if (funcs.Count != 0)
            {
                foreach (Test test in student.testInfo)
                {
                    foreach (var item in funcs)
                    {
                        item.Invoke(stringBuilder, test);
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
