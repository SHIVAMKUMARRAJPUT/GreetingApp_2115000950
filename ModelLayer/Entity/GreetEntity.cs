﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    public class GreetEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

    }
}
