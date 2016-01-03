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
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace rumba
{
    public partial class Form1 : Form
    {
        private static string path = null;
        private static string localIp = null;
        private static int port = 8050;

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

            textBox1.Text = localIp;
        }

        public void HandleIncome(int port)
        {
            bool done = false;

            UdpClient listener = new UdpClient(port);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);

            try
            {
                while (!done)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }

        public void RefreshHosts(string subnet, out List<string> hosts)
        {
            hosts = new List<string>();

            for (int i = 0; i <= 255; i++)
            {
                string host = subnet + i;

                Ping ping = new Ping();
                PingReply reply = ping.Send(IPAddress.Parse(host), 100);
                if (reply.Status == IPStatus.Success)
                {
                    hosts.Add(host);
                }

            }
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            string subnet = localIp.Remove(10);

         //   Task task = Task.Factory.StartNew(() => RefreshHosts(subnet, out hosts));

           
        }

        #endregion
    }
}
