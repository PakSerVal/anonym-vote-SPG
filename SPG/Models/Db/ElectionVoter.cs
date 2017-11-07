using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SPG.Models.Db
{
    public class ElectionVoter
    {
        [Required]
        public int ElectionId { get; set; }

        public Election Election { get; set; }

        [Required]
        public int VoterId { get; set; }

        public User Voter { get; set; }
    }
}
