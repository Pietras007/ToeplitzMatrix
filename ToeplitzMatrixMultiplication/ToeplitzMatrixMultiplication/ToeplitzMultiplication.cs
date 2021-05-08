using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.IntegralTransforms;


namespace ToeplitzMatrixMultiplication
{
    public static class ToeplitzMultiplication
    {
        public static float[] Compute(float[,] matrix, float[] vec)
        {
            int n = vec.Length;

            int pow = 1;
            while (pow < 2 * n + 2)
                pow *= 2;

            //calculation of vector representing circulation matrix
            float[] a, x;
            a = new float[pow];
            x = new float[pow];


            for (int i = 0; i < n; i++)
            {
                a[i] = matrix[0, i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                a[n + i] = matrix[i, 0];
            }

            for (int i = 0; i < n; i++)
            {
                x[i] = vec[i];
            }
            float[] res;
            float[] u;
            float[] v = a;
            Fourier.ForwardReal(v, v.Length - 2, FourierOptions.NoScaling);
            float[] y = x;
            Fourier.ForwardReal(y, y.Length - 2, FourierOptions.NoScaling);
            u = v.Zip(y, (a1, a2) => a1 * a2).ToArray();
            Fourier.InverseReal(u, u.Length - 2, FourierOptions.NoScaling);
            res = new float[vec.Length];

            for (int i = 0; i < vec.Length; i++)
                res[i] = u[i];
            return res;
        }
    }
}
