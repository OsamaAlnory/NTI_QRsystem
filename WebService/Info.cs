using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebService
{
    [DataContract]
    public class info
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Studentname { get; set; }

        [DataMember]
        public TimeSpan? Atime { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }
    }
}