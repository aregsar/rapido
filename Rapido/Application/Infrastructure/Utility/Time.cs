using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Application.Infrastructure.Utility
{
    /// <summary>
    /// Abstraction wrapper for a point in time for consistant time througout application 
    /// </summary>
    public class Time
    {
        public static DateTime Current
        {
            get
            {
                //TODO: implement timeshift
                //if(AppSettings.ShiftTime)return DateTime.UtcNow.Add(AppSettings.TimeShiftAmmount);
               
                return DateTime.UtcNow;
            }
        }


        /*
         * DateTime.Format => 9/5/01
        DateTime.FormatAlphaMonth => September 5, 2011

         3.HoursAgo
         * 3.HoursFromNow
         *    3.MinutesAgo
         3.MinutesFromNow
         *  *    3.SecondsAgo
         3.SecondsFromNow
         *   3.DaysAgo
         3.DaysFromNow
         3.MonthsAgo
         * 3.MonthsFromNow
            3.YearsAgo
         * 3.YearsFromNow
         * 
         * 
     
        
        
         */
        
    }
}