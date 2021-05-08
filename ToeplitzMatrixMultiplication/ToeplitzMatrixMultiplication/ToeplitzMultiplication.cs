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
        public static float[] Compute2(float[,] matrix, float[] vec)
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

        public static float[] Compute3(float[,] matrix, float[] vec)
        {
            int n = vec.Length;

            //calculation of vector representing circulation matrix
            float[] a, x;
            a = new float[2 * n + 2];
            x = new float[2 * n + 2];

            int idx = 0;
            for (int i = 0; i < n; i++)
            {
                a[idx] = matrix[i, 0];
                idx++;
            }
            a[idx] = matrix[0, 0];
            idx++;

            for (int i = n - 1; i > 0; i--)
            {
                a[idx] = matrix[0, i];
                idx++;
            }

            for (int i = 0; i < n; i++)
            {
                x[i] = vec[i];
            }

            float[] res;
            float[] u;
            float[] v = a;
            Fourier.ForwardReal(v, v.Length - 2);
            float[] y = x;
            Fourier.ForwardReal(y, y.Length - 2);
            u = v.Zip(y, (a1, a2) => a1 * a2).ToArray();
            Fourier.InverseReal(u, u.Length - 2);
            res = new float[vec.Length];

            for (int i = 0; i < vec.Length; i++)
                res[i] = u[i];
            return res;
        }

        public static Complex[] Compute(float[,] matrix, float[] vec)
        {
            int n = vec.Length;

            //calculation of vector representing circulation matrix
            Complex[] a, x;
            a = new Complex[2 * n ];
            x = new Complex[2 * n ];

            int idx = 0;
            for (int i = 0; i < n; i++)
            {
                a[idx] = matrix[i, 0];
                idx++;
            }
            a[idx] = matrix[0, 0];
            idx++;

            for (int i = n - 1; i > 0; i--)
            {
                a[idx] = matrix[0, i];
                idx++;
            }

            for (int i = 0; i < n; i++)
            {
                x[i] = vec[i];
            }


            int N = x.Length;

            Complex[] _a = fft(a);
            Complex[] b = fft(x);

            Complex[] c = new Complex[N];
            for (int i = 0; i < N; i++)
            {
                c[i] = _a[i].times(b[i]);
            }

            return ifft(c);
        }

        public static Complex[] fft(Complex[] x)
        {
            int N = x.Length;
            if (N == 1) return new Complex[] { x[0] };
            if (N % 2 != 0) { throw new Exception("N is not a power of 2"); }
            Complex[] even = new Complex[N / 2];
            for (int k = 0; k < N / 2; k++)
            {
                even[k] = x[2 * k];
            }
            Complex[] q = fft(even);

            Complex[] odd = even;
            for (int k = 0; k < N / 2; k++)
            {
                odd[k] = x[2 * k + 1];
            }
            Complex[] r = fft(odd);

            Complex[] y = new Complex[N];
            for (int k = 0; k < N / 2; k++)
            {
                double kth = -2 * k * Math.PI / N;
                Complex wk = new Complex(Math.Cos(kth), Math.Sin(kth));
                y[k] = q[k].plus(wk.times(r[k]));
                y[k + N / 2] = q[k].minus(wk.times(r[k]));
            }
            return y;
        }


        public static Complex[] ifft(Complex[] x)
        {
            int N = x.Length;
            Complex[] y = new Complex[N];


            for (int i = 0; i < N; i++)
            {
                y[i] = x[i].conjugate();
            }

            y = fft(y);
            for (int i = 0; i < N; i++)
            {
                y[i] = (y[i].conjugate()).times(1.0 / N);
            }

            return y;

        }

        public static Complex conjugate(this Complex x) { return new Complex(x.Real, -x.Imaginary); }
        public static Complex times(this Complex x, double alpha)
        {
            return new Complex(alpha * x.Real, alpha * x.Imaginary);
        }

        public static Complex plus(this Complex x, Complex b)
        {
            Complex a = x;
            double real = a.Real + b.Real;
            double imag = a.Imaginary + b.Imaginary;
            return new Complex(real, imag);
        }

        public static Complex minus(this Complex x, Complex b)
        {
            Complex a = x;
            double real = a.Real - b.Real;
            double imag = a.Imaginary - b.Imaginary;
            return new Complex(real, imag);
        }

        public static Complex times(this Complex x, Complex b)
        {
            Complex a = x;
            double real = a.Real * b.Real - a.Imaginary * b.Imaginary;
            double imag = a.Real * b.Imaginary + a.Imaginary * b.Real;
            return new Complex(real, imag);
        }
    }
}
