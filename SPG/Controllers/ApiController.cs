using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SPG.Data;
using SPG.Models;
using SPG.Models.Entities;
using SPG.Models.Api.Input;
using SPG.Utils;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SPG.Controllers
{
    [Route("api/spg")]
    public class ApiController : Controller
    {
        private ElectContext electContext;
        private Config config;

        public ApiController(ElectContext electContext, Config config)
        {
            this.electContext = electContext;
            this.config = config;
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
                return BadRequest(new { message = "Ошибка при регистрации" });
            }
            return BadRequest(ModelState);
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
                    return Ok(new { id = user.ID, username = user.Username, LIK = user.LIK, role = user.Role.ToString("g"), isCastingDone = user.isCastingDone });
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
                Bulletins bulletinModel = new Bulletins(config);
                Users userModel = new Users(electContext);
                User user = userModel.getUserById(filter.UserId);
                if (bulletinModel.sendBulletin(filter.UserId, filter.Data, filter.Signature, user.SignaturePubExponent, user.SignatureModulus))
                {
                    user.isCastingDone = true;
                    electContext.Users.Update(user);
                    electContext.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest(new { message = "Ошибка" });
        }

        [HttpGet("get-elgamal-pub-key")]
        public IActionResult getPubKeys()
        {
            return Ok(config.MixNetPubKey);
        }

        //Admin
        [HttpPost("get-election-by-id")]
        public IActionResult getElectionById([FromBody] GetElectionByIdFilter filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter.user, electContext))
                {
                    Elections electionModel = new Elections(electContext);
                    Election election = electionModel.getElectionById(filter.electionId);
                    if (election != null)
                    {
                        return Ok(election);
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost("get-all-elections")]
        public IActionResult getAllElections([FromBody] User filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter, electContext)) {
                    List<Election> elections = electContext.Elections.Include(e => e.Candidates).ToList();
                    return Ok(elections);
                }
            }
            return BadRequest();
        }

        [HttpPost("get-all-candidates")]
        public IActionResult getAllCandidates([FromBody] User filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter, electContext))
                {
                    List<Candidate> candidates = electContext.Candidates.ToList();
                    return Ok(candidates);
                }
            }
            return BadRequest();
        }

        [HttpPost("add-election")]
        public IActionResult addElection([FromBody] SaveElectionFilter filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter.user, electContext))
                {
                    Election election = filter.election;
                    Elections electionModel = new Elections(electContext);
                    electionModel.addElection(election);
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("update-election")]
        public IActionResult updateElection([FromBody] SaveElectionFilter filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter.user, electContext))
                {
                    Election election = filter.election;
                    Elections electionModel = new Elections(electContext);
                    electionModel.updateElection(election);
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("delete-election")]
        public IActionResult deleteElection([FromBody] DeleteElectionFilter filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter.user, electContext))
                {
                    Elections electionModel = new Elections(electContext);
                    electionModel.deleteElection(filter.electionId);
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("get-all-users")]
        public IActionResult getAllUsers([FromBody] User filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter, electContext))
                {
                    Users userModel = new Users(electContext);
                    var outputUsers = userModel.getAllUsers();
                    return Ok(outputUsers);
                }
            }
            return BadRequest();
        }

        [HttpPost("add-user")]
        public IActionResult addUser([FromBody] AddUserFilter filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter.user, electContext))
                {
                    Users userModel= new Users(electContext);
                    userModel.addUser(filter.addUser, filter.elections);
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("delete-user")]
        public IActionResult deleteUser([FromBody] DeleteUserFilter filter)
        {
            if (ModelState.IsValid)
            {
                if (UserUtils.isAdmin(filter.user, electContext))
                {
                    Users userModel = new Users(electContext);
                    userModel.deleteUser(filter.userId);
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
