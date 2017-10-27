using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace SPG.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        [HttpPost("get-canditates-list")]
        public string GetCandidatesList()
        {
            return "список кандитатов";
        }

        [HttpPost("send-vote")]
        public string SendVote()
        {
            return "подтверждение";
        }
    }
}
