using System;

namespace LabWork4
{
    public class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public long IdCode { get; set; } 
        public string Residence { get; set; }

        public Student(string surname, string name, long idCode, string residence)
        {
            Surname = surname;
            Name = name;
            IdCode = idCode;
            Residence = residence;
        }

        public override string ToString()
        {
            return $"[{IdCode}] {Surname} {Name}, Місце проживання: {Residence}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота 1.4 ===");
            Console.WriteLine("Варіант 19: Двоспрямований бульбашковий алгоритм сортування (за спаданням ІПН)\n");

            int count = 0;
            while (count <= 0)
            {
                Console.Write("Введіть кількість студентів: ");
                if (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
                {
                    Console.WriteLine("[Помилка] Введіть коректне додатне число.");
                }
            }

            Student[] students = new Student[count];
            Console.WriteLine("\n--- Введення даних студентів ---");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nСтудент #{i + 1}:");

                Console.Write("Прізвище: ");
                string surname = Console.ReadLine();

                Console.Write("Ім'я: ");
                string name = Console.ReadLine();

                long idCode = 0;
                bool validId = false;
                while (!validId)
                {
                    Console.Write("Ідентифікаційний код (число): ");
                    if (long.TryParse(Console.ReadLine(), out idCode) && idCode > 0)
                    {
                        validId = true;
                    }
                    else
                    {
                        Console.WriteLine("[Помилка] Код має бути додатним числом.");
                    }
                }

                Console.Write("Місце проживання: ");
                string residence = Console.ReadLine();

                students[i] = new Student(surname, name, idCode, residence);
            }

            Console.WriteLine("\n--- Масив студентів ПЕРЕД сортуванням ---");
            PrintArray(students);

            CocktailShakerSort(students);
 
            Console.WriteLine("\n--- Масив студентів ПІСЛЯ сортування (за спаданням ІПН) ---");
            PrintArray(students);

            Console.WriteLine("\nНатисніть Enter для виходу...");
            Console.ReadLine();
        }

        static void CocktailShakerSort(Student[] array)
        {
            bool swapped = true;
            int start = 0;
            int end = array.Length - 1;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end; ++i)
                {
                    if (array[i].IdCode < array[i + 1].IdCode)
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end--;

                for (int i = end - 1; i >= start; --i)
                {
                    if (array[i].IdCode < array[i + 1].IdCode)
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        swapped = true;
                    }
                }

                start++; 
            }
        }

        static void Swap(ref Student a, ref Student b)
        {
            Student temp = a;
            a = b;
            b = temp;
        }

        static void PrintArray(Student[] array)
        {
            foreach (var student in array)
            {
                Console.WriteLine(student.ToString());
            }
        }
    }
}