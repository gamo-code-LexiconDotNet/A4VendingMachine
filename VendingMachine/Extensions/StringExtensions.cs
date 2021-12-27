using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public static class StringExtensions
    {
        public static bool BeginsWithVowel(this string target)
        {
            if (string.IsNullOrEmpty(target) && target.Length > 0)
            {
                if ("aouei".Contains(target.ToLower()[0]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
