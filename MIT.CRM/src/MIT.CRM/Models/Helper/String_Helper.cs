using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.CRM.Models.Helper
{
    public class String_Helper
    {
        public static string mensaguem(string palavra)
        {
            //System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;

            // This is our Unicode string:
            //string s_unicode = "abcéabc";

            // Convert a string to utf-8 bytes.
            byte[] utf8Bytes = System.Text.Encoding.ASCII.GetBytes(palavra);

            // Convert utf-8 bytes to a string.
            string s_unicode2 = System.Text.Encoding.ASCII.GetString(utf8Bytes);

            return s_unicode2;
        }
    }
}
