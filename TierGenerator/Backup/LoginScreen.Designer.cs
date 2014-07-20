namespace TierGenerator
{
    partial class LoginScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginScreen));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnConnection = new System.Windows.Forms.Button();
            this.gbSQLServerCredential = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.rbtnSqlServerAuthentication = new System.Windows.Forms.RadioButton();
            this.rbtnWindowsAuthentication = new System.Windows.Forms.RadioButton();
            this.txtCatalog = new System.Windows.Forms.TextBox();
            this.txtSqlServer = new System.Windows.Forms.TextBox();
            this.lblCatalog = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbSQLServerCredential.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(529, 33);
            this.pnlHeader.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TierGenerator.Properties.Resources.DbInfo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 28);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.gbSQLServerCredential);
            this.panel2.Controls.Add(this.rbtnSqlServerAuthentication);
            this.panel2.Controls.Add(this.rbtnWindowsAuthentication);
            this.panel2.Controls.Add(this.txtCatalog);
            this.panel2.Controls.Add(this.txtSqlServer);
            this.panel2.Controls.Add(this.lblCatalog);
            this.panel2.Controls.Add(this.lblServer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(529, 169);
            this.panel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnConnection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 32);
            this.panel1.TabIndex = 9;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(12, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // btnConnection
            // 
            this.btnConnection.BackColor = System.Drawing.Color.White;
            this.btnConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnection.Location = new System.Drawing.Point(410, 5);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(100, 23);
            this.btnConnection.TabIndex = 8;
            this.btnConnection.Text = "C&onnect";
            this.btnConnection.UseVisualStyleBackColor = false;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // gbSQLServerCredential
            // 
            this.gbSQLServerCredential.Controls.Add(this.txtPassword);
            this.gbSQLServerCredential.Controls.Add(this.lblPassword);
            this.gbSQLServerCredential.Controls.Add(this.txtLogin);
            this.gbSQLServerCredential.Controls.Add(this.lblLogin);
            this.gbSQLServerCredential.Enabled = false;
            this.gbSQLServerCredential.Location = new System.Drawing.Point(23, 73);
            this.gbSQLServerCredential.Name = "gbSQLServerCredential";
            this.gbSQLServerCredential.Size = new System.Drawing.Size(494, 54);
            this.gbSQLServerCredential.TabIndex = 6;
            this.gbSQLServerCredential.TabStop = false;
            this.gbSQLServerCredential.Text = "SQL Server Credentials";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(315, 19);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(172, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "sa2005";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(256, 22);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "&Password";
            // 
            // txtLogin
            // 
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Location = new System.Drawing.Point(54, 19);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(172, 20);
            this.txtLogin.TabIndex = 1;
            this.txtLogin.Text = "sa";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(15, 21);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(33, 13);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "&Login";
            // 
            // rbtnSqlServerAuthentication
            // 
            this.rbtnSqlServerAuthentication.AutoSize = true;
            this.rbtnSqlServerAuthentication.Location = new System.Drawing.Point(178, 50);
            this.rbtnSqlServerAuthentication.Name = "rbtnSqlServerAuthentication";
            this.rbtnSqlServerAuthentication.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbtnSqlServerAuthentication.Size = new System.Drawing.Size(151, 17);
            this.rbtnSqlServerAuthentication.TabIndex = 5;
            this.rbtnSqlServerAuthentication.Text = "&SQL Server Authentication";
            this.rbtnSqlServerAuthentication.UseVisualStyleBackColor = true;
            this.rbtnSqlServerAuthentication.CheckedChanged += new System.EventHandler(this.rbtnSqlServerAuthentication_CheckedChanged);
            // 
            // rbtnWindowsAuthentication
            // 
            this.rbtnWindowsAuthentication.AutoSize = true;
            this.rbtnWindowsAuthentication.Checked = true;
            this.rbtnWindowsAuthentication.Location = new System.Drawing.Point(19, 50);
            this.rbtnWindowsAuthentication.Name = "rbtnWindowsAuthentication";
            this.rbtnWindowsAuthentication.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbtnWindowsAuthentication.Size = new System.Drawing.Size(140, 17);
            this.rbtnWindowsAuthentication.TabIndex = 4;
            this.rbtnWindowsAuthentication.TabStop = true;
            this.rbtnWindowsAuthentication.Text = "&Windows Authentication";
            this.rbtnWindowsAuthentication.UseVisualStyleBackColor = true;
            this.rbtnWindowsAuthentication.CheckedChanged += new System.EventHandler(this.rbtnSqlServerAuthentication_CheckedChanged);
            // 
            // txtCatalog
            // 
            this.txtCatalog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCatalog.Location = new System.Drawing.Point(338, 11);
            this.txtCatalog.Name = "txtCatalog";
            this.txtCatalog.Size = new System.Drawing.Size(172, 20);
            this.txtCatalog.TabIndex = 3;
            this.txtCatalog.Text = "CCMS";
            // 
            // txtSqlServer
            // 
            this.txtSqlServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqlServer.Location = new System.Drawing.Point(77, 11);
            this.txtSqlServer.Name = "txtSqlServer";
            this.txtSqlServer.Size = new System.Drawing.Size(172, 20);
            this.txtSqlServer.TabIndex = 1;
            this.txtSqlServer.Text = "LocalHost";
            // 
            // lblCatalog
            // 
            this.lblCatalog.AutoSize = true;
            this.lblCatalog.Location = new System.Drawing.Point(286, 14);
            this.lblCatalog.Name = "lblCatalog";
            this.lblCatalog.Size = new System.Drawing.Size(43, 13);
            this.lblCatalog.TabIndex = 2;
            this.lblCatalog.Text = "&Catalog";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(9, 14);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(62, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "S&QL Server";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 202);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tier Generator";
            this.Load += new System.EventHandler(this.LoginScreen_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbSQLServerCredential.ResumeLayout(false);
            this.gbSQLServerCredential.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnSqlServerAuthentication;
        private System.Windows.Forms.RadioButton rbtnWindowsAuthentication;
        private System.Windows.Forms.TextBox txtCatalog;
        private System.Windows.Forms.TextBox txtSqlServer;
        private System.Windows.Forms.Label lblCatalog;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.GroupBox gbSQLServerCredential;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
    }
}

