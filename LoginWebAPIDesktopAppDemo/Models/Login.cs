﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginWebAPIDesktopAppDemo.Models
{
    public class Login
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}