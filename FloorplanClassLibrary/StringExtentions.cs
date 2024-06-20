using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    
    public static class StringExtensions
    {
        public static string Clean(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Keep only alphanumeric characters, spaces, commas, and slashes
            return new string(input.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == ',' || c == '/').ToArray()).Trim();
        }
    }

    

}
