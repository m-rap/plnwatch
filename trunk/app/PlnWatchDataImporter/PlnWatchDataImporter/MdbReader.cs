using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Configuration;

namespace PlnWatchDataImporter
{
    public class MdbReaderProgressEventArgs : EventArgs
    {
        public bool ClearCurrentLine
        {
            set;
            get;
        }
        public int Left
        {
            set;
            get;
        }
        public int Width
        {
            set;
            get;
        }

        public MdbReaderProgressEventArgs(bool clearCurrentLine, int left, int width)
        {
            ClearCurrentLine = clearCurrentLine;
            Left = left;
            Width = width;
        }
    }

    public delegate void MdbReaderProgressEventHandler(object sender, MdbReaderProgressEventArgs e);

    public enum MdbReadResult
    {
        FAILED,
        SUCCESS,
        MYSQLNOTFOUND
    }

    public class MdbReader
    {
        #region attributes
        string mysqlpath;
        string mysqlhost;
        string mysqluser;
        string mysqlpass;
        string mysqldb;
        OleDbConnection oleDbConnection;
        MySqlConnection mySqlConnection;
        string dmlDilFileName = "dml_dil.sql.tmp";
        string dmlSorekFileName = "dml_sorek.sql.tmp";
        string logFileName = "readmdb.log";
        #endregion

        #region properties
        public string DilMdbPath
        {
            set;
            get;
        }
        public string DilKodeArea
        {
            set;
            get;
        }
        public string DilBlTh
        {
            set;
            get;
        }
        public string DilTableName
        {
            set;
            get;
        }

        public string SorekMdbPath
        {
            set;
            get;
        }
        public string SorekBlTh
        {
            set;
            get;
        }
        public string SorekKodeArea
        {
            set;
            get;
        }
        public string SorekTableName
        {
            set;
            get;
        }

        public string PpobMdbPath
        {
            set;
            get;
        }
        public string PpobBlTh
        {
            set;
            get;
        }
        public string PpobTableName
        {
            set;
            get;
        }
        
        public string ProgressText
        {
            set;
            get;
        }
        #endregion

        public event MdbReaderProgressEventHandler ProgressTextChanged;
        void OnProgressTextChanged(MdbReaderProgressEventArgs e)
        {
            if (ProgressTextChanged != null)
                ProgressTextChanged(this, e);
        }

        public MdbReader()
        {
            Initialize();
            mySqlConnection = new MySqlConnection("server=" + mysqlhost + ";User Id=" + mysqluser + ";password=" + mysqlpass + ";database=" + mysqldb);
        }

        #region methods
        void Initialize()
        {
            mysqlpath = ConfigurationManager.AppSettings["mysqlpath"];
            mysqlhost = ConfigurationManager.AppSettings["mysqlhost"];
            mysqluser = ConfigurationManager.AppSettings["mysqluser"];
            mysqlpass = ConfigurationManager.AppSettings["mysqlpass"];
            mysqldb = ConfigurationManager.AppSettings["mysqldb"];
        }

        string FileBrowse()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Microsoft Office Access Files (*.mdb)|*.mdb";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;
            return null;
        }

