﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APK
{
    public class Admin : User
    {
        public Admin(Person p, string nick, string pwd, int group) : base(p, nick, pwd, group) { }

        

    }
}
