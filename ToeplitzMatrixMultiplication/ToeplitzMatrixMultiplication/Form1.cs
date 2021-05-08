using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToeplitzMatrixMultiplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            random = new Random();
        }

        Random random;
        private float[,] toeplitzMatriz;
        private float[] toeplitzVector;

        private float[] result;

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files|*.txt";
            openFileDialog.Title = "Select a Text File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openFileDialog.FileName);
            }
        }

        private void LoadFile(string path)
        {
            //toeplitzMatriz = new int[,]
            //{
            //    { 7, 11, 5, 6 },
            //    { 3, 7, 11, 5 },
            //    { 8, 3, 7, 11},
            //    { 1, 8, 3, 7 }
            //};

            //toeplitzVector = new int[]
            //{
            //    1,2,3,4
            //};

            /*List<int> read = new List<int>();
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        foreach (var word in line.Split(','))
                        {
                            int numer;
                            Int32.TryParse(word, out numer);
                            read.Add(numer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file could not be read: " + ex.Message);
            }*/

        }

        private float[] MultiplyMatrixAndVector(float[,] mtx, float[] vec)
        {
            int n = vec.Length;
            float[] result = new float[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i] += mtx[i, j] * vec[j];
                }
            }

            return result;
        }

        private float[,] GenerateToeplitzMatrix(int n)
        {
            float[] cMtx = new float[2 * n - 1];
            float[,] result = new float[n, n];
            for (int i = 0; i < 2 * n - 1; i++)
            {
                cMtx[i] = random.Next(0, int.MaxValue / 100);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = cMtx[n - 1 - i + j];
                }
            }

            return result;
        }

        private float[] GenerateToeplitzVector(int n)
        {
            float[] result = new float[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = random.Next(0, int.MaxValue / 100000000);
            }

            return result;
        }

        private void startComputationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = ToeplitzMultiplication.Compute(toeplitzMatriz, toeplitzVector);
        }

        private void downloadResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (result == null || result.Length == 0)
            {
                throw new Exception("error");
            }
            else
            {

            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<int[,]> toeplitzMatrices = new List<int[,]>();
            //List<int[]> vertices = new List<int[]>();
            for(int i=1;i<100;i++)
            {
                var mtx = GenerateToeplitzMatrix(i * 10);
                var v = GenerateToeplitzVector(i * 10);
                var res = MultiplyMatrixAndVector(mtx, v);
                var res2 = ToeplitzMultiplication.Compute(mtx, v);
                for(int j=0;j<res.Length;j++)
                {
                    if(res[j] != res2[j])
                    {
                        int x = 69;
                    }
                    else
                    {
                        int x = 0;
                    }
                }
            }


        }
    }
}
