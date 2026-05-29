using System;

namespace HomeworkLUP
{
    public class LUPDecomposition
    {
        private double[][] L;
        private double[][] U;
        private double[][] P;
        private int n;

        public LUPDecomposition(double[][] A)
        {
            n = A.Length;

            // Ініціалізація зубчастих масивів (jagged arrays)
            L = new double[n][];
            U = new double[n][];
            P = new double[n][];
            double[][] tempU = new double[n][];
            int[] pi = new int[n];

            for (int i = 0; i < n; i++)
            {
                L[i] = new double[n];
                U[i] = new double[n];
                P[i] = new double[n];
                tempU[i] = new double[n];
                pi[i] = i;

                for (int j = 0; j < n; j++)
                {
                    tempU[i][j] = A[i][j];
                    P[i][j] = (i == j) ? 1.0 : 0.0;
                }
            }

            // Алгоритм розкладання з частковим вибором головного елемента (Partial Pivoting)
            for (int k = 0; k < n; k++)
            {
                double max = 0;
                int kPrime = k;
                for (int i = k; i < n; i++)
                {
                    if (Math.Abs(tempU[i][k]) > max)
                    {
                        max = Math.Abs(tempU[i][k]);
                        kPrime = i;
                    }
                }

                // Перестановка рядків у масиві перестановок
                int tempPi = pi[k];
                pi[k] = pi[kPrime];
                pi[kPrime] = tempPi;

                // Перестановка рядків у tempU та P
                SwapRows(tempU, k, kPrime);
                SwapRows(P, k, kPrime);

                // Перестановка рядків у L (до поточного стовпця k)
                for (int i = 0; i < k; i++)
                {
                    double temp = L[k][i];
                    L[k][i] = L[kPrime][i];
                    L[kPrime][i] = temp;
                }

                U[k][k] = tempU[k][k];
                for (int i = k + 1; i < n; i++)
                {
                    L[i][k] = tempU[i][k] / U[k][k];
                    U[k][i] = tempU[k][i];
                }

                for (int i = k + 1; i < n; i++)
                {
                    for (int j = k + 1; j < n; j++)
                    {
                        tempU[i][j] -= L[i][k] * U[k][j];
                    }
                }
            }

            // Заповнення діагоналі L одиницями
            for (int i = 0; i < n; i++)
            {
                L[i][i] = 1.0;
            }
        }

        private void SwapRows(double[][] matrix, int row1, int row2)
        {
            double[] temp = matrix[row1];
            matrix[row1] = matrix[row2];
            matrix[row2] = temp;
        }

        // Метод розв'язання системи: Ax = b -> LUx = Pb -> Ly = Pb -> Ux = y
        public double[] Solve(double[] b)
        {
            double[] Pb = new double[n];
            // 1. Множення P на b
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Pb[i] += P[i][j] * b[j];
                }
            }

            // 2. Пряма підстановка: розв'язання Ly = Pb
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                y[i] = Pb[i];
                for (int j = 0; j < i; j++)
                {
                    y[i] -= L[i][j] * y[j];
                }
            }

            // 3. Зворотна підстановка: розв'язання Ux = y
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = y[i];
                for (int j = i + 1; j < n; j++)
                {
                    x[i] -= U[i][j] * x[j];
                }
                x[i] /= U[i][i];
            }
            return x;
        }

        public double[][] GetL() => L;
        public double[][] GetU() => U;
        public double[][] GetP() => P;
    }
}