﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Entities
{
    public class Q_USER_GROUP
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string GROUP_NAME { get; set; } = null!;
    }
}
