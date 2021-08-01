using Microsoft.Extensions.Configuration;
using MY_CALS_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Repositories
{
    public interface IRepoMeals
    {
        public List<Meal> GetMeals();
        public void AddMeal<T>(T entity) where T : class;

        public Task<bool> SaveMeal();

    }

    public class RepoMeals : IRepoMeals
    {
        //private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public RepoMeals(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void AddMeal<T>(T entity) where T : class
        {
            await _dbContext.AddAsync(entity);

        }

        public List<Meal> GetMeals()
        {
            return _dbContext.Meals.ToList();
        }

        public async Task<bool> SaveMeal()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }
    }
}
