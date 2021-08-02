using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace DLL
{
    public class Utility
    {
        public static bool IsNotEmpty(string str)
        {
            return !IsEmpty(str);
        }

        public static bool IsEmpty(string str)
        {
            return (null == str || "" == str.Trim());
        }

        public static string FormatToBR(string s)
        {
            string strText = s;

            if (s.IndexOf("\r\n") > 0)
            {
                strText = strText.Replace("\r\n", "<br>");
            }
            else if (s.IndexOf("\n") > 0)
            {
                strText = strText.Replace("\r", "<br>");
            }
            else if (s.IndexOf("\r") > 0)
            {
                strText = strText.Replace("\n", "<br>");
            }

            return strText;
        }

        public static string StrConvToHankaku(string strText)
        {
            string strHankaku = Strings.StrConv(strText, VbStrConv.Narrow, 0);
            return strHankaku;
        }

        public static string TrimByByteCount(string str, int maxLength)
        {
            string trimedString = "";

            int currentByteCount = 0;

            int strByteCount = GetByteCountShift_JIS(str);

            if (strByteCount <= maxLength) { return str; }

            for (int i = 0; i < maxLength; i++)
            {
                currentByteCount += isZenkaku(str[i].ToString()) ? 2 : 1;

                if (currentByteCount > maxLength) { return trimedString; }

                trimedString += str[i];
            }

            return trimedString;
        }

        public static int GetByteCountShift_JIS(string mojiretu)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int num = sjisEnc.GetByteCount(mojiretu);
            return num;
        }

        public static bool isZenkaku(string str)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int num = sjisEnc.GetByteCount(str);
            return num == str.Length * 2;
        }

        public static string ConvertYYYY_MM_DD(string yyyy_mm_dd)
        {
            yyyy_mm_dd = yyyy_mm_dd.Replace('-', '/');
            string[] aryYYYY_DD_DD = yyyy_mm_dd.Split('/');
            if (aryYYYY_DD_DD.Length != 3 && aryYYYY_DD_DD[0].Length != 4)
            {
                return yyyy_mm_dd;
            }

            return yyyy_mm_dd.Substring(2, 8);
        }

        public static int ConvertToWareki(int yyyy, ref string GenGou)
        {
            if (yyyy <= 1988) { return -1; } // 昭和より前は対象外。

            GenGou = "平成";
            int offset = 1988;

            return yyyy - offset;
        }
    }
}
