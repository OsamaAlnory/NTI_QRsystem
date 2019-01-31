using System;
using System.Collections.Generic;
using System.Text;

namespace NTI_QRsystem.DBK
{
   public class Studentinfo
    {
        
        public string LecId { get; set; }
        public string Studentname { get; set; }
        public string ATime { get; set; }



        public Studentinfo(string studentname, string lecid, string atime)
        {
            LecId = lecid;
            Studentname = studentname;
            ATime = atime;

        }

        public override string ToString()
        {
            return Studentname;
        }
    }
}

