using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace ReadMdb
{
    class MdbReader
    {
        string blth;
        string kodeArea;
        
        OleDbConnection oleDbConnection;
        MySqlConnection mySqlConnection;
        string dilMdbPath;

        string dmlDilFileName = "dml_dil.sql.tmp";
        string logFileName = "readmdb.log";

        public MdbReader()
        {
            bool cekinput;
            do
            {
                Console.Write("Masukkan bulan dan tahun (MMYYY, kosongkan jika menggunakan bulan dan tahun sekarang): ");
                blth = Console.ReadLine();
                Console.WriteLine(blth);

                int n;
                if (!(cekinput = ((blth.Length == 6 && int.TryParse(blth, out n)) || blth == "")))
                    Console.WriteLine("Input salah.");
            } while (!cekinput);

            Console.Write("Masukkan kode area: ");
            kodeArea = Console.ReadLine();
            if (kodeArea.Length > 5) kodeArea.Substring(0, 5);
            Console.WriteLine(kodeArea + "\n");

            mySqlConnection = new MySqlConnection("server=localhost;User Id=root;database=plnwatch");
            ReadDil();
        }

        string FileBrowse()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;
            return null;
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
                    
                    while (reader.Read() && i < 1000)
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
                        Program.ClearCurrentConsoleLine();
                        Console.Write("[Jumlah record:" + ++i + "]");
                    }
                    dilStreamWriter.WriteLine(";");
                }

                mySqlConnection.Open();
                MySqlScript myScript = new MySqlScript(mySqlConnection, File.ReadAllText(@dmlDilFileName));
                myScript.Execute();

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
                

                Console.WriteLine("Apakah Anda ingin menyimpan file DML? (y/n)");

                string input;
                while ((input = Console.ReadLine()) != "")
                {
                    if (input == "y")
                    {
                        Console.WriteLine("Tulis nama file: ");
                        string newfilename = Console.ReadLine();
                        File.Copy(dmlDilFileName, newfilename + ".sql");
                        File.Delete(@dmlDilFileName);
                        break;
                    }
                    if (input == "n") break;
                }

                mySqlConnection.Close();
                oleDbConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("Tekan enter..");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }
    }
}
