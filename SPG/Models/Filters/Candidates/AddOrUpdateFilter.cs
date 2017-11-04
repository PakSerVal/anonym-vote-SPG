using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SPG.Models.Db;

namespace SPG.Models.Filters.Candidates
{
    public class AddOrUpdateFilter
    {
        [Required]
        public Candidate Candidate { get; set; }

        [Required]
        public int ElectionId { get; set; }
    }
}
