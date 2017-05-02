﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zane.Common.Extensions.Base
{
    internal class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            property.ShouldSerialize = instance =>
            {
                try
                {
                    PropertyInfo prop = (PropertyInfo)member;
                    if (prop.CanRead)
                    {
                        prop.GetValue(instance, null);
                        return true;
                    }
                }
                catch
                {
                }
                return false;
            };

            return property;
        }
    }
    internal class MemoryStreamJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(MemoryStream).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var bytes = serializer.Deserialize<byte[]>(reader);
            return bytes != null ? new MemoryStream(bytes) : new MemoryStream();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytes = ((MemoryStream)value).ToArray();
            serializer.Serialize(writer, bytes);
        }
    }
    public static class Extensions_Json
    {
        /// <summary>
        /// 将此对象序列化成json字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            StringBuilder sb = new StringBuilder();
            JsonSerializer serializer = new JsonSerializer()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ContractResolver = new CustomResolver()
            };
            serializer.Converters.Add(new MemoryStreamJsonConverter());
            StringWriter sw = new StringWriter(sb);
            serializer.Serialize(sw, obj);
            return sb.ToString();
        }

        /// <summary>
        /// 将Json字符串转换为指定类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T Convert2Model<T>(this string jsonStr) where T : class
        {
            if (jsonStr.IsNullOrEmpty())
            {
                return null;
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = jsonStr.Length;
            if (typeof(T) == typeof(DataTable))
            {
                object[] list = serializer.Deserialize(jsonStr, typeof(object[])) as object[];
                DataTable dt = new DataTable();
                foreach (var item in list[0] as Dictionary<string, object>)
                {
                    dt.Columns.Add(item.Key);
                }
                foreach (var item in list)
                {
                    var dic = item as Dictionary<string, object>;
                    dt.Rows.Add(dic.Values.ToArray());
                }
                return dt as T;
            }
            return serializer.Deserialize(jsonStr, typeof(T)) as T;
        }



        public static void JsonString2Html(this string jsonStr, StringBuilder sb)
        {
            if (jsonStr.IsNullOrWhiteSpace())
            {
                jsonStr = "{}";
            }
            var json = JContainer.Parse(jsonStr);
            json.JsonObject2Html(sb);
        }
        public static void JsonObject2Html(this JToken token, StringBuilder sb, string parentName = null)
        {
            if (token is JArray)
            {
                parentName = parentName.IsNullOrEmpty() ? "root" : parentName += "-item";
                sb.AppendLine("<ol class='" + parentName + "'>");

                foreach (var item in token.Children())
                {
                    sb.AppendLine("<li>");
                    JsonObject2Html(item, sb, parentName);
                    sb.AppendLine("</li>");
                }
                sb.AppendLine("</ol>");
            }
            else if (token is JObject)
            {
                parentName = parentName.IsNullOrEmpty() ? "root" : parentName += "-item";
                sb.AppendLine("<ul class='" + parentName + "'>");
                foreach (var item in token.Children())
                {
                    JsonObject2Html(item, sb, parentName);
                }
                sb.AppendLine("</ul>");
            }
            else if (token is JProperty)
            {
                JProperty p = token as JProperty;
                sb.AppendLine("<li class='" + parentName + "-" + p.Name + "'>");
                sb.Append("<span class='key'>" + p.Name + "</span>:");
                foreach (var item in p.Children())
                {
                    JsonObject2Html(item, sb, p.Name);
                }
                sb.AppendLine("</li>");
            }
            else if (token is JValue)
            {
                JValue v = token as JValue;
                string value = v.Value == null ? null : WebUtility.HtmlEncode(v.Value.ToString());
                sb.Append("<span class='value'>" + value + "</span>");
            }
        }
    }
}
