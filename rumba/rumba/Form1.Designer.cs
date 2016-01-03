namespace rumba
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_start = new System.Windows.Forms.Button();
            this.button_files = new System.Windows.Forms.Button();
            this.listBox_users = new System.Windows.Forms.ListBox();
            this.listBox_users_files = new System.Windows.Forms.ListBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.listBox_files = new System.Windows.Forms.ListBox();
            this.button_download = new System.Windows.Forms.Button();
            this.button_refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(12, 12);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_files
            // 
            this.button_files.Location = new System.Drawing.Point(12, 70);
            this.button_files.Name = "button_files";
            this.button_files.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button_files.Size = new System.Drawing.Size(104, 23);
            this.button_files.TabIndex = 1;
            this.button_files.Text = "Wybierz katalog";
            this.button_files.UseVisualStyleBackColor = true;
            this.button_files.Click += new System.EventHandler(this.button_files_Click);
            // 
            // listBox_users
            // 
            this.listBox_users.FormattingEnabled = true;
            this.listBox_users.Location = new System.Drawing.Point(12, 133);
            this.listBox_users.Name = "listBox_users";
            this.listBox_users.Size = new System.Drawing.Size(104, 147);
            this.listBox_users.TabIndex = 3;
            this.listBox_users.SelectedIndexChanged += new System.EventHandler(this.listBox_users_SelectedIndexChanged);
            // 
            // listBox_users_files
            // 
            this.listBox_users_files.FormattingEnabled = true;
            this.listBox_users_files.Location = new System.Drawing.Point(122, 133);
            this.listBox_users_files.Name = "listBox_users_files";
            this.listBox_users_files.Size = new System.Drawing.Size(348, 147);
            this.listBox_users_files.TabIndex = 4;
            this.listBox_users_files.SelectedIndexChanged += new System.EventHandler(this.listBox_users_files_SelectedIndexChanged);
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(12, 104);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(75, 23);
            this.button_connect.TabIndex = 5;
            this.button_connect.Text = "Połącz";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // listBox_files
            // 
            this.listBox_files.FormattingEnabled = true;
            this.listBox_files.Location = new System.Drawing.Point(122, 12);
            this.listBox_files.Name = "listBox_files";
            this.listBox_files.Size = new System.Drawing.Size(348, 82);
            this.listBox_files.TabIndex = 6;
            this.listBox_files.SelectedIndexChanged += new System.EventHandler(this.listBox_files_SelectedIndexChanged);
            // 
            // button_download
            // 
            this.button_download.Location = new System.Drawing.Point(122, 104);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(75, 23);
            this.button_download.TabIndex = 7;
            this.button_download.Text = "Pobierz";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(12, 41);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(75, 23);
            this.button_refresh.TabIndex = 8;
            this.button_refresh.Text = "Odśwież";
            this.button_refresh.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(482, 290);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.listBox_files);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.listBox_users_files);
            this.Controls.Add(this.listBox_users);
            this.Controls.Add(this.button_files);
            this.Controls.Add(this.button_start);
            this.Name = "Form1";
            this.Text = "Rumba";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_files;
        private System.Windows.Forms.ListBox listBox_users;
        private System.Windows.Forms.ListBox listBox_users_files;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.ListBox listBox_files;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.Button button_refresh;

    }
}

