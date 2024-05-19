using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Exceptions
{
    public class ErrorDetails
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string MessageAlt { get; set; }
        public object Result { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
