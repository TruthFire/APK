using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

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
            string sql = "";
            if(u.GetGroup() == 1)
            {
                sql = string.Format("INSERT INTO `users`" +
                    "(`login`, `password`, `name`, `surename`, `u_group`, `s_group`, `l_rang`)" +
                    " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    u.GetNick(), u.GetPwd(), u.GetName(),u.GetSurename(), u.GetGroup(), u.GetInfo(), DBNull.Value
                    );
            }
            if (u.GetGroup() == 2)
            {
                sql = string.Format("INSERT INTO `users`" +
                    "(`login`, `password`, `name`, `surename`, `u_group`, `s_group`, `l_rang`)" +
                    " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    u.GetNick(), u.GetPwd(), u.GetName(), u.GetSurename(), u.GetGroup(), DBNull.Value, u.GetInfo()
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

        public bool CheckId(int id)
        {
            string sql = string.Format("SELECT `id` FROM `users` WHERE `id`={0}", id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            bool rez = Convert.ToInt32(cmd.ExecuteScalar()) != 0;
            dbConnection.Close();
            return rez;

        }

        public void updateUser(User u)
        {
            string sql = string.Format("UPDATE `users` SET " +
                "`login`='{0}',`password`='{1}',`name`='{2}',`surename`='{3}',`u_group`='1',`s_group`='{4}',`l_rang`='{5}' " +
                "WHERE `id`={6}", u.GetNick(), u.GetPwd(), u.GetName(), u.GetSurename(), u.GetInfo(), DBNull.Value, u.GetId());
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

        public int GetUserId(string name, string surename, string group)
        {
            string sql = string.Format("SELECT `id` FROM `Users` WHERE (`name`='{0}' AND `surename`='{1}' AND `s_group`='{2}')", name, surename, group);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            if (rdr.Read())
            {
                id = Convert.ToInt32(rdr["id"]);
            }
            rdr.Close();
            dbConnection.Close();
            return id;

        }

        public User GetUser(int id)
        {
           
            string sql = string.Format("SELECT * FROM `Users` WHERE `id` = '{0}'", id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                string name = rdr["name"].ToString();
                string surename = rdr["surename"].ToString();
                int Group = Convert.ToInt32(rdr["u_group"]);
                string s_group = null;
                string nick = rdr["login"].ToString();
                string pwd = rdr["password"].ToString();
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

        public DataTable FillUserGridView()
        {
            dbConnection.Open();
            MySqlDataAdapter Da = new();
            string sql = "SELECT `id`, `login`, `name`, `surename`, `s_group` FROM `Users` WHERE `u_group`=1";
            Da.SelectCommand = new MySqlCommand(sql, dbConnection);
            DataTable dt = new();
            Da.Fill(dt);
            dbConnection.Close();
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
            dbConnection.Close();
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
            List<String> Lecturers = new();
            string sql = "SELECT `name`, `surename`, `l_rang` FROM `users` WHERE `u_group` = 2";
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Lecturers.Add(rdr["l_rang"].ToString() + " " + rdr["name"].ToString() + " " +rdr["surename"].ToString());
            }
            dbConnection.Close();

            return Lecturers.ToArray();
        }

        protected string[] getGroupIds(string[] groups)
        {
            List<String> Groups = new();
            for (int i = 0; i < groups.Length; i++)
            {
                string sql = string.Format("SELECT `id` FROM `s_groups` WHERE `group_name` = '{0}'", groups[i]);
                dbConnection.Open();
                MySqlCommand cmd = new(sql, dbConnection);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Groups.Add(rdr["id"].ToString());
                }
                dbConnection.Close();
            }
            return Groups.ToArray();

        }

        public int getSubjectIdByGroup(int sGroupId, string subject)
        {
            string sql = string.Format("SELECT `id` FROM `subjects` WHERE `name`='{0}'", subject);
            //string sql = string.Format("SELECT `id` FROM `subject_groups` WHERE `group_id`={0} AND `subject_id`='{1}'", subject, sGroupId);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            while(rdr.Read())
            {
                id = (int)rdr["id"];
            }
            rdr.Close();
            dbConnection.Close();
            return id;

        }

        public int getSubjectId(string subjectName, int l_id)
        {
            string sql = string.Format("SELECT `id` FROM `subjects` WHERE `name` = '{0}' AND `lecturer` = {1}", subjectName, l_id);
            dbConnection.Open();
            int id = 0;
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                id = Convert.ToInt32(rdr["id"]);
            }
            dbConnection.Close();
            return id;
        }

        private int getLecturerId(string l)
        {
            string[] lSplitted = l.Split(' ');
            string sql = string.Format("SELECT `id` FROM `users` WHERE `l_rang` = '{0}' AND `name`='{1}' AND `surename`='{2}'", lSplitted[0], lSplitted[1], lSplitted[2]);
            int id = 0;
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                id = Convert.ToInt32(rdr["id"]);
            }
            dbConnection.Close();
            return id;

        }

        public void addSubject(string lecturer, string json, string title, string[] groupList)
        {
            string[] lecturerSplitted = lecturer.Split(' ');
            string sql = string.Format("SELECT `id` FROM `users` WHERE `l_rang`= '{0}' AND `name` = '{1}' AND `surename` = '{2}';",lecturerSplitted[0],lecturerSplitted[1], lecturerSplitted[2]);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            if(rdr.Read())
            {
                id = Convert.ToInt32(rdr["id"]);
            }
            dbConnection.Close();
            try
            {
                if (id != 0)
                {
                    sql = string.Format("INSERT INTO `subjects`(`name`, `marks`, `lecturer`) VALUES ('{0}','{1}','{2}')", title, json, id);
                    Exec(sql);
                    string[] gIds = getGroupIds(groupList);
                    int s_id = getSubjectId(title,getLecturerId(lecturer));


                    for(int i = 0; i < gIds.Length; i++)
                    {
                        sql = string.Format("INSERT INTO `subject_groups`(`group_id`, `subject_id`) VALUES ('{0}','{1}')", gIds[i], s_id);
                        Exec(sql);
                    }
                }
            }
            catch(Exception exc)
            {
                throw new ArgumentNullException("id klaida.\n" + exc);
            }
        }

        public string[] getAllGroups()
        {
            List<String> groups = new List<String>();
            string sql = "SELECT `group_name` FROM `s_groups` WHERE 1";
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                groups.Add(rdr["group_name"].ToString());
            }
            dbConnection.Close();

            return groups.ToArray();
        }

        public string[] getStudentSubjects(User u)
        {
            List<String> subjects = new();
            List<int> subject_ids = new();
            int s_g_id = GetGroupId(u.GetInfo());
            string sql = string.Format("SELECT `subject_id` FROM `subject_groups` where `group_id` = {0}", s_g_id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                subject_ids.Add((int)rdr["subject_id"]);
            }
            rdr.Close();
            foreach(int s_id in subject_ids)
            {
                sql = string.Format("SELECT `name` FROM `subjects` WHERE `id`={0}",s_id);
                cmd = new(sql, dbConnection);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    subjects.Add(rdr["name"].ToString());
                }
                rdr.Close();

            }
            dbConnection.Close();

            return subjects.ToArray();
            

        }

        public string[] getMySubjectTitles(int l_id)
        {
            List<String> subjects = new List<String>();
            string sql = string.Format("SELECT `name` FROM `subjects` WHERE `lecturer`={0}",l_id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                subjects.Add(rdr["name"].ToString());
            }
            dbConnection.Close();

            return subjects.ToArray();
        }

        protected List<int> GetSubjectGroupsIds(int s_id)
        {
            List<int> g_ids = new();
            string sql = string.Format("SELECT `group_id` FROM `subject_groups` WHERE `subject_id`={0}", s_id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                g_ids.Add(Convert.ToInt32(rdr["group_id"]));
            }
            dbConnection.Close();

            return g_ids;
        }

        public string[] GetSubjectGroups(int s_id)
        {
            List<String> groups = new();
            List<int> g_ids = GetSubjectGroupsIds(s_id);
            dbConnection.Open();
            foreach(int id in g_ids)
            {
                string sql = string.Format("SELECT `group_name` FROM `s_groups` WHERE `id`={0}", id);
                
                MySqlCommand cmd = new(sql, dbConnection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    groups.Add(rdr["group_name"].ToString());
                }
                rdr.Close();
                
            }
            dbConnection.Close();

            return groups.ToArray();
        }

        public string[] GetGroupStudents(string group)
        {
            List<string> students = new();
            string sql = string.Format("SELECT `name`, `surename` FROM `users` WHERE `s_group`='{0}';", group);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                students.Add(rdr["name"].ToString() + " " + rdr["surename"].ToString());
            }
            dbConnection.Close();

            return students.ToArray();
        }

        public int GetGroupId(string group_name)
        {
            string sql = string.Format("SELECT `id` FROM `s_groups` WHERE `group_name` = '{0}'", group_name);
            int id = 0;
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                id = (int)rdr["id"];
            }
            rdr.Close();
            dbConnection.Close();
            return id;
        }

        public int[] GetStudentMarks(int stud_id, int sub_id)
        {
            int[] allmarks = new int[4];
            string json = "";
            Marks m = new();
            string sql = string.Format("SELECT `id` FROM `marks` WHERE `student_id`={0} AND `subject_id`={1}", stud_id, sub_id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            bool rez = Convert.ToInt32(cmd.ExecuteScalar()) != 0;
            dbConnection.Close();
            if(rez)
            {
                sql = string.Format("SELECT `student_marks` FROM `marks` WHERE `student_id`={0} AND `subject_id`={1}", stud_id, sub_id);
                dbConnection.Open();
                cmd = new(sql, dbConnection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    
                    json = rdr["student_marks"].ToString();
                    m = JsonConvert.DeserializeObject<Marks>(json);
                    allmarks = m.stud_marks;
                }
                dbConnection.Close();
                rdr.Close();
            }
            else if(!rez)
            {
                Marks sm = new();
                sm.stud_marks = new int[]{ 0,0,0,0 };
                json = JsonConvert.SerializeObject(sm);
                allmarks = sm.stud_marks;
                sql = string.Format("INSERT INTO `marks`(`subject_id`, `student_id`, `student_marks`) VALUES ('{0}','{1}','{2}')",sub_id, stud_id, json);
                Exec(sql);
            }
            return allmarks;
        }

        public int[] GetCoefficients(int subject_id)
        {
            string sql = String.Format("SELECT `marks` FROM `subjects` WHERE `id`={0}", subject_id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string json = "";
            while (rdr.Read())
            {
                json = rdr["marks"].ToString();
            }
            dbConnection.Close();
            MarkCoefficients mc = new();
            mc = JsonConvert.DeserializeObject<MarkCoefficients>(json);
            return mc.Coefficients;
        }

        public void RemoveGroup(int id)
        {
            string sql = String.Format("DELETE FROM `s_groupsps` WHERE `id` = '{0}'", id);
            Exec(sql);
        }

        public void UpdateMarks(int student_id, int subject_id, string marks)
        {
            string sql = string.Format("UPDATE `marks` SET `student_marks`='{0}' WHERE `subject_id`={1} AND `student_id` = {2}",marks, subject_id, student_id);
            Exec(sql); 
        }

    }
}
