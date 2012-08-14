using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace PlnWatchDataImporter
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();

        [DllImport("kernel32")]
        static extern bool FreeConsole();

        MdbReader mdbReader;
        OptionsForm optionsForm;

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

        private void dilMdbPathBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = dilMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dilMdbPathTextBox.Text = openFileDialog.FileName;
                try
                {
                    Regex regex = new Regex(@"[\s_.]");
                    string[] filename = regex.Split(dilMdbPathTextBox.Text.Substring(dilMdbPathTextBox.Text.LastIndexOf('\\') + 1));
                    if (filename[0].ToUpper() != "DIL") throw new ArgumentException(filename[0].ToUpper());
                    dilKodeAreaTextBox.Text = filename[1];
                    dilBulanTextBox.Text = filename[2].Substring(0, 2);
                    dilTahunTextBox.Text = filename[2].Substring(2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nama file terlihat seperti tidak dikenal, tetapi tetap ditambahkan. " + ex.Message);
                }
            }
        }

        private void sorekMdbPathBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = sorekMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sorekMdbPathTextBox.Text = openFileDialog.FileName;
                try
                {
                    Regex regex = new Regex(@"[\s_.]");
                    string[] filename = regex.Split(sorekMdbPathTextBox.Text.Substring(sorekMdbPathTextBox.Text.LastIndexOf('\\') + 1));
                    if (filename[0].ToUpper() != "SOREK") throw new ArgumentException(filename[0].ToUpper());
                    sorekKodeAreaTextBox.Text = filename[1];
                    sorekBulanTextBox.Text = filename[2].Substring(4);
                    sorekTahunTextBox.Text = filename[2].Substring(0, 4);
                    sorekTableNameTextBox.Text = "SOREK_" + sorekTahunTextBox.Text + "_" + sorekBulanTextBox.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nama file terlihat seperti tidak dikenal, tetapi tetap ditambahkan. " + ex.Message);
                }
            }
        }

        private void ppobMdbPathBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = ppobMdbPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ppobMdbPathTextBox.Text = openFileDialog.FileName;
                try
                {
                    Regex regex = new Regex(@"[\s_.]");
                    string[] filename = regex.Split(ppobMdbPathTextBox.Text.Substring(ppobMdbPathTextBox.Text.LastIndexOf('\\') + 1));
                    if (filename[0].ToUpper() != "PPOB") throw new ArgumentException(filename[0].ToUpper());
                    ppobBulanTextBox.Text = filename[2].Substring(4);
                    ppobTahunTextBox.Text = filename[2].Substring(0, 4);
                    ppobTableNameTextBox.Text = "PPOB_LPB_" + filename[2];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nama file terlihat seperti tidak dikenal, tetapi tetap ditambahkan. " + ex.Message);
                }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            mdbReader = new MdbReader();
            if (dilCheckBox.Checked)
            {
                mdbReader.DilMdbPath = dilMdbPathTextBox.Text;
                mdbReader.DilKodeArea = dilKodeAreaTextBox.Text;
                mdbReader.DilBlTh = dilBulanTextBox.Text + dilTahunTextBox.Text;
                mdbReader.DilTableName = dilTableNameTextBox.Text;
            }
            if (sorekCheckBox.Checked)
            {
                mdbReader.SorekMdbPath = sorekMdbPathTextBox.Text;
                mdbReader.SorekKodeArea = sorekKodeAreaTextBox.Text;
                mdbReader.SorekBlTh = sorekBulanTextBox.Text + sorekTahunTextBox.Text;
                mdbReader.SorekTableName = sorekTableNameTextBox.Text;
            }
            if (ppobCheckBox.Checked)
            {
                mdbReader.PpobMdbPath = ppobMdbPathTextBox.Text;
                mdbReader.PpobBlTh = ppobBulanTextBox.Text + ppobTahunTextBox.Text;
                mdbReader.PpobTableName = ppobTableNameTextBox.Text;
            }
            mdbReader.ProgressTextChanged += mdbReader_ProgressTextChanged;
            AllocConsole();
            mdbReader.tesproc();
            if (dilCheckBox.Checked)
            {
                mdbReader.ReadDil();
            }
            if (sorekCheckBox.Checked)
            {
                mdbReader.ReadSorek();
            }
            if (ppobCheckBox.Checked)
            {
                mdbReader.ReadPpob();
            }
            Console.Write("Tekan Enter...");
            while (Console.ReadKey().Key != ConsoleKey.Enter);
            FreeConsole();
        }

        void mdbReader_ProgressTextChanged(object sender, MdbReaderProgressEventArgs e)
        {
            Console.WriteLine(mdbReader.ProgressText);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (optionsForm == null)
                optionsForm = new OptionsForm();
            optionsForm.ShowDialog();
        }
    }
}
