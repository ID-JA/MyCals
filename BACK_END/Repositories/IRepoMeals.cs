using AutoMapper;
using Microsoft.Extensions.Configuration;
using MY_CALS_BACKEND.Dto.Meals;
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
        public Task<bool> DeleteMeal(int Id_Meal);
        public Task<bool> EditMeal(int Id_Meal,MealForEditDTO mealEditDTO);
        public Task<bool> SaveMeal();

        public Task<MealForDisplayDTO> GetUserMealById(int Id);

    }

    public class RepoMeals : IRepoMeals
    {
        //private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public RepoMeals(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async void AddMeal<T>(T entity) where T : class
        {
            await _dbContext.AddAsync(entity);

        }

        public async Task<bool> DeleteMeal(int Id_Meal)
        {
            var mealToDelete = await _dbContext.Meals.FindAsync(Id_Meal);
            if (mealToDelete != null)
            {
                _dbContext.Meals.Remove(mealToDelete);
                return true;
            }
            return false;
        }

        public async Task<bool> EditMeal(int Id_Meal, MealForEditDTO mealEditDTO)
        {
            var mealToEdit = await _dbContext.Meals.FindAsync(Id_Meal);
            if(mealToEdit != null)
            {
                mealToEdit.Name = mealEditDTO.Name;
                mealToEdit.Description = mealEditDTO.Description;
                mealToEdit.Date = mealEditDTO.Date;
                mealToEdit.Calories = mealEditDTO.Calories;
                _dbContext.Update(mealToEdit);
                return true;
            }

            return false;
        }

        public List<Meal> GetMeals()
        {
            return _dbContext.Meals.ToList();
        }

        public async Task<MealForDisplayDTO> GetUserMealById(int Id)
        {
            var mealOfUser = await _dbContext.Meals.FindAsync(Id);
            return _mapper.Map<MealForDisplayDTO>(mealOfUser);

        }

        public async Task<bool> SaveMeal()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }
    }
}
