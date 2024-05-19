using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.DTOs.Response.Quiz
{
    public class SummaryDTO
    {
        public string username { get; set; } = null!;
        public int score { get; set; }
        public int rank { get; set; }
    }
}
