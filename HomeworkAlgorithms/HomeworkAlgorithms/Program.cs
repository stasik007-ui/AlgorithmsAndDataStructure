using System;
using System.Text;

namespace HomeworkLUP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Домашня робота (Перший рівень) ===");
            Console.WriteLine("Метод LUP-розкладання СЛАР. Варіант 19\n");

            int n = ReadInt("Введіть розмірність матриці n (для 19 варіанта це 3): ");

            // Ініціалізація масивів
            double[][] A = new double[n][];
            for (int i = 0; i < n; i++)
            {
                A[i] = new double[n];
            }

            double[] b = new double[n];

            Console.WriteLine("\nВведіть коефіцієнти матриці A:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i][j] = ReadDouble($"A[{i + 1}][{j + 1}]: ");
                }
            }

            Console.WriteLine("\nВведіть вектор вільних членів b:");
            for (int i = 0; i < n; i++)
            {
                b[i] = ReadDouble($"b[{i + 1}]: ");
            }

            // Виведення заданої системи
            Console.WriteLine("\n--- Задана система лінійних алгебраїчних рівнянь ---");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{A[i][j],8:F2} * x{j + 1} ");
                    if (j < n - 1) Console.Write("+ ");
                }
                Console.WriteLine($"= {b[i],8:F2}");
            }

            // Виконання LUP-розкладання
            LUPDecomposition lup = new LUPDecomposition(A);

            // Виведення результатів розкладання
            Console.WriteLine("\n--- Матриця P (Перестановок) ---");
            PrintMatrix(lup.GetP());

            Console.WriteLine("\n--- Матриця L (Нижня трикутна) ---");
            PrintMatrix(lup.GetL());

            Console.WriteLine("\n--- Матриця U (Верхня трикутна) ---");
            PrintMatrix(lup.GetU());

            // Знаходження та виведення розв'язку
            double[] x = lup.Solve(b);
            Console.WriteLine("\n--- Вектор розв'язку x ---");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine($"x[{i + 1}] = {x[i]:F4}");
            }

            Console.WriteLine("\nНатисніть Enter для виходу...");
            Console.ReadLine();
        }

        // Допоміжний метод для безпечного зчитування цілих чисел
        static int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result > 0)
                {
                    return result;
                }
                Console.WriteLine("[Помилка] Введіть коректне ціле додатне число.");
            }
        }

        // Допоміжний метод для безпечного зчитування чисел з плаваючою комою
        static double ReadDouble(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Replace('.', ',');
                if (double.TryParse(input, out result))
                {
                    return result;
                }
                Console.WriteLine("[Помилка] Введіть коректне число.");
            }
        }

        // Допоміжний метод для форматованого виведення матриць
        static void PrintMatrix(double[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write($"{matrix[i][j],8:F3} ");
                }
                Console.WriteLine();
            }
        }
    }
}