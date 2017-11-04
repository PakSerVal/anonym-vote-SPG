using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SPG.Models.Db
{
    public class Election
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Candidate> Candidates { get; set; }
    }
}
