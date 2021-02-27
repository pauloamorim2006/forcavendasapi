using IniParser;
using IniParser.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ERP.Business.Helpers
{
    public static class Utils
    {
        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        public static IniData GetResult(string value)
        {
            var parser = new FileIniDataParser();
            return parser.ReadData(new StreamReader(Utils.GenerateStreamFromString(value)));
        }

        public static IniData ReadIniFile(string filePath)
        {
            var parser = new FileIniDataParser();
            return parser.ReadFile(filePath);
        }

        public static T GetAttribute<T>(this Enum valorEnum) where T : System.Attribute
        {
            var type = valorEnum.GetType();
            var memInfo = type.GetMember(valorEnum.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string GetDescription(this Enum valorEnum)
        {
            return valorEnum.GetAttribute<DescriptionAttribute>().Description;
        }
    }
}
