using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SPG.Models.Db
{
    public enum UserRole : byte
    {
        admin,
        voter
    }

    public class User
    {
        public int ID { get; set; }

        [Required]
        public string LIK { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public ICollection<ElectionVoter> ElectionVoters { get; set; }
    }
}
