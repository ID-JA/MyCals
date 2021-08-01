using AutoMapper;
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
        public  Task<UserAccount> GetUserById(int Id);
        public Task<bool> DeleteUserAccount(int Id);
        public Task<bool> Save();
    }


    public class RepoUserAccount : IRepoUserAccount
    {
        private ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public RepoUserAccount(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> DeleteUserAccount(int Id)
        {
            var userToDelete = await _dbContext.UserAccounts.FindAsync(Id);
            if (userToDelete !=null)
            {
                _dbContext.UserAccounts.Remove(userToDelete);
                return true;
            }
            return false;
        }

        public List<UserAccount> GetUsersAccounts()
        {
            var allUsers = _dbContext.UserAccounts.ToList();
            return allUsers;
        }

        public async Task<UserAccount> GetUserById(int Id)
        {
            var user = await _dbContext.UserAccounts.FindAsync(Id);
            return user;
        }

        public async Task<bool> Save()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }
    }
}
