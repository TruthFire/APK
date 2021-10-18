﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

/* CREATE TABLE "User" (
	"id"	INTEGER NOT NULL,
	"nick"	TEXT NOT NULL,
	"password"	TEXT NOT NULL,
	"name"	TEXT NOT NULL,
	"surename"	TEXT NOT NULL,
	"dob"	TEXT NOT NULL,
	"avatar"	TEXT,
	"user_group"	INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT)
)
*/


namespace PU3
{
    public class Db
    {
        SQLiteConnection dbConnection = new SQLiteConnection("Data Source=pu.db");
        public Db()
        {
            if(!File.Exists("pu.db"))
            {
                SQLiteConnection.CreateFile("pu.db");
            }
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=pu.db");
            dbConnection.Open();
            string sql = "CREATE TABLE IF NOT EXISTS \"User\" (" +
                "\"id\"	INTEGER NOT NULL, \"nick\"	TEXT NOT NULL, \"password\"	TEXT NOT NULL," +
                "\"name\"	TEXT NOT NULL, \"surename\"	TEXT NOT NULL, \"dob\" TEXT NOT NULL, \"avatar\"   TEXT," +
                "\"user_group\"  INTEGER, PRIMARY KEY(\"id\" AUTOINCREMENT))";
            SQLiteCommand create = new SQLiteCommand(sql, dbConnection);
            create.ExecuteNonQuery();
            dbConnection.Close();
        }
        
        public int CreateUser(User u)
        {

            dbConnection.Open();
              string sql = string.Format(
                  "INSERT INTO User(nick, password, name, surename, dob, user_group) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', 1);",
              u.GetNick(), u.GetPwd(), u.Person.GetName(), u.Person.GetSurename(), u.Person.GetDob()
            );
           
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConnection);
            var asd = cmd.ExecuteNonQuery();
            dbConnection.Close();
            return asd;

        }

        public int TryAuth(string name, string pwd)
        {
            dbConnection.Open();
            string sql = string.Format("SELECT id FROM User WHERE (nick='{0}' AND password='{1}')", name, pwd);
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConnection);
            int s= Convert.ToInt32(cmd.ExecuteScalar());
            dbConnection.Close();
            return s;
        }

        public User GetUser(string nick, string pwd)
        {

            string sql = string.Format("SELECT * FROM User WHERE id = {0}", TryAuth(nick, pwd));
            dbConnection.Open();
            SQLiteCommand cmd = new(sql, dbConnection);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            if(rdr.Read())
            {
                string name = rdr["name"].ToString();
                string surename = rdr["surename"].ToString();
                DateTime dob = Convert.ToDateTime(rdr["dob"]);
                int Group = Convert.ToInt32(rdr["group"]);
                Person p = new(name, surename, dob);
                User u = new(p, nick, pwd, Group);
                dbConnection.Close();
                return u;
            }
            else
            {
                dbConnection.Close();
                return null;
            }
            

        }

    }
}
