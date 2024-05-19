using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Exensions
{

    public static class EnumDbNullExtensions
    {
        public static dynamic SqlVal(this Enum val)
        {
            return Convert.ToInt32(val) == 0 ? DBNull.Value : val;
        }

        public static dynamic SqlVal(this int val)
        {
            return Convert.ToInt32(val) == 0 ? DBNull.Value : val;
        }

        public static dynamic SqlVal(this int? val)
        {
            return val == null ? DBNull.Value : val;
        }

        public static dynamic SqlVal(this string val)
        {
            return string.IsNullOrEmpty(val) ? DBNull.Value : val;
        }

        public static dynamic SqlVal(this bool? val)
        {
            return val == null ? DBNull.Value : val;
        }
    }
}
