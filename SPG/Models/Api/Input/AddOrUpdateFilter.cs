using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SPG.Models.Entities;

namespace SPG.Models.Api.Input
{
    public class AddOrUpdateFilter
    {
        [Required]
        public Candidate Candidate { get; set; }

        [Required]
        public int ElectionId { get; set; }
    }
}
