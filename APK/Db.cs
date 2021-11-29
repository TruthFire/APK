using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;

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


namespace APK
{
    public class Db
    {
        MySqlConnection dbConnection = new(@"server=localhost;userid=root;password=;database=APK");
        public Db()
        {
   
        }

         public void CreateUser(User u)
         {
            string sql = "";//= string.Format(
            //"INSERT INTO `users`(`login`, `password`, `name`, `surename`, `u_group`) VALUES ('{0}', '{1}', '{2}', '{3}', 1,);",
           // u.GetNick(), u.GetPwd(), u.GetName(), u.GetSurename()
           // );
            if(u.GetGroup() == 1)
            {
                sql = string.Format("INSERT INTO `users`" +
                    "(`login`, `password`, `name`, `surename`, `u_group`, `s_group`, `l_rang`)" +
                    " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    u.GetNick(), u.GetPwd(), u.GetName(),u.GetSurename(), u.GetGroup(), u.GetS_Group(), DBNull.Value
                    );
            }
            if (u.GetGroup() == 2)
            {
                sql = string.Format("INSERT INTO `users`" +
                    "(`login`, `password`, `name`, `surename`, `u_group`, `s_group`, `l_rang`)" +
                    " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    u.GetNick(), u.GetPwd(), u.GetName(), u.GetSurename(), u.GetGroup(), DBNull.Value, "lekt."
                    );
            }
            if (u.GetGroup() == 3)
            {
                sql = string.Format("INSERT INTO `users`" +
                    "(`login`, `password`, `name`, `surename`, `u_group`, `s_group`, `l_rang`)" +
                    " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    u.GetNick(), u.GetPwd(), u.GetName(), u.GetSurename(), u.GetGroup(), DBNull.Value, DBNull.Value
                    );
            }
            Exec(sql);
         }

        public bool CheckNick(string nick)
        {
            string sql = string.Format("SELECT `id` FROM `Users` where `login`='{0}'", nick);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            bool rez = Convert.ToInt32(cmd.ExecuteScalar()) != 0;
            dbConnection.Close();
            return rez;

        }


        public int TryAuth(string nick, string pwd)
        { 
            string sql = string.Format("SELECT `id` FROM `Users` WHERE (`login`='{0}' AND `password`='{1}')", nick, pwd);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            int s = Convert.ToInt32(cmd.ExecuteScalar());
            dbConnection.Close();
            return s;
        }

        public User GetUser(string nick, string pwd)
        {

            string sql = string.Format("SELECT * FROM `Users` WHERE `id` = '{0}'", TryAuth(nick, pwd));
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                string name = rdr["name"].ToString();
                string surename = rdr["surename"].ToString();
               // DateTime dob = Convert.ToDateTime(rdr["dob"]);
                int Group = Convert.ToInt32(rdr["u_group"]);
                string s_group = null;
                if(Group == 1)
                {
                    s_group = rdr["s_group"].ToString();
                }
                Person p = new(name, surename);
                User u = new(p, nick, pwd, Group, s_group);
                dbConnection.Close();
                return u;
            }
            else
            {
                dbConnection.Close();
                return null;
            }
        }

        public void DeleteUser(int id)
        {
            string sql = String.Format("DELETE FROM `Users` WHERE `id` = '{0}'", id);
            Exec(sql);
        }

        /*public void UpdateAvatar(int id, string fn)
        {
            string sql = String.Format("UPDATE `User` SET `avatar` = '{0}' WHERE `id` = {1}", fn, id);
            Exec(sql); 
            
        }*/

        protected void Exec(string sql)
        {
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            cmd.ExecuteNonQuery();
            dbConnection.Close();
        }

        public bool CheckPwd(int id, string pwd)
        {
            
            string sql = String.Format("SELECT `password` FROM `Users` WHERE `id` = '{0}'", id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string pass = "";
            while (rdr.Read())
            {
                   pass = rdr["password"].ToString();
            }
            dbConnection.Close();
            return pass == pwd;
        }

        public void SetPwd(string newPwd, int id)
        {
            string sql = String.Format("UPDATE `Users` SET `password` = {0} WHERE id={1}", newPwd, id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            cmd.ExecuteNonQuery();
            dbConnection.Close();
        }
        
        /*public string GetAvatar(int id)
        {
            string sql = string.Format("SELECT `avatar` FROM `Users` where `id` = '{0}'", id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string avtr = "NoAvatar";
            while (rdr.Read())
            {
                avtr = rdr["avatar"].ToString();
            }
            dbConnection.Close();
            return avtr;
        }*/

        public DataTable FillUserGridView()
        {
            dbConnection.Open();
            MySqlDataAdapter Da = new();
            string sql = "SELECT `id`, `login`, `name`, `surename`, `s_group` FROM `Users` WHERE `u_group`=1";
            Da.SelectCommand = new MySqlCommand(sql, dbConnection);
            DataTable dt = new();
            Da.Fill(dt);

            return dt;
        }

        public DataTable FillSGroupGridView()
        {
            dbConnection.Open();
            MySqlDataAdapter Da = new();
            string sql = "SELECT `id`, `group_name` FROM `s_groups` WHERE 1";
            Da.SelectCommand = new MySqlCommand(sql, dbConnection);
            DataTable dt = new();
            Da.Fill(dt);
            return dt;
        }

        public bool checkGroup(string gName)
        {
            string sql = string.Format("SELECT `id` FROM `s_groups` WHERE `group_name` = '{0}'", gName);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            bool rez = Convert.ToInt32(cmd.ExecuteScalar()) != 0;
            dbConnection.Close();
            return rez;

        }

        public void CreateGroup(string name)
        {
            string sql = string.Format("INSERT INTO `s_groups`(`group_name`) VALUES ('{0}')", name);
            Exec(sql);
        }
        public string[] getStudGroupList()
        {
            List<String> Groups = new List<String>();

            string sql = "SELECT `group_name` FROM `s_groups`";
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                Groups.Add(rdr["group_name"].ToString());
            }
            dbConnection.Close();
            return Groups.ToArray();

        }


        public string[] getLectList()
        {
            List<String> Lecturers = new List<String>();
            string sql = "SELECT `name`, `surename`, `l_rang` FROM `users` WHERE `u_group` = 2";
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Lecturers.Add(rdr["l_rang"].ToString() + rdr["name"].ToString() + " " +rdr["surename"].ToString());
            }
            dbConnection.Close();

            return Lecturers.ToArray();
        }

    }
}
