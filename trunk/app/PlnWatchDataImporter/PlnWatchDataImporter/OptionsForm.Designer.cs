namespace PlnWatchDataImporter
{
    partial class OptionsForm
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
            this.mySqlGroupBox = new System.Windows.Forms.GroupBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.mySqlHostTextBox = new System.Windows.Forms.TextBox();
            this.mySqlDbTextBox = new System.Windows.Forms.TextBox();
            this.mySqlUserTextBox = new System.Windows.Forms.TextBox();
            this.mySqlPassTextBox = new System.Windows.Forms.TextBox();
            this.mySqlHostLabel = new System.Windows.Forms.Label();
            this.mySqlDbLabel = new System.Windows.Forms.Label();
            this.mySqlUserLabel = new System.Windows.Forms.Label();
            this.mySqlPassLabel = new System.Windows.Forms.Label();
            this.mySqlPathTextBox = new System.Windows.Forms.TextBox();
            this.mySqlPathLabel = new System.Windows.Forms.Label();
            this.mySqlPathBrowseButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mySqlGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mySqlGroupBox
            // 
            this.mySqlGroupBox.Controls.Add(this.mySqlPathBrowseButton);
            this.mySqlGroupBox.Controls.Add(this.mySqlPathLabel);
            this.mySqlGroupBox.Controls.Add(this.mySqlPathTextBox);
            this.mySqlGroupBox.Controls.Add(this.mySqlPassLabel);
            this.mySqlGroupBox.Controls.Add(this.mySqlUserLabel);
            this.mySqlGroupBox.Controls.Add(this.mySqlDbLabel);
            this.mySqlGroupBox.Controls.Add(this.mySqlHostLabel);
            this.mySqlGroupBox.Controls.Add(this.mySqlPassTextBox);
            this.mySqlGroupBox.Controls.Add(this.mySqlUserTextBox);
            this.mySqlGroupBox.Controls.Add(this.mySqlDbTextBox);
            this.mySqlGroupBox.Controls.Add(this.mySqlHostTextBox);
            this.mySqlGroupBox.Location = new System.Drawing.Point(12, 12);
            this.mySqlGroupBox.Name = "mySqlGroupBox";
            this.mySqlGroupBox.Size = new System.Drawing.Size(260, 177);
            this.mySqlGroupBox.TabIndex = 0;
            this.mySqlGroupBox.TabStop = false;
            this.mySqlGroupBox.Text = "MySql";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(197, 195);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Tutup";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // mySqlHostTextBox
            // 
            this.mySqlHostTextBox.Location = new System.Drawing.Point(109, 19);
            this.mySqlHostTextBox.Name = "mySqlHostTextBox";
            this.mySqlHostTextBox.Size = new System.Drawing.Size(145, 20);
            this.mySqlHostTextBox.TabIndex = 0;
            this.mySqlHostTextBox.Text = "localhost";
            // 
            // mySqlDbTextBox
            // 
            this.mySqlDbTextBox.Location = new System.Drawing.Point(109, 45);
            this.mySqlDbTextBox.Name = "mySqlDbTextBox";
            this.mySqlDbTextBox.Size = new System.Drawing.Size(145, 20);
            this.mySqlDbTextBox.TabIndex = 1;
            // 
            // mySqlUserTextBox
            // 
            this.mySqlUserTextBox.Location = new System.Drawing.Point(109, 72);
            this.mySqlUserTextBox.Name = "mySqlUserTextBox";
            this.mySqlUserTextBox.Size = new System.Drawing.Size(145, 20);
            this.mySqlUserTextBox.TabIndex = 2;
            // 
            // mySqlPassTextBox
            // 
            this.mySqlPassTextBox.Location = new System.Drawing.Point(109, 99);
            this.mySqlPassTextBox.Name = "mySqlPassTextBox";
            this.mySqlPassTextBox.Size = new System.Drawing.Size(145, 20);
            this.mySqlPassTextBox.TabIndex = 3;
            // 
            // mySqlHostLabel
            // 
            this.mySqlHostLabel.AutoSize = true;
            this.mySqlHostLabel.Location = new System.Drawing.Point(6, 22);
            this.mySqlHostLabel.Name = "mySqlHostLabel";
            this.mySqlHostLabel.Size = new System.Drawing.Size(63, 13);
            this.mySqlHostLabel.TabIndex = 4;
            this.mySqlHostLabel.Text = "Nama Host:";
            // 
            // mySqlDbLabel
            // 
            this.mySqlDbLabel.AutoSize = true;
            this.mySqlDbLabel.Location = new System.Drawing.Point(6, 48);
            this.mySqlDbLabel.Name = "mySqlDbLabel";
            this.mySqlDbLabel.Size = new System.Drawing.Size(56, 13);
            this.mySqlDbLabel.TabIndex = 5;
            this.mySqlDbLabel.Text = "Database:";
            // 
            // mySqlUserLabel
            // 
            this.mySqlUserLabel.AutoSize = true;
            this.mySqlUserLabel.Location = new System.Drawing.Point(6, 75);
            this.mySqlUserLabel.Name = "mySqlUserLabel";
            this.mySqlUserLabel.Size = new System.Drawing.Size(58, 13);
            this.mySqlUserLabel.TabIndex = 6;
            this.mySqlUserLabel.Text = "Username:";
            // 
            // mySqlPassLabel
            // 
            this.mySqlPassLabel.AutoSize = true;
            this.mySqlPassLabel.Location = new System.Drawing.Point(6, 102);
            this.mySqlPassLabel.Name = "mySqlPassLabel";
            this.mySqlPassLabel.Size = new System.Drawing.Size(56, 13);
            this.mySqlPassLabel.TabIndex = 7;
            this.mySqlPassLabel.Text = "Password:";
            // 
            // mySqlPathTextBox
            // 
            this.mySqlPathTextBox.Location = new System.Drawing.Point(6, 144);
            this.mySqlPathTextBox.Name = "mySqlPathTextBox";
            this.mySqlPathTextBox.Size = new System.Drawing.Size(173, 20);
            this.mySqlPathTextBox.TabIndex = 8;
            // 
            // mySqlPathLabel
            // 
            this.mySqlPathLabel.AutoSize = true;
            this.mySqlPathLabel.Location = new System.Drawing.Point(6, 128);
            this.mySqlPathLabel.Name = "mySqlPathLabel";
            this.mySqlPathLabel.Size = new System.Drawing.Size(90, 13);
            this.mySqlPathLabel.TabIndex = 9;
            this.mySqlPathLabel.Text = "Lokasi mysql.exe:";
            // 
            // mySqlPathBrowseButton
            // 
            this.mySqlPathBrowseButton.Location = new System.Drawing.Point(185, 141);
            this.mySqlPathBrowseButton.Name = "mySqlPathBrowseButton";
            this.mySqlPathBrowseButton.Size = new System.Drawing.Size(69, 23);
            this.mySqlPathBrowseButton.TabIndex = 10;
            this.mySqlPathBrowseButton.Text = "Cari...";
            this.mySqlPathBrowseButton.UseVisualStyleBackColor = true;
            this.mySqlPathBrowseButton.Click += new System.EventHandler(this.mySqlPathBrowseButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 236);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.mySqlGroupBox);
            this.Name = "OptionsForm";
            this.Text = "Opsi";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.VisibleChanged += new System.EventHandler(this.OptionsForm_VisibleChanged);
            this.mySqlGroupBox.ResumeLayout(false);
            this.mySqlGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox mySqlGroupBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox mySqlPassTextBox;
        private System.Windows.Forms.TextBox mySqlUserTextBox;
        private System.Windows.Forms.TextBox mySqlDbTextBox;
        private System.Windows.Forms.TextBox mySqlHostTextBox;
        private System.Windows.Forms.Label mySqlHostLabel;
        private System.Windows.Forms.Label mySqlDbLabel;
        private System.Windows.Forms.Label mySqlUserLabel;
        private System.Windows.Forms.Label mySqlPassLabel;
        private System.Windows.Forms.TextBox mySqlPathTextBox;
        private System.Windows.Forms.Label mySqlPathLabel;
        private System.Windows.Forms.Button mySqlPathBrowseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}