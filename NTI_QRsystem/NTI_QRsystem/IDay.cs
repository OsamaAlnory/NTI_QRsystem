using System;
using System.Collections.Generic;
using System.Text;

namespace NTI_QRsystem
{
    public class IDay
    {
        
        public static DayOfWeek GetDayByTranslatedDay(string t)
        {
            t = t.ToLower();
            if(t == "lördag")
            {
                return DayOfWeek.Saturday;
            } else if(t == "söndag")
            {
                return DayOfWeek.Sunday;
            } else if(t == "måndag")
            {
                return DayOfWeek.Monday;
            } else if(t == "tisdag")
            {
                return DayOfWeek.Tuesday;
            } else if(t == "onsdag")
            {
                return DayOfWeek.Wednesday;
            } else if(t == "torsdag")
            {
                return DayOfWeek.Thursday;
            } else if(t == "fredag")
            {
                return DayOfWeek.Friday;
            }
            return DayOfWeek.Saturday;
        }

    }
}
