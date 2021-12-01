﻿namespace APK
{
    public class User : Person
    {

        protected string Nick { get; set; }
        protected string Pwd { get; set; }
        protected int Group { get; set; }
        protected int Id { get; set; }
        protected string Info { get; set; }


        public User(Person p, string nick, string pwd, int group, string extra = null)
        {
            Name = p.GetName();
            Surename = p.GetSurename();
            Info = extra;
            Nick = nick;
            Pwd = pwd;
            Group = group;
            Id = GetId();
            
        }

        public string GetNick()
        {
            return Nick;
        }
        public string GetPwd()
        {
            return Pwd;
        }

        public int GetGroup()
        {
            return Group;
        }

        public int GetId()
        {
            if (Id == 0)
            {
                Db DataBase = new();
                this.Id = DataBase.TryAuth(Nick, Pwd);
            }
            return this.Id;
        }

        public bool IsAdmin()
        {
            return Group == 2;

        }

        public string getInfo()
        {
            return Info;
        }


    }

    public class Marks
    {
        public int[] stud_marks = new int[4];
    }
}
