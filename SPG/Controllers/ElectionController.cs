using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPG.Data;
using SPG.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace SPG.Controllers
{
    [Route("api/spg/elections")]
    public class ElectionController : Controller
    {
        private ElectContext electContext;

        public ElectionController(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        [HttpPost("get")]
        public IActionResult GetList()
        {
            List<Election> elections = electContext.Elections.Include(e => e.Candidates).AsNoTracking().ToList();
            return Ok(elections);
        }

        [HttpPost("get-voters")]
        public IActionResult getVotersById([FromBody][Bind("ID")] Election electionWrap)
        {
            if (electionWrap.ID != 0)
            {
                int electionId = electionWrap.ID;
                Election election = electContext.Elections
                    .Include(e => e.ElectionVoters)
                    .ThenInclude(ev => ev.Voter)
                    .SingleOrDefault(e => e.ID == electionId);
                List<User> voters = new List<User>();
                foreach(ElectionVoter ev in election.ElectionVoters)
                {
                    voters.Add(ev.Voter);
                }
                return Ok(voters);
                
            }
            return BadRequest(new { message = "ID не указан" });
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody][Bind("Name")] Election election)
        {
            if (ModelState.IsValid)
            {
                electContext.Elections.Add(election);
                electContext.SaveChanges();
                return Ok();
            }
            return BadRequest(new { message = "Ошибка валидации" });
        }

        [HttpPost("add-voter")]
        public IActionResult AddVoter([FromBody] ElectionVoter electionVoter)
        {
            if (ModelState.IsValid)
            {
                electContext.ElectionVoters.Add(electionVoter);
                electContext.SaveChanges();
                return Ok();
            }
            return BadRequest(new { message = "Ошибка валидации" });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Election election)
        {
            if (ModelState.IsValid)
            {
                if (electContext.Elections.FirstOrDefault(e => e.ID == election.ID) != null)
                {
                    electContext.Elections.Update(election);
                    electContext.SaveChanges();
                    return Ok(election);
                }
                return BadRequest(new { message = "Таких выборов не существуют не существует" });
            }
            return BadRequest(new { message = "Ошибка валидации" });
        }

        [HttpPost("delete/{electionId}")]
        public IActionResult Delete([FromBody] int? electionId)
        {
            if (electionId != null)
            {
                Election election = electContext.Elections.FirstOrDefault(e => e.ID == electionId);
                if (election != null)
                {
                    electContext.Elections.Remove(election);
                    electContext.SaveChanges();
                    return Ok();
                }
                return BadRequest(new { message = "Выборы с таким id не найдены" });
            }
            return BadRequest(new { message = "id выборов не указан" });
        }
    }
}
