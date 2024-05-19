using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Entities
{
    public class Q_CHOICE
    {
        [Key]
        public int ID { get; set; }

        public string CHOICE { get; set; } = null!;

        public int QUESTION_ID { get; set; }

        public int SCORE { get; set; }
    }
}
