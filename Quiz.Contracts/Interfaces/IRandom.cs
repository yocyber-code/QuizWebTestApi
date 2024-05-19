﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Interfaces
{
    public interface IRandom
    {
        string RandomInteger(int digit);
        string RandomAlphabet(int digit);
        string RandomAlphaNumberic(int digit);
    }
}
