using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Zane.Common.Extensions.Base
{
    public static class Extensions_Extract
    {
        /// <summary>
        /// 从字符串中抽取出价格,如果没抽到，返回null。
        /// </summary>
        /// <param name="priceStr"></param>
        /// <returns></returns>
        public static double? ExtractPrice(this string str)
        {
            var result = str.ExtractNumber();
            if (result.Length > 0)
            {
                return Math.Abs(result[0]);
            }
            return null;
        }
        /// <summary>
        /// 从字符串中抽取出double,如果没抽到，返回null。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double? ExtractDouble(this string str)
        {
            var result = str.ExtractNumber();
            if (result.Length > 0)
            {
                return result[0];
            }
            return null;
        }

        public static double[] ExtractNumber(this string str)
        {
            List<double> result = new List<double>();
            string pattern = @"(?<num>-?((\d{1,3},)?(\d{3},)+\d{3}|\d+)(\.\d+)?) *(?<unit>[k千万]?)";
            foreach (Match item in Regex.Matches(str.ToLower(),pattern))
            {
                string temp = item.Groups["num"].Value;
                temp = Extensions_String.ToDBC(temp);
                double num = temp.ToDouble();
                switch (item.Groups["unit"].Value)
                {
                    case "k":
                    case "千":
                        num *= 1000;
                        break;
                    case "万":
                        num *= 10000;
                        break;
                    default:
                        break;
                }
                result.Add(num);
            }
            return result.ToArray();
        }
        /// <summary>
        /// 从字符串中抽取出整数,如果没抽到，返回null。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ExtractInt(this string str)
        {
            var result = str.ExtractNumber();
            if (result.Length > 0)
            {
                return Convert.ToInt32(result[0]);
            }
            return null;
        }
        /// <summary>
        /// 从字符串中抽取出无符号整数,如果没抽到，返回null。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static uint? ExtractUInt(this string str)
        {
            var result = str.ExtractNumber();
            if (result.Length > 0)
            {
                return Convert.ToUInt32(result[0]);
            }
            return null;
        }
    }
}
