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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int[,] toeplitzMatriz;
        private int[] toeplitzVector;

        private int[] result;

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
            }
            
        }

        private void startComputationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void downloadResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(result == null || result.Length == 0)
            {
                throw new Exception("error");
            }
            else
            {

            }
        }
    }
}
