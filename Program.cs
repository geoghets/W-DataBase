using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;


//https://web.archive.org/web/20190910153157/http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
//https://www.youtube.com/watch?v=anTP-mgktiI
//http://zetcode.com/csharp/sqlite/

/* CREATE TABLE "WShapes" ("Label" TEXT, "H" REAL,"W" INTEGER);
*/
namespace W_DataBase
{
    class Program
    {

        static void createTable(Database dataBaseObject)
        {
            string sql = "CREATE TABLE highscores (name VARCHAR(20), score INT)";
            SQLiteCommand command = new SQLiteCommand(sql, dataBaseObject.myConnection);

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        static void readFromTable(Database dataBaseObject)
        {
            string query = "Select * FROM albums";
            SQLiteCommand myCommand = new SQLiteCommand(query, dataBaseObject.myConnection);
            dataBaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Console.WriteLine("Album: {0} - Artist: {1}", result["title"], result["artist"]);
                }
            }
            dataBaseObject.CloseConnection();
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        static void insertIntoTable(Database dataBaseObject)
        {
            string query = "INSERT INTO albums ('title', 'artist') VALUES (@title, @artist)";
            SQLiteCommand myCommand = new SQLiteCommand(query, dataBaseObject.myConnection);
            dataBaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@title", "the chronoc3");
            myCommand.Parameters.AddWithValue("@artist", "dottore andr3e");
            var result = myCommand.ExecuteNonQuery();
            dataBaseObject.CloseConnection();
            Console.WriteLine("Rows Atted : {0}", result);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static void Main(string[] args)
        {


            /*                string titles = "Type,AISC_Manual_Label,W,A,d,ddet,bf,bfdet,tw,twdet,twdet/2,tf,tfdet,kdes,kdet,k1,bf/2tf,h/tw,Ix,Zx,Sx,rx,Iy,Zy,Sy,ry,J,Cw,Wno,Sw1,Qf,Qw,rts,ho,PA,PB,PC,PD,T,WGi";
                            string[] words = titles.Split(',');
                            string titles1 = "";
                            int i = 0;  

                            foreach (string word in words)
                            {
                                string varTy;
                                if (i < 2)
                                {
                                    varTy = "TEXT"; 
                                }
                                else 
                                {
                                    varTy = "REAL";
                                }

                                titles1 += $"'{word}'  {varTy} , ";
                                i += 1;
                                //Console.WriteLine(titles1);
                                //Console.WriteLine(word);
                            }

                        titles1 = titles1.Substring(0, titles1.Length - 2);
                        Console.WriteLine(titles1);

                        */

            string queryTitles = @"'Type'  TEXT , 'AISC_Manual_Label'  TEXT , 'W'  REAL , 'A'  REAL , 'd'  REAL , 'ddet'  REAL , 'bf'  REAL , 'bfdet'  REAL , 'tw'  REAL , 'twdet'  REAL , 'twdet/2'  REAL , 'tf'  REAL , 'tfdet'  REAL , 'kdes'  REAL , 'kdet'  REAL , 'k1'  REAL , 'bf/2tf'  REAL , 'h/tw'  REAL , 'Ix'  REAL , 'Zx'  REAL , 'Sx'  REAL , 'rx'  REAL , 'Iy'  REAL , 'Zy'  REAL , 'Sy'  REAL , 'ry'  REAL , 'J'  REAL , 'Cw'  REAL , 'Wno'  REAL , 'Sw1'  REAL , 'Qf'  REAL , 'Qw'  REAL , 'rts'  REAL , 'ho'  REAL , 'PA'  REAL , 'PB'  REAL , 'PC'  REAL , 'PD'  REAL , 'T'  REAL , 'WGi'  REAL";
            string queryTitles2 = @"Type,AISC_Manual_Label,W,A,d,ddet,bf,bfdet,tw,twdet,'twdet/2',tf,tfdet,kdes,kdet,k1,'bf/2tf','h/tw',Ix,Zx,Sx,rx,Iy,Zy,Sy,ry,J,Cw,Wno,Sw1,Qf,Qw,rts,ho,PA,PB,PC,PD,T,WGi";
            Database dataBaseObject1 = new Database();

            dataBaseObject1.myConnection.Open();
            string query = "CREATE TABLE Shapes (" + queryTitles + ")";
            SQLiteCommand command = new SQLiteCommand(query, dataBaseObject1.myConnection);
            command.ExecuteNonQuery();



            /* string ph = "H";
             query = "insert into Shapes (Label, " + ph + ") values ('W8X10', 8)";
             command = new SQLiteCommand(query, dataBaseObject1.myConnection);
             command.ExecuteNonQuery();

 */
            //Put one line into DB
            /*            string LineOne = "'W','44X335',335,98.5,44,44,15.9,16,1.03,1,0.5,1.77,1.75,2.56,3,1.75,4.5,38,31100,1620,1410,17.8,1200,236,150,3.49,74.7,535000,168,1180,278,805,4.24,42.2,132,148,104,120,38,5.5";

                        string query1 = "insert into Shapes ("+queryTitles2+") values ("+LineOne+")";
                        command = new SQLiteCommand(query1, dataBaseObject1.myConnection);
                        command.ExecuteNonQuery();*/

            //READ THE LINES WITH STREAMREADER AND ADD TO DB
            StreamReader sr = new StreamReader(@"C:\Users\admin\source\repos\W DataBase\bin\Debug\netcoreapp3.1\W.csv");

            string s;
            do
            {
                s = sr.ReadLine();

                string query1 = "insert into Shapes (" + queryTitles2 + ") values (" + s + ")";
                command = new SQLiteCommand(query1, dataBaseObject1.myConnection);
                command.ExecuteNonQuery();

            } while (s != null);

            dataBaseObject1.myConnection.Close();

            //READ THE LINES WITH STREAMREADER AND PRINT
            /* StreamReader sr = new StreamReader(@"C:\Users\admin\source\repos\W DataBase\bin\Debug\netcoreapp3.1\W.csv");

             int j = 0;
             string s;
             do
             {
                 s = sr.ReadLine();
                 if (j > 0)
                 {
                     Console.WriteLine(s);
                 }
                 j += 1;
             } while (s != null);*/



            /*query = "insert into Shapes (Label, H) values ('W10X12', 10)";
            command = new SQLiteCommand(query, dataBaseObject1.myConnection);
            command.ExecuteNonQuery();

            query = "select * from Shapes order by H desc";
            command = new SQLiteCommand(query, dataBaseObject1.myConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
                Console.WriteLine("Label: " + reader["Label"] + "\tHeight: " + reader["H"]);*/



        }
    }
}
