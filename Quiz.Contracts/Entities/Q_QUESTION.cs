using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Entities
{
    public class Q_QUESTION
    {
        [Key]
        public int ID { get; set; }

        public string QUESTION { get; set; } = null!;

        public int USERGROUP { get; set; }
    }
}
