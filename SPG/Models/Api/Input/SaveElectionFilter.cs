using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SPG.Models.Entities;

namespace SPG.Models.Api.Input
{
    public class SaveElectionFilter
    {
        [Required]
        public User user { get; set; }

        [Required]
        public Election election { get; set; }
    }
}
