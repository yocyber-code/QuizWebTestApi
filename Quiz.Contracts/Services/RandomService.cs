using Quiz.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Services
{
    public class RandomService : IRandom
    {
        public string RandomInteger(int digit)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, digit)
                         .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomAlphabet(int digit)
        {
            Random random = new Random();
            const string chars = "0123456789abcdefghijklimnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, digit)
                         .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomAlphaNumberic(int digit)
        {
            Random random = new Random();
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, digit)
                         .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
