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
using System.Net;

namespace rumba
{
    public partial class Form1 : Form
    {
        private static string path = null;
        private static string localIp = null;

        public Form1()
        {
            InitializeComponent();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                }
            }
        }

        public void HandleIncome(int port)
        {
            UdpListener listener = new UdpListener();
        }

        #region EventHandlers

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (button_start.Text == "Start")
            {
                button_start.Text = "Stop";
                int port = 8050;

                Task.Factory.StartNew(() => HandleIncome(port));

            }
            else
            {
                button_start.Text = "Start";
            }
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

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
