using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;


namespace W_DataBase
{
    class Database
    {
        public SQLiteConnection myConnection;
        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=WShapes.sqlite3");

            //if a database file does not exist, create one, and 
            if (!File.Exists("./WShapes.sqlite3"))
            {
                SQLiteConnection.CreateFile("WShapes.sqlite3");
                Console.WriteLine("File Created");
            }
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }
        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
