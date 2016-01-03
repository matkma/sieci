using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace rumba
{
    public partial class Form1 : Form
    {
        private static string path = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_start_Click(object sender, EventArgs e)
        {

        }

        private void button_files_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            path = fbd.SelectedPath;
            string[] filePaths = Directory.GetFiles(path);

            foreach (string filePath in filePaths)
            {
                if (!listBox_files.Items.Contains(filePath))
                {
                    listBox_files.Items.Add(filePath);
                }
            }
        }

        private void button_connect_Click(object sender, EventArgs e)
        {

        }

        private void button_download_Click(object sender, EventArgs e)
        {

        }

        private void listBox_users_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox_users_files_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox_files_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
