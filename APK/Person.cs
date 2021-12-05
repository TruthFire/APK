namespace APK
{
    public class Person
    {

        protected string Name, Surename;


        public Person(string name, string surename)
        {
            Name = name;
            Surename = surename;
        }

        public Person() { }


        public string GetName()
        {
            return Name;
        }
        public string GetSurename()
        {
            return Surename;
        }

        public string PrintInfo()
        {
            string s = "Vardas: " + Name + "\nPavarde: " + Surename;
            return s;
        }
    }
}
