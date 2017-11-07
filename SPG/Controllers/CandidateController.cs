using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPG.Data;
using SPG.Models.Db;
using SPG.Models.Filters.Candidates;
using Microsoft.EntityFrameworkCore;

namespace SPG.Controllers
{
    [Route("api/spg/candidates")]
    public class CandidateController : Controller
    {
        private ElectContext electContext;

        public CandidateController(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        [HttpPost("get")]
        public IActionResult GetList()
        {
            List<Candidate> candidates = electContext.Candidates.Include(c => c.Election).AsNoTracking().ToList();
            return Ok(candidates);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AddOrUpdateFilter filter)
        {
            if (ModelState.IsValid)
            {
                Candidate candidate = filter.Candidate;
                Election election = electContext.Elections.FirstOrDefault(e => e.ID == filter.ElectionId);
                if (electContext != null)
                {
                    candidate.Election = election;
                }
                electContext.Candidates.Add(candidate);
                electContext.SaveChanges();
                return Ok();
            }
            return BadRequest(new { message = "Ошибка валидации" });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] AddOrUpdateFilter filter)
        {
            if (ModelState.IsValid)
            {
                Candidate candidate = filter.Candidate;
                if (electContext.Candidates.FirstOrDefault(c => c.ID == candidate.ID) != null)
                {
                    Election election = electContext.Elections.FirstOrDefault(e => e.ID == filter.ElectionId);
                    if (electContext != null)
                    {
                        candidate.Election = election;
                    }
                    electContext.Candidates.Update(candidate);
                    electContext.SaveChanges();
                    return Ok(candidate);
                }
                return BadRequest(new { message = "Такого кандидата не существует" });
            }
            return BadRequest(new { message = "Ошибка валидации" });
        }

        [HttpPost("delete/{candidateId}")]
        public IActionResult Delete([FromBody] int? candidateId)
        {
            if (candidateId != null)
            {
                Candidate candidate = electContext.Candidates.FirstOrDefault(c => c.ID == candidateId);
                if (candidate != null)
                {
                    electContext.Candidates.Remove(candidate);
                    electContext.SaveChanges();
                    return Ok();
                }
                return BadRequest(new { message = "Кандидат с таким id не найден" });
            }
            return BadRequest(new { message = "id кандидата не указан" });
        }
    }
}
