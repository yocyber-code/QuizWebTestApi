using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Entities
{
    public class Q_SCORE_BOARD : BaseVersionModel
    {
        public int USER_ID { get; set; }
        public int SCORE { get; set; }
        public int GROUP_ID { get; set; }
    }
}
