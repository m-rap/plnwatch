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
        string dilMdbPath;
        OleDbConnection oleDbConnection;
        MySqlConnection mySqlConnection;

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
            while ((dilMdbPath = FileBrowse()) == null);
            Console.WriteLine("File dipilih: " + dilMdbPath);

            try
            {
                oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dilMdbPath);
                oleDbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, NOTELP, KODEPOS, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG FROM DIL_MAIN", oleDbConnection);
                OleDbDataReader reader = cmd.ExecuteReader();

                MySqlTransaction myTransaction = null;
                try
                {
                    DateTime start = DateTime.Now;
                    Console.WriteLine("Membaca database. Silakan menunggu.. " + start.ToShortTimeString());

                    mySqlConnection.Open();
                    myTransaction = mySqlConnection.BeginTransaction();
                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = mySqlConnection;
                    myCmd.Transaction = myTransaction;

                    int i = 0;
                    while (reader.Read() && i < 1000)
                    {
                        DateTime tglpsg;
                        if (reader["TGLPASANG_KWH"].ToString() != "")
                            tglpsg = (DateTime)reader["TGLPASANG_KWH"];
                        else
                            tglpsg = new DateTime();

                        StringBuilder sb = new StringBuilder();
                        sb.Append("INSERT INTO dil (JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, NOTELP, KODEPOS, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG, KODEAREA) VALUES ('")
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
                        myCmd.CommandText = sb.ToString();
                        myCmd.ExecuteNonQuery();
                        Program.ClearCurrentConsoleLine();
                        Console.Write("[Jumlah record:" + ++i + "]");
                    }

                    myTransaction.Commit();
                    mySqlConnection.Close();
                    DateTime end = DateTime.Now;
                    Console.WriteLine("\nSemua record DIL berhasil dimasukkan ke MySql. " + end.ToShortTimeString());
                    Console.WriteLine("Total waktu: " + (end - start).TotalMilliseconds);
                }
                catch (Exception ex)
                {
                    try
                    {
                        myTransaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine(ex2.Message);
                    }
                    Console.WriteLine(ex.Message);
                }
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
