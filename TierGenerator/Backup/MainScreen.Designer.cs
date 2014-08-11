namespace TierGenerator
{
    partial class MainScreen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpTables = new System.Windows.Forms.TabPage();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.gvTables = new System.Windows.Forms.DataGridView();
            this.IsSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableSchema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbSetting = new System.Windows.Forms.TabPage();
            this.chkBL = new System.Windows.Forms.CheckBox();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.lblOutputDir = new System.Windows.Forms.Label();
            this.txtSpPrefix = new System.Windows.Forms.TextBox();
            this.lblSpPrefix = new System.Windows.Forms.Label();
            this.txtClassPrefix = new System.Windows.Forms.TextBox();
            this.lblClassPrefix = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTables)).BeginInit();
            this.tbSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 69);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TierGenerator.Properties.Resources.title;
            this.pictureBox2.Location = new System.Drawing.Point(0, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(277, 66);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::TierGenerator.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(610, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 63);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnGenerate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 400);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(678, 37);
            this.panel2.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(7, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.BackColor = System.Drawing.Color.White;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Location = new System.Drawing.Point(566, 6);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpTables);
            this.tabControl1.Controls.Add(this.tbSetting);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 331);
            this.tabControl1.TabIndex = 0;
            // 
            // tpTables
            // 
            this.tpTables.BackColor = System.Drawing.Color.White;
            this.tpTables.Controls.Add(this.chkSelectAll);
            this.tpTables.Controls.Add(this.gvTables);
            this.tpTables.Location = new System.Drawing.Point(4, 22);
            this.tpTables.Name = "tpTables";
            this.tpTables.Padding = new System.Windows.Forms.Padding(3);
            this.tpTables.Size = new System.Drawing.Size(670, 305);
            this.tpTables.TabIndex = 0;
            this.tpTables.Text = "Tables";
            this.tpTables.ToolTipText = "tables";
            this.tpTables.UseVisualStyleBackColor = true;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(9, 7);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chkSelectAll.TabIndex = 1;
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // gvTables
            // 
            this.gvTables.AllowUserToAddRows = false;
            this.gvTables.AllowUserToDeleteRows = false;
            this.gvTables.AllowUserToResizeColumns = false;
            this.gvTables.AllowUserToResizeRows = false;
            this.gvTables.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsSelected,
            this.TableName,
            this.TableType,
            this.TableSchema,
            this.ClassName});
            this.gvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvTables.Location = new System.Drawing.Point(3, 3);
            this.gvTables.Name = "gvTables";
            this.gvTables.RowHeadersVisible = false;
            this.gvTables.ShowEditingIcon = false;
            this.gvTables.Size = new System.Drawing.Size(664, 299);
            this.gvTables.TabIndex = 0;
            // 
            // IsSelected
            // 
            this.IsSelected.DataPropertyName = "IsSelected";
            this.IsSelected.HeaderText = "";
            this.IsSelected.Name = "IsSelected";
            this.IsSelected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsSelected.Width = 25;
            // 
            // TableName
            // 
            this.TableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TableName.DataPropertyName = "TableName";
            this.TableName.HeaderText = "Table Name";
            this.TableName.Name = "TableName";
            this.TableName.ReadOnly = true;
            // 
            // TableType
            // 
            this.TableType.DataPropertyName = "TableType";
            this.TableType.HeaderText = "TableType";
            this.TableType.Name = "TableType";
            this.TableType.Visible = false;
            // 
            // TableSchema
            // 
            this.TableSchema.DataPropertyName = "TableSchema";
            this.TableSchema.HeaderText = "TableSchema";
            this.TableSchema.Name = "TableSchema";
            this.TableSchema.Visible = false;
            // 
            // ClassName
            // 
            this.ClassName.DataPropertyName = "ClassName";
            this.ClassName.HeaderText = "ClassName";
            this.ClassName.Name = "ClassName";
            this.ClassName.Visible = false;
            // 
            // tbSetting
            // 
            this.tbSetting.BackColor = System.Drawing.Color.White;
            this.tbSetting.Controls.Add(this.chkBL);
            this.tbSetting.Controls.Add(this.btnOpenDir);
            this.tbSetting.Controls.Add(this.txtOutputDir);
            this.tbSetting.Controls.Add(this.lblOutputDir);
            this.tbSetting.Controls.Add(this.txtSpPrefix);
            this.tbSetting.Controls.Add(this.lblSpPrefix);
            this.tbSetting.Controls.Add(this.txtClassPrefix);
            this.tbSetting.Controls.Add(this.lblClassPrefix);
            this.tbSetting.Controls.Add(this.txtProjectName);
            this.tbSetting.Controls.Add(this.lblProjectName);
            this.tbSetting.Location = new System.Drawing.Point(4, 22);
            this.tbSetting.Name = "tbSetting";
            this.tbSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tbSetting.Size = new System.Drawing.Size(670, 305);
            this.tbSetting.TabIndex = 1;
            this.tbSetting.Text = "Setting";
            this.tbSetting.UseVisualStyleBackColor = true;
            // 
            // chkBL
            // 
            this.chkBL.AutoSize = true;
            this.chkBL.Checked = true;
            this.chkBL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBL.Enabled = false;
            this.chkBL.Location = new System.Drawing.Point(39, 138);
            this.chkBL.Name = "chkBL";
            this.chkBL.Size = new System.Drawing.Size(175, 17);
            this.chkBL.TabIndex = 9;
            this.chkBL.Text = "Generate Business && Data layer";
            this.chkBL.UseVisualStyleBackColor = true;
            // 
            // btnOpenDir
            // 
            this.btnOpenDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenDir.Location = new System.Drawing.Point(461, 99);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(20, 20);
            this.btnOpenDir.TabIndex = 8;
            this.btnOpenDir.Text = "...";
            this.btnOpenDir.UseVisualStyleBackColor = true;
            this.btnOpenDir.Click += new System.EventHandler(this.btnOpenDir_Click);
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutputDir.Location = new System.Drawing.Point(162, 99);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(295, 20);
            this.txtOutputDir.TabIndex = 7;
            // 
            // lblOutputDir
            // 
            this.lblOutputDir.BackColor = System.Drawing.Color.Transparent;
            this.lblOutputDir.Location = new System.Drawing.Point(8, 101);
            this.lblOutputDir.Name = "lblOutputDir";
            this.lblOutputDir.Size = new System.Drawing.Size(140, 18);
            this.lblOutputDir.TabIndex = 6;
            this.lblOutputDir.Text = "Output Directory:";
            this.lblOutputDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSpPrefix
            // 
            this.txtSpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpPrefix.Location = new System.Drawing.Point(162, 73);
            this.txtSpPrefix.Name = "txtSpPrefix";
            this.txtSpPrefix.Size = new System.Drawing.Size(319, 20);
            this.txtSpPrefix.TabIndex = 5;
            // 
            // lblSpPrefix
            // 
            this.lblSpPrefix.BackColor = System.Drawing.Color.Transparent;
            this.lblSpPrefix.Location = new System.Drawing.Point(8, 75);
            this.lblSpPrefix.Name = "lblSpPrefix";
            this.lblSpPrefix.Size = new System.Drawing.Size(140, 18);
            this.lblSpPrefix.TabIndex = 4;
            this.lblSpPrefix.Text = "Store Procedure Prefix:";
            this.lblSpPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClassPrefix
            // 
            this.txtClassPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClassPrefix.Location = new System.Drawing.Point(162, 47);
            this.txtClassPrefix.Name = "txtClassPrefix";
            this.txtClassPrefix.Size = new System.Drawing.Size(319, 20);
            this.txtClassPrefix.TabIndex = 3;
            // 
            // lblClassPrefix
            // 
            this.lblClassPrefix.BackColor = System.Drawing.Color.Transparent;
            this.lblClassPrefix.Location = new System.Drawing.Point(8, 49);
            this.lblClassPrefix.Name = "lblClassPrefix";
            this.lblClassPrefix.Size = new System.Drawing.Size(140, 18);
            this.lblClassPrefix.TabIndex = 2;
            this.lblClassPrefix.Text = "Class Prefix:";
            this.lblClassPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProjectName
            // 
            this.txtProjectName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProjectName.Location = new System.Drawing.Point(162, 21);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(319, 20);
            this.txtProjectName.TabIndex = 1;
            // 
            // lblProjectName
            // 
            this.lblProjectName.BackColor = System.Drawing.Color.Transparent;
            this.lblProjectName.Location = new System.Drawing.Point(8, 24);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(140, 18);
            this.lblProjectName.TabIndex = 0;
            this.lblProjectName.Text = "Project Name:";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainScreen
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(678, 437);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainScreen";
            this.Text = "Tier Generator";
            this.Shown += new System.EventHandler(this.MainScreen_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpTables.ResumeLayout(false);
            this.tpTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTables)).EndInit();
            this.tbSetting.ResumeLayout(false);
            this.tbSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpTables;
        private System.Windows.Forms.TabPage tbSetting;
        private System.Windows.Forms.DataGridView gvTables;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.TextBox txtSpPrefix;
        private System.Windows.Forms.Label lblSpPrefix;
        private System.Windows.Forms.TextBox txtClassPrefix;
        private System.Windows.Forms.Label lblClassPrefix;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Label lblOutputDir;
        private System.Windows.Forms.Button btnOpenDir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.CheckBox chkBL;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableSchema;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
    }
}