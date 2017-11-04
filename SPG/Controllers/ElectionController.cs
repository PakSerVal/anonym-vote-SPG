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
    [Route("api/elections")]
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
