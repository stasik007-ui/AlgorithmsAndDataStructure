using System;

namespace LabWork3_Level1
{
    public class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
        public uint StudentId { get; set; } 
        public string ForeignLanguage { get; set; }

        public Student(string surname, string name, int course, string group, uint studentId, string foreignLanguage)
        {
            Surname = surname;
            Name = name;
            Course = course;
            Group = group;
            StudentId = studentId;
            ForeignLanguage = foreignLanguage;
        }
    }

    public class TreeNode
    {
        public Student Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(Student data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    public class BinaryTree
    {
        public TreeNode Root { get; private set; }

        public BinaryTree()
        {
            Root = null;
        }

        public void Insert(Student newStudent)
        {
            Root = InsertRecursive(Root, newStudent);
        }

        private TreeNode InsertRecursive(TreeNode node, Student newStudent)
        {
            if (node == null)
            {
                return new TreeNode(newStudent);
            }

            if (newStudent.StudentId < node.Data.StudentId)
            {
                node.Left = InsertRecursive(node.Left, newStudent);
            }
            else if (newStudent.StudentId > node.Data.StudentId)
            {
                node.Right = InsertRecursive(node.Right, newStudent);
            }
            else
            {
                Console.WriteLine($"[!] Студент з квитком №{newStudent.StudentId} вже існує у дереві.");
            }

            return node;
        }

        public void PrintParallelTraversal()
        {
            PrintTableHeader();
            if (Root == null)
            {
                Console.WriteLine(string.Format("| {0,-95} |", "Дерево порожнє"));
            }
            else
            {
                InOrderRecursive(Root);
            }
            PrintTableFooter();
        }

        private void InOrderRecursive(TreeNode node)
        {
            if (node != null)
            {
                InOrderRecursive(node.Left);        
                PrintStudentRow(node.Data);         
                InOrderRecursive(node.Right);     
            }
        }

        private void PrintTableHeader()
        {
            Console.WriteLine(new string('-', 99));
            Console.WriteLine(string.Format("| {0,-12} | {1,-15} | {2,-12} | {3,-6} | {4,-10} | {5,-20} |",
                "Ст. квиток", "Прізвище", "Ім'я", "Курс", "Група", "Іноземна мова"));
            Console.WriteLine(new string('-', 99));
        }

        private void PrintStudentRow(Student s)
        {
            Console.WriteLine(string.Format("| {0,-12} | {1,-15} | {2,-12} | {3,-6} | {4,-10} | {5,-20} |",
                s.StudentId, s.Surname, s.Name, s.Course, s.Group, s.ForeignLanguage));
        }

        private void PrintTableFooter()
        {
            Console.WriteLine(new string('-', 99));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота 1.3 (Рівень 1) ===");
            Console.WriteLine("Варіант 19. Нелінійні структури даних: Бінарне дерево пошуку\n");

            BinaryTree studentTree = new BinaryTree();
            Console.WriteLine("[*] Бінарне дерево створено.\n");

            Student[] studentsToAdd = new Student[]
            {
                new Student("Коваленко", "Олег", 2, "КН-21", 10054, "Англійська"),
                new Student("Шевченко", "Марія", 1, "КН-11", 10020, "Німецька"),
                new Student("Бойко", "Іван", 3, "КН-32", 10089, "Англійська"),
                new Student("Ткаченко", "Анна", 2, "КН-21", 10045, "Французька"),
                new Student("Мельник", "Петро", 4, "КН-41", 10102, "Англійська"),
                new Student("Коваленко", "Олег", 2, "КН-21", 10054, "Англійська")
            };

            Console.WriteLine("--- Додавання елементів у дерево ---");
            foreach (var student in studentsToAdd)
            {
                studentTree.Insert(student);
                Console.WriteLine($"[+] Операція додавання (Квиток №{student.StudentId}) виконана.");
            }

            Console.WriteLine("\n--- Вміст дерева (Паралельний обхід / In-order) ---");
            Console.WriteLine("При паралельному обході ключі (студентські квитки) виводяться за зростанням:");
            studentTree.PrintParallelTraversal();

            Console.ReadLine();
        }
    }
}