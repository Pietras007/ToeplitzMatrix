using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToeplitzMatrixMultiplication
{
    public partial class Random_Computation_Test : Form
    {
        Random random;
        public Random_Computation_Test()
        {
            InitializeComponent();
            onLoadForm();
            random = new Random();
        }

        private void onLoadForm()
        {
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label25.Text = "";
            label26.Text = "";
            label27.Text = "";
            label28.Text = "";
            label29.Text = "";
            label30.Text = "";
            label31.Text = "";
            label32.Text = "";
            label33.Text = "";
            label34.Text = "";
            label35.Text = "";
            label36.Text = "";
            label37.Text = "";
            label38.Text = "";
            label39.Text = "";
            label40.Text = "";
            label41.Text = "";
            label42.Text = "";
            label43.Text = "";
            label45.Text = "";
            label46.Text = "";
            label47.Text = "";
            label48.Text = "";
            label49.Text = "";
            label50.Text = "";
            label51.Text = "";
            label52.Text = "";
            label53.Text = "";
            label54.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            onLoadForm();
            int indexer = 0;
            for (int i = 32; i < int.MaxValue; i*=2)
            {
                indexer++;
                if(indexer == 11)
                {
                    break;
                }

                var mtx = ToeplitzGenerator.GenerateToeplitzMatrix(i, random);
                var v = ToeplitzGenerator.GenerateToeplitzVector(i, random);
                DateTime startTimeNlogN = DateTime.UtcNow;
                var res2 = ToeplitzMultiplication.Compute(mtx.Item1, v, true);
                DateTime endTimeNlogN = DateTime.UtcNow;

                DateTime startTimeNlogNLibrary = DateTime.UtcNow;
                var res3 = ToeplitzMultiplication.Compute(mtx.Item1, v, false);
                DateTime endTimeNlogNLibrary = DateTime.UtcNow;

                DateTime startTimeNSquare = DateTime.UtcNow;
                var res = MultiplyMatrixAndVector(mtx.Item1, v);
                DateTime endTimeNSquare = DateTime.UtcNow;

                double nSquareTime = (endTimeNSquare - startTimeNSquare).TotalMilliseconds;
                double nLogNTime = (endTimeNlogN - startTimeNlogN).TotalMilliseconds;
                double nLogNTimeLibrary = (endTimeNlogNLibrary - startTimeNlogNLibrary).TotalMilliseconds;
                switch (indexer)
                {
                    case 1:
                        label14.Text = nSquareTime.ToString();
                        label15.Text = nLogNTime.ToString();
                        label54.Text = nLogNTimeLibrary.ToString();
                        label16.Text = i + "x" + i;
                        break;
                    case 2:
                        label17.Text = nSquareTime.ToString();
                        label18.Text = nLogNTime.ToString();
                        label53.Text = nLogNTimeLibrary.ToString();
                        label19.Text = i + "x" + i;
                        break;
                    case 3:
                        label20.Text = nSquareTime.ToString();
                        label21.Text = nLogNTime.ToString();
                        label52.Text = nLogNTimeLibrary.ToString();
                        label22.Text = i + "x" + i;
                        break;
                    case 4:
                        label23.Text = nSquareTime.ToString();
                        label24.Text = nLogNTime.ToString();
                        label51.Text = nLogNTimeLibrary.ToString();
                        label25.Text = i + "x" + i;
                        break;
                    case 5:
                        label26.Text = nSquareTime.ToString();
                        label27.Text = nLogNTime.ToString();
                        label50.Text = nLogNTimeLibrary.ToString();
                        label28.Text = i + "x" + i;
                        break;
                    case 6:
                        label29.Text = nSquareTime.ToString();
                        label30.Text = nLogNTime.ToString();
                        label49.Text = nLogNTimeLibrary.ToString();
                        label31.Text = i + "x" + i;
                        break;
                    case 7:
                        label32.Text = nSquareTime.ToString();
                        label33.Text = nLogNTime.ToString();
                        label48.Text = nLogNTimeLibrary.ToString();
                        label34.Text = i + "x" + i;
                        break;
                    case 8:
                        label35.Text = nSquareTime.ToString();
                        label36.Text = nLogNTime.ToString();
                        label47.Text = nLogNTimeLibrary.ToString();
                        label37.Text = i + "x" + i;
                        break;
                    case 9:
                        label38.Text = nSquareTime.ToString();
                        label39.Text = nLogNTime.ToString();
                        label46.Text = nLogNTimeLibrary.ToString();
                        label40.Text = i + "x" + i;
                        break;
                    case 10:
                        label41.Text = nSquareTime.ToString();
                        label42.Text = nLogNTime.ToString();
                        label45.Text = nLogNTimeLibrary.ToString();
                        label43.Text = i + "x" + i;
                        break;
                }
                progressBar1.Value = indexer * 10;
            }

            progressBar1.Value = 0;
            button1.Enabled = true;
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
    }
}
