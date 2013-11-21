#region License

// Droid Manager - An Android Phone Tools Suite
// Copyright (C) 2011
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;


namespace Common
{
    public static class Util
    {

      
        public static bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidMAC(string str)
        {
            if (string.IsNullOrEmpty(str)||str.Length!=12)
            {
                return false;
            }
            string pstr = "^[0-9a-fA-F]*$";
            bool match = Regex.IsMatch(str, pstr);
            return match;

        }

        /// <summary>
        /// 判断是否合法的SN号，SN必须为数字字母组合，并且长度在6到32之间，末尾必须为数字
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public static bool IsValidSN(string sn)
        {
            if (sn == null)
            {
                return false;
            }
            if (sn.Length < 6||sn.Length>32)
            {
                return false;
            }
            foreach (Char c in sn)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            return Char.IsDigit(sn[sn.Length-1]);
        }

      
        public static String CalculateIMEI(String imeiString)
        {
           // String imeiString = header + sn;

            return imeiString + GetIMEICheckDigit(imeiString);
        }

        /// <summary>
        /// 是否有效的IMEI号
        /// </summary>
        /// <param name="imeiString"></param>
        /// <returns></returns>
        public static bool IsValidIMEI(String imeiString)
        {
            if (string.IsNullOrEmpty(imeiString) || imeiString.Length < 14 || imeiString.Length>15)
            {
                return false;
            }

            foreach (Char c in imeiString)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }


            if (imeiString.Length == 15)
            {
                return imeiString.Equals(CalculateIMEI(imeiString.Substring(0, 14)));
            }
            else
            {
                return true;
            }

        }

        public static char GetIMEICheckDigit(String imei)
        {
            int i;
            int sum1 = 0, sum2 = 0, total = 0;
            int temp = 0;

            for (i = 0; i < 14; i++)
            {
                if ((i % 2) == 0)
                {
                    sum1 = sum1 + imei[i] - '0';
                }
                else
                {
                    temp = (imei[i] - '0') * 2;
                    if (temp < 10)
                    {
                        sum2 = sum2 + temp;
                    }
                    else
                    {
                        sum2 = sum2 + 1 + temp - 10;
                    }
                }
            }

            total = sum1 + sum2;

            if ((total % 10) == 0)
            {
                return '0';
            }
            else
            {
                return (char)(((total / 10) * 10) + 10 - total + '0');
            }
        }

        /// <summary>
        /// 将MAC地址转换成XX:XX:XX:XX:XX格式
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static string GetMACToWrite(string mac)
        {

            if (mac.Length != 12)
            {
                return null;
            }
            else
            {
                return string.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                    mac.Substring(0, 2), 
                    mac.Substring(2, 2),
                    mac.Substring(4, 2),
                    mac.Substring(6, 2),
                    mac.Substring(8, 2),
                    mac.Substring(10, 2));
            }

        }
    }
}