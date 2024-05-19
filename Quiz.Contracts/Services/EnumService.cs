using Quiz.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Quiz.Contracts.Constants.Common;

namespace Quiz.Contracts.Services
{
    public class EnumService : IEnumService
    {
        public string GetDescriptionByValue(string enumValue)
        {
            //Type enumType = typeof(MaterialUnitType);
            //foreach (FieldInfo fieldInfo in enumType.GetFields())
            //{
            //    if (Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute)
            //    {
            //        if (fieldInfo.Name == enumValue)
            //        {
            //            return descriptionAttribute.Description;
            //        }
            //    }
            //}
            return null; // Return null if the enum value is not found
        }
    }
}
