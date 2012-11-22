using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace PlnWatchDataImporter
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            mySqlHostTextBox.Text = ConfigurationManager.AppSettings["mysqlhost"];
            mySqlUserTextBox.Text = ConfigurationManager.AppSettings["mysqluser"];
            mySqlPassTextBox.Text = ConfigurationManager.AppSettings["mysqlpass"];
            mySqlDbTextBox.Text = ConfigurationManager.AppSettings["mysqldb"];
            mySqlPathTextBox.Text = ConfigurationManager.AppSettings["mysqlpath"];
            if (ConfigurationManager.AppSettings["batchmode"] == "1")
                batchModeRadioButton.Checked = true;
            else
                transactionRadioButton.Checked = false;
            mySqlPathTextBox.Enabled = mySqlPathBrowseButton.Enabled = batchModeRadioButton.Checked;
        }

        public void SaveConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("mysqlhost");
            config.AppSettings.Settings.Add("mysqlhost", mySqlHostTextBox.Text);
            config.AppSettings.Settings.Remove("mysqluser");
            config.AppSettings.Settings.Add("mysqluser", mySqlUserTextBox.Text);
            config.AppSettings.Settings.Remove("mysqlpass");
            config.AppSettings.Settings.Add("mysqlpass", mySqlPassTextBox.Text);
            config.AppSettings.Settings.Remove("mysqldb");
            config.AppSettings.Settings.Add("mysqldb", mySqlDbTextBox.Text);
            config.AppSettings.Settings.Remove("mysqlpath");
            config.AppSettings.Settings.Add("mysqlpath", mySqlPathTextBox.Text);
            config.AppSettings.Settings.Remove("batchmode");
            if (batchModeRadioButton.Checked)
                config.AppSettings.Settings.Add("batchmode", "1");
            else
                config.AppSettings.Settings.Add("batchmode", "0");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            SaveConfig();
            Close();
        }

        private void OptionsForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                mySqlHostTextBox.Text = ConfigurationManager.AppSettings["mysqlhost"];
                mySqlUserTextBox.Text = ConfigurationManager.AppSettings["mysqluser"];
                mySqlPassTextBox.Text = ConfigurationManager.AppSettings["mysqlpass"];
                mySqlDbTextBox.Text = ConfigurationManager.AppSettings["mysqldb"];
                mySqlPathTextBox.Text = ConfigurationManager.AppSettings["mysqlpath"];
            }
        }

        private void mySqlPathBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = mySqlPathTextBox.Text;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                mySqlPathTextBox.Text = openFileDialog.FileName;
        }

        private void execModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            mySqlPathTextBox.Enabled = mySqlPathBrowseButton.Enabled = batchModeRadioButton.Checked;
        }
    }
}
