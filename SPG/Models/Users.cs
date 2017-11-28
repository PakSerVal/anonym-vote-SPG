using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPG.Data;
using SPG.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SPG.Utils;
using System.Security.Cryptography;
using SPG.Models.Api.Input;

namespace SPG.Models
{
    public class Users
    {
        private ElectContext electContext;

        public Users(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        public User getUserById(int userId)
        {
            User user = electContext.Users
                .SingleOrDefault(u => u.ID == userId);
            return user;
        }

        public bool registerUser(RegisterFilter filter)
        {
            User userToRegister = electContext.Users.FirstOrDefault(u => u.LIK == filter.LIK && u.isRegistred == false && u.Username != filter.Username);
            if (userToRegister != null)
            {
                userToRegister.isRegistred = true;
                userToRegister.Username = filter.Username;
                userToRegister.SignatureModulus = filter.SignatureModulus;
                userToRegister.SignaturePubExponent = filter.SignaturePubExponent;
                userToRegister.salt = UserUtils.getSalt();
                userToRegister.Password = UserUtils.getPasswordHash(filter.Password, userToRegister.salt);
                electContext.Users.Update(userToRegister);
                electContext.SaveChanges();
                return true;
            }
            return false;
        }

        public User loginUser(LoginFilter filter)
        {
            User userToLogin = electContext.Users.FirstOrDefault(u => u.Username == filter.Username);
            string userPassword = UserUtils.getPasswordHash(filter.Password, userToLogin.salt);
            if (userToLogin.Password == userPassword)
                return userToLogin;
            return null;
        }

        public List<Election> getElectionsByUserId(int userId)
        {
            User user = electContext.Users
                .Include(u => u.ElectionVoters)
                .ThenInclude(ev => ev.Election)
                .ThenInclude(e => e.Candidates)
                .SingleOrDefault(u => u.ID == userId && u.isCastingDone == false);
            if (user != null)
            {
                List<Election> elections = new List<Election>();
                foreach (ElectionVoter ev in user.ElectionVoters)
                {
                    ev.Voter = null;
                    elections.Add(ev.Election);
                }
                return elections;
            }
            return null;
        }

        public List<object> getAllUsers()
        {

            List<User> users = electContext.Users.Include(u => u.ElectionVoters).ThenInclude(ev => ev.Election).ToList();
            List<object> outputUsers = new List<object>();
            foreach (User user in users)
            {
                List<object> elections = new List<object>();
                foreach (ElectionVoter ev in user.ElectionVoters)
                {
                    elections.Add(new
                    {
                        id = ev.ElectionId,
                        name = ev.Election.Name
                    }
                    );
                }
                var userData = new
                {
                    id = user.ID,
                    lik = user.LIK,
                    role = user.Role.ToString("g"),
                    username = user.Username,
                    isRegistred = user.isRegistred,
                    isCastingDone = user.isCastingDone,
                    elections = elections
                };
                outputUsers.Add(userData);
            }
            return outputUsers;
        }

        public int addUser(User user, Election[] elections)
        {
            List<ElectionVoter> electionVoters = new List<ElectionVoter>();
            foreach(Election election in elections)
            {
                ElectionVoter ev = new ElectionVoter();
                ev.ElectionId = election.ID;
                ev.VoterId = user.ID;
                electionVoters.Add(ev);
            }
            user.ElectionVoters = electionVoters;
            electContext.Users.Add(user);
            electContext.SaveChanges();
            return user.ID;
        }

        public int deleteUser(int userId)
        {
            User user = electContext.Users.Include(u => u.ElectionVoters).AsNoTracking()
                .SingleOrDefault(u => u.ID == userId);
            foreach(ElectionVoter ev in user.ElectionVoters)
            {
                electContext.ElectionVoters.Remove(ev);
            }
            electContext.Users.Remove(user);
            electContext.SaveChanges();
            return userId;
        }
    }
}
