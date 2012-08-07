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
        static void Main(string[] args)
        {
            try
            {
                string kodeArea;
                string blth;
                int n;
                bool cekinput;

                FileStream fslog = File.Open(@"log.txt", FileMode.Create);
                StreamWriter swlog = new StreamWriter(fslog);

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
                kodeArea.Substring(0, 5);
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

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM DIL_MAIN", oleDbConnection);
                OleDbDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Please wait..");
                swlog.WriteLine(DateTime.Now.ToShortTimeString());
                Console.WriteLine(DateTime.Now.ToShortTimeString());
                int i = 0;
                sw.WriteLine("INSERT INTO dil (JENIS_MK, IDPEL, NAMA, TARIF, DAYA, LINGKUNGAN, TGLPASANG, MEREK_KWH, KDGARDU, NOTIANG, KODEAREA) VALUES");
                while (reader.Read())
                {
                    /*
                    dil tdil = new dil
                    {
                        DAYA = int.Parse(reader["DAYA"].ToString()),
                        IDPEL = int.Parse(reader["IDPEL"].ToString()),
                        JENIS_MK = reader["JENIS_MK"].ToString(),
                        KDGARDU = reader["KDGARDU"].ToString(),
                        LINGKUNGAN = reader["LINGKUNGAN"].ToString(),
                        MEREK_KWH = reader["MEREK_KWH"].ToString(),
                        NAMA = reader["NAMA"].ToString(),
                        NOTIANG = reader["NOTIANG"].ToString(),
                        TARIF = reader["TARIF"].ToString(),
                        TGLPASANG = (DateTime) reader["TGLPASANG"]
                    };
                    plnwatchContext.dil.AddObject(tdil);
                    plnwatchContext.SaveChanges();
                     */
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
                      .Append(reader["JENIS_MK"].ToString()).Append("', ")
                      .Append(reader["IDPEL"].ToString()).Append(", '")
                      .Append(reader["NAMA"].ToString().Replace("'", "''")).Append("', '")
                      .Append(reader["TARIF"].ToString()).Append("', ")
                      .Append(reader["DAYA"].ToString()).Append(", '")
                      .Append(reader["LINGKUNGAN"].ToString().Replace("'", "''")).Append("', '")
                      .Append(tglpsg.Year.ToString("0000")).Append("-").Append(tglpsg.Month.ToString("00")).Append("-").Append(tglpsg.Day.ToString("00")).Append("', '")
                      .Append(reader["MEREK_KWH"].ToString().Replace("'", "''")).Append("', '")
                      .Append(reader["KDGARDU"].ToString()).Append("', '")
                      .Append(reader["NOTIANG"].ToString()).Append("', '")
                      .Append(kodeArea).Append("')");
                    sw.WriteLine(sb.ToString());
                    i++;
                }
                sw.Close();
                fs.Close();
                oleDbConnection.Close();
                Console.WriteLine(DateTime.Now.ToShortTimeString());
                swlog.WriteLine(DateTime.Now.ToShortTimeString());
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd.exe", "/c \"C:\\xampp\\mysql\\bin\\mysql.exe\" -hlocalhost -uroot -p plnwatch < " + filename);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                Console.WriteLine(proc.StandardOutput.ReadToEnd());

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
