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
            Election election = electContext.Elections
                .SingleOrDefault(e => e.ID == electionId);
            return election;
        }
    }
}
