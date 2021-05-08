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
        public static Complex[] Compute(Complex[,] matrix, Complex[] vec)
        {
            int n = vec.Length;

            //calculation of vector representing circulation matrix
            Complex[] a = new Complex[2 * n];
            for (int i = 0; i < n; i++)
            {
                a[i] = matrix[0, i];
            }
            for (int i = n-1; i >= 0; i--)
            {
                a[n + i] = matrix[i, 0];
            }

            Complex[] x = new Complex[2 * n];
            for (int i = 0; i < n; i++)
            {
                x[i] = vec[i];
            }

            Complex[] v = a;
            Fourier.Forward(v);
            Complex[] y = x;
            Fourier.Forward(y);
            Complex[] u = v.Zip(y, (a1, a2) => a1 * a2).ToArray();
            Fourier.Inverse(u);
            Complex[] res = new Complex[vec.Length];

            for (int i = 0; i < vec.Length; i++)
                res[i] = u[i];
            return u;
        }
    }
}
