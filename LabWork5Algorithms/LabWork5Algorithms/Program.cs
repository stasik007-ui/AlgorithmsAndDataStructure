using System;
using System.Text;

namespace LabWork_Search
{
    public class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public long IdCode { get; set; }
        public string StudyMode { get; set; } 

        public Student(string surname, string name, long idCode, string studyMode)
        {
            Surname = surname;
            Name = name;
            IdCode = idCode;
            StudyMode = studyMode;
        }

        public override string ToString()
        {
            return string.Format("| {0,-12} | {1,-12} | {2,-15} | {3,-10} |",
                Surname, Name, IdCode, StudyMode);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Дослідження алгоритмів пошуку ===");
            Console.WriteLine("Варіант 19: Інтерполяційний пошук. Видалення студента-заочника за ІПН.\n");

            int minSize = 20;
            Student[] students = new Student[minSize];
            int currentCount = 0;

            Console.Write($"Мінімальна кількість елементів за методичкою - {minSize}.\nСкільки студентів ви бажаєте ввести вручну? (решта згенерується автоматично): ");
            if (!int.TryParse(Console.ReadLine(), out int manualCount) || manualCount < 0)
            {
                manualCount = 0;
            }

            if (manualCount > minSize)
            {
                minSize = manualCount;
                students = new Student[minSize];
            }

            for (int i = 0; i < manualCount; i++)
            {
                Console.WriteLine($"\n--- Введення студента #{i + 1} ---");
                Console.Write("Прізвище: ");
                string surname = Console.ReadLine();

                Console.Write("Ім'я: ");
                string name = Console.ReadLine();

                long idCode = 0;
                while (idCode <= 0)
                {
                    Console.Write("Ідентифікаційний код (число): ");
                    long.TryParse(Console.ReadLine(), out idCode);
                }

                string studyMode = "";
                while (studyMode != "денна" && studyMode != "заочна")
                {
                    Console.Write("Умова навчання (введіть 'денна' або 'заочна'): ");
                    studyMode = Console.ReadLine()?.ToLower();
                }

                students[currentCount++] = new Student(surname, name, idCode, studyMode);
            }

            Random rnd = new Random();
            string[] surnames = { "Коваленко", "Ткаченко", "Бойко", "Мельник", "Шевченко", "Іваненко", "Петренко", "Лисенко" };
            string[] names = { "Олег", "Анна", "Іван", "Марія", "Петро", "Олена", "Максим", "Дарина" };
            string[] modes = { "денна", "заочна" };

            while (currentCount < minSize)
            {
                students[currentCount++] = new Student(
                    surnames[rnd.Next(surnames.Length)],
                    names[rnd.Next(names.Length)],
                    rnd.Next(100000000, 999999999), // Випадковий ІПН
                    modes[rnd.Next(modes.Length)]
                );
            }

            Console.WriteLine("\n[*] Масив успішно сформовано (Невпорядкований).");
            PrintArray(students, currentCount);

            Console.WriteLine("\n[*] Сортування масиву за зростанням ІПН (для інтерполяційного пошуку)...");
            BubbleSortById(students, currentCount);
            PrintArray(students, currentCount);

            Console.Write("\nВведіть ідентифікаційний код студента для пошуку (і можливого видалення): ");
            if (long.TryParse(Console.ReadLine(), out long searchId))
            {
                int foundIndex;
                Student foundStudent = InterpolationSearch(students, currentCount, searchId, out foundIndex);

                if (foundStudent != null)
                {
                    Console.WriteLine($"\n[+] Студента знайдено: {foundStudent}");

                    // Перевірка умови: якщо навчається заочно - видалити
                    if (foundStudent.StudyMode == "заочна")
                    {
                        Console.WriteLine("[-] Умова виконується (навчається заочно). Видаляємо студента...");
                        DeleteStudentAt(ref students, ref currentCount, foundIndex);

                        Console.WriteLine("\n--- Оновлений масив після видалення ---");
                        PrintArray(students, currentCount);
                    }
                    else
                    {
                        Console.WriteLine("[!] Студент навчається на денній формі. Видалення скасовано.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[-] Студента з таким ідентифікаційним кодом не знайдено (результат: null).");
                }
            }
            else
            {
                Console.WriteLine("Некоректний формат коду.");
            }

            Console.ReadLine();
        }

        static void BubbleSortById(Student[] arr, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (arr[j].IdCode > arr[j + 1].IdCode)
                    {
                        Student temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        static Student InterpolationSearch(Student[] arr, int count, long key, out int index)
        {
            int low = 0;
            int high = count - 1;
            index = -1;

            while (low <= high && key >= arr[low].IdCode && key <= arr[high].IdCode)
            {
                if (low == high)
                {
                    if (arr[low].IdCode == key)
                    {
                        index = low;
                        return arr[low];
                    }
                    return null;
                }

                long pos = low + ((key - arr[low].IdCode) * (high - low)) / (arr[high].IdCode - arr[low].IdCode);

                if (pos < 0 || pos >= count) break;

                if (arr[(int)pos].IdCode == key)
                {
                    index = (int)pos;
                    return arr[(int)pos];
                }

                if (arr[(int)pos].IdCode < key)
                    low = (int)pos + 1;
                else
                    high = (int)pos - 1;
            }

            return null;
        }

        static void DeleteStudentAt(ref Student[] arr, ref int count, int index)
        {
            for (int i = index; i < count - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            arr[count - 1] = null; 
            count--; 
        }

        static void PrintArray(Student[] arr, int count)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine(string.Format("| {0,-12} | {1,-12} | {2,-15} | {3,-10} |", "Прізвище", "Ім'я", "Ідент. код", "Форма навч."));
            Console.WriteLine(new string('-', 60));
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(arr[i].ToString());
            }
            Console.WriteLine(new string('-', 60));
        }
    }
}