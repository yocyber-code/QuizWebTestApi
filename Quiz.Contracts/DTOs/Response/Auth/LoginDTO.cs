using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.DTOs.Response.Auth
{
    public class LoginDTO
    {
        public int id { get; set; }
        public string username { get; set; }
        public int group { get; set; }
    }
}
