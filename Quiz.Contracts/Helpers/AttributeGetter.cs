using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quiz.Contracts.Helpers
{
    public static class _
    {
        public static string DisplayName<T>(string propertyName)
        {
            MemberInfo prop = typeof(T).GetProperty(propertyName);

            var displayNameAttribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);

            if (displayNameAttribute.Length > 0)
            {
                return (displayNameAttribute[0] as DisplayNameAttribute).DisplayName;
            }
            else
            {
                return prop.Name;
            }
        }
        public static string JsonPropertyName<T>(string propertyName)
        {
            MemberInfo prop = typeof(T).GetProperty(propertyName);

            try
            {
                var jsonPropertyNameAttribute = prop.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false);

                if (jsonPropertyNameAttribute.Length > 0)
                {
                    return (jsonPropertyNameAttribute[0] as JsonPropertyNameAttribute).Name;
                }
                else
                {
                    return prop.Name;
                }
            }
            catch
            {
                return prop.Name;
            }

        }
    }
}
