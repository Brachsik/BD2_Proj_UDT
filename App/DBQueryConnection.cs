using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace projectUDT_app
{
    public class DBQueryConnection
    {
        private SqlConnection conn;
        private static string sqlconn_str;

        public DBQueryConnection()
        {

            sqlconn_str = @"Data Source=WINSERV01;Initial Catalog=ProjektBD;Integrated Security=True";
            conn = new SqlConnection(sqlconn_str);
        }

        public void SelectQuery(string query, List<string> attributes)
        {
            try
            {
                this.conn.Open();
                //Console.WriteLine(query);
                SqlCommand c = new SqlCommand(query, this.conn);
                SqlDataReader reader = c.ExecuteReader();

                bool flag = true;
                while (reader.Read())
                {
                    flag = false;
                    foreach (var element in attributes)
                    {
                        Console.WriteLine(reader[element].ToString());
                    }
                    Console.WriteLine();
                }
                if (flag) throw new ArgumentException("Brak danych!");

            }
            catch (SqlException e)
            {
                Console.WriteLine("Brak polaczenia z baza!");
                throw new ArgumentException(e.Message.Split('$')[1]);
            }
            finally
            {
                this.conn.Close();
            }
        }

        public void InsertQuery(string query)
        {
            try
            {
                this.conn.Open();
                //Console.WriteLine(query);
                SqlCommand c = new SqlCommand(query, this.conn);
                SqlDataReader reader = c.ExecuteReader();
                throw new ArgumentException("Pomyslnie wprowadzono nowe dane!");
            }
            catch (SqlException e)
            {
                throw new ArgumentException(e.Message.Split('$')[1]);
            }
            finally
            {
                this.conn.Close();
            }
        }

        public void DeleteQuery(string query)
        {
            try
            {
                this.conn.Open();
                //Console.WriteLine(query);
                SqlCommand c = new SqlCommand(query, this.conn);
                int reader = c.ExecuteNonQuery();
                if (reader != 0)
                {
                    throw new ArgumentException("Pomyslne usuniecie liczby rekordow: " + reader.ToString());
                }
                else
                {
                    throw new ArgumentException("Brak danych do usuniecia!");
                }

            }
            catch (SqlException e)
            {
                throw new ArgumentException(e.Message.Split('$')[1]);
            }
            finally
            {
                this.conn.Close();
            }
        }

    }
}