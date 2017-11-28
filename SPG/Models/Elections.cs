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
    public class Elections
    {
        private ElectContext electContext;

        public Elections(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        public Election getElectionById(int electionId)
        {
            Election election = electContext.Elections.Include(e => e.Candidates).AsNoTracking()
                .SingleOrDefault(e => e.ID == electionId);
            return election;
        }

        public int addElection(Election election)
        {
            electContext.Elections.Add(election);
            electContext.SaveChanges();
            return election.ID;
        }

        public int updateElection(Election election)
        {
            Election elec = getElectionById(election.ID);
            List<Candidate> candidates = elec.Candidates.ToList();
            foreach(Candidate candidate in candidates)
            {
                electContext.Candidates.Remove(candidate);
            }
            electContext.SaveChanges();
            foreach(Candidate candidate in election.Candidates)
            {
                if (candidate.ID != 0)
                {
                    candidate.ID = 0;
                }
            }
            elec.Name = election.Name;
            elec.Candidates = election.Candidates;
            electContext.Elections.Update(elec);
            electContext.SaveChanges();
            return election.ID;
        }

        public int deleteElection(int electionId)
        {
            Election election = getElectionById(electionId);
            if (election != null)
            {
                foreach (Candidate candidate in election.Candidates)
                {
                    electContext.Candidates.Remove(candidate);
                }
                electContext.Elections.Remove(election);
                electContext.SaveChanges();
            }
            return electionId;
        }
    }
}
