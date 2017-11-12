using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPG.Data;
using SPG.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SPG.Utils;
using System.Security.Cryptography;

namespace SPG.Models
{
    public class Candidates
    {
        private ElectContext electContext;

        public Candidates(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        public Candidate getCandidateById(int candidateId)
        {
            Candidate candidate = electContext.Candidates
                .SingleOrDefault(c => c.ID == candidateId);
            return candidate;
        }
    }
}
