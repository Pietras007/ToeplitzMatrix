using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToeplitzMatrixMultiplication
{
    public partial class GenerateFile : Form
    {
        Random random;
        public GenerateFile()
        {
            InitializeComponent();
            random = new Random();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = (int)numericUpDown1.Value;
            (float[,], float[]) mtx = ToeplitzGenerator.GenerateToeplitzMatrix(size, random);
            float[] vec = ToeplitzGenerator.GenerateToeplitzVector(size, random);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    stringBuilder.Append(mtx.Item1[i, j].ToString());
                    if (j != size - 1)
                    {
                        stringBuilder.Append(", ");
                    }
                    else
                    {
                        stringBuilder.Append('\n');
                    }
                }
            }

            stringBuilder.Append('\n');
            for (int i = 0; i < size; i++)
            {
                stringBuilder.Append(vec[i]);
                if (i != size - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            DateTime dateTime = DateTime.Now;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files|*.txt";
            saveFileDialog.FileName = "Toeplitz example";// + dateTime.ToString();
            string path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            path = System.IO.Path.GetDirectoryName(path);
            saveFileDialog.InitialDirectory = path;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        sw.WriteLine(stringBuilder);
                    }


                    MessageBox.Show("Successfully saved");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected problem accured: " + ex.Message);
                }
            }
        }
    }
}
