﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace ReadMdb
{
    class MdbReader
    {
        string mysqlpath;
        string mysqlhost;
        string mysqluser;
        string mysqlpass;
        string mysqldb;

        string blth;
        string kodeArea;
        
        OleDbConnection oleDbConnection;
        MySqlConnection mySqlConnection;
        string dilMdbPath;
        string sorekMdbPath;

        string dmlDilFileName = "dml_dil.sql.tmp";
        string dmlSorekFileName = "dml_sorek.sql.tmp";
        string logFileName = "readmdb.log";

        public MdbReader()
        {
            Initialize();
            mySqlConnection = new MySqlConnection("server=" + mysqlhost + ";User Id=" + mysqluser + ";password=" + mysqlpass + ";database=" + mysqldb);

            bool cekinput;
            do
            {
                Console.Write("Masukkan bulan dan tahun (MMYYYY, kosongkan jika menggunakan bulan dan tahun sekarang): ");
                blth = Console.ReadLine();
                if (blth == "")
                    blth = DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString("0000");
                Console.WriteLine(blth);

                int n;
                if (!(cekinput = ((blth.Length == 6 && int.TryParse(blth, out n)) || blth == "")))
                    Console.WriteLine("Input salah.");
            } while (!cekinput);

            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT ConfigValue FROM config WHERE ConfigKey = 'BlTh';", mySqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();
            bool blthExists = false;
            if (reader.Read())
            {
                reader.Close();
                blthExists = true;
                cmd.CommandText = "UPDATE config SET ConfigValue = '" + blth + "' WHERE ConfigKey = 'BlTh';";
                cmd.ExecuteNonQuery();
            }
            
            if (!blthExists)
            {
                cmd.CommandText = "INSERT INTO config VALUES ('BlTh', '" + blth + "');";
                cmd.ExecuteNonQuery();
            }
            mySqlConnection.Close();

            Console.Write("Masukkan kode area: ");
            kodeArea = Console.ReadLine();
            if (kodeArea.Length > 5) kodeArea.Substring(0, 5);
            Console.WriteLine(kodeArea + "\n");

            ReadDil();
        }

        void Initialize()
        {
            mysqlpath = System.Configuration.ConfigurationSettings.AppSettings["mysqlpath"];
            mysqlhost = System.Configuration.ConfigurationSettings.AppSettings["mysqlhost"];
            mysqluser = System.Configuration.ConfigurationSettings.AppSettings["mysqluser"];
            mysqlpass = System.Configuration.ConfigurationSettings.AppSettings["mysqlpass"];
            mysqldb = System.Configuration.ConfigurationSettings.AppSettings["mysqldb"];
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
                Console.WriteLine(e.Data);
        }

        void ReadDil()
        {
            Console.WriteLine("Pilih file database DIL dengan ekstensi *.mdb..");
            dilMdbPath = FileBrowse();
            Console.WriteLine("File dipilih: " + dilMdbPath);
            
            try
            {
                int i = 0;
                if (!File.Exists(@dmlDilFileName))
                    File.Create(@dmlDilFileName).Close();
                DateTime start = DateTime.Now;
                Console.WriteLine("Membaca database. Silakan menunggu.. " + start.ToLongTimeString());
                using (StreamWriter dilStreamWriter = new StreamWriter(@dmlDilFileName))
                {

                    oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dilMdbPath);
                    oleDbConnection.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, NOTELP, KODEPOS, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG FROM DIL_MAIN", oleDbConnection);
                    OleDbDataReader reader = cmd.ExecuteReader();

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
                            .Append(reader["JENIS_MK"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', ")
                            .Append(reader["IDPEL"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append(", '")
                            .Append(reader["NAMA"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["TARIF"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', ")
                            .Append(reader["DAYA"].ToString()).Append(", '")
                            .Append(reader["PNJ"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["NAMAPNJ"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["NOBANG"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["RT"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["RW"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["LINGKUNGAN"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["NOTELP"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["KODEPOS"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(tglpsg.Year.ToString("0000")).Append("-").Append(tglpsg.Month.ToString("00")).Append("-").Append(tglpsg.Day.ToString("00")).Append("', '")
                            .Append(reader["MEREK_KWH"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["KDGARDU"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(reader["NOTIANG"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                            .Append(kodeArea).Append("')");
                        dilStreamWriter.WriteLine(sb.ToString());
                        Program.ClearCurrentConsoleLine(15);
                        Console.Write("[Jumlah record:" + ++i + "]");
                    }
                    dilStreamWriter.WriteLine(";");
                }

                Process p = new Process();
                string pArguments = "/c \"" + mysqlpath + "\" -h" + mysqlhost + " -u" + mysqluser + ((mysqlpass == "") ? "" : " -p" + mysqlpass) + " " + mysqldb + " < " + dmlDilFileName;
                Console.WriteLine("\nMengeksekusi MySql. Silakan menunggu..\n");
                p.StartInfo = new ProcessStartInfo("cmd.exe", pArguments)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                p.Start();
                p.OutputDataReceived += new DataReceivedEventHandler(ProcessOutputReceived);
                p.BeginOutputReadLine();
                p.WaitForExit();

                DateTime end = DateTime.Now;
                TimeSpan timeElapsed = end - start;
                Console.WriteLine("\nSemua record DIL berhasil dimasukkan ke MySql. " + end.ToLongTimeString());
                Console.WriteLine("Total waktu: " + timeElapsed.TotalMilliseconds);

                if (!File.Exists(@logFileName))
                    File.Create(@logFileName).Close();
                using (StreamWriter dilStreamWriter = new StreamWriter(@logFileName, true))
                {
                    dilStreamWriter.WriteLine("/* ::DIL convert:: " + DateTime.Now.ToLongDateString());
                    dilStreamWriter.WriteLine("Records: " + i);
                    dilStreamWriter.WriteLine("Start: " + start.ToLongTimeString());
                    dilStreamWriter.WriteLine("End: " + end.ToLongTimeString());
                    dilStreamWriter.WriteLine("Time Elapsed: " + timeElapsed.TotalMilliseconds);
                    dilStreamWriter.WriteLine("*/\n");
                }

                Console.WriteLine("Apakah Anda ingin menyimpan file DML DIL? (y/n)");

                string input;
                while ((input = Console.ReadLine()) != "")
                {
                    if (input == "y")
                    {
                        Console.WriteLine("Tulis nama file: ");
                        string newfilename = Console.ReadLine();
                        File.Move(dmlDilFileName, newfilename + ".sql");
                        break;
                    }
                    if (input == "n") break;
                }

                oleDbConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("Tekan enter..");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }

        void ReadSorek()
        {
            //Console.WriteLine("Pilih file database SOREK dengan ekstensi *.mdb..");
            //sorekMdbPath = FileBrowse();
            //Console.WriteLine("File dipilih: " + sorekMdbPath);

            //try
            //{
            //    int i = 0;
            //    if (!File.Exists(@dmlSorekFileName))
            //        File.Create(@dmlSorekFileName).Close();
            //    DateTime start = DateTime.Now;
            //    Console.WriteLine("Membaca database. Silakan menunggu.. " + start.ToLongTimeString());
            //    using (StreamWriter sorekStreamWriter = new StreamWriter(@dmlSorekFileName))
            //    {

            //        oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sorekMdbPath);
            //        oleDbConnection.Open();
            //        OleDbCommand cmd = new OleDbCommand("SELECT BLTH, IDPEL, TGLBACA, PEMKWH, KODEAREA FROM DIL_MAIN", oleDbConnection);
            //        OleDbDataReader reader = cmd.ExecuteReader();

            //        sorekStreamWriter.WriteLine("INSERT INTO sorek (BLTH, IDPEL, TGLBACA, PEMKWH, KODEAREA) VALUES ");

            //        while (reader.Read())
            //        {
            //            DateTime tglpsg;
            //            if (reader["TGLPASANG_KWH"].ToString() != "")
            //                tglpsg = (DateTime)reader["TGLPASANG_KWH"];
            //            else
            //                tglpsg = new DateTime();

            //            StringBuilder sb = new StringBuilder();
            //            if (i > 0)
            //                sb.Append(", ");
            //            sb.Append("('")
            //                .Append(reader["JENIS_MK"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', ")
            //                .Append(reader["IDPEL"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append(", '")
            //                .Append(reader["NAMA"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["TARIF"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', ")
            //                .Append(reader["DAYA"].ToString()).Append(", '")
            //                .Append(reader["PNJ"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["NAMAPNJ"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["NOBANG"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["RT"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["RW"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["LINGKUNGAN"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["NOTELP"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["KODEPOS"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(tglpsg.Year.ToString("0000")).Append("-").Append(tglpsg.Month.ToString("00")).Append("-").Append(tglpsg.Day.ToString("00")).Append("', '")
            //                .Append(reader["MEREK_KWH"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["KDGARDU"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(reader["NOTIANG"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
            //                .Append(kodeArea).Append("')");
            //            sorekStreamWriter.WriteLine(sb.ToString());
            //            Program.ClearCurrentConsoleLine(15);
            //            Console.Write("[Jumlah record:" + ++i + "]");
            //        }
            //        sorekStreamWriter.WriteLine(";");
            //    }

            //    Process p = new Process();
            //    string pArguments = "/c \"" + mysqlpath + "\" -h" + mysqlhost + " -u" + mysqluser + ((mysqlpass == "") ? "" : " -p" + mysqlpass) + " " + mysqldb + " < " + dmlDilFileName;
            //    Console.WriteLine("\nMengeksekusi MySql. Silakan menunggu..\n");
            //    p.StartInfo = new ProcessStartInfo("cmd.exe", pArguments)
            //    {
            //        RedirectStandardOutput = true,
            //        UseShellExecute = false,
            //        CreateNoWindow = true
            //    };
            //    p.Start();
            //    p.OutputDataReceived += new DataReceivedEventHandler(ProcessOutputReceived);
            //    p.BeginOutputReadLine();
            //    p.WaitForExit();

            //    DateTime end = DateTime.Now;
            //    TimeSpan timeElapsed = end - start;
            //    Console.WriteLine("\nSemua record DIL berhasil dimasukkan ke MySql. " + end.ToLongTimeString());
            //    Console.WriteLine("Total waktu: " + timeElapsed.TotalMilliseconds);

            //    if (!File.Exists(@logFileName))
            //        File.Create(@logFileName).Close();
            //    using (StreamWriter sorekStreamWriter = new StreamWriter(@logFileName, true))
            //    {
            //        sorekStreamWriter.WriteLine("/* ::DIL convert:: " + DateTime.Now.ToLongDateString());
            //        sorekStreamWriter.WriteLine("Records: " + i);
            //        sorekStreamWriter.WriteLine("Start: " + start.ToLongTimeString());
            //        sorekStreamWriter.WriteLine("End: " + end.ToLongTimeString());
            //        sorekStreamWriter.WriteLine("Time Elapsed: " + timeElapsed.TotalMilliseconds);
            //        sorekStreamWriter.WriteLine("*/\n");
            //    }

            //    Console.WriteLine("Apakah Anda ingin menyimpan file DML DIL? (y/n)");

            //    string input;
            //    while ((input = Console.ReadLine()) != "")
            //    {
            //        if (input == "y")
            //        {
            //            Console.WriteLine("Tulis nama file: ");
            //            string newfilename = Console.ReadLine();
            //            File.Move(dmlSorekFileName, newfilename + ".sql");
            //            break;
            //        }
            //        if (input == "n") break;
            //    }

            //    oleDbConnection.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Console.Write("Tekan enter..");
            //while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }
    }
}
