using System;

namespace LabWork2_1
{
    public class IntegralCalculator
    {
        public delegate double MathFunction(double x);

        public static double RectangleMethod(MathFunction f, double a, double b, double h)
        {
            double sum = 0;
            int n = (int)Math.Round((b - a) / h);

            for (int i = 0; i < n; i++)
            {
              
                double x = a + i * h + h / 2.0;
                sum += f(x);
            }
            return sum * h;
        }

        public static double TrapezoidMethod(MathFunction f, double a, double b, double h)
        {
            int n = (int)Math.Round((b - a) / h);

            double sum = (f(a) + f(b)) / 2.0;

            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += f(x);
            }
            return sum * h;
        }

        public static double SimpsonMethod(MathFunction f, double a, double b, double h)
        {
            int n = (int)Math.Round((b - a) / h);

            // Метод Сімпсона вимагає ПАРНОЇ кількості відрізків
            if (n % 2 != 0)
            {
                n++;
            }

            double actualH = (b - a) / n;
            double sum = f(a) + f(b);

            for (int i = 1; i < n; i++)
            {
                double x = a + i * actualH;
                if (i % 2 == 0)
                {
                    sum += 2 * f(x); 
                }
                else
                {
                    sum += 4 * f(x); 
                }
            }
            return sum * actualH / 3.0;
        }
    }
}