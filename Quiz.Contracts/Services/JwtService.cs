using Quiz.Contracts.Interfaces;

using Newtonsoft.Json.Linq;

using JWT.Algorithms;
using JWT.Builder;
using Quiz.Contracts.Entities;

namespace Quiz.Contracts.Services
{
    public class JwtService : IJwt
    {
        public const string privateKey = "J6k2eVCTXDp5b97u6gNH5GaaqHDxCmzz2wv3PRPFRsuW2UavK8LGPRauC4VSeaetKTMtVmVzAC8fh8Psvp8PFybEvpYnULHfRpM8TA2an7GFehrLLvawVJdSRqh2unCnWehhh2SJMMg5bktRRapA8EGSgQUV8TCafqdSEHNWnGXTjjsMEjUpaxcADDNZLSYPMyPSfp6qe5LMcd5S9bXH97KeeMGyZTS2U8gp3LGk2kH4J4F3fsytfpe9H9qKwgjb";

        public async Task<string> CreateJWTExpireAsync(string userId, string email, string role, int SecondExpired)
        {
            int unixTimestamp = (int)DateTime.UtcNow.AddSeconds(SecondExpired + 2).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            var token = JwtBuilder.Create()
                       .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                       .WithSecret(privateKey)
                       .AddClaim("exp", unixTimestamp)
                       .AddClaim("nameid", userId)
                       .AddClaim("email", email)
                       .AddClaim("role", role)

                       .Encode();

            return token;
        }

        public async Task<string> CreateJWTAsync(string userId, string email)
        {
            var token = JwtBuilder.Create()
                       .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                       .WithSecret(privateKey)
                       .AddClaim("sub", userId)
                       .AddClaim("email", email)
                       .Encode();

            return token;
        }

        public JObject DecodeJWT(string token)
        {
            var json = JwtBuilder.Create()
                     .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                     .WithSecret(privateKey)
                     .MustVerifySignature()
                     .Decode(token);
            return JObject.Parse(json);
        }
    }
}
