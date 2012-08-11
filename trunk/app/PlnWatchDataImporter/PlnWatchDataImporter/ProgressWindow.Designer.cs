namespace PlnWatchDataImporter
{
    partial class ProgressWindow
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
            this.progressTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // progressTextBox
            // 
            this.progressTextBox.Location = new System.Drawing.Point(12, 12);
            this.progressTextBox.Multiline = true;
            this.progressTextBox.Name = "progressTextBox";
            this.progressTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.progressTextBox.Size = new System.Drawing.Size(268, 227);
            this.progressTextBox.TabIndex = 0;
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.progressTextBox);
            this.Name = "ProgressWindow";
            this.Text = "ProgressWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox progressTextBox;
    }
}