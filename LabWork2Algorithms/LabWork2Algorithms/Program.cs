using System;

namespace LabWork2
{
    public class VectorElement
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public VectorElement(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetRadius()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public int GetKey()
        {
            double angleOX = Math.Atan2(Y, X) * (180.0 / Math.PI);
            if (angleOX < 0) angleOX += 360;

            double angleOY = 90.0 - angleOX;
            if (angleOY < 0) angleOY += 360;

            return (int)Math.Round(angleOY);
        }

        public override string ToString()
        {
            return $"Вектор({X:F2}; {Y:F2}), довжина={GetRadius():F2}, кут(OY)={GetKey()}°";
        }
    }

    public class HashTable
    {
        private VectorElement[] table;
        public int Size { get; private set; }

        public HashTable(int size)
        {
            Size = size;
            table = new VectorElement[Size];
        }

        private int HashFunction(int key)
        {
            return Math.Abs(key) % Size;
        }

        public bool Insert(VectorElement element)
        {
            int key = element.GetKey();
            int index = HashFunction(key);

            if (table[index] == null)
            {
                table[index] = element;
                return true;
            }
            return false;
        }

        public void Print()
        {
            Console.WriteLine(new string('-', 85));
            Console.WriteLine(string.Format("| {0,-10} | {1,-10} | {2,-55} |", "Позиція", "Ключ", "Елемент (Значення)"));
            Console.WriteLine(new string('-', 85));

            for (int i = 0; i < Size; i++)
            {
                if (table[i] != null)
                {
                    Console.WriteLine(string.Format("| {0,-10} | {1,-10} | {2,-55} |",
                        i, table[i].GetKey(), table[i].ToString()));
                }
                else
                {
                    Console.WriteLine(string.Format("| {0,-10} | {1,-10} | {2,-55} |",
                        i, "---", "Порожньо"));
                }
            }
            Console.WriteLine(new string('-', 85));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота 2 (Рівень 1) ===");
            Console.WriteLine("Варіант 19: Вектор, Ключ - Кут з віссю OY, Метод хешування - Ділення\n");

            int size = 0;
            while (size <= 0)
            {
                Console.Write("Введіть розмір хеш-таблиці: ");
                int.TryParse(Console.ReadLine(), out size);
            }

            HashTable myHashTable = new HashTable(size);
            Random random = new Random();
            int elementsToInsert = size / 2; 
            int insertedCount = 0;

            Console.WriteLine("\n--- Генерація та вставлення елементів ---");
            while (insertedCount < elementsToInsert)
            {
                double x = random.NextDouble() * 20 - 10;
                double y = random.NextDouble() * 20 - 10;

                if (Math.Abs(x) < 0.01 && Math.Abs(y) < 0.01) continue;

                VectorElement newVector = new VectorElement(x, y);

                if (myHashTable.Insert(newVector))
                {
                    Console.WriteLine($"[+] Додано: {newVector}");
                    insertedCount++;
                }
            }

            Console.WriteLine("\n--- Вміст хеш-таблиці ---");
            myHashTable.Print();
            Console.ReadLine();
        }
    }
}