using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace classes
{
    public class DatabaseConnection
    {
        private static string server = "localhost";
        private static string database = "prodavnica";
        private static string port = "3360";
        private static string user = "root";
        private static string password = "";
        private static MySqlConnection conn = null;

        public static string Server
        {
            get
            {
                return server;
            }
            set
            {
                server = value;
            }
        }
        public static string Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }
        public static string Database
        {
            get
            {
                return database;
            }
            set
            {
                database = value;
            }
        }
        public static string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        public static string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public static void testConnection()
        {
            string connStr = $"server={server};user={user};database={database};port=3306;password={password};convert zero datetime=True";
            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public static bool createConnection()
        {
            if (conn == null)
            {
                string connStr = $"server={server};user={user};database={database};port=3306;password={password};convert zero datetime=True";
                conn = new MySqlConnection(connStr);
                conn.Open();
                return true;
            }
            return false;
        }

        public static void closeConnection()
        {
            conn.Close();
            conn = null;
        }

        //"SELECT username, password FROM users WHERE usenrame = @username";
        public static MySqlDataReader select(string query, Dictionary<string, string> parameters)
        {
            createConnection();
            MySqlDataReader rdr = null;

            MySqlCommand cmd = new MySqlCommand(query, conn);
            foreach (var key in parameters.Keys)
            {
                cmd.Parameters.AddWithValue($"@{key}", parameters[key]);
            }

            rdr = cmd.ExecuteReader();

            //rdr.Close();

            //conn.Close();
            return rdr;
        }

        //kopija select naredbe sa  Dictionary<string, int>
        public static MySqlDataReader select2(string query, Dictionary<string, int> parameters)
        {
            createConnection();
            MySqlDataReader rdr = null;

            MySqlCommand cmd = new MySqlCommand(query, conn);
            foreach (var key in parameters.Keys)
            {
                cmd.Parameters.AddWithValue($"@{key}", parameters[key]);
            }

            rdr = cmd.ExecuteReader();

            //rdr.Close();

            //conn.Close();
            return rdr;
        }







        //INSERT INTO users (username, password) VALUES (@username, @password)";
        public static void insert(string query, Dictionary<string, string> parameters)
        {
            createConnection();

            MySqlCommand cmd = new MySqlCommand(query, conn);
            foreach (var key in parameters.Keys)
            {
                cmd.Parameters.AddWithValue($"@{key}", parameters[key]);
            }

            cmd.ExecuteNonQuery();

            closeConnection();
        }

        //kopija insert naredbe sa  Dictionary<string, int>
        public static void insert2(string query, Dictionary<string, int> parameters)
        {
            createConnection();

            MySqlCommand cmd = new MySqlCommand(query, conn);
            foreach (var key in parameters.Keys)
            {
                cmd.Parameters.AddWithValue($"@{key}", parameters[key]);
            }

            cmd.ExecuteNonQuery();

            closeConnection();
        }


        //UPDATE users SET password=@password WHERE username=@username";
        public static void update(string query, Dictionary<string, string> parameters)
        {
            insert(query, parameters);
        }

        //kopija update naredbe sa  Dictionary<string, int>
        public static void update2(string query, Dictionary<string, int> parameters)
        {
            insert2(query, parameters);
        }



        //DELETE users WHERE username=@username";
        public static void delete(string query, Dictionary<string, string> parameters)
        {
            insert(query, parameters);
        }

        public static string hash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
