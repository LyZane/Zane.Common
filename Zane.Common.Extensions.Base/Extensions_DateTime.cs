using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zane.Common.Extensions.Base
{
    public static class Extensions_DateTime
    {
        public static string ToTextString(this TimeSpan span)
        {
            if (span.TotalSeconds < 1)
            {
                return span.TotalMilliseconds.Round(2) + "毫秒";
            }
            if (span.TotalMinutes < 1)
            {
                return span.TotalSeconds.Round(2) + "秒";
            }
            if (span.TotalHours < 1)
            {
                return span.TotalMinutes.Round(2) + "分钟";
            }
            if (span.TotalDays < 1)
            {
                return span.Hours + "小时" + span.Minutes + "分钟" + span.Seconds + "秒";
            }
            if (span.TotalHours < 24)
            {
                return span.Hours + "小时" + span.Minutes + "分钟" + span.Seconds + "秒";
            }
            if (span.Days < 10)
            {
                return span.Days + "天" + span.Hours + "小时" + span.Minutes + "分钟" + span.Seconds + "秒";
            }
            return "";
        }
        public static string ToTextString(this DateTime time)
        {
            if (time == null || time == DateTime.MinValue)
            {
                return "无";
            }
            if (time > DateTime.Now)
            {
                return time.ToString("现在");
            }
            TimeSpan span = DateTime.Now - time;
            if (span.TotalMinutes < 1)
            {
                return "刚刚";
            }
            if (span.TotalHours < 1)
            {
                return span.Minutes + "分钟前";
            }
            if (span.TotalDays < 1)
            {
                return span.Hours + "小时" + span.Minutes + "分钟前";
            }
            if (span.TotalHours < 24)
            {
                return "昨天" + time.ToString("HH:mm");
            }
            if (span.Days < 10)
            {
                return span.Days + "天" + span.Hours + "小时" + span.Minutes + "分钟前";
            }
            return time.ToString("yyyy年MM月dd日 HH:mm:ss");
        }
        public static string ToTextString_Short(this TimeSpan span)
        {
            string result = "";
            if (span.TotalSeconds < 1)
            {
                return span.TotalMilliseconds.Round(2) + "毫秒";
            }
            if (span.TotalMinutes < 1)
            {
                return span.TotalSeconds.Round(2) + "秒";
            }
            if (span.TotalHours < 1)
            {
                return span.TotalMinutes.Round(2) + "分钟";
            }
            if (span.TotalDays < 1)
            {
                result = span.Hours + "小时" + span.Minutes + "分钟" + span.Seconds + "秒";
            }
            else if (span.TotalHours < 24)
            {
                result = span.Hours + "小时" + span.Minutes + "分钟" + span.Seconds + "秒";
            }
            else
            {
                result = span.Days + "天" + span.Hours + "小时" + span.Minutes + "分钟" + span.Seconds + "秒";
            }
            var temp = result.RegexLogic(@"(.+?[^\d]+)(0[^\d]+)*", "$1");
            if (temp.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in temp)
                {
                    sb.Append(item);
                }
                return sb.ToString();
            }
            return result;
        }
        /// <summary>
        /// 返回每个月多少天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private static int Get_MonthDays(int year, int month)
        {
            var days = new Dictionary<int, int>()
            {
                { 1,31},
                { 2,(( year % 4 == 0 && year % 100 != 0) || year % 400 == 0) ? 29 : 28},
                { 3,31},
                { 4,30},
                { 5,31},
                { 6,30},
                { 7,31},
                { 8,31},
                { 9,30},
                { 10,31},
                { 11,30},
                { 12,31},
            };
            return days[month];
        }
        public static string ToTextString_Short(this DateTime time)
        {
            if (time == null || time == DateTime.MinValue)
            {
                return "无";
            }
            TimeSpan span;
            bool isbefore = false;
            if (DateTime.Now > time)
            {
                span = DateTime.Now - time;
                isbefore = true;
            }
            else
            {
                span = time - DateTime.Now;
            }

            if (span.TotalMinutes < 1)
            {
                return isbefore ? "刚刚" : "马上";
            }
            if (span.TotalHours < 1)
            {
                return span.Minutes + "分钟" + (isbefore ? "前" : "后");
            }
            if (span.TotalDays < 1)
            {
                return span.Hours + "小时" + span.Minutes + "分钟" + (isbefore ? "前" : "后");
            }
            if (span.TotalHours < 24)
            {
                return (isbefore ? "昨天" : "明天") + time.ToString("HH:mm");
            }
            string result = "";
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int month_1 = Get_MonthDays(year, month);
            int month_2 = month_1 + (month - 1 > 0 ? Get_MonthDays(year, month - 1) : 31);
            int month_3 = month_2 + (month - 2 > 0 ? Get_MonthDays(year, month - 2) : 31);
            if (span.Days > month_3)
            {
                return isbefore ? "很久以前" : "很久以后";
            }
            if (span.Days > month_2)
            {
                result = "2个月" + (span.Days - month_2) + "天" + span.Hours + "小时" + span.Minutes + "分钟" + (isbefore ? "前" : "后");
            }
            else if (span.Days > month_1)
            {
                result = "1个月" + (span.Days - month_1) + "天" + span.Hours + "小时" + span.Minutes + "分钟" + (isbefore ? "前" : "后");
            }
            else if (span.Days <= month_1)
            {
                result = span.Days + "天" + span.Hours + "小时" + span.Minutes + "分钟" + (isbefore ? "前" : "后");
            }
            else
            {
                return time.ToString("yyyy年MM月dd日 HH:mm:ss");
            }
            var temp = result.RegexLogic(@"(.+?[^\d]+)(0[^\d]+)*", "$1");
            if (temp.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in temp)
                {
                    sb.Append(item);
                }
                return sb.ToString();
            }
            return result;
        }
    }
}
