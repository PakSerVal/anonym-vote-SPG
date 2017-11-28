using SPG.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SPG.Models.Entities;

namespace SPG.Models.Api.Input
{
    public class AddUserFilter
    {
        [Required]
        public User user { get; set; }

        [Required]
        public Election[] elections{ get; set; }

        [Required]
        public User addUser { get; set; }
    }
}
