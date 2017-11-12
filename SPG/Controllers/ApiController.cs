using Microsoft.AspNetCore.Mvc;
using SPG.Data;
using SPG.Models;
using SPG.Models.Entities;
using SPG.Utils;
using SPG.Models.Api.Input;
using System.Collections.Generic;


namespace SPG.Controllers
{
    [Route("api/spg")]
    public class ApiController : Controller
    {
        private ElectContext electContext;

        public ApiController(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        [HttpPost("register-user")]
        public IActionResult RegisterUser([FromBody] RegisterFilter filter)
        {
            if (ModelState.IsValid)
            {
                Users usersModel = new Users(electContext);
                if (usersModel.registerUser(filter))
                {
                    return Ok();
                }
            }
            return BadRequest(new { message = "Ошибка" });
        }

        [HttpPost("login-user")]
        public IActionResult LoginUser([FromBody] LoginFilter filter)
        {
            if (ModelState.IsValid)
            {
                Users usersModel = new Users(electContext);
                User user = usersModel.loginUser(filter);
                if (user != null)
                {
                    return Ok(new { id = user.ID, username = user.Username, LIK = user.LIK, role = user.Role.ToString("g") });
                }
            }
            return BadRequest(new { message = "Ошибка" });
        }

        [HttpPost("get-elections-by-user")]
        public IActionResult getElectionsByUserId([FromBody] GetElectionsByUserIdFilter filter)
        {
            if (ModelState.IsValid && filter.UserId != 0)
            {
                int userId = filter.UserId;
                Users usersModel = new Users(electContext);
                List<Election> elections = usersModel.getElectionsByUserId(userId);
                if (elections != null)
                    return Ok(elections);
            }
            return BadRequest(new { message = "Ошибка" });
        }

        [HttpPost("send-bulletin")]
        public IActionResult sendBulletin([FromBody] SendBulletinFilter filter)
        {
            if (ModelState.IsValid)
            {
                    return Ok();
            }
            return BadRequest(new { message = "Ошибка" });
        }
    }
}
