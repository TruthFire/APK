﻿using System;

namespace APK
{
    public class Person
    {

        protected string Name, Surename;
        //protected DateTime Dob;


        public Person(string name, string surename)
        {
            Name = name;
            Surename = surename;
          //  Dob = dob;
        }

        public Person() { }

        /*public string GetAge()
        {
            return DateTime.Now.AddYears(-(Dob.Year + Convert.ToInt32(IsBdBeen()))).ToString("yy");
        }*/

        public string GetName()
        {
            return Name;
        }
        public string GetSurename()
        {
            return Surename;
        }
       /* public DateTime GetDob()
        {
            return Dob;
        }
        public int DaysToBd() // kiek dienu liko iki gd
        {
            DateTime curr = DateTime.Now;
            int year = curr.Year + Convert.ToInt32(!IsBdBeen()); //
            double days = (new DateTime(year, Dob.Month, Dob.Day) - curr).TotalDays;
            return (int)days;
        }

        protected bool IsBdBeen() // ar buvo gimtadienis simet?
        {
            bool rez = false;
            DateTime curr = DateTime.Now;
            if (curr.Month < Dob.Month || curr.Month == Dob.Month && curr.Day < Dob.Day) //jei buvo = grazinam true(1), else false (0)
                rez = true;

            return rez;
        }*/

        public string PrintInfo()
        {
            string s = "Vardas: " + Name + "\nPavarde: " + Surename;
            return s;
        }
    }
}
