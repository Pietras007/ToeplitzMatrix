using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ToeplitzMatrixMultiplication
{
    public static class ToeplitzMultiplication
    {
        public static Complex[] Compute(float[,] matrix, float[] vec, bool onlyPowerOf2 = false)
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
            Complex[] res = new Complex[x.Length];

            if(!onlyPowerOf2)
            {
                //var fft = new DoubleComplexForward1DFFT(a.Length);
                //var ra = new DoubleComplexVector(a.Length);
                //for (int i = 0; i < a.Length; i++)
                //    ra[i] = new DoubleComplex(a[i].Real, a[i].Imaginary);
                //var resa = fft.FFT(ra);
                //var rx = new DoubleComplexVector(x.Length);
                //for (int i = 0; i < x.Length; i++)
                //    rx[i] = new DoubleComplex(x[i].Real, x[i].Imaginary);
                //var resx = fft.FFT(rx);
                //for (int i = 0; i < x.Length; i++)
                //    res[i] = resa[i].Times(resx[i]);
            }
            else
            {
                Complex[] _a = FFT(a);
                Complex[] _x = FFT(x);
                for (int i = 0; i < x.Length; i++)
                    res[i] = _a[i].Times(_x[i]);
            }

            return InverseFFT(res, onlyPowerOf2);
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


        public static Complex[] InverseFFT(Complex[] x, bool onlyPowerOf2 = false)
        {
            Complex[] y = new Complex[x.Length];

            for (int i = 0; i < x.Length; i++)
                y[i] = x[i].Conjugate();

            if (!onlyPowerOf2)
            {
                //var fft = new DoubleComplexForward1DFFT(y.Length);
                //var ry = new DoubleComplexVector(y.Length);
                //for (int i = 0; i < y.Length; i++)
                //    ry[i] = new DoubleComplex(y[i].Real, y[i].Imaginary);
                //var resy = fft.FFT(ry);

                //for (int i = 0; i < y.Length; i++)
                //    y[i] = new Complex(resy[i].Real, resy[i].Imag);
            }
            else
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
        //public static Complex Times(this DoubleComplex a, DoubleComplex b)
        //{
        //    double real = a.Real * b.Real - a.Imag * b.Imag;
        //    double imag = a.Real * b.Imag + a.Imag * b.Real;
        //    return new Complex(real, imag);
        //}

    }
}
