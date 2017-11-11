using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SPG.Models.Enities
{
    public class Candidate
    {
        public int ID { get; set; }

        [Required]
        public string FIO { get; set; }

        public Election Election { get; set; }
    }
}
