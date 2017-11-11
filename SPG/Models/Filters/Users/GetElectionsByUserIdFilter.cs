using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SPG.Models.Filters.Users
{
    public class GetElectionsByUserIdFilter
    {
        [Required]
        public int UserId { get; set; }
    }
}
