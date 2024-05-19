using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Interfaces
{
    public interface IEnumService
    {
        public string GetDescriptionByValue(string enumValue);
    }
}
