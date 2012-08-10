using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlnWatchDataImporter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dilGroupBox.Enabled = dilCheckBox.Checked;
            sorekGroupBox.Enabled = sorekCheckBox.Checked;
            ppobGroupBox.Enabled = ppobCheckBox.Checked;
        }

        private void dilCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dilGroupBox.Enabled = dilCheckBox.Checked;
        }

        private void sorekCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            sorekGroupBox.Enabled = sorekCheckBox.Checked;
        }

        private void ppobCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ppobGroupBox.Enabled = ppobCheckBox.Checked;
        }

        private void dilMdbPathTextBox_Enter(object sender, EventArgs e)
        {
            openFileDialog.FileName = dilMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                dilMdbPathTextBox.Text = openFileDialog.FileName;
        }

        private void sorekMdbPathTextBox_Enter(object sender, EventArgs e)
        {
            openFileDialog.FileName = sorekMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                sorekMdbPathTextBox.Text = openFileDialog.FileName;
        }

        private void ppobMdbPathTextBox_Enter(object sender, EventArgs e)
        {
            openFileDialog.FileName = ppobMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ppobMdbPathTextBox.Text = openFileDialog.FileName;
        }

        private void dilMdbPathTextBox_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = dilMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                dilMdbPathTextBox.Text = openFileDialog.FileName;
        }

        private void sorekMdbPathTextBox_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = sorekMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                sorekMdbPathTextBox.Text = openFileDialog.FileName;
        }

        private void ppobMdbPathTextBox_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = ppobMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ppobMdbPathTextBox.Text = openFileDialog.FileName;
        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }
    }
}
