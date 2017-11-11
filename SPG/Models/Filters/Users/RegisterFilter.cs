using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SPG.Models.Filters.Users
{
    public class RegisterFilter
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string LIK { get; set; }

        [Required]
        public string SignatureModulus { get; set; }

        [Required]
        public string SignaturePubExponent { get; set; }
    }
}
