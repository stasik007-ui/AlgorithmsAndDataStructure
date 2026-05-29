using System;
using System.Text;

namespace LabWork2_1
{
    class Program
    {
        static double TargetFunction(double x)
        {
            return (1 + Math.Sqrt(x)) / (x * x);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота 2.1 (Числове інтегрування) ===");
            Console.WriteLine("Варіант 19. Функція: f(x) = (1 + √x) / x^2\n");

            // Зчитування початкових даних з перевіркою
            double a = ReadDouble("Введіть початок інтервалу a: ");
            double b = ReadDouble("Введіть кінець інтервалу b: ");
            double h = ReadDouble("Введіть крок інтегрування h: ");

            if (a >= b || h <= 0 || h > (b - a))
            {
                Console.WriteLine("\n[Помилка] Некоректні дані: a має бути меншим за b, а крок h має бути додатним і меншим за довжину інтервалу.");
                return;
            }

            Console.WriteLine("\n--- Результати обчислень ---");

            double rectResult = IntegralCalculator.RectangleMethod(TargetFunction, a, b, h);
            Console.WriteLine($"Метод прямокутників: {rectResult:F6}");

            double trapResult = IntegralCalculator.TrapezoidMethod(TargetFunction, a, b, h);
            Console.WriteLine($"Метод трапецій:      {trapResult:F6}");

            double simpResult = IntegralCalculator.SimpsonMethod(TargetFunction, a, b, h);
            Console.WriteLine($"Метод Сімпсона:      {simpResult:F6}");

            Console.WriteLine("\nТочне аналітичне значення: ~1.750000");

            Console.WriteLine("\nНатисніть Enter для виходу...");
            Console.ReadLine();
        }

        static double ReadDouble(string message)
        {
            double result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()?.Replace('.', ','); 

                if (double.TryParse(input, out result))
                {
                    return result;
                }
                Console.WriteLine("[Помилка] Будь ласка, введіть коректне число.");
            }
        }
    }
}