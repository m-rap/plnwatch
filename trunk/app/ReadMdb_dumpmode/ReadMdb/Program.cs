using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;

namespace ReadMdb
{
    class Program
    {
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static void Main(string[] args)
        {
            try
            {
                string kodeArea;
                string blth;
                int n;
                bool cekinput;

                string logfilename = "log.txt";

                if (File.Exists(@logfilename))
                {
                    File.Delete(@logfilename);
                    File.Create(@logfilename);
                }
                StreamWriter swlog = new StreamWriter(@logfilename, true);

                do
                {
                    Console.Write("Masukkan tahun dan bulan (YYYYMM, kosongkan jika menggunakan tahun dan bulan sekarang): ");
                    blth = Console.ReadLine();
                    Console.WriteLine(blth);
                    if (!(cekinput = ((blth.Length == 6 && int.TryParse(blth, out n)) || blth == "")))
                    {
                        Console.WriteLine("Input salah.");
                    }
                } while (!cekinput);
                
                Console.Write("Masukkan kode area: ");
                kodeArea = Console.ReadLine();
                if (kodeArea.Length > 5) kodeArea.Substring(0, 5);
                Console.WriteLine(kodeArea);


                /******* DIL ******/
                OleDbConnection oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=dil.mdb");
                //plnwatchEntities plnwatchContext = new plnwatchEntities();
                MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;User Id=root;database=plnwatch");
                string filename = "dml_dil.sql";
                FileStream fs = File.Open(@filename, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                oleDbConnection.Open();
                mySqlConnection.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG, KODEAREA FROM DIL_MAIN", oleDbConnection);
                OleDbDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Please wait..");
                
                swlog.WriteLine(DateTime.Now.ToShortTimeString());
                swlog.Close();
                swlog = new StreamWriter(@logfilename, true);

                int i = 0;
                sw.WriteLine("INSERT INTO dil (JENIS_MK, IDPEL, NAMA, TARIF, DAYA, PNJ, NAMAPNJ, NOBANG, RT, RW, LINGKUNGAN, TGLPASANG_KWH, MEREK_KWH, KDGARDU, NOTIANG, KODEAREA) VALUES");
                while (reader.Read())
                {
                    Console.Clear();
                    Console.WriteLine("Please wait.. " + i);
                    DateTime tglpsg;
                    if (reader["TGLPASANG_KWH"].ToString() != "")
                        tglpsg = (DateTime)reader["TGLPASANG_KWH"];
                    else
                        tglpsg = new DateTime();
                    
                    //string mySql = "INSERT INTO dil (DAYA, IDPEL, JENIS_MK, KDGARDU, LINGKUNGAN, MEREK_KWH, NAMA, NOTIANG, TARIF, TGLPASANG) VALUES (" + reader["DAYA"].ToString() + ", " + reader["IDPEL"].ToString() + ", '" + reader["JENIS_MK"].ToString() + "', '" + reader["KDGARDU"].ToString() + "', '" + reader["LINGKUNGAN"].ToString() + "', '" + reader["MEREK_KWH"].ToString() + "', '" + reader["NAMA"].ToString() + "', '" + reader["NOTIANG"].ToString() + "', '" + reader["TARIF"].ToString() + "', '" + tglpsg.Year.ToString("0000") + "-" + tglpsg.Month.ToString("00") + "-" + tglpsg.Day.ToString("00") + "');";
                    //MySqlCommand myCmd = new MySqlCommand(mySql, mySqlConnection);
                    //myCmd.ExecuteNonQuery();
                    
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
                      .Append(tglpsg.Year.ToString("0000")).Append("-").Append(tglpsg.Month.ToString("00")).Append("-").Append(tglpsg.Day.ToString("00")).Append("', '")
                      .Append(reader["MEREK_KWH"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                      .Append(reader["KDGARDU"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                      .Append(reader["NOTIANG"].ToString().Replace("'", "''").Replace("\\", "\\\\")).Append("', '")
                      .Append(kodeArea).Append("')");
                    sw.WriteLine(sb.ToString());
                    i++;
                }
                sw.WriteLine(";");
                sw.Close();
                fs.Close();
                oleDbConnection.Close();
                
                swlog.WriteLine(DateTime.Now.ToShortTimeString());
                sw.Close();
                swlog = new StreamWriter(@logfilename, true);

                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd.exe", "/c \"C:\\xampp\\mysql\\bin\\mysql.exe\" -hlocalhost -uroot -p plnwatch < " + filename);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                Console.WriteLine(proc.StandardOutput.ReadToEnd());
                proc.WaitForExit();
                proc.Close();

                Console.WriteLine(DateTime.Now.ToShortTimeString());
                swlog.WriteLine(DateTime.Now.ToShortTimeString());
                swlog.Close();

                
                /******* SOREK ******/
                //oleDbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sorek.mdb");
                //filename = "dml_sorek.sql";
                //fs = File.Open(@filename, FileMode.Create);
                //sw = new StreamWriter(fs);
                

                mySqlConnection.Close();
                Console.WriteLine("Export completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
