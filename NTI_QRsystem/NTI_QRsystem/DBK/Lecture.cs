using System;
using System.Collections.Generic;
using System.Text;

namespace NTI_QRsystem.DBK
{
    public class Lecture
    {
        public string Rid { get; set; }
        public string AdminID { get; set; }
        public string Class { get; set; }
        public TimeSpan LecTime { get; set; }
        public string DeviceID { get; set; }
        public string Extra { get; set; }
    }
}
