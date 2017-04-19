using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zane.Common.Extensions.Base
{
    public static class Extensions_String
    {
        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        
        public static string FormatWith(this string source, params object[] parameters)
        {
            return string.Format(CultureInfo.InvariantCulture, source, parameters);
        }
        
        /// <summary>
        /// 返回该字符串的MD5加密结果
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt(this string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string tempStr = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                tempStr += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return tempStr;
        }
        public static bool IsMatch(this string input, string regStr)
        {
            return Regex.IsMatch(input, regStr, RegexOptions.IgnoreCase);
        }

        public static string Match(this string input, string regStr)
        {
            return Regex.Match(input, regStr, RegexOptions.IgnoreCase).Value;
        }

        /// <summary>
        /// 将文本进行正则处理
        /// 如果regReplaceStr为null，且regMatchStr失败，返回null。
        /// </summary>
        /// <param name="input"></param>
        /// <param name="regMatchStr"></param>
        /// <param name="regReplaceStr"></param>
        /// <returns></returns>
        public static List<string> RegexLogic(this string input, string regMatchStr, string regReplaceStr = null)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(regMatchStr))
            {
                return list;
            }

            Regex reg = new Regex(regMatchStr, RegexOptions.IgnoreCase);

            foreach (Match item in reg.Matches(input))
            {
                list.Add(
                    regReplaceStr == null ?
                    item.Value :
                    item.Result(regReplaceStr)
                    );
            }
            if (list == null && !string.IsNullOrEmpty(regReplaceStr))
            {
                list = new List<string>();
                list.Add(input);
            }
            return list;
        }

        public static string Replace(this string input, string regMatchStr, string regReplaceStr, int count)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(regMatchStr))
            {
                return input;
            }
            Regex reg = new Regex(regMatchStr, RegexOptions.IgnoreCase);
            if (count > 0)
            {
                return reg.Replace(input, regReplaceStr, count);
            }
            else
            {
                return reg.Replace(input, regReplaceStr);
            }
        }

        /// <summary>
        /// 将字符串按某字符切割，返回切割后的数组，数组中不包含空对象
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] SplitNoEmpty(this string str, params string[] separators)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new string[0];
            }
            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 将字符串以换行符切割
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitNoEmptyByLine(this string str)
        {
            return str.SplitNoEmpty(Environment.NewLine, "\r", "\n");
        }
        /// <summary>
        /// 将字符串按某字符切割，返回切割后的数组，数组中不包含空对象。并且将结果数组转成int数组。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static int[] SplitNoEmpty_Int(this string str, params string[] separator)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new int[0];
            }
            string[] temp = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int[] result = new int[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                result[i] = Convert.ToInt32(temp[i]);
            }
            return result;
        }

        /// <summary>
        /// 检查元素value是否存在于用splitStr连接的字符串中。
        /// 例：("1,2,3" | "3" | "，") ==> true
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static bool Contains(this string str, string value, string splitStr)
        {
            str = splitStr + str + splitStr;
            value = splitStr + value + splitStr;
            return str.Contains(value);
        }
        /// <summary>
        /// 检查字符串中是否包含数组中的某个字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool Contains(this string str, string[] arr)
        {
            foreach (var item in arr)
            {
                if (str.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 从左至右截取字符串，直到遇到subStr（不包含subStr）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="subStr"></param>
        /// <returns></returns>
        public static string SubString_Left(this string str, string subStr)
        {
            if (!str.Contains(subStr))
            {
                return str;
            }
            return str.Substring(0, str.IndexOf(subStr));
        }
        /// <summary>
        /// 从第一个subStr开始截取字符串，直到结尾（不包含subStr）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="subStr"></param>
        /// <returns></returns>
        public static string SubString_Right(this string str, string subStr)
        {
            if (!str.Contains(subStr))
            {
                return "";
            }
            return str.Substring(str.IndexOf(subStr) + subStr.Length);
        }
        /// <summary>
        /// 从最后一个subStr开始截取字符串，直到结尾（不包含subStr）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="subStr"></param>
        /// <returns></returns>
        public static string SubString_Right_Last(this string str, string subStr)
        {
            if (!str.Contains(subStr))
            {
                return str;
            }
            return str.Substring(str.LastIndexOf(subStr) + subStr.Length);
        }
        /// <summary>
        /// 从左至右截取字符串，直到遇到最后一个subStr（不包含subStr）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="subStr"></param>
        /// <returns></returns>
        public static string SubString_Left_Last(this string str, string subStr)
        {
            if (!str.Contains(subStr))
            {
                return str;
            }
            return str.Substring(0, str.LastIndexOf(subStr));
        }
        /// <summary>
        /// 匹配字符串的末尾是否为trimStr，如果是，去除该末尾。
        /// 例：("1,2,3," , ",") ==> "1,2,3"
        /// </summary>
        /// <param name="str"></param>
        /// <param name="trimStr"></param>
        /// <returns></returns>
        public static string Trim_Right(this string str, string trimStr)
        {
            if (str == null) return str;
            if (str.Length < trimStr.Length)
            {
                return str;
            }
            if (Right(str, trimStr.Length) != trimStr)
            {
                return str;
            }
            return Left(str, -(trimStr.Length));
        }
        /// <summary>
        /// 生成字符串的缩略字符串。
        /// 原始字符串长度超出length时，将截断此字符串，并以...结尾。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Clipped(this string str, int length)
        {
            if (str == null) return str;
            if (length < 0)
            {
                length = str.Length + length;
            }
            length = length < 0 ? 0 : length;
            if (str.Length <= length)
            {
                return str;
            }
            else
            {
                return str.Substring(0, length) + "...";
            }
        }
        /// <summary>
        /// 从左往右获取字符串指定长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string str, int length)
        {
            if (str == null) return str;
            if (length < 0)
            {
                length = str.Length + length;
            }
            length = length < 0 ? 0 : length;
            if (str.Length <= length)
            {
                return str;
            }
            else
            {
                return str.Substring(0, length);
            }
        }
        /// <summary>
        /// 从右往左获取字符串指定长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string str, int length)
        {
            if (str == null) return str;
            if (length < 0)
            {
                length = str.Length + length;
            }
            length = length < 0 ? 0 : length;
            if (str.Length <= length)
            {
                return str;
            }
            else
            {
                return str.Substring(str.Length - length, length);
            }
        }

        public static string ToString(this string[] array, string separator)
        {
            if (array == null)
            {
                return "";
            }
            return string.Join(separator, array);
        }
        public static string ToString(this List<string> array, string separator)
        {
            if (array == null)
            {
                return "";
            }
            return array.ToArray().ToString(separator);
        }
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
