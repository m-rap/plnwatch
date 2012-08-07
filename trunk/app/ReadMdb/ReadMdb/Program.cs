using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace ReadMdb
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
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
                int i = 0;
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
                    Console.WriteLine("Please wait.. " + i++);
                    DateTime tglpsg;
                    if (reader["TGLPASANG_KWH"].ToString() != "")
                        tglpsg = (DateTime)reader["TGLPASANG_KWH"];
                    else
                        tglpsg = new DateTime();
                    string mySql = "INSERT INTO dil (DAYA, IDPEL, JENIS_MK, KDGARDU, LINGKUNGAN, MEREK_KWH, NAMA, NOTIANG, TARIF, TGLPASANG) VALUES (" + reader["DAYA"].ToString() + ", " + reader["IDPEL"].ToString() + ", '" + reader["JENIS_MK"].ToString() + "', '" + reader["KDGARDU"].ToString() + "', '" + reader["LINGKUNGAN"].ToString() + "', '" + reader["MEREK_KWH"].ToString() + "', '" + reader["NAMA"].ToString() + "', '" + reader["NOTIANG"].ToString() + "', '" + reader["TARIF"].ToString() + "', '" + tglpsg.Year.ToString("0000") + "-" + tglpsg.Month.ToString("00") + "-" + tglpsg.Day.ToString("00") + "');";
                    //MySqlCommand myCmd = new MySqlCommand(mySql, mySqlConnection);
                    //myCmd.ExecuteNonQuery();
                    sw.WriteLine(mySql);
                }
                sw.Close();
                fs.Close();
                oleDbConnection.Close();
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
