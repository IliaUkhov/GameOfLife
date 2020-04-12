using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace gameoflife
{
    public static class DBConnector
    {
        private static MySqlConnection connection;

        public static void Connect()
        {
            string server   = "35.228.218.72";
            string database = "game_of_life_db";
            string user     = "root";
            string password = "sample_password";
            string sslM     = "none";

            string connectionString = string.Format(
            "server={0}; userid={1}; password={2}; database={3}; SslMode={4}",
                 server, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (MySqlException)
            {
                System.Diagnostics.Debug.WriteLine("Connection failed");
                Console.WriteLine("Connection failed");
                connection.Close();
            }
        }

        public static List<(int, string, string, string, string)> GetUsers()
        {
            string query = "select * from users;";

            MySqlCommand command = new MySqlCommand(query, connection);

            var list = new List<(int, string, string, string, string)>();

            try
            {
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add((  reader.GetInt32(0), 
                                reader.GetString(1), 
                                reader.GetString(2), 
                                reader.GetString(3),
                                reader.GetString(4)
                                ));
                }
                   
            }
            catch (MySqlException)
            {
                LogQueryError();
            }

            return list;
        }

        public static void AddUser(string name, string email, string pwd)
        {
            string date = DateTime.Now.ToString("yy-MM-dd hh:mm:ss");
            string query = string.Format(
            "insert into users (name, email, password, regdate) values ('{0}', '{1}', '{2}', '{3}');",
                 name, email, pwd, date);

            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                LogQueryError();
            }
        }

        public static (int, string) CheckLogin(string email, string pwd)
        {
            string query = string.Format(
                "select id, name, password from users where email = '{0}';", email);

            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.GetString(2) == pwd)
                    {
                        return (reader.GetInt32(0), reader.GetString(1));
                    }
                }

            }
            catch (MySqlException)
            {
                LogQueryError();
            }

            return (-1, "");
        }

        public static void UploadPattern(int userId, string description, string cells, 
                int patternHeight, int patternWidth)
        {

            string query = string.Format(
                "insert into patterns (author, description, cells, height, width) " +
                "values ({0}, '{1}', '{2}', {3}, {4});",
                userId, description, cells, patternHeight, patternWidth);

            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

            }
            catch (MySqlException)
            {
                LogQueryError();
            }
        }

        public static List<(int, string, string)> GetPatternList()
        {
            string query = "select p.id, u.name, p.description " +
            	"from users as u " +
            	"right join patterns as p on u.id = p.author;";

            var list = new List<(int, string, string)>();

            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add((  reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                        ));
                }
            }
            catch (MySqlException)
            {
                LogQueryError();
            }

            return list;
        }

        public static (string, int, int) GetPattern(int id)
        {
            string query = string.Format("select cells, height, width from patterns where id = {0};", id);

            (string, int, int) pattern = ("", 0, 0);

            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    pattern.Item1 = reader.GetString(0);
                    pattern.Item2 = reader.GetInt32(1);
                    pattern.Item3 = reader.GetInt32(2);

                    System.Diagnostics.Debug.WriteLine("Pattern:" + pattern.Item1);
                }
            }
            catch (MySqlException)
            {
                LogQueryError();
            }

            return pattern;
        }

        public static List<(string, string)> GetComments()
        {
            string query = "select author, text from comments";

            var list = new List<(string, string)>();

            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add((  reader.GetString(0),
                                reader.GetString(1)
                        ));
                }
            }
            catch (MySqlException)
            {
                LogQueryError();
            }

            return list;
        }

        public static void UploadComment(string author, string text)
        {
            string query = string.Format(
                "insert into comments (author, text) " +
                "values ('{0}', '{1}');", author, text);

            try
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

            }
            catch (MySqlException)
            {
                LogQueryError();
            }
        }

        public static void Disconnect()
        {
            connection.Close();
        }

        private static void LogQueryError()
        {
            System.Diagnostics.Debug.WriteLine("Query error");
            Console.WriteLine("Query error");
        }
    }
}
