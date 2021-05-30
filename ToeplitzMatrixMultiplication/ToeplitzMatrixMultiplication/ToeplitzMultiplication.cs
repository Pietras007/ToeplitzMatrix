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
        public static Complex[] Compute(float[,] matrix, float[] vec)
        {
            //preparing input data
            Complex[] a, x;
            a = new Complex[2 * vec.Length];
            x = new Complex[2 * vec.Length];

            int idx = 0;
            for (int i = 0; i < vec.Length; i++)
            {
                a[idx] = matrix[i, 0];
                idx++;
            }
            a[idx] = matrix[0, 0];
            idx++;

            for (int i = vec.Length - 1; i > 0; i--)
            {
                a[idx] = matrix[0, i];
                idx++;
            }

            for (int i = 0; i < vec.Length; i++)
                x[i] = vec[i];

            // calculation starts
            Complex[] _a = FFT(a);
            Complex[] _x = FFT(x);

            Complex[] res = new Complex[x.Length];
            for (int i = 0; i < x.Length; i++)
                res[i] = _a[i].Times(_x[i]);

            return InverseFFT(res);
        }

        public static Complex[] FFT(Complex[] x)
        {
            // validation
            if (x.Length == 1)
                return new Complex[] { x[0] };
            if (x.Length % 2 != 0)
                throw new Exception("Length of the input vector has to be a power of 2");

            Complex[] even = new Complex[x.Length / 2];

            for (int k = 0; k < x.Length / 2; k++)
                even[k] = x[2 * k];

            Complex[] q = FFT(even);
            Complex[] odd = even;

            for (int k = 0; k < x.Length / 2; k++)
                odd[k] = x[2 * k + 1];
            Complex[] r = FFT(odd);

            Complex[] y = new Complex[x.Length];
            for (int k = 0; k < y.Length / 2; k++)
            {
                double angle = -2 * k * Math.PI / y.Length;
                Complex wk = new Complex(Math.Cos(angle), Math.Sin(angle));
                y[k] = q[k] + wk.Times(r[k]);
                y[k + y.Length / 2] = q[k] - wk.Times(r[k]);
            }
            return y;
        }


        public static Complex[] InverseFFT(Complex[] x)
        {
            Complex[] y = new Complex[x.Length];

            for (int i = 0; i < x.Length; i++)
                y[i] = x[i].Conjugate();

            y = FFT(y);

            for (int i = 0; i < y.Length; i++)
                y[i] = (y[i].Conjugate()).Times(1.0 / y.Length);

            return y;
        }

        public static Complex Conjugate(this Complex x) 
        { 
            return new Complex(x.Real, -x.Imaginary);
        }
        public static Complex Times(this Complex x, double alpha)
        {
            return new Complex(alpha * x.Real, alpha * x.Imaginary);
        }

        public static Complex Times(this Complex a, Complex b)
        {
            double real = a.Real * b.Real - a.Imaginary * b.Imaginary;
            double imag = a.Real * b.Imaginary + a.Imaginary * b.Real;
            return new Complex(real, imag);
        }
    }
}
