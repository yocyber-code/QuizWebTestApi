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
        Task<string> CreateJWTExpireAsync(string userId, string username, int SecondExpired);
        JObject DecodeJWT(string token);
    }
}
