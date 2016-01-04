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

        private static string msg_check = "check";
        private static string msg_check_back = "check_back";

        private static UdpClient listener = null;

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
            bool done = false;

            listener = new UdpClient(port);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);

            try
            {
                while (!done)
                {
                    Console.WriteLine("dupa");
                    byte[] bytes = listener.Receive(ref groupEP);
                    if (bytes.ToString().Equals(msg_check))
                    {
                        SendContent(groupEP.Address.ToString(), msg_check_back);
                    }
                    if (bytes.ToString().Equals(msg_check_back))
                    {
                        string machineName = null;
                        string host = groupEP.Address.ToString();

                        try
                        {
                            machineName = Dns.GetHostByAddress(host).HostName;
                        }
                        catch (Exception ex)
                        {
                            machineName = "";
                        }
                        this.Invoke((MethodInvoker)(() => listBox_users.Items.Add(host + " " + machineName)));
                        //Console.WriteLine("Connected to the client {0}, {1}", groupEP, Encoding.ASCII.GetString(bytes));
                        
                        //SendContent(host, msg_check);
                        //this.Invoke((MethodInvoker)(() => listBox_users.Items.Add(host + " " + machineName)));
                    }
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

        public void RefreshHosts(string subnet, ref ListBox listbox)
        {
            List<string> hosts = new List<string>();

            for (int i = 0; i <= 10; i++)
            {
                string host = subnet + "." + i;
                Ping ping = new Ping();
                PingReply reply = ping.Send(IPAddress.Parse(host), 20);

                if (reply.Status == IPStatus.Success)
                {
                    string machineName = null;
                    if (host == localIp)
                    {
                        machineName = Dns.GetHostName();
                        this.Invoke((MethodInvoker)(() => listBox_users.Items.Add(host + " (ja) " + machineName)));
                    }
                    else
                    {
                        SendContent(host, msg_check);
                    }
                }
            }
        }

        public void SendContent(string ip, string message)
        {
            UdpClient udp = new UdpClient();
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(ip), port);
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            udp.Send(sendBytes, sendBytes.Length, groupEP);
            udp.Close();
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

                if (listener != null)
                {
                    listener.Close();
                }
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
            //Console.WriteLine(listBox_users.SelectedItem.ToString());
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

        private void button_refresh_Click(object sender, EventArgs e)
        {
            listBox_users.Items.Clear();
            string subnet = localIp.Remove(localIp.LastIndexOf('.'));
            var task = Task.Factory.StartNew(() => RefreshHosts(subnet, ref listBox_users));
        }

        #endregion
    }
}
