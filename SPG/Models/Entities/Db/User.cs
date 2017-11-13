using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SPG.Models.Entities
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

        public string Username { get; set; }

        public string Password { get; set; }

        public string SignatureModulus { get; set; }

        public string SignaturePubExponent { get; set; }

        public string salt { get; set; }

        public bool isRegistred { get; set; } = false;

        public bool isCastingDone { get; set; } = false;

        public ICollection<ElectionVoter> ElectionVoters { get; set; }
    }
}
