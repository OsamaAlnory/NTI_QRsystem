using System;
using System.Collections.Generic;
using System.Text;

namespace NTI_QRsystem.DB
{
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Class { get; set; }
        public bool isAdmin { get; set; }
        public bool isLogged { get; set; }
        public string Pnumber { get; set; }
        public string MobileID { get; set; }
    }
}
