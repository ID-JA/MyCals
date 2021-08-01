using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MY_CALS_BACKEND.Dto.Meals;
using MY_CALS_BACKEND.Models;
using MY_CALS_BACKEND.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MY_CALS_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IRepoMeals _repoMeals;
        private readonly IMapper _mapper;
        private int userId;
        public MealsController(IRepoMeals meals, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            //userId = Int16.Parse(httpContextAccessor.HttpContext.User.FindFirst("nameid").Value);
            userId = Int16.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _repoMeals = meals;
            _mapper = mapper;
        }

        // GET: api/<MealsController>
        [HttpGet("mymeals")]
        public IEnumerable<Meal> GetMeals()
        {
            return _repoMeals.GetMeals();
        }

        // GET api/<MealsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MealsController>
        [HttpPost("addmeal")]
        [Authorize]
        public async Task<IActionResult> AddNewMeal(MealForAddDTO mealForAdd)
        {
            mealForAdd.Id_User = userId;
            _repoMeals.AddMeal(_mapper.Map<Meal>(mealForAdd));
            var result = await _repoMeals.SaveMeal();
            if (result)
            {
                return Ok("Meal has been created successfully");
            }
            return BadRequest("Something wrong happened please try again");
        }

        // PUT api/<MealsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MealsController>/5
        [HttpDelete("deletemeal/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _repoMeals.GetUserMealById(id);
            if (meal != null)
            {

                if (meal.Id_User == userId)
                {
                     await _repoMeals.DeleteMeal(id);
                    var result = await _repoMeals.SaveMeal();
                    if (result)
                    {
                        return Ok("Meal has been deleted successfully");
                    }

                }
                return BadRequest("Bad Action");
            }
            return NotFound("This meal doesn't exist try again");
        }
    }
}
