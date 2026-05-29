using System;
using System.Numerics;

namespace LabWork_Combinatorics
{
    public class Combinatorics
    {
        public static BigInteger Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("Факторіал від'ємного числа не існує.");

            BigInteger result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        public static BigInteger Arrangement(int n, int k)
        {
            if (k < 0 || k > n) return 0;
            return Factorial(n) / Factorial(n - k);
        }

        public static BigInteger Combination(int n, int k)
        {
            if (k < 0 || k > n) return 0;
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }
    }
}