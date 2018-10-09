using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace nlotto_gen.Services
{
    public static class DataAccess
    {
        //public static int count;
        static DataAccess()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=db.db"))
            {
                db.Open();
                string tableCommand = "CREATE TABLE IF NOT " +
                           "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                           "Entry1 tinyint NULL, " +
                           "Entry2 tinyint NULL, " +
                           "Entry3 tinyint NULL, " + 
                           "Entry4 tinyint NULL, " +
                           "Entry5 tinyint NULL, "+ 
                           "Entry6 tinyint NULL, "+
                           "EntryNum int NULL" +
                           ");";
                SqliteCommand cmd = new SqliteCommand(tableCommand, db);
                cmd.ExecuteReader();

                cmd = new SqliteCommand("select count(*) from MyTable", db);
                //count =(int)cmd.ExecuteScalar();
            }
        }

        public async static void Insert(int a, int b, int c, int d, int e, int f, int n)
        {
            using(SqliteConnection db = new SqliteConnection("Filename=db.db"))
            {
                await db.OpenAsync();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = db;
                cmd.CommandText = "insert into MyTable values (NULL, @one, @two, @three, @four, @five, @six, @num);";
                cmd.Parameters.AddWithValue("@one", a);
                cmd.Parameters.AddWithValue("@two", b);
                cmd.Parameters.AddWithValue("@three", c);
                cmd.Parameters.AddWithValue("@four", d);
                cmd.Parameters.AddWithValue("@five", e);
                cmd.Parameters.AddWithValue("@six", f);
                cmd.Parameters.AddWithValue("@num", n);
                cmd.ExecuteReader();
                //count++;
            }
        }

        public async static Task<List<string>> Query(string str)
        {
            List<string> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection("Filename=db.db"))
            { 
                await db.OpenAsync();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = db;
                cmd.CommandText = str;
                //try
                {
                    SqliteDataReader query = cmd.ExecuteReader();
                    while(query.Read())
                    {
                        entries.Add(
                            query.GetString(1) + "  " + query.GetString(2) + "  " + query.GetString(3)
                            + "  " +
                            query.GetString(4) + "  " + query.GetString(5) + "  " + query.GetString(6)
                            + " number : " + query.GetString(7)
                            );
                    }
                }
                //catch { }
            }
            return entries;
        }

        public async static void Drop()
        {
            using(SqliteConnection db = new SqliteConnection("Filename=db.db"))
            {
                await db.OpenAsync();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = db;
                cmd.CommandText = "delete from MyTable;";
                cmd.ExecuteReader();
            }
        }

        public async static Task<List<int>> ListofTask(int mxGameNumber) // html 가져올 page return
        {
            List<int> ret = new List<int>();
            int bef = 0;
            using (SqliteConnection db = new SqliteConnection("Filename=db.db"))
            {
                await db.OpenAsync();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = db;
                cmd.CommandText = "select * from MyTable order by EntryNum";
//                cmd.CommandText = "select * from MyTable";
                SqliteDataReader query = await cmd.ExecuteReaderAsync();
                while(query.Read())
                {
                    string now = query.GetString(7);
                    int nown = Int32.Parse(now);
                    if (nown != bef +1)
                    {
                        for(int i=bef+1; i<nown; i++)
                        {
                            ret.Add(i);
                        }
                    }
                    bef = Int32.Parse(now);
                }
            }
            for (int i = bef + 1; i <= mxGameNumber; i++) ret.Add(i);
            return ret;
        }
    }
}
