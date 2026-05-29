using System;
using System.IO;
using System.Text;

namespace LabWork2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота 2.2 (Алгоритми ідентифікації) ===");
            Console.WriteLine("Варіант 19: Слово починається з '0', закінчується '1', має дві частини цифр, розділених '!'");

            string filePath = "words_input.txt";

            Console.WriteLine("\n--- Введення даних ---");
            Console.WriteLine("Вводьте слова по одному в рядок.");
            Console.WriteLine("(Щоб завершити введення та розпочати аналіз, просто натисніть Enter на порожньому рядку):");

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                while (true)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        break;
                    }
                    writer.WriteLine(input.Trim());
                }
            }
            Console.WriteLine($"\n[*] Дані успішно збережено у текстовий файл: {filePath}");


            Console.WriteLine("\n--- Результати ідентифікації ---");
            RegexIdentifier identifier = new RegexIdentifier();
            Console.WriteLine($"Використовується регулярний вираз: {identifier.GetPattern()}\n");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("[Помилка] Файл не знайдено.");
                return;
            }

            int matchCount = 0;


            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                string line;
                int lineNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    if (identifier.IsMatch(line))
                    {
                        Console.WriteLine($"[Рядок {lineNumber}] [+] ЗНАЙДЕНО ЗБІГ: {line}");
                        matchCount++;
                    }
                    else
                    {
                        Console.WriteLine($"[Рядок {lineNumber}] [-] Не підходить:  {line}");
                    }
                    lineNumber++;
                }
            }

            Console.WriteLine($"\n[*] Загалом знайдено слів, що відповідають критерію: {matchCount}");
            Console.WriteLine("\nНатисніть Enter для виходу...");
            Console.ReadLine();
        }
    }
}