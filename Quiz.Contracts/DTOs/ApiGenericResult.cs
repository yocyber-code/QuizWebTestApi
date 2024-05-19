using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.DTOs
{
    [Serializable]
    public class ApiGenericResult<T> where T : class
    {
        public int Code { get; set; }
        public int Count { get; set; }
        public string Message { get; set; }
        public string MessageAlt { get; set; }
        public T Results { get; set; }
    }
}
