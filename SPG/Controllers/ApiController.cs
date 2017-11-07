using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPG.Data;
using SPG.Models.Db;

namespace SPG.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private ElectContext electContext;

        public ApiController(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        [HttpPost("get-elections")]
        public IActionResult GetElectionsList([FromBody][Bind("LIK")] User userLikWrap)
        {
            if (ModelState.IsValid)
            {
                User user = electContext.Users.FirstOrDefault(u => u.LIK == userLikWrap.LIK);
                if (user != null)
                {
                   return new UserController(electContext).getElectionsById(user);
                }
            }
            return BadRequest(new { message = "Ошибка валидации" });
        }

        [HttpPost("send-vote")]
        public string SendVote()
        {
            return "подтверждение";
        }
    }
}
