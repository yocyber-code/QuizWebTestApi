using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.DTOs.Request.Auth
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public int Group_Id { get; set; }
    }
}
