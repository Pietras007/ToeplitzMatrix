using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeplitzMatrixMultiplication
{
    public static class ToeplitzGenerator
    {
        public static (float[,], float[]) GenerateToeplitzMatrix(int n, Random random)
        {
            float[] cMtx = new float[2 * n - 1];
            float[,] result = new float[n, n];
            for (int i = 0; i < 2 * n - 1; i++)
            {
                cMtx[i] = random.Next(1, 30);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = cMtx[n - 1 - i + j];
                }
            }

            return (result, cMtx);
        }

        public static float[] GenerateToeplitzVector(int n, Random random)
        {
            float[] result = new float[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = random.Next(1, 30);
            }

            return result;
        }
    }
}
