namespace APK
{
    public class User : Person
    {

        protected string Nick { get; set; }
        protected string Pwd { get; set; }
        protected int Group { get; set; }
        protected int Id { get; set; }
        protected string S_group { get; set; }


        public User(Person p, string nick, string pwd, int group, string s_group = null)
        {
            Name = p.GetName();
            Surename = p.GetSurename();
            S_group = s_group;
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


    }
}
