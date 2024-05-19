using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.DTOs.Response.Auth
{
    public class LoginDTO
    {
        public string Token { get; set; } = null!;
    }
}
