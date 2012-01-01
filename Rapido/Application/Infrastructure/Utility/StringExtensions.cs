using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rapido.Infrastructure.Utility
{
    public static class StringExtensions
    {
        public static string Camelize(this string s)
        {
            string us = s.ToUpper();

            return us.Substring(0, 1) + s.Substring(1);
        }


        public static string TrimTail(this string s, int numChars = 1)
        {
            return s.Substring(s.Length - numChars,numChars);
        }

        public static string TrimHead(this string s, int numChars = 1)
        {
            return s.Substring(s.Length - numChars, numChars);
        }

        /*
        s.nil
s.empty
s.nilorempty
s.blank
s.ok


char s.startchar
char s.endchar
string s.charat(index)
string s.lastchar
string s.firstchar
string s.lastchars(count 3)
string s.firstchars(count 1)
string s.indextolast(index 3)
string s.indextofirst(index 1)


string s.removelastchar
string s.removefirstchar
string s.removelastchars(count 3)
string s.removefirstchars(count 1)
string s.removeindextolast(index 3)
string s.removeindextofirst(index 1)

string s.chars(index 2, index 20)
string s.removechars(index 2, index 20)

        
         */
    }
}