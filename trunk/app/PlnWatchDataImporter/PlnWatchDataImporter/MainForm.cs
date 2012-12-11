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
using System.IO;
using System.Security.AccessControl;

namespace PlnWatchDataImporter
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();

        [DllImport("kernel32")]
        static extern bool AttachConsole(int pid);

        [DllImport("kernel32")]
        static extern bool FreeConsole();

        Importer impoter;
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
                    dilKodeAreaTextBox.Text = filename[1].Substring(filename[1].Length - 3);
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
                    sorekKodeAreaTextBox.Text = filename[1].Substring(filename[1].Length - 3);
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
            Enabled = false;
            impoter = new Importer();
            if (dilCheckBox.Checked)
            {
                impoter.DilMdbPath = dilMdbPathTextBox.Text;
                impoter.DilKodeArea = dilKodeAreaTextBox.Text;
                impoter.DilBlTh = dilBulanTextBox.Text + dilTahunTextBox.Text;
                impoter.DilTableName = dilTableNameTextBox.Text;
            }
            if (sorekCheckBox.Checked)
            {
                impoter.SorekMdbPath = sorekMdbPathTextBox.Text;
                impoter.SorekKodeArea = sorekKodeAreaTextBox.Text;
                impoter.SorekBlTh = sorekBulanTextBox.Text + sorekTahunTextBox.Text;
                impoter.SorekTableName = sorekTableNameTextBox.Text;
            }
            if (ppobCheckBox.Checked)
            {
                impoter.PpobMdbPath = ppobMdbPathTextBox.Text;
                impoter.PpobBlTh = ppobBulanTextBox.Text + ppobTahunTextBox.Text;
                impoter.PpobTableName = ppobTableNameTextBox.Text;
            }
            impoter.ProgressTextChanged += mdbReader_ProgressTextChanged;
            
            AllocConsole();
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            StreamWriter standardErrorOutput = new StreamWriter(Console.OpenStandardError());
            standardErrorOutput.AutoFlush = true;
            Console.SetError(standardErrorOutput);
            StreamReader standardInput = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(standardInput);
            if (dilCheckBox.Checked)
            {
                impoter.ImportDil();
            }
            if (sorekCheckBox.Checked)
            {
                impoter.ImportSorek();
            }
            if (ppobCheckBox.Checked)
            {
                impoter.ImportPpob();
            }
            Console.Write("Tekan Enter...");
            while (Console.Read() != (int) ConsoleKey.Enter);
            FreeConsole();
            Enabled = true;
        }

        void mdbReader_ProgressTextChanged(object sender, MdbReaderProgressEventArgs e)
        {
            if (e != null && e.ClearCurrentLine)
                Program.ClearCurrentConsoleLine(e.Left, e.Width);
            Console.WriteLine(impoter.ProgressText);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (optionsForm == null)
                optionsForm = new OptionsForm();
            optionsForm.ShowDialog();
        }
    }
}
