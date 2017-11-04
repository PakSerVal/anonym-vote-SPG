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

        [HttpPost("get-canditates-list")]
        public IActionResult GetCandidatesList([FromBody][Bind("LIK")] User user)
        {
            if (ModelState.IsValid)
            {
                if (electContext.Users.FirstOrDefault(u => u.LIK == user.LIK) != null)
                {
                    //TODO: Разобраться с RedirectToAction (почему-то не работает)
                    return new CandidateController(electContext).GetList();
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
