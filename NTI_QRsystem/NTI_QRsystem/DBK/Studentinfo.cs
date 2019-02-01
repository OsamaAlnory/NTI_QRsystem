using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NTI_QRsystem.DBK
{
   public class Studentinfo
    {
        
        public string Studentname { get; set; }
        public string ATime { get; set; }
        public Color color { get; set; }

        public override string ToString()
        {
            return Studentname;
        }
    }
}