        void ProcessOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                ProgressText = e.Data;
                OnProgressTextChanged(null);
            }
        }

        public void RunCmd(string arguments)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("cmd.exe", arguments)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();
            p.OutputDataReceived += ProcessOutputReceived;
            p.ErrorDataReceived += ProcessOutputReceived;
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
        }

        public void tesproc()
        {
            RunCmd("/c asdasd");
        }

        public MdbReadResult ReadDil()
        {
            MdbReadResult result;
            try
            {
                mySqlConnection.Open();
                MySqlCommand mycmd = new MySqlCommand("SELECT ConfigValue FROM config WHERE ConfigKey = 'BlTh';", mySqlConnection);
                MySqlDataReader myreader = mycmd.ExecuteReader();
                bool blthExists = false;
                if (myreader.Read())
                    blthExists = true;
                myreader.Close();
                if (!blthExists)
                {
                    mycmd.CommandText = "INSERT INTO config VALUES ('BlTh', '" + DilBlTh + "');";
                    mycmd.ExecuteNonQuery();
                }
                else
                {
                    mycmd.CommandText = "UPDATE config SET ConfigValue = '" + DilBlTh + "' WHERE ConfigKey = 'BlTh';";
                    mycmd.ExecuteNonQuery();
                }
                mySqlConnection.Close();

                int i = 0;
                DateTime start = DateTime.Now;
                
                ProgressText = "Membaca database. Silakan menunggu.. " + start.ToLongTimeString();
                OnProgressTextChanged(null);
                
                using (StreamWriter dilStreamWriter = new StreamWriter(@dmlDilFileName))
                {
                    ProgressText = "Membuka koneksi Ms Access..";
                    OnProgressTextChanged(null);

                    oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DilMdbPath);
                    oleDbConnection.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, NOTELP, KODEPOS, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG FROM " + DilTableName, oleDbConnection);
                    OleDbDataReader reader = cmd.ExecuteReader();

                    ProgressText = "Mengeksekusi reader dan menulis file dump..";
                    OnProgressTextChanged(null);

                    dilStreamWriter.WriteLine("INSERT INTO dil (JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, NOTELP, KODEPOS, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG, KODEAREA) VALUES ");
                    
                    while (reader.Read())
                    {
                        DateTime tglpsg;
                        if (reader["TGLPASANG_KWH"].ToString() != "")
                            tglpsg = (DateTime)reader["TGLPASANG_KWH"];
                        else
                            tglpsg = new DateTime();

                        StringBuilder sb = new StringBuilder();
                        if (i > 0)
                            sb.Append(", ");
                        sb.Append("('")
                            .Append(reader["JENIS_MK"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["IDPEL"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["NAMA"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["TARIF"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', ")
                            .Append(reader["DAYA"].ToString()).Append(", '")
                            .Append(reader["PNJ"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["NAMAPNJ"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["NOBANG"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["RT"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["RW"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["LINGKUNGAN"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["NOTELP"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["KODEPOS"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(tglpsg.Year.ToString("0000")).Append("-").Append(tglpsg.Month.ToString("00")).Append("-").Append(tglpsg.Day.ToString("00")).Append("', '")
                            .Append(reader["MEREK_KWH"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["KDGARDU"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(reader["NOTIANG"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(DilKodeArea).Append("')");
                        dilStreamWriter.WriteLine(sb.ToString());

                        string n = (++i).ToString();
                        ProgressText = "Jumlah record:" + n + "";
                        if (i > 1)
                            OnProgressTextChanged(new MdbReaderProgressEventArgs(true, 14, n.Length));
                        else
                            OnProgressTextChanged(null);
                    }
                    dilStreamWriter.WriteLine(";");
                }

                DateTime end, startExecuteMySql;
                TimeSpan timeElapsed;

                if (File.Exists(@mysqlpath))
                {
                    string pArguments = "/c \"" + mysqlpath + "\" -h" + mysqlhost + " -u" + mysqluser + ((mysqlpass == "") ? "" : " -p" + mysqlpass) + " " + mysqldb + " < " + dmlDilFileName;

                    ProgressText = "Mengeksekusi MySql. Silakan menunggu..";
                    OnProgressTextChanged(null);

                    startExecuteMySql = DateTime.Now;

                    RunCmd(pArguments);

                    result = MdbReadResult.SUCCESS;
                    end = DateTime.Now;

                    ProgressText = "\nSemua record DIL berhasil dimasukkan ke MySql. " + end.ToLongTimeString();
                    OnProgressTextChanged(null);
                }
                else
                {
                    result = MdbReadResult.MYSQLNOTFOUND;
                    end = startExecuteMySql = DateTime.Now;
                    ProgressText = "MySql.exe tidak ditemukan. File dump akan disimpan...";
                    OnProgressTextChanged(null);
                }

                timeElapsed = end - start;
                ProgressText = "Total waktu: " + timeElapsed.TotalMilliseconds;
                OnProgressTextChanged(null);

                using (StreamWriter dilStreamWriter = new StreamWriter(@logFileName, true))
                {
                    dilStreamWriter.WriteLine("/* ::DIL convert:: " + DateTime.Now.ToLongDateString());
                    dilStreamWriter.WriteLine("Records: " + i);
                    dilStreamWriter.WriteLine("Start: " + start.ToLongTimeString());
                    dilStreamWriter.WriteLine("Start Execute MySql: " + startExecuteMySql.ToLongTimeString());
                    dilStreamWriter.WriteLine("End: " + end.ToLongTimeString());
                    dilStreamWriter.WriteLine("Time Elapsed: " + timeElapsed.TotalMilliseconds);
                    dilStreamWriter.WriteLine("*/\n");
                }

                

                //Console.WriteLine("Apakah Anda ingin menyimpan file DML DIL? (y/n)");

                //string input;
                //while ((input = Console.ReadLine()) != "")
                //{
                //    if (input == "y")
                //    {
                //        Console.WriteLine("Tulis nama file: ");
                //        string newfilename = Console.ReadLine();
                //        File.Move(dmlDilFileName, newfilename + ".sql");
                //        break;
                //    }
                //    if (input == "n") {
                //        File.Delete(dmlDilFileName);
                //        break;
                //    }
                //}

                oleDbConnection.Close();
            }
            catch (Exception ex)
            {
                result = MdbReadResult.FAILED;
                ProgressText = ex.Message;
                OnProgressTextChanged(null);
            }
            return result;
        }

        public MdbReadResult ReadSorek()
        {
            MdbReadResult result;
            MySqlCommand mycmd;
            MySqlDataReader myreader;
            try
            {
                int i = 0;
                DateTime start = DateTime.Now;
                string sorekTh = SorekBlTh.Substring(2), sorekBl = SorekBlTh.Substring(0, 2), sorekTableName = "SOREK_" + sorekTh + "_" + sorekBl;

                ProgressText = "Membaca database. Silakan menunggu.. " + start.ToLongTimeString();
                OnProgressTextChanged(null);

                using (StreamWriter sorekStreamWriter = new StreamWriter(@dmlSorekFileName))
                {
                    ProgressText = "Membuka koneksi Ms Access..";
                    OnProgressTextChanged(null);

                    oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SorekMdbPath);
                    oleDbConnection.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT IDPEL, TGLBACA, PEMKWH FROM " + SorekTableName, oleDbConnection);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    
                    sorekStreamWriter.WriteLine("" +
                        "CREATE TABLE IF NOT EXISTS `" + sorekTableName + "` (\n" +
                        "    `IDPEL` varchar(12) NOT NULL,\n" +
                        "    `TGLBACA` date default NULL,\n" +
                        "    `PEMKWH` double default NULL,\n" +
                        "    `KODEAREA` varchar(5) default NULL,\n" +
                        "    `JAMNYALA` double default NULL,\n" +
                        "    PRIMARY KEY  (`IDPEL`)\n" +
                        ") ENGINE=MyISAM DEFAULT CHARSET=latin1;" +
                    "");

                    sorekStreamWriter.WriteLine("INSERT INTO `" + sorekTableName + "` (IDPEL, TGLBACA, PEMKWH, KODEAREA, JAMNYALA) VALUES ");

                    ProgressText = "Mengeksekusi reader, mengkalkulasi jam nyala, dan menulis file dump..";
                    OnProgressTextChanged(null);

                    mySqlConnection.Open();
                    mycmd = mySqlConnection.CreateCommand();
                    while (reader.Read())
                    {
                        string tglbaca, idpel = reader["IDPEL"].ToString();
                        try
                        {
                            tglbaca = reader["TGLBACA"].ToString().Trim().Insert(6, "-").Insert(4, "-");
                        }
                        catch (Exception ex)
                        {
                            ProgressText = ex.Message;
                            OnProgressTextChanged(null);
                            tglbaca = "0000-00-00";
                        }

                        mycmd.CommandText = "SELECT daya FROM dil WHERE IDPEL = '" + idpel + "';";
                        myreader = mycmd.ExecuteReader();
                        double daya = -1;
                        while (myreader.Read())
                            daya = double.Parse(myreader["DAYA"].ToString());
                        myreader.Close();
                        double pemkwh = double.Parse(reader["PEMKWH"].ToString()),
                               jamnyala = (daya == -1) ? -1 : pemkwh * 1000 / daya;

                        StringBuilder sb = new StringBuilder();
                        if (i > 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append("('")
                            .Append(reader["IDPEL"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                            .Append(tglbaca).Append("', ")
                            .Append(reader["PEMKWH"].ToString()).Append(", '")
                            .Append(SorekKodeArea).Append("', ")
                            .Append(jamnyala).Append(")");
                        sorekStreamWriter.WriteLine(sb.ToString());
                        string n = (++i).ToString();
                        ProgressText = "Jumlah record:" + n + "";
                        if (i > 1)
                            OnProgressTextChanged(new MdbReaderProgressEventArgs(true, 14, n.Length));
                        else
                            OnProgressTextChanged(null);
                    }
                    mySqlConnection.Close();
                    sorekStreamWriter.WriteLine(";");
                }

                DateTime end, startExecuteMySql;
                TimeSpan timeElapsed;

                if (File.Exists(@mysqlpath))
                {
                    ProgressText = "Mengecek keberadaan tabel " + sorekTableName + "..";
                    OnProgressTextChanged(null);
                    mySqlConnection.Open();
                    mycmd = mySqlConnection.CreateCommand();
                    mycmd.CommandText = "SHOW TABLES WHERE Tables_in_" + mysqldb + " like '" + sorekTableName + "';";
                    myreader = mycmd.ExecuteReader();
                    if (myreader.Read())
                    {
                        myreader.Close();
                        mycmd.CommandText = "DROP TABLE " + sorekTableName + ";";
                        mycmd.ExecuteNonQuery();
                    }
                    mySqlConnection.Close();

                    string pArguments = "/c \"" + mysqlpath + "\" -h" + mysqlhost + " -u" + mysqluser + ((mysqlpass == "") ? "" : " -p" + mysqlpass) + " " + mysqldb + " < " + dmlSorekFileName;

                    ProgressText = "Mengeksekusi MySql. Silakan menunggu..";
                    OnProgressTextChanged(null);

                    startExecuteMySql = DateTime.Now;

                    RunCmd(pArguments);

                    result = MdbReadResult.SUCCESS;
                    end = DateTime.Now;

                    ProgressText = "\nSemua record DIL berhasil dimasukkan ke MySql. " + end.ToLongTimeString();
                    OnProgressTextChanged(null);

                }
                else
                {
                    result = MdbReadResult.MYSQLNOTFOUND;
                    end = startExecuteMySql = DateTime.Now;
                    ProgressText = "MySql.exe tidak ditemukan. File dump akan disimpan...";
                    OnProgressTextChanged(null);
                }

                timeElapsed = end - start;
                ProgressText = "Total waktu: " + timeElapsed.TotalMilliseconds;
                OnProgressTextChanged(null);

                using (StreamWriter sorekStreamWriter = new StreamWriter(@logFileName, true))
                {
                    sorekStreamWriter.WriteLine("/* ::SOREK_" + sorekTh + "_" + sorekBl + " convert:: " + DateTime.Now.ToLongDateString());
                    sorekStreamWriter.WriteLine("Records: " + i);
                    sorekStreamWriter.WriteLine("Start: " + start.ToLongTimeString());
                    sorekStreamWriter.WriteLine("Start Execute MySql: " + startExecuteMySql.ToLongTimeString());
                    sorekStreamWriter.WriteLine("End: " + end.ToLongTimeString());
                    sorekStreamWriter.WriteLine("Time Elapsed: " + timeElapsed.TotalMilliseconds);
                    sorekStreamWriter.WriteLine("*/\n");
                }

                oleDbConnection.Close();
            }
            catch (Exception ex)
            {
                result = MdbReadResult.FAILED;
                ProgressText = ex.Message;
                OnProgressTextChanged(null);
            }
            return result;
        }

        public MdbReadResult ReadPpob()
        {
            MdbReadResult result = MdbReadResult.FAILED;
            return result;
        }
        #endregion
    }
}
