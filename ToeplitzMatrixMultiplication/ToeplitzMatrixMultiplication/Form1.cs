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

        private Complex[] result;

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
            List<int> read = new List<int>();
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    string line;
                    bool start = true;
                    int idx = 0;
                    int len = -1;
                    while ((line = stream.ReadLine()) != null)
                    {
                        if(start)
                        {
                            start = false;
                            len = line.Split(',').Length;
                            toeplitzMatriz = new float[len, len];
                            toeplitzVector = new float[len];
                        }

                        if (line.Contains(','))
                        {
                            var numbers = line.Split(',');
                            int i = 0;

                            if (idx == len + 1)
                            {
                                foreach (var n in numbers)
                                {
                                    toeplitzVector[i] = float.Parse(numbers[i]);
                                    i++;
                                }
                            }
                            else
                            {
                                foreach (var n in numbers)
                                {
                                    toeplitzMatriz[idx, i] = float.Parse(numbers[i]);
                                    i++;
                                }
                            }
                        }

                        idx++;
                    }
                }

                MessageBox.Show("Successfully loaded");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file could not be read: " + ex.Message);
            }
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

        private (float[,], float[]) GenerateToeplitzMatrix(int n)
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

        private float[] GenerateToeplitzVector(int n)
        {
            float[] result = new float[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = random.Next(1, 30);
            }

            return result;
        }

        private void startComputationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = ToeplitzMultiplication.Compute(toeplitzMatriz, toeplitzVector);
            MessageBox.Show("Successfully computed");
        }

        private void downloadResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files|*.txt";
            saveFileDialog.Title = "Select a Text File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFile(saveFileDialog.FileName);
                MessageBox.Show("Successfully saved");
            }
        }

        public void SaveFile(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {

                    StringBuilder sb = new StringBuilder(8);
                    for (int i = 0; i < result.Length/2; i++)
                    {
                        sb.Append(result[i].Real);
                        if (i != result.Length/2 - 1)
                        {
                            sb.Append(", ");
                        }
                    }
                    sw.WriteLine(sb);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected problem accured: " + ex.Message);
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<int[,]> toeplitzMatrices = new List<int[,]>();
            //List<int[]> vertices = new List<int[]>();
            for (int i = 4; i < 20000; i *= 2)
            {
                var mtx = GenerateToeplitzMatrix(i);
                var v = GenerateToeplitzVector(i);
                var res = MultiplyMatrixAndVector(mtx.Item1, v);
                var res2 = ToeplitzMultiplication.Compute(mtx.Item1, v);
                //for (int j = 0; j < res.Length; j++)
                //{
                //    if (res[j] != res2[j].Magnitude)
                //    {
                //        int x = 69;
                //    }
                //    else
                //    {
                //        int x = 0;
                //    }
                //}
            }

            MessageBox.Show("Tested successfully");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random_Computation_Test test = new Random_Computation_Test();
            test.Show();
        }
    }
}
