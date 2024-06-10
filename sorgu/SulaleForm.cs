using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

class Program
{
    static void Main(string[] args)
    {

        string connectionString = "Server=localhost;Database=101m;Uid=root;Pwd=;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
        }
    }
}