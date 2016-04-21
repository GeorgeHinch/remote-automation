using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    static class SubstringExtension
    {
        #region Get string value between first [a] and last [b]
        public static string Between (this string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }

            int adjustedPosA = posA + a.Length;

            if (adjustedPosA >= posB)
            {
                return "";
            }

            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }
        #endregion

        #region Get string value after first [a]
        public static string Before (this string value, string a)
        {
            int posA = value.IndexOf(a);

            if (posA == -1)
            {
                return "";
            }

            return value.Substring(0, posA);
        }
        #endregion

        #region Get string value after last [a]
        public static string After (this string value, string a)
        {
            int posA = value.LastIndexOf(a);

            if (posA == -1)
            {
                return "";
            }

            int adjustedPosA = posA + a.Length;

            if (adjustedPosA >= value.Length)
            {
                return "";
            }

            return value.Substring(adjustedPosA);
        }
        #endregion
    }
}
