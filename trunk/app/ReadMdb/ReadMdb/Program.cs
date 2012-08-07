using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace ReadMdb
{
    class Program
    {
        static void Main(string[] args)
        {
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\xampp\htdocs\plnwatch\db\mdb\" + args[0]);
            plnwatchEntities plnwatchContext = new plnwatchEntities();
            
            try
            {
                connection.Open();
                String sql = "SELECT * FROM DIL_MAIN";
                OleDbCommand cmd = new OleDbCommand(sql, connection);
                OleDbDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Please wait..");
                while (reader.Read())
                {
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
                }
                connection.Close();
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
