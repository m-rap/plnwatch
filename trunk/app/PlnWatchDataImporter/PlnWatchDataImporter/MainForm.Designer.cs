namespace PlnWatchDataImporter
{
    partial class MainForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dilGroupBox = new System.Windows.Forms.GroupBox();
            this.dilTableNameLabel = new System.Windows.Forms.Label();
            this.dilBlThLabel = new System.Windows.Forms.Label();
            this.dilKodeAreaLabel = new System.Windows.Forms.Label();
            this.dilTableNameTextBox = new System.Windows.Forms.TextBox();
            this.dilTahunTextBox = new System.Windows.Forms.TextBox();
            this.dilBulanTextBox = new System.Windows.Forms.TextBox();
            this.dilKodeAreaTextBox = new System.Windows.Forms.TextBox();
            this.dilMdbPathLabel = new System.Windows.Forms.Label();
            this.dilMdbPathTextBox = new System.Windows.Forms.TextBox();
            this.sorekGroupBox = new System.Windows.Forms.GroupBox();
            this.sorekTableNameLabel = new System.Windows.Forms.Label();
            this.sorekBlThLabel = new System.Windows.Forms.Label();
            this.sorekKodeAreaLabel = new System.Windows.Forms.Label();
            this.sorekTableNameTextBox = new System.Windows.Forms.TextBox();
            this.sorekTahunTextBox = new System.Windows.Forms.TextBox();
            this.sorekBulanTextBox = new System.Windows.Forms.TextBox();
            this.sorekKodeAreaTextBox = new System.Windows.Forms.TextBox();
            this.sorekMdbPathLabel = new System.Windows.Forms.Label();
            this.sorekMdbPathTextBox = new System.Windows.Forms.TextBox();
            this.ppobGroupBox = new System.Windows.Forms.GroupBox();
            this.ppobTableNameLabel = new System.Windows.Forms.Label();
            this.ppobBlThLabel = new System.Windows.Forms.Label();
            this.ppobKodeAreaLabel = new System.Windows.Forms.Label();
            this.ppobTableNameTextBox = new System.Windows.Forms.TextBox();
            this.ppobTahunTextBox = new System.Windows.Forms.TextBox();
            this.ppobBulanTextBox = new System.Windows.Forms.TextBox();
            this.ppobKodeAreaTextBox = new System.Windows.Forms.TextBox();
            this.ppobMdbPathLabel = new System.Windows.Forms.Label();
            this.ppobMdbPathTextBox = new System.Windows.Forms.TextBox();
            this.importLabel = new System.Windows.Forms.Label();
            this.dilCheckBox = new System.Windows.Forms.CheckBox();
            this.sorekCheckBox = new System.Windows.Forms.CheckBox();
            this.ppobCheckBox = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dilGroupBox.SuspendLayout();
            this.sorekGroupBox.SuspendLayout();
            this.ppobGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Microsoft Access Files|*.mdb";
            // 
            // dilGroupBox
            // 
            this.dilGroupBox.Controls.Add(this.dilTableNameLabel);
            this.dilGroupBox.Controls.Add(this.dilBlThLabel);
            this.dilGroupBox.Controls.Add(this.dilKodeAreaLabel);
            this.dilGroupBox.Controls.Add(this.dilTableNameTextBox);
            this.dilGroupBox.Controls.Add(this.dilTahunTextBox);
            this.dilGroupBox.Controls.Add(this.dilBulanTextBox);
            this.dilGroupBox.Controls.Add(this.dilKodeAreaTextBox);
            this.dilGroupBox.Controls.Add(this.dilMdbPathLabel);
            this.dilGroupBox.Controls.Add(this.dilMdbPathTextBox);
            this.dilGroupBox.Location = new System.Drawing.Point(12, 56);
            this.dilGroupBox.Name = "dilGroupBox";
            this.dilGroupBox.Size = new System.Drawing.Size(304, 129);
            this.dilGroupBox.TabIndex = 0;
            this.dilGroupBox.TabStop = false;
            this.dilGroupBox.Text = "DIL";
            // 
            // dilTableNameLabel
            // 
            this.dilTableNameLabel.AutoSize = true;
            this.dilTableNameLabel.Location = new System.Drawing.Point(6, 100);
            this.dilTableNameLabel.Name = "dilTableNameLabel";
            this.dilTableNameLabel.Size = new System.Drawing.Size(68, 13);
            this.dilTableNameLabel.TabIndex = 8;
            this.dilTableNameLabel.Text = "Nama Tabel:";
            // 
            // dilBlThLabel
            // 
            this.dilBlThLabel.AutoSize = true;
            this.dilBlThLabel.Location = new System.Drawing.Point(6, 74);
            this.dilBlThLabel.Name = "dilBlThLabel";
            this.dilBlThLabel.Size = new System.Drawing.Size(88, 13);
            this.dilBlThLabel.TabIndex = 7;
            this.dilBlThLabel.Text = "BLTH (mm/yyyy):";
            // 
            // dilKodeAreaLabel
            // 
            this.dilKodeAreaLabel.AutoSize = true;
            this.dilKodeAreaLabel.Location = new System.Drawing.Point(6, 48);
            this.dilKodeAreaLabel.Name = "dilKodeAreaLabel";
            this.dilKodeAreaLabel.Size = new System.Drawing.Size(60, 13);
            this.dilKodeAreaLabel.TabIndex = 6;
            this.dilKodeAreaLabel.Text = "Kode Area:";
            // 
            // dilTableNameTextBox
            // 
            this.dilTableNameTextBox.Location = new System.Drawing.Point(100, 97);
            this.dilTableNameTextBox.Name = "dilTableNameTextBox";
            this.dilTableNameTextBox.Size = new System.Drawing.Size(198, 20);
            this.dilTableNameTextBox.TabIndex = 5;
            this.dilTableNameTextBox.Text = "DIL_MAIN";
            // 
            // dilTahunTextBox
            // 
            this.dilTahunTextBox.Location = new System.Drawing.Point(153, 71);
            this.dilTahunTextBox.Name = "dilTahunTextBox";
            this.dilTahunTextBox.Size = new System.Drawing.Size(66, 20);
            this.dilTahunTextBox.TabIndex = 4;
            // 
            // dilBulanTextBox
            // 
            this.dilBulanTextBox.Location = new System.Drawing.Point(100, 71);
            this.dilBulanTextBox.Name = "dilBulanTextBox";
            this.dilBulanTextBox.Size = new System.Drawing.Size(47, 20);
            this.dilBulanTextBox.TabIndex = 3;
            // 
            // dilKodeAreaTextBox
            // 
            this.dilKodeAreaTextBox.Location = new System.Drawing.Point(100, 45);
            this.dilKodeAreaTextBox.Name = "dilKodeAreaTextBox";
            this.dilKodeAreaTextBox.Size = new System.Drawing.Size(198, 20);
            this.dilKodeAreaTextBox.TabIndex = 2;
            // 
            // dilMdbPathLabel
            // 
            this.dilMdbPathLabel.AutoSize = true;
            this.dilMdbPathLabel.Location = new System.Drawing.Point(6, 22);
            this.dilMdbPathLabel.Name = "dilMdbPathLabel";
            this.dilMdbPathLabel.Size = new System.Drawing.Size(56, 13);
            this.dilMdbPathLabel.TabIndex = 1;
            this.dilMdbPathLabel.Text = "File *.mdb:";
            // 
            // dilMdbPathTextBox
            // 
            this.dilMdbPathTextBox.Location = new System.Drawing.Point(100, 19);
            this.dilMdbPathTextBox.Name = "dilMdbPathTextBox";
            this.dilMdbPathTextBox.Size = new System.Drawing.Size(198, 20);
            this.dilMdbPathTextBox.TabIndex = 0;
            this.dilMdbPathTextBox.Click += new System.EventHandler(this.dilMdbPathTextBox_EnterOrClick);
            this.dilMdbPathTextBox.Enter += new System.EventHandler(this.dilMdbPathTextBox_EnterOrClick);
            // 
            // sorekGroupBox
            // 
            this.sorekGroupBox.Controls.Add(this.sorekTableNameLabel);
            this.sorekGroupBox.Controls.Add(this.sorekBlThLabel);
            this.sorekGroupBox.Controls.Add(this.sorekKodeAreaLabel);
            this.sorekGroupBox.Controls.Add(this.sorekTableNameTextBox);
            this.sorekGroupBox.Controls.Add(this.sorekTahunTextBox);
            this.sorekGroupBox.Controls.Add(this.sorekBulanTextBox);
            this.sorekGroupBox.Controls.Add(this.sorekKodeAreaTextBox);
            this.sorekGroupBox.Controls.Add(this.sorekMdbPathLabel);
            this.sorekGroupBox.Controls.Add(this.sorekMdbPathTextBox);
            this.sorekGroupBox.Location = new System.Drawing.Point(12, 191);
            this.sorekGroupBox.Name = "sorekGroupBox";
            this.sorekGroupBox.Size = new System.Drawing.Size(304, 129);
            this.sorekGroupBox.TabIndex = 9;
            this.sorekGroupBox.TabStop = false;
            this.sorekGroupBox.Text = "SOREK";
            // 
            // sorekTableNameLabel
            // 
            this.sorekTableNameLabel.AutoSize = true;
            this.sorekTableNameLabel.Location = new System.Drawing.Point(6, 100);
            this.sorekTableNameLabel.Name = "sorekTableNameLabel";
            this.sorekTableNameLabel.Size = new System.Drawing.Size(68, 13);
            this.sorekTableNameLabel.TabIndex = 8;
            this.sorekTableNameLabel.Text = "Nama Tabel:";
            // 
            // sorekBlThLabel
            // 
            this.sorekBlThLabel.AutoSize = true;
            this.sorekBlThLabel.Location = new System.Drawing.Point(6, 74);
            this.sorekBlThLabel.Name = "sorekBlThLabel";
            this.sorekBlThLabel.Size = new System.Drawing.Size(88, 13);
            this.sorekBlThLabel.TabIndex = 7;
            this.sorekBlThLabel.Text = "BLTH (mm/yyyy):";
            // 
            // sorekKodeAreaLabel
            // 
            this.sorekKodeAreaLabel.AutoSize = true;
            this.sorekKodeAreaLabel.Location = new System.Drawing.Point(6, 48);
            this.sorekKodeAreaLabel.Name = "sorekKodeAreaLabel";
            this.sorekKodeAreaLabel.Size = new System.Drawing.Size(60, 13);
            this.sorekKodeAreaLabel.TabIndex = 6;
            this.sorekKodeAreaLabel.Text = "Kode Area:";
            // 
            // sorekTableNameTextBox
            // 
            this.sorekTableNameTextBox.Location = new System.Drawing.Point(100, 97);
            this.sorekTableNameTextBox.Name = "sorekTableNameTextBox";
            this.sorekTableNameTextBox.Size = new System.Drawing.Size(198, 20);
            this.sorekTableNameTextBox.TabIndex = 5;
            // 
            // sorekTahunTextBox
            // 
            this.sorekTahunTextBox.Location = new System.Drawing.Point(153, 71);
            this.sorekTahunTextBox.Name = "sorekTahunTextBox";
            this.sorekTahunTextBox.Size = new System.Drawing.Size(66, 20);
            this.sorekTahunTextBox.TabIndex = 4;
            // 
            // sorekBulanTextBox
            // 
            this.sorekBulanTextBox.Location = new System.Drawing.Point(100, 71);
            this.sorekBulanTextBox.Name = "sorekBulanTextBox";
            this.sorekBulanTextBox.Size = new System.Drawing.Size(47, 20);
            this.sorekBulanTextBox.TabIndex = 3;
            // 
            // sorekKodeAreaTextBox
            // 
            this.sorekKodeAreaTextBox.Location = new System.Drawing.Point(100, 45);
            this.sorekKodeAreaTextBox.Name = "sorekKodeAreaTextBox";
            this.sorekKodeAreaTextBox.Size = new System.Drawing.Size(198, 20);
            this.sorekKodeAreaTextBox.TabIndex = 2;
            // 
            // sorekMdbPathLabel
            // 
            this.sorekMdbPathLabel.AutoSize = true;
            this.sorekMdbPathLabel.Location = new System.Drawing.Point(6, 22);
            this.sorekMdbPathLabel.Name = "sorekMdbPathLabel";
            this.sorekMdbPathLabel.Size = new System.Drawing.Size(56, 13);
            this.sorekMdbPathLabel.TabIndex = 1;
            this.sorekMdbPathLabel.Text = "File *.mdb:";
            // 
            // sorekMdbPathTextBox
            // 
            this.sorekMdbPathTextBox.Location = new System.Drawing.Point(100, 19);
            this.sorekMdbPathTextBox.Name = "sorekMdbPathTextBox";
            this.sorekMdbPathTextBox.Size = new System.Drawing.Size(198, 20);
            this.sorekMdbPathTextBox.TabIndex = 0;
            this.sorekMdbPathTextBox.Click += new System.EventHandler(this.sorekMdbPathTextBox_EnterOrClick);
            this.sorekMdbPathTextBox.Enter += new System.EventHandler(this.sorekMdbPathTextBox_EnterOrClick);
            // 
            // ppobGroupBox
            // 
            this.ppobGroupBox.Controls.Add(this.ppobTableNameLabel);
            this.ppobGroupBox.Controls.Add(this.ppobBlThLabel);
            this.ppobGroupBox.Controls.Add(this.ppobKodeAreaLabel);
            this.ppobGroupBox.Controls.Add(this.ppobTableNameTextBox);
            this.ppobGroupBox.Controls.Add(this.ppobTahunTextBox);
            this.ppobGroupBox.Controls.Add(this.ppobBulanTextBox);
            this.ppobGroupBox.Controls.Add(this.ppobKodeAreaTextBox);
            this.ppobGroupBox.Controls.Add(this.ppobMdbPathLabel);
            this.ppobGroupBox.Controls.Add(this.ppobMdbPathTextBox);
            this.ppobGroupBox.Location = new System.Drawing.Point(12, 326);
            this.ppobGroupBox.Name = "ppobGroupBox";
            this.ppobGroupBox.Size = new System.Drawing.Size(304, 129);
            this.ppobGroupBox.TabIndex = 10;
            this.ppobGroupBox.TabStop = false;
            this.ppobGroupBox.Text = "PPOB";
            // 
            // ppobTableNameLabel
            // 
            this.ppobTableNameLabel.AutoSize = true;
            this.ppobTableNameLabel.Location = new System.Drawing.Point(6, 100);
            this.ppobTableNameLabel.Name = "ppobTableNameLabel";
            this.ppobTableNameLabel.Size = new System.Drawing.Size(68, 13);
            this.ppobTableNameLabel.TabIndex = 8;
            this.ppobTableNameLabel.Text = "Nama Tabel:";
            // 
            // ppobBlThLabel
            // 
            this.ppobBlThLabel.AutoSize = true;
            this.ppobBlThLabel.Location = new System.Drawing.Point(6, 74);
            this.ppobBlThLabel.Name = "ppobBlThLabel";
            this.ppobBlThLabel.Size = new System.Drawing.Size(88, 13);
            this.ppobBlThLabel.TabIndex = 7;
            this.ppobBlThLabel.Text = "BLTH (mm/yyyy):";
            // 
            // ppobKodeAreaLabel
            // 
            this.ppobKodeAreaLabel.AutoSize = true;
            this.ppobKodeAreaLabel.Location = new System.Drawing.Point(6, 48);
            this.ppobKodeAreaLabel.Name = "ppobKodeAreaLabel";
            this.ppobKodeAreaLabel.Size = new System.Drawing.Size(60, 13);
            this.ppobKodeAreaLabel.TabIndex = 6;
            this.ppobKodeAreaLabel.Text = "Kode Area:";
            // 
            // ppobTableNameTextBox
            // 
            this.ppobTableNameTextBox.Location = new System.Drawing.Point(100, 97);
            this.ppobTableNameTextBox.Name = "ppobTableNameTextBox";
            this.ppobTableNameTextBox.Size = new System.Drawing.Size(198, 20);
            this.ppobTableNameTextBox.TabIndex = 5;
            // 
            // ppobTahunTextBox
            // 
            this.ppobTahunTextBox.Location = new System.Drawing.Point(153, 71);
            this.ppobTahunTextBox.Name = "ppobTahunTextBox";
            this.ppobTahunTextBox.Size = new System.Drawing.Size(66, 20);
            this.ppobTahunTextBox.TabIndex = 4;
            // 
            // ppobBulanTextBox
            // 
            this.ppobBulanTextBox.Location = new System.Drawing.Point(100, 71);
            this.ppobBulanTextBox.Name = "ppobBulanTextBox";
            this.ppobBulanTextBox.Size = new System.Drawing.Size(47, 20);
            this.ppobBulanTextBox.TabIndex = 3;
            // 
            // ppobKodeAreaTextBox
            // 
            this.ppobKodeAreaTextBox.Location = new System.Drawing.Point(100, 45);
            this.ppobKodeAreaTextBox.Name = "ppobKodeAreaTextBox";
            this.ppobKodeAreaTextBox.Size = new System.Drawing.Size(198, 20);
            this.ppobKodeAreaTextBox.TabIndex = 2;
            // 
            // ppobMdbPathLabel
            // 
            this.ppobMdbPathLabel.AutoSize = true;
            this.ppobMdbPathLabel.Location = new System.Drawing.Point(6, 22);
            this.ppobMdbPathLabel.Name = "ppobMdbPathLabel";
            this.ppobMdbPathLabel.Size = new System.Drawing.Size(56, 13);
            this.ppobMdbPathLabel.TabIndex = 1;
            this.ppobMdbPathLabel.Text = "File *.mdb:";
            // 
            // ppobMdbPathTextBox
            // 
            this.ppobMdbPathTextBox.Location = new System.Drawing.Point(100, 19);
            this.ppobMdbPathTextBox.Name = "ppobMdbPathTextBox";
            this.ppobMdbPathTextBox.Size = new System.Drawing.Size(198, 20);
            this.ppobMdbPathTextBox.TabIndex = 0;
            this.ppobMdbPathTextBox.Click += new System.EventHandler(this.ppobMdbPathTextBox_EnterOrClick);
            this.ppobMdbPathTextBox.Enter += new System.EventHandler(this.ppobMdbPathTextBox_EnterOrClick);
            // 
            // importLabel
            // 
            this.importLabel.AutoSize = true;
            this.importLabel.Location = new System.Drawing.Point(17, 32);
            this.importLabel.Name = "importLabel";
            this.importLabel.Size = new System.Drawing.Size(39, 13);
            this.importLabel.TabIndex = 11;
            this.importLabel.Text = "Import:";
            // 
            // dilCheckBox
            // 
            this.dilCheckBox.AutoSize = true;
            this.dilCheckBox.Location = new System.Drawing.Point(62, 31);
            this.dilCheckBox.Name = "dilCheckBox";
            this.dilCheckBox.Size = new System.Drawing.Size(43, 17);
            this.dilCheckBox.TabIndex = 12;
            this.dilCheckBox.Text = "DIL";
            this.dilCheckBox.UseVisualStyleBackColor = true;
            this.dilCheckBox.CheckedChanged += new System.EventHandler(this.dilCheckBox_CheckedChanged);
            // 
            // sorekCheckBox
            // 
            this.sorekCheckBox.AutoSize = true;
            this.sorekCheckBox.Location = new System.Drawing.Point(111, 31);
            this.sorekCheckBox.Name = "sorekCheckBox";
            this.sorekCheckBox.Size = new System.Drawing.Size(63, 17);
            this.sorekCheckBox.TabIndex = 13;
            this.sorekCheckBox.Text = "SOREK";
            this.sorekCheckBox.UseVisualStyleBackColor = true;
            this.sorekCheckBox.CheckedChanged += new System.EventHandler(this.sorekCheckBox_CheckedChanged);
            // 
            // ppobCheckBox
            // 
            this.ppobCheckBox.AutoSize = true;
            this.ppobCheckBox.Location = new System.Drawing.Point(180, 31);
            this.ppobCheckBox.Name = "ppobCheckBox";
            this.ppobCheckBox.Size = new System.Drawing.Size(55, 17);
            this.ppobCheckBox.TabIndex = 14;
            this.ppobCheckBox.Text = "PPOB";
            this.ppobCheckBox.UseVisualStyleBackColor = true;
            this.ppobCheckBox.CheckedChanged += new System.EventHandler(this.ppobCheckBox_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(241, 27);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 15;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(328, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Keluar";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.toolsToolStripMenuItem.Text = "Peralatan";
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem1.Text = "Opsi...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 467);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.ppobCheckBox);
            this.Controls.Add(this.sorekCheckBox);
            this.Controls.Add(this.dilCheckBox);
            this.Controls.Add(this.importLabel);
            this.Controls.Add(this.ppobGroupBox);
            this.Controls.Add(this.sorekGroupBox);
            this.Controls.Add(this.dilGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "PLN Watch Data Importer";
            this.dilGroupBox.ResumeLayout(false);
            this.dilGroupBox.PerformLayout();
            this.sorekGroupBox.ResumeLayout(false);
            this.sorekGroupBox.PerformLayout();
            this.ppobGroupBox.ResumeLayout(false);
            this.ppobGroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox dilGroupBox;
        private System.Windows.Forms.TextBox dilMdbPathTextBox;
        private System.Windows.Forms.TextBox dilKodeAreaTextBox;
        private System.Windows.Forms.Label dilMdbPathLabel;
        private System.Windows.Forms.TextBox dilTahunTextBox;
        private System.Windows.Forms.TextBox dilBulanTextBox;
        private System.Windows.Forms.TextBox dilTableNameTextBox;
        private System.Windows.Forms.Label dilKodeAreaLabel;
        private System.Windows.Forms.Label dilBlThLabel;
        private System.Windows.Forms.Label dilTableNameLabel;
        private System.Windows.Forms.GroupBox sorekGroupBox;
        private System.Windows.Forms.Label sorekTableNameLabel;
        private System.Windows.Forms.Label sorekBlThLabel;
        private System.Windows.Forms.Label sorekKodeAreaLabel;
        private System.Windows.Forms.TextBox sorekTableNameTextBox;
        private System.Windows.Forms.TextBox sorekTahunTextBox;
        private System.Windows.Forms.TextBox sorekBulanTextBox;
        private System.Windows.Forms.TextBox sorekKodeAreaTextBox;
        private System.Windows.Forms.Label sorekMdbPathLabel;
        private System.Windows.Forms.TextBox sorekMdbPathTextBox;
        private System.Windows.Forms.GroupBox ppobGroupBox;
        private System.Windows.Forms.Label ppobTableNameLabel;
        private System.Windows.Forms.Label ppobBlThLabel;
        private System.Windows.Forms.Label ppobKodeAreaLabel;
        private System.Windows.Forms.TextBox ppobTableNameTextBox;
        private System.Windows.Forms.TextBox ppobTahunTextBox;
        private System.Windows.Forms.TextBox ppobBulanTextBox;
        private System.Windows.Forms.TextBox ppobKodeAreaTextBox;
        private System.Windows.Forms.Label ppobMdbPathLabel;
        private System.Windows.Forms.TextBox ppobMdbPathTextBox;
        private System.Windows.Forms.Label importLabel;
        private System.Windows.Forms.CheckBox dilCheckBox;
        private System.Windows.Forms.CheckBox sorekCheckBox;
        private System.Windows.Forms.CheckBox ppobCheckBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
    }
}

