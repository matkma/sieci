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
        #region Variables & constructor

        private static string path = null;
        private static string localIp = null;
        private static string connectedIp = null;
        private static int port = 8050;

        private static string msg_check = "check";
        private static string msg_check_back = "check_back";
        private static string msg_file_leader = "@F@";
        private static string msg_file_request_leader = "@R@";
        private static string msg_list_leader = "@L@";
        private static string msg_list_start = "start_listing";
        private static string msg_list_continue = "cont_listing";

        private static UdpClient listener = null;

        public static string data = null;


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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Network

        public void HandleIncome(int port)
        {
            bool done = false;

            listener = new UdpClient(port);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);

            try
            {
                int i = 0;

                while (!done)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    string msg_received = System.Text.Encoding.UTF8.GetString(bytes);

                    if (msg_received.Equals(msg_check))
                    {
                        SendContent(groupEP.Address.ToString(), msg_check_back);
                    }
                    else if (msg_received.Equals(msg_check_back))
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
                    }
                    else if (msg_received.StartsWith(msg_list_leader))
                    {
                        string message = msg_received.Substring(msg_list_leader.Length);

                        if (!listBox_users_files.Items.Contains(message))
                        {
                            this.Invoke((MethodInvoker)(() => listBox_users_files.Items.Add(msg_received.Substring(msg_list_leader.Length))));
                        }

                        SendContent(groupEP.Address.ToString(), msg_list_continue);
                    }
                    else if (msg_received.Equals(msg_list_start) || msg_received.Equals(msg_list_continue))
                    {
                        if (msg_received.Equals(msg_list_start))
                        {
                            i = 0;
                            if (listBox_files.Items.Count > 0)
                            {
                                SendContent(groupEP.Address.ToString(), msg_list_leader + listBox_files.Items[i]);
                            }
                        }
                        else
                        {
                            if (i < listBox_files.Items.Count)
                            {
                                SendContent(groupEP.Address.ToString(), msg_list_leader + listBox_files.Items[i]);
                            }
                        }

                        i++;
                    }
                    else if (msg_received.StartsWith(msg_file_request_leader))
                    {
                        string message = msg_received.Substring(msg_file_request_leader.Length);
                        SendFile(groupEP.Address.ToString(), message);
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

        public void SendFile(string ip, string path)
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(groupEP);
                string fileName = "D:\\test.txt";

                // Send file fileName to remote device
                Console.WriteLine("Sending {0} to the host.", fileName);

                //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                //client.Send(msg);
                client.SendFile(fileName);

                client.Shutdown(SocketShutdown.Send);
                client.Close();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

            /*IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(ip), port);
            TcpClient client = new TcpClient(AddressFamily.InterNetwork);
            //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(groupEP);
            if (client.Connected)
            {
                Console.WriteLine("Połączono, wysyłam...");
                client.Client.SendFile(path);
            }
            //client.Client.Shutdown(SocketShutdown.Both);
            client.Close();*/
        }

        public void ReceiveFile(string ip)
        {
            byte[] bytes = new Byte[1024];
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();
                data = null;

                while (true)
                {
                    using (var output = File.Create("D://plik.dat"))
                    {
                        Console.WriteLine("Client connected. Starting to receive the file");

                        // read the file in chunks of 1KB
                        var buffer = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = listener.Receive(buffer)) > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                        }
                    }

                    //http://stackoverflow.com/questions/10182751/server-client-send-receive-simple-text

                    //bytes = new byte[1024];
                    
                    //int bytesRec = handler.Receive(bytes);
                    //data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //if (data.IndexOf("<EOF>") > -1)
                    //{
                    //     break;
                    //}
                }

                Console.WriteLine("Text received : {0}", data);

                //byte[] msg = Encoding.ASCII.GetBytes(data);
                //handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

                listener.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion

        #region EventHandlers

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
            if (listBox_users.SelectedIndex != -1)
            {
                string ip = listBox_users.SelectedItem.ToString();

                if (ip != null)
                {
                    int endPoint = ip.IndexOf(" ");
                    ip = ip.Substring(0, endPoint).Trim();
                    connectedIp = ip;

                    SendContent(ip, msg_list_start);
                }
            }
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            if (listBox_users_files.SelectedIndex != -1)
            {
                string path = listBox_users_files.SelectedItem.ToString();
                
                if (path != null && connectedIp != null)
                {
                    SendContent(connectedIp, msg_file_request_leader + path);
                    Task.Factory.StartNew(() => ReceiveFile(connectedIp));
                }
            }
        }

        private void listBox_users_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_users_files.Items.Count > 0)
            {
                listBox_users_files.Items.Clear();
            }

            connectedIp = null;
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

        private void button_clear_listBox_files_Click(object sender, EventArgs e)
        {
            listBox_files.Items.Clear();
        }

        #endregion

    }
}
