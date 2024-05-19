using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        CultureInfo CultureInfo { get; }
        DateTime UnixTime { get; }
        DateTime ToUTC(DateTime currentDate);
        DateTime FromString(string date, string? time);
    }
}
