﻿
using MySql.Data.MySqlClient;

namespace Web2023Project.libs

{
    public class DBConnection
    {
        public static MySqlConnection getConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "websellphone";
            string username = "root";
            string password = "123456789";
            string connString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + password;
            MySqlConnection conn= new MySqlConnection(connString);
            return conn;
        }
    }
}