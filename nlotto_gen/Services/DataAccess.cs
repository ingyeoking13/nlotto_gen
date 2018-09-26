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
        static DataAccess()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=db.db"))
            {
                db.Open();
                string tableCommand = "CREATE TABLE IF NOT " +
                           "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                           "Text_Entry NVARCHAR(2048) NULL );";
                SqliteCommand cmd = new SqliteCommand(tableCommand, db);
                cmd.ExecuteReader();
            }
        }

        public async static void Insert(string str)
        {
            using(SqliteConnection db = new SqliteConnection("Filename=db.db"))
            {
                await db.OpenAsync();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = db;
                cmd.CommandText = "insert into MyTable values (NULL, @SEX);";
                cmd.Parameters.AddWithValue("@SEX", str);
                cmd.ExecuteReader();
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
                        entries.Add(query.GetString(1));
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

    }
}
