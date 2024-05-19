using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.DTOs.Response.Quiz
{
    public class LoadDTO
    {
        public int question_id { get; set; }
        public int choice_id { get; set; }
    }
}
