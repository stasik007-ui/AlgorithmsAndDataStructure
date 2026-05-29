using System;

namespace LabWork1_1
{
    public class ArrayStack
    {
        private double[] elements; 
        private int top;         
        private int capacity;      

        public ArrayStack(int size)
        {
            capacity = size;
            elements = new double[capacity];
            top = -1;
        }

        public bool IsFull()
        {
            return (top + 1) == capacity;
        }

        public bool IsEmpty()
        {
            return (top + 1) == 0;
        }

        public bool Push(double item)
        {
            if (IsFull())
            {
                return false;
            }

            top++;
            elements[top] = item;
            return true;
        }

        public double Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Помилка: Стек порожній. Видалення неможливе.");
            }

            double itemToRemove = elements[top];
            top--;
            return itemToRemove;
        }

        public void Print()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Стек наразі порожній.");
                return;
            }

            Console.Write("Поточний стек (від вершини до дна): ");
            for (int i = top; i >= 0; i--)
            {
                Console.Write($"{elements[i]}   ");
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота 1.1 ===");
            Console.WriteLine("Варіант 19: Стек (векторне розміщення), тип елементів — double\n");

            int size = 0;
            while (size <= 0)
            {
                Console.Write("Введіть максимальний розмір стека (ціле додатне число): ");
                if (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
                {
                    Console.WriteLine("Некоректне введення. Будь ласка, введіть число більше 0.");
                }
            }

            ArrayStack userStack = new ArrayStack(size);
            Console.WriteLine($"\n[*] Стек успішно створено на {size} елементів.");

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n-------------------------------------------------");
                Console.WriteLine("МЕНЮ УПРАВЛІННЯ СТЕКОМ:");
                Console.WriteLine("1. Додати елемент (Push)");
                Console.WriteLine("2. Видалити елемент (Pop)");
                Console.WriteLine("3. Вивести поточний стан стека");
                Console.WriteLine("4. Перевірити, чи порожній стек (IsEmpty)");
                Console.WriteLine("5. Перевірити, чи заповнений стек (IsFull)");
                Console.WriteLine("6. Вийти з програми");
                Console.Write("Оберіть дію (1-6): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть дійсне число для додавання: ");
                        string inputVal = Console.ReadLine();

                        inputVal = inputVal?.Replace('.', ',');

                        if (double.TryParse(inputVal, out double value))
                        {
                            if (userStack.Push(value))
                            {
                                Console.WriteLine($"[Успіх] Елемент {value} додано до стека.");
                            }
                            else
                            {
                                Console.WriteLine("[Помилка] Не вдалося додати елемент. Стек переповнений!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("[Помилка] Введено некоректне число.");
                        }
                        break;

                    case "2":
                        try
                        {
                            double poppedValue = userStack.Pop();
                            Console.WriteLine($"[Успіх] З вершини стека видалено елемент: {poppedValue}");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"[Виняток] {ex.Message}");
                        }
                        break;

                    case "3":
                        userStack.Print();
                        break;

                    case "4":
                        if (userStack.IsEmpty())
                            Console.WriteLine("Результат: Так, стек порожній.");
                        else
                            Console.WriteLine("Результат: Ні, у стеку є елементи.");
                        break;

                    case "5":
                        if (userStack.IsFull())
                            Console.WriteLine("Результат: Так, стек повністю заповнений.");
                        else
                            Console.WriteLine("Результат: Ні, у стеку ще є вільне місце.");
                        break;

                    case "6":
                        running = false;
                        Console.WriteLine("Програму завершено. На все добре!");
                        break;

                    default:
                        Console.WriteLine("[Помилка] Невірний пункт меню. Спробуйте знову.");
                        break;
                }
            }
        }
    }
}