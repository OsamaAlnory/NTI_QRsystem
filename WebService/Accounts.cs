using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebService
{
    [DataContract]
    public class accounts
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public bool isAdmin { get; set; }
    }
}