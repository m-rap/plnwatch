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
    public partial class ProgressWindow : Form
    {
        MdbReader mdbReader;
        string progressText = "";

        public ProgressWindow(MdbReader mdbReader)
        {
            InitializeComponent();
            this.mdbReader = mdbReader;
            this.mdbReader.ProgressTextChanged += mdbReader_ProgressTextChanged;
        }

        void mdbReader_ProgressTextChanged(object sender, MdbReaderProgressEventArgs e)
        {
            progressTextBox.Text = mdbReader.ProgressText;
            //if (e == null)
            //    progressText += ("\r\n" + mdbReader.ProgressText);
            //else
            //{
            //    if (e.ClearCurrentLine)
            //    {
            //        int i = progressText.LastIndexOf("\r\n");
            //        if (i == -1)
            //            progressText = mdbReader.ProgressText;
            //        else
            //        {
            //            progressText = progressText.Remove(i) +"\r\n" + mdbReader.ProgressText;
            //        }
            //    }
            //    else
            //        progressText += ("\r\n" + mdbReader.ProgressText);
            //}
        }

        private void ProgressWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            progressTextBox.Text = "";
        }
    }
}
