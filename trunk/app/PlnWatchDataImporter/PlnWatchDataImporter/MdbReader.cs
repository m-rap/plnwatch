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
using System.Globalization;

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

    public class Importer
    {
        #region attributes
        string mysqlpath;
        string mysqlhost;
        string mysqluser;
        string mysqlpass;
        string mysqldb;
        bool batchMode;
        OleDbConnection oleDbConnection;
        MySqlConnection mySqlConnection;
        string dmlDilFileName = "dml_dil.sql.tmp";
        string dmlSorekFileName = "dml_sorek.sql.tmp";
        string dmlPpobFileName = "dml_ppob.sql.tmp";
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

        public Importer()
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
            batchMode = (ConfigurationManager.AppSettings["batchmode"] == "1") ? true : false;
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

        public MdbReadResult ImportDil()
        {
            MdbReadResult result;
            try
            {
                #region update DilBlTh in option
                mySqlConnection.Open();
                MySqlCommand mycmd = new MySqlCommand("SELECT `OptionValue` FROM `Option` WHERE `OptionKey` = 'DilBlTh';", mySqlConnection);
                MySqlDataReader myreader = mycmd.ExecuteReader();
                bool blthExists = false;
                if (myreader.Read())
                    blthExists = true;
                myreader.Close();
                if (!blthExists)
                {
                    mycmd.CommandText = "INSERT INTO `Option` VALUES ('DilBlTh', '" + DilBlTh + "');";
                    mycmd.ExecuteNonQuery();
                }
                else
                {
                    mycmd.CommandText = "UPDATE `Option` SET `OptionValue` = '" + DilBlTh + "' WHERE `OptionKey` = 'DilBlTh';";
                    mycmd.ExecuteNonQuery();
                }
                mySqlConnection.Close();
                #endregion

                int i = 0;
                DateTime start = DateTime.Now, end, startExecuteMySql;
                TimeSpan timeElapsed;
                
                ProgressText = "Membaca database. Silakan menunggu.. " + start.ToLongTimeString();
                OnProgressTextChanged(null);
                
                ProgressText = "Membuka koneksi Ms Access..";
                OnProgressTextChanged(null);

                oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DilMdbPath);
                oleDbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, NOTELP, KODEPOS, TGLPASANG_KWH, KDPEMBMETER, MEREK_KWH, KDGARDU, NOTIANG, KDDK, TGLPDL, TGLNYALA_PB, TGLRUBAH_MK FROM " + DilTableName, oleDbConnection);
                OleDbDataReader reader = cmd.ExecuteReader();

                StringBuilder sb = new StringBuilder("INSERT INTO dil (JENIS_MK, IDPEL, NAMA, TARIF, DAYA, ALAMAT, NOTELP, KODEPOS, TGLPASANG_KWH, KDPEMBMETER, MEREK_KWH, KDGARDU, NOTIANG, KODEAREA, KDDK, TGLPDL, TGLNYALA_PB, TGLRUBAH_MK) VALUES ");

                batchMode = false;
                if (batchMode)
                {
                    #region
                    using (StreamWriter dilStreamWriter = new StreamWriter(@dmlDilFileName))
                    {
                        ProgressText = "Batch Mode: true";
                        OnProgressTextChanged(null);

                        ProgressText = "Mengeksekusi reader dan menulis file dump..";
                        OnProgressTextChanged(null);

                        dilStreamWriter.WriteLine(sb.ToString());

                        while (reader.Read())
                        {
                            sb = new StringBuilder();
                            if (i > 0)
                                sb.Append(", ");
                            sb.Append(PopulateDilValueSQL(reader));
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
                    #endregion
                }
                else
                {
                    #region
                    startExecuteMySql = DateTime.Now;
                    ProgressText = "Batch Mode: false";
                    OnProgressTextChanged(null);

                    try
                    {
                        ProgressText = "Membuka koneksi ke server MySql";
                        OnProgressTextChanged(null);

                        mySqlConnection.Open();
                        mycmd = mySqlConnection.CreateCommand();
                        mycmd.Connection = mySqlConnection;

                        ProgressText = "Memulai transaksi";
                        OnProgressTextChanged(null);

                        mycmd.Transaction = mySqlConnection.BeginTransaction();

                        ProgressText = "Mulai memasukkan data..";
                        OnProgressTextChanged(null);

                        while (reader.Read())
                        {
                            StringBuilder fullSql = new StringBuilder();
                            fullSql.Append(sb).Append(PopulateDilValueSQL(reader)).Append(";");

                            mycmd.CommandText = fullSql.ToString();
                            mycmd.ExecuteNonQuery();

                            string n = (++i).ToString();
                            ProgressText = "Jumlah record:" + n + "";
                            if (i > 1)
                                OnProgressTextChanged(new MdbReaderProgressEventArgs(true, 14, n.Length));
                            else
                                OnProgressTextChanged(null);
                        }

                        ProgressText = "Transaksi berakhir, melakukan commit.";
                        OnProgressTextChanged(null);

                        mycmd.Transaction.Commit();
                        mySqlConnection.Close();
                        result = MdbReadResult.SUCCESS;
                    }
                    catch (Exception ex)
                    {
                        ProgressText = ex.Message;
                        OnProgressTextChanged(null);
                        mycmd.Transaction.Rollback();
                        result = MdbReadResult.FAILED;
                    }
                    end = DateTime.Now;
                    #endregion
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

                #region save dml
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
                #endregion

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

        public StringBuilder PopulateDilValueSQL(OleDbDataReader reader)
        {
            StringBuilder sb = new StringBuilder();
            DateTime tglpsg;
            if (reader["TGLPASANG_KWH"].ToString() != "")
                tglpsg = (DateTime)reader["TGLPASANG_KWH"];
            else
                tglpsg = new DateTime();

            StringBuilder alamat = new StringBuilder();
            string tempAlamat = reader["PNJ"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim();
            if (tempAlamat != "")
                alamat.Append(tempAlamat);
            tempAlamat = reader["NAMAPNJ"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim();
            if (tempAlamat != "")
                alamat.Append(" ").Append(tempAlamat);
            tempAlamat = reader["NOBANG"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim();
            if (tempAlamat != "")
                alamat.Append(tempAlamat);

            DateTime tglpdl;
            if (reader["TGLPDL"].ToString() != "")
                tglpdl = (DateTime)reader["TGLPDL"];
            else
                tglpdl = new DateTime();

            DateTime tglnyala;
            if (reader["TGLNYALA_PB"].ToString() != "")
                tglnyala = (DateTime)reader["TGLNYALA_PB"];
            else
                tglnyala = new DateTime();

            DateTime tglrubah;
            if (reader["TGLRUBAH_MK"].ToString() != "")
                tglrubah = (DateTime)reader["TGLRUBAH_MK"];
            else
                tglrubah = new DateTime();

            sb.Append("('")
                .Append(reader["JENIS_MK"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(reader["IDPEL"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(reader["NAMA"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(reader["TARIF"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', ")
                .Append(reader["DAYA"].ToString()).Append(", '")
                .Append(alamat).Append("', '")
                .Append(reader["NOTELP"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(reader["KODEPOS"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(tglpsg.Year.ToString("0000")).Append("-").Append(tglpsg.Month.ToString("00")).Append("-").Append(tglpsg.Day.ToString("00")).Append("', '")
                .Append(reader["KDPEMBMETER"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(reader["MEREK_KWH"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(reader["KDGARDU"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(reader["NOTIANG"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(DilKodeArea).Append("', '")
                .Append(reader["KDDK"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                .Append(tglpdl.Year.ToString("0000")).Append("-").Append(tglpdl.Month.ToString("00")).Append("-").Append(tglpdl.Day.ToString("00")).Append("', '")
                .Append(tglnyala.Year.ToString("0000")).Append("-").Append(tglnyala.Month.ToString("00")).Append("-").Append(tglnyala.Day.ToString("00")).Append("', '")
                .Append(tglrubah.Year.ToString("0000")).Append("-").Append(tglrubah.Month.ToString("00")).Append("-").Append(tglrubah.Day.ToString("00")).Append("'")
                .Append(")");
            return sb;
        }

        public MdbReadResult ImportSorek()
        {
            MdbReadResult result;
            MySqlCommand mycmd = null;
            MySqlDataReader myreader = null;
            try
            {
                int i = 0;
                DateTime start = DateTime.Now, end, startExecuteMySql;
                TimeSpan timeElapsed;

                string sorekTh = SorekBlTh.Substring(2), sorekBl = SorekBlTh.Substring(0, 2), sorekTableName = "SOREK_" + sorekBl + sorekTh;

                ProgressText = "Membaca database. Silakan menunggu.. " + start.ToLongTimeString();
                OnProgressTextChanged(null);

                ProgressText = "Membuka koneksi Ms Access..";
                OnProgressTextChanged(null);

                oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SorekMdbPath);
                oleDbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT IDPEL, TGLBACA, PEMKWH, FAKM, KWHLWBP, KWHWBP, KWHKVARH FROM " + SorekTableName, oleDbConnection);
                OleDbDataReader reader = cmd.ExecuteReader();

                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
                StringBuilder joinSql = new StringBuilder(), selectSql = new StringBuilder();
                List<String> sorekTables = new List<string>();
                bool hitungTren = false;

                #region cek tabel sorek
                try
                {
                    mySqlConnection.Open();
                    
                    ProgressText = "Mengecek keberadaan tabel " + sorekTableName + " di MySql dengan kode area " + SorekKodeArea;
                    OnProgressTextChanged(null);
                    
                    mycmd = mySqlConnection.CreateCommand();
                    mycmd.CommandText = "SHOW TABLES WHERE Tables_in_" + mysqldb + " like '" + sorekTableName + "';";
                    myreader = mycmd.ExecuteReader();
                    if (myreader.Read())
                    {
                        myreader.Close();
                        ProgressText = "Menghapus record sorek di tabel " + sorekTableName + " dengan kode area " + SorekKodeArea;
                        OnProgressTextChanged(null);
                        mycmd.CommandText = "DELETE FROM " + sorekTableName + " WHERE KODEAREA = '" + SorekKodeArea + "';";
                        ProgressText = mycmd.ExecuteNonQuery() + " record telah dihapus";
                        OnProgressTextChanged(null);
                    }
                    else
                    {
                        ProgressText = "Tabel " + sorekTableName + " belum ada.";
                        OnProgressTextChanged(null);
                        myreader.Close();
                    }

                    #region membuat sql join sorek 6 bln
                    mycmd.CommandText = "SHOW TABLES WHERE Tables_in_" + mysqldb + " LIKE 'SOREK_%' AND Tables_in_" + mysqldb + " <  '" + sorekTableName + "';";
                    myreader = mycmd.ExecuteReader();
                    int count = 0;

                    // sorek_0912 s3 left join sorek_0812 s2 on s3.idpel = s2.idpel left join sorek_0712 s1 on s2.idpel = s1.idpel 
                    
                    while (myreader.Read())
                    {
                        sorekTables.Add(myreader[0].ToString());
                        count++;
                    }
                    for (int j = count - 1; j >= 0 && j >= count - 6; j--)
                    {
                        if (j != count - 1)
                        {
                            joinSql.Append(" LEFT JOIN ").Append(sorekTables[j]).Append(" s").Append(j).Append(" ON s").Append(j + 1).Append(".IDPEL = s").Append(j).Append(".IDPEL");
                            selectSql.Append(", ");
                        }
                        else
                        {
                            joinSql.Append(sorekTables[j]).Append(" s").Append(j);
                            hitungTren = true;
                        }
                        selectSql.Append("s").Append(j).Append(".KWHLWBP + s").Append(j).Append(".KWHWBP + s").Append(j).Append(".KWHKVARH as KWH").Append(j);
                    }
                    #endregion

                    mySqlConnection.Close();
                }
                catch (Exception ex)
                {
                    ProgressText = ex.Message;
                    OnProgressTextChanged(null);
                }
                #endregion
                
                batchMode = false; //////// force to non batch mode. 
                if (batchMode)
                {
                    #region

                    using (StreamWriter sorekStreamWriter = new StreamWriter(@dmlSorekFileName))
                    {
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
                                .Append(jamnyala.ToString(cultureInfo)).Append(")");
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

                    if (File.Exists(@mysqlpath))
                    {
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
                    #endregion
                }
                else
                {
                    #region
                    startExecuteMySql = DateTime.Now;
                    ProgressText = "Batch Mode: false";
                    OnProgressTextChanged(null);

                    try
                    {
                        ProgressText = "Membuka koneksi ke server MySql";
                        OnProgressTextChanged(null);

                        mySqlConnection.Open();
                        mycmd = mySqlConnection.CreateCommand();
                        mycmd.Connection = mySqlConnection;

                        ProgressText = "Memulai transaksi";
                        OnProgressTextChanged(null);

                        mycmd.Transaction = mySqlConnection.BeginTransaction();

                        ProgressText = "Membuat tabel " + sorekTableName + "..";
                        OnProgressTextChanged(null);

                        mycmd.CommandText = "" +
                            "CREATE TABLE IF NOT EXISTS `" + sorekTableName + "` (\n" +
                            "  `IDPEL` varchar(12) NOT NULL,\n" +
                            "  `TGLBACA` date default NULL,\n" +
                            "  `PEMKWH` int(11) default NULL,\n" +
                            "  `KODEAREA` varchar(5) default NULL,\n" +
                            "  `JAMNYALA` INT(11) default NULL,\n" +
                            "  `FAKM` varchar(10) default NULL,\n" +
                            "  `KWHLWBP` int(11) default NULL,\n" +
                            "  `KWHWBP` int(11) default NULL,\n" +
                            "  `KWHKVARH` int(11) default NULL,\n" +
                            "  `TREN` enum('naik','turun','flat') NULL,\n" +
                            "  PRIMARY KEY  (`IDPEL`)\n" +
                            ") ENGINE=MyISAM DEFAULT CHARSET=latin1;";
                        mycmd.ExecuteNonQuery();

                        ProgressText = "Mulai memasukkan data..";
                        OnProgressTextChanged(null);

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
                            float daya = -1;
                            while (myreader.Read())
                                daya = float.Parse(myreader["DAYA"].ToString());
                            myreader.Close();
                            float pemkwh = float.Parse(reader["PEMKWH"].ToString()),
                                  jamnyala = (daya == -1) ? -1 : pemkwh * 1000 / daya;

                            string tren = "NULL";
                            if (hitungTren)
                            {
                                float average = 0;
                                mycmd.CommandText = "SELECT " + selectSql.ToString() + " FROM " + joinSql.ToString() + " WHERE s" + (sorekTables.Count - 1) + ".IDPEL = '" + idpel + "';";
                                myreader = mycmd.ExecuteReader();
                                if (myreader.Read())
                                {
                                    for (int j = sorekTables.Count - 1; j >= sorekTables.Count - 6 && j >= 0; j--)
                                    {
                                        try
                                        {
                                            average += Convert.ToInt32(myreader["KWH" + j].ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            ProgressText = ex.Message;
                                            OnProgressTextChanged(null);
                                        }
                                    }
                                    average /= ((sorekTables.Count >= 6) ? 6 : sorekTables.Count);
                                    float kwh = 0;
                                    try
                                    {
                                        kwh += Convert.ToInt32(reader["KWHLWBP"].ToString());
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        kwh += Convert.ToInt32(reader["KWHWBP"].ToString());
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        kwh += Convert.ToInt32(reader["KWHKVARH"].ToString());
                                    }
                                    catch (Exception ex) { }

                                    float ratio = (kwh - average) / average;
                                    if (ratio < -0.25)
                                        tren = "turun";
                                    else if (ratio >= -0.05 && ratio <= 0.05)
                                        tren = "flat";
                                    else if (ratio > 0.25)
                                        tren = "naik";
                                }
                                myreader.Close();
                            }

                            StringBuilder sb = new StringBuilder("INSERT INTO `" + sorekTableName + "` (IDPEL, TGLBACA, PEMKWH, KODEAREA, JAMNYALA, FAKM, KWHLWBP, KWHWBP, KWHKVARH, TREN) VALUES ('");
                            
                            sb
                                .Append(reader["IDPEL"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                                .Append(tglbaca).Append("', ")
                                .Append(pemkwh.ToString(cultureInfo)).Append(", '")
                                .Append(SorekKodeArea).Append("', ")
                                .Append(Math.Round(jamnyala)).Append(", '")
                                .Append(reader["FAKM"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                                .Append(reader["KWHLWBP"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                                .Append(reader["KWHWBP"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', '")
                                .Append(reader["KWHKVARH"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', ")
                                .Append((tren == "NULL") ? tren : "'" + tren + "'").Append(")");

                            mycmd.CommandText = sb.ToString();
                            mycmd.ExecuteNonQuery();

                            string n = (++i).ToString();
                            ProgressText = "Jumlah record:" + n + "";
                            if (i > 1)
                                OnProgressTextChanged(new MdbReaderProgressEventArgs(true, 14, n.Length));
                            else
                                OnProgressTextChanged(null);
                        }

                        ProgressText = "Transaksi berakhir, melakukan commit.";
                        OnProgressTextChanged(null);

                        mycmd.Transaction.Commit();
                        mySqlConnection.Close();
                        result = MdbReadResult.SUCCESS;
                    }
                    catch (Exception ex)
                    {
                        ProgressText = ex.Message;
                        OnProgressTextChanged(null);
                        mycmd.Transaction.Rollback();
                        result = MdbReadResult.FAILED;
                    }
                    end = DateTime.Now;
                    #endregion
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

        public MdbReadResult ImportPpob()
        {
            MdbReadResult result = MdbReadResult.FAILED;
            try
            {
                #region update PpobBlTh
                mySqlConnection.Open();
                MySqlCommand mycmd = new MySqlCommand("SELECT OptionValue FROM `Option` WHERE `OptionKey` = 'PpobBlTh';", mySqlConnection);
                MySqlDataReader myreader = mycmd.ExecuteReader();
                bool blthExists = false;
                if (myreader.Read())
                    blthExists = true;
                myreader.Close();
                if (!blthExists)
                {
                    mycmd.CommandText = "INSERT INTO `Option` VALUES ('PpobBlTh', '" + PpobBlTh + "');";
                    mycmd.ExecuteNonQuery();
                }
                else
                {
                    mycmd.CommandText = "UPDATE `Option` SET `OptionValue` = '" + PpobBlTh + "' WHERE `OptionKey` = 'PpobBlTh';";
                    mycmd.ExecuteNonQuery();
                }
                mySqlConnection.Close();
                #endregion

                int i = 0;
                DateTime start = DateTime.Now, end, startExecuteMySql;
                TimeSpan timeElapsed;

                string ppobTh = PpobBlTh.Substring(2), ppobBl = PpobBlTh.Substring(0, 2);

                ProgressText = "Membaca database. Silakan menunggu.. " + start.ToLongTimeString();
                OnProgressTextChanged(null);

                ProgressText = "Membuka koneksi Ms Access..";
                OnProgressTextChanged(null);

                oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + PpobMdbPath);
                oleDbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT IDPEL, PEMKWH, RPTAG, TGLBAYAR, JAMBAYAR FROM " + PpobTableName + " ORDER BY IDPEL ASC", oleDbConnection);
                OleDbDataReader reader = cmd.ExecuteReader();

                #region truncate ppob
                try
                {
                    ProgressText = "Truncate tabel PPOB di MySql";
                    OnProgressTextChanged(null);
                    mySqlConnection.Open();
                    mycmd = mySqlConnection.CreateCommand();
                    mycmd.CommandText = "TRUNCATE TABLE DPH;";
                    mycmd.ExecuteNonQuery();
                    ProgressText = "Truncate tabel PPOB sukses";
                    OnProgressTextChanged(null);

                    mySqlConnection.Close();
                }
                catch (Exception ex)
                {
                    ProgressText = ex.Message;
                    OnProgressTextChanged(null);
                }
                #endregion
                
                batchMode = false;
                if (batchMode)
                {
                    #region
                    using (StreamWriter dilStreamWriter = new StreamWriter(@dmlPpobFileName))
                    {
                        ProgressText = "Batch Mode: true";
                        OnProgressTextChanged(null);

                        ProgressText = "Mengeksekusi reader dan menulis file dump..";
                        OnProgressTextChanged(null);

                        dilStreamWriter.WriteLine("INSERT INTO DPH (IDPEL, JMLBELI) VALUES ");

                        while (reader.Read())
                        {
                            StringBuilder sb = new StringBuilder();
                            if (i > 0)
                                sb.Append(", ");
                            sb.Append("('")
                                .Append(reader["IDPEL"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim()).Append("', ")
                                .Append(reader["JMLBELI"].ToString()).Append(")");
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

                    if (File.Exists(@mysqlpath))
                    {
                        string pArguments = "/c \"" + mysqlpath + "\" -h" + mysqlhost + " -u" + mysqluser + ((mysqlpass == "") ? "" : " -p" + mysqlpass) + " " + mysqldb + " < " + dmlPpobFileName;

                        ProgressText = "Mengeksekusi MySql. Silakan menunggu..";
                        OnProgressTextChanged(null);

                        startExecuteMySql = DateTime.Now;

                        RunCmd(pArguments);

                        result = MdbReadResult.SUCCESS;
                        end = DateTime.Now;

                        ProgressText = "\nSemua record PPOB berhasil dimasukkan ke MySql. " + end.ToLongTimeString();
                        OnProgressTextChanged(null);
                    }
                    else
                    {
                        result = MdbReadResult.MYSQLNOTFOUND;
                        end = startExecuteMySql = DateTime.Now;
                        ProgressText = "MySql.exe tidak ditemukan. File dump akan disimpan...";
                        OnProgressTextChanged(null);
                    }
                    #endregion
                }
                else
                {
                    #region
                    startExecuteMySql = DateTime.Now;
                    ProgressText = "Batch Mode: false";
                    OnProgressTextChanged(null);

                    try
                    {
                        ProgressText = "Membuka koneksi ke server MySql";
                        OnProgressTextChanged(null);

                        mySqlConnection.Open();
                        mycmd = mySqlConnection.CreateCommand();
                        mycmd.Connection = mySqlConnection;

                        ProgressText = "Memulai transaksi";
                        OnProgressTextChanged(null);

                        mycmd.Transaction = mySqlConnection.BeginTransaction();

                        ProgressText = "Mulai memasukkan data..";
                        OnProgressTextChanged(null);
                        //TODO: from date and time string to datetime, get max datetime, insert to mysql
                        //DataRow dphRow = new 
                        //DataRowBuilder dphRowBuilder = 
                        string idpel = "";
                        DateTime tglbayar = DateTime.MinValue; //tglbayar terakhir
                        float pemkwh = 0, rptag = 0; //pemkwh & rptag terakhir
                        int jmlbeli = 0; //jmlbeli dalam sebulan
                        CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
                        int j = 0;
                        while (reader.Read())
                        {
                            string tempIdpel = reader["IDPEL"].ToString().Replace("'", "''").Replace("\\", "\\\\").Trim();
                            if (j == 0)
                                idpel = tempIdpel;
                            string tglBayarString = reader["TGLBAYAR"].ToString().Trim();
                            string jamBayarString = reader["JAMBAYAR"].ToString().Trim();
                            DateTime tempTglBayar = DateTime.MinValue;
                            if (tglBayarString != "")
                            {
                                int[] tglBayarArrayInt = { int.Parse(tglBayarString.Substring(0, 4)), int.Parse(tglBayarString.Substring(4, 2)), int.Parse(tglBayarString.Substring(6)) };
                                if (jamBayarString != "")
                                {
                                    int[] jamBayarArrayInt = { int.Parse(jamBayarString.Substring(0, 2)), int.Parse(jamBayarString.Substring(2, 2)), int.Parse(jamBayarString.Substring(4)) };
                                    tempTglBayar = new DateTime(tglBayarArrayInt[0], tglBayarArrayInt[1], tglBayarArrayInt[2], jamBayarArrayInt[0], jamBayarArrayInt[1], jamBayarArrayInt[2]);
                                }
                                else
                                {
                                    tempTglBayar = new DateTime(tglBayarArrayInt[0], tglBayarArrayInt[1], tglBayarArrayInt[2]);
                                }
                            }
                            if (idpel == tempIdpel)
                            {
                                if (tglbayar < tempTglBayar)
                                {
                                    tglbayar = tempTglBayar;
                                    pemkwh = float.Parse(reader["PEMKWH"].ToString());
                                    rptag = float.Parse(reader["RPTAG"].ToString());
                                    tglbayar = tempTglBayar;
                                }
                                jmlbeli++;
                            }
                            else
                            {

                                StringBuilder sb = new StringBuilder("INSERT INTO DPH (IDPEL, JMLBELI, PEMKWH, RPTAG, TGLBAYAR) VALUES  ('");

                                sb
                                    .Append(idpel).Append("', ")
                                    .Append(jmlbeli).Append(", ")
                                    .Append(pemkwh.ToString(cultureInfo)).Append(", ")
                                    .Append(rptag.ToString(cultureInfo)).Append(", '")
                                    .Append(tglbayar.ToString("yyyy-MM-dd HH:mm")).Append("'")
                                    .Append(");");

                                mycmd.CommandText = sb.ToString();
                                mycmd.ExecuteNonQuery();

                                idpel = tempIdpel;
                                tglbayar = tempTglBayar;
                                pemkwh = float.Parse(reader["PEMKWH"].ToString());
                                rptag = float.Parse(reader["RPTAG"].ToString());
                                tglbayar = tempTglBayar;
                                jmlbeli = 0;

                                string n = (++i).ToString();
                                ProgressText = "Jumlah record:" + n + "";
                                if (i > 1)
                                    OnProgressTextChanged(new MdbReaderProgressEventArgs(true, 14, n.Length));
                                else
                                    OnProgressTextChanged(null);
                            }
                            j++;
                        }

                        ProgressText = "Transaksi berakhir, melakukan commit.";
                        OnProgressTextChanged(null);

                        mycmd.Transaction.Commit();
                        mySqlConnection.Close();
                        result = MdbReadResult.SUCCESS;
                    }
                    catch (Exception ex)
                    {
                        ProgressText = ex.Message;
                        OnProgressTextChanged(null);
                        mycmd.Transaction.Rollback();
                        result = MdbReadResult.FAILED;
                    }
                    end = DateTime.Now;
                    #endregion
                }

                timeElapsed = end - start;
                ProgressText = "Total waktu: " + timeElapsed.TotalMilliseconds;
                OnProgressTextChanged(null);

                using (StreamWriter dilStreamWriter = new StreamWriter(@logFileName, true))
                {
                    dilStreamWriter.WriteLine("/* ::PPOB convert:: " + DateTime.Now.ToLongDateString());
                    dilStreamWriter.WriteLine("Records: " + i);
                    dilStreamWriter.WriteLine("Start: " + start.ToLongTimeString());
                    dilStreamWriter.WriteLine("Start Execute MySql: " + startExecuteMySql.ToLongTimeString());
                    dilStreamWriter.WriteLine("End: " + end.ToLongTimeString());
                    dilStreamWriter.WriteLine("Time Elapsed: " + timeElapsed.TotalMilliseconds);
                    dilStreamWriter.WriteLine("*/\n");
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
        #endregion
    }
}
