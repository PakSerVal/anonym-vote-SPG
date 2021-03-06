﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SPG.Models.Entities;

namespace SPG.Models.Api.Input
{
    public class GetElectionByIdFilter
    {
        [Required]
        public User user { get; set; }

        [Required]
        public int electionId { get; set; }
    }
}
