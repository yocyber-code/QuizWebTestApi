using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Interfaces
{
    public interface IJwt
    {
        Task<string> CreateJWTExpireAsync(string userId, string email,string role, int SecondExpired);
        Task<string> CreateJWTAsync(string userId, string email);
        JObject DecodeJWT(string token);
    }
}
