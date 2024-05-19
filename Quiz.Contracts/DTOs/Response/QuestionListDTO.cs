using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.DTOs.Response
{
    public class QuestionListDTO
    {
        public int id { get; set; }
        public string question { get; set; } = null!;
        public List<ChoiceDTO> choices { get; set; } = new List<ChoiceDTO>();

    }

    public class ChoiceDTO
    {
        public int id { get; set; }
        public string choice { get; set; } = null!;
    }
}
