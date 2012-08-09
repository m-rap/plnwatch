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

        string dmlDilFileName = "dml_dil.sql";
        StreamWriter dilStreamWriter;

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
                if (File.Exists(@dmlDilFileName))
                {
                    File.Delete(@dmlDilFileName);
                    File.Create(@dmlDilFileName);
                }
                dilStreamWriter = new StreamWriter(@dmlDilFileName, false);

                oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dilMdbPath);
                oleDbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, NOTELP, KODEPOS, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG FROM DIL_MAIN", oleDbConnection);
                OleDbDataReader reader = cmd.ExecuteReader();

                DateTime start = DateTime.Now;
                Console.WriteLine("Membaca database. Silakan menunggu.. " + start.ToShortTimeString());

                dilStreamWriter.WriteLine("INSERT INTO dil (JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, NOTELP, KODEPOS, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG, KODEAREA) VALUES ");
                int i = 0;
                while (reader.Read() && i < 1000)
                {
                    Program.ClearCurrentConsoleLine();
                    Console.Write("[Jumlah record:" + i + "]");
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
                    i++;
                }
                dilStreamWriter.WriteLine(";");
                dilStreamWriter.Close();

                mySqlConnection.Open();
                MySqlScript myScript = new MySqlScript(mySqlConnection, File.ReadAllText(@dmlDilFileName));
                myScript.Execute();
                mySqlConnection.Close();
                oleDbConnection.Close();

                DateTime end = DateTime.Now;
                Console.WriteLine("\nSemua record DIL berhasil dimasukkan ke MySql. " + end.ToShortTimeString());
                Console.WriteLine("Total waktu: " + (end - start).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ReadDil();
            }
            Console.WriteLine("Tekan enter..");
            Console.ReadKey();
        }
    }
}
