using MY_CALS_BACKEND.Dto.UserAccount;
using MY_CALS_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Repositories
{
    public interface IRepoUserAccount
    {
        public List<UserAccount> GetUsersAccounts();
        public UserForDisplayDTO GetUserById(int Id);
        public void DeleteUserAccount(int Id);
    }


    public class RepoUserAccount : IRepoUserAccount
    {
        private ApplicationDbContext _dbContext;
        public RepoUserAccount(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteUserAccount(int Id)
        {
            throw new NotImplementedException();
        }

        public List<UserAccount> GetUsersAccounts()
        {
            var allUsers = _dbContext.UserAccounts.ToList();
            return allUsers;
        }

        public UserForDisplayDTO GetUserById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
