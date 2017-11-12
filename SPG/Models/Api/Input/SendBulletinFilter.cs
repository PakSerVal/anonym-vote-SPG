using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SPG.Models.Api.Input
{
    public class SendBulletinFilter
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public string Signature { get; set; }
    }
}
