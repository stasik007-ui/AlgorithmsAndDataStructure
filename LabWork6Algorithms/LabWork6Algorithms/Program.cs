using System;
using System.Diagnostics;

namespace LabWork1_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=== Лабораторна робота 1.6 (Аналіз алгоритмів) ===");
                Console.WriteLine("Варіант 19: Сортування злиттям (двоколійне). Одновимірний масив.");
                Console.WriteLine("1. Ввести масив вручну (для демонстрації роботи)");
                Console.WriteLine("2. Провести емпіричний аналіз (N=100, N^2=10000, N^3=1000000)");
                Console.WriteLine("3. Вийти");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManualInputMode();
                        break;
                    case "2":
                        RunPerformanceAnalysis();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("[Помилка] Невірний вибір.");
                        break;
                }
            }
        }

        // --- Режим ручного введення даних ---
        static void ManualInputMode()
        {
            Console.Write("\nВведіть кількість елементів масиву: ");
            if (!int.TryParse(Console.ReadLine(), out int size) || size <= 0)
            {
                Console.WriteLine("Некоректний розмір.");
                return;
            }

            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Елемент [{i}]: ");
                if (!int.TryParse(Console.ReadLine(), out array[i]))
                {
                    Console.WriteLine("Будь ласка, вводьте лише цілі числа.");
                    i--; 
                }
            }

            Console.WriteLine("\nМасив ДО сортування:");
            PrintArray(array);

            MergeSort(array, 0, array.Length - 1);

            Console.WriteLine("\nМасив ПІСЛЯ сортування (Сортування злиттям):");
            PrintArray(array);
        }

        static void RunPerformanceAnalysis()
        {
            int n1 = 100;
            int n2 = 10000;
            int n3 = 1000000;

            int[] sizes = { n1, n2, n3 };
            int iterations = 5; 

            Console.WriteLine("\n--- Емпіричний аналіз швидкодії (у наносекундах) ---");
            Console.WriteLine($"Алгоритм: Сортування злиттям. Усереднення за {iterations} спроб.\n");

            Console.WriteLine(string.Format("{0,-15} | {1,-20}", "Розмір масиву", "Середній час (нс)"));
            Console.WriteLine(new string('-', 40));

            foreach (int size in sizes)
            {
                double totalNanoseconds = 0;

                for (int i = 0; i < iterations; i++)
                {
                    int[] array = GenerateRandomArray(size);

                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    MergeSort(array, 0, array.Length - 1);

                    sw.Stop();

                    
                    double nanoseconds = (double)sw.ElapsedTicks / Stopwatch.Frequency * 1_000_000_000;
                    totalNanoseconds += nanoseconds;
                }

                double averageTime = totalNanoseconds / iterations;
                Console.WriteLine(string.Format("{0,-15} | {1,-20:F0}", size, averageTime));
            }

            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Скопіюйте ці дані у Microsoft Excel для побудови графіка залежності.");
        }

        static void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                MergeSort(array, left, mid);
                MergeSort(array, mid + 1, right);

                Merge(array, left, mid, right);
            }
        }

        static void Merge(int[] array, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            Array.Copy(array, left, leftArray, 0, n1);
            Array.Copy(array, mid + 1, rightArray, 0, n2);

            int i = 0, j = 0;
            int k = left;

            while (i < n1 && j < n2)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                array[k] = leftArray[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                array[k] = rightArray[j];
                j++;
                k++;
            }
        }

        static int[] GenerateRandomArray(int size)
        {
            Random rnd = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rnd.Next(-10000, 10000);
            }
            return array;
        }
        static void PrintArray(int[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }
    }
}
