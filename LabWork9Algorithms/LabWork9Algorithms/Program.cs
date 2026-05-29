using System;
using System.Numerics;
using System.Text;

namespace LabWork_Combinatorics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота (Комбінаторні алгоритми) ===");
            Console.WriteLine("Варіант 19: Формування державної екзаменаційної комісії.");
            Console.WriteLine("Склад: голова, заступник, секретар (унікальні) + 2 члени комісії (однакові).\n");

            int n = 0;
            while (n < 5)
            {
                Console.Write("Введіть загальну кількість викладачів (за варіантом це 11, але мінімум 5): ");
                if (!int.TryParse(Console.ReadLine(), out n) || n < 5)
                {
                    Console.WriteLine("[Помилка] Кількість викладачів має бути цілим числом не менше 5.");
                }
            }

            BigInteger managementRoles = Combinatorics.Arrangement(n, 3);

            BigInteger regularMembers = Combinatorics.Combination(n - 3, 2);

            BigInteger totalWays = managementRoles * regularMembers;

            Console.WriteLine("\n--- Результати обчислень ---");
            Console.WriteLine($"1. Варіантів обрати голову, заступника та секретаря (Розміщення A({n}, 3)): {managementRoles}");
            Console.WriteLine($"2. Варіантів обрати 2-х членів комісії з решти {n - 3} осіб (Комбінація C({n - 3}, 2)): {regularMembers}");
            Console.WriteLine($"3. Загальна кількість варіантів складу комісії: {totalWays}");

            Console.WriteLine("\nНатисніть Enter для виходу...");
            Console.ReadLine();
        }
    }
}